using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public float moveSpeed = 5f; 
    public float sprintSpeed = 8f; 
    private float currentSpeed; 

    public float jumpHeight = 1.5f; 
    public float jumpTime = 0.3f; 
    private bool isJumping = false; 
    
    private Rigidbody2D rb; 
    private Vector2 movement;
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    private AnimationHandler animationHandler;
    private Inventory inventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 중복된 PlayerManager 제거
            return;
        }
    }

    void Start()
    {
        if (!IsOwner) 
        {
            Destroy(this);
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        currentSpeed = moveSpeed;

        inventory = GetComponent<Inventory>();
        
        // 현재 씬이 바뀌면 플레이어 활성화/비활성화 처리
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 카메라 설정
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }

    void Update()
    {
        if (!IsOwner || !gameObject.activeSelf) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animationHandler.Move(movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = currentSpeed;
        }

        if (spriteRenderer == null)
        {
            return;
        }

        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.GetComponentInChildren<InventoryUI>();
        }
    }

    private void Jump()
    {
        if (isJumping) return;

        isJumping = true;
        animationHandler.Jump(true);
        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine()
    {
        isJumping = true;
        float timeElapsed = 0f;
        float halfJumpTime = jumpTime / 2f;
        Vector3 startPosition = transform.position;

        while (timeElapsed < jumpTime)
        {
            float jumpProgress = timeElapsed < halfJumpTime 
                ? timeElapsed / halfJumpTime 
                : (jumpTime - timeElapsed) / halfJumpTime; 

            float currentJumpHeight = jumpHeight * jumpProgress;
            transform.position = new Vector3(startPosition.x, startPosition.y + currentJumpHeight, startPosition.z);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        isJumping = false;
        animationHandler.Jump(false);
    }

    void FixedUpdate()
    {
        if (!IsOwner || !gameObject.activeSelf) return;

        rb.velocity = movement.normalized * moveSpeed;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BallSpawnerScene")
        {
            gameObject.SetActive(false); // 기존 플레이어 비활성화
        }
        else if (scene.name == "MainScene")
        {
            gameObject.SetActive(true); // 다시 활성화
            mainCamera = Camera.main; // 카메라도 다시 설정
        }
    }
}
