using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float sprintSpeed = 8f; // 달리기 속도
    private float currentSpeed; // 현재 속도

    public float jumpHeight = 1.5f; // 점프 높이
    public float jumpTime = 0.3f; // 점프 지속 시간
    private bool isJumping = false; // 점프 상태 확인
    
    private Rigidbody2D rb; 
    private Vector2 movement;
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    private AnimationHandler animationHandler;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (!IsOwner) Destroy(this); // 본인 캐릭터만 조작 가능
        rb = GetComponent<Rigidbody2D>();

        animationHandler = GetComponent<AnimationHandler>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        mainCamera = Camera.main;

        currentSpeed = moveSpeed;
    }

    void Update()
    {
        if (!IsOwner) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animationHandler.Move(movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Shift 키 속도 변경
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed; // 속도 증가
        }
        else
        {
            moveSpeed = currentSpeed; // 기본 속도로 복구
        }

        // 좌우 이동 방향에 따라 플레이어 회전
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            return;
        }
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false; // 오른쪽
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true; // 왼쪽
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
                ? timeElapsed / halfJumpTime // 점프
                : (jumpTime - timeElapsed) / halfJumpTime; // 하강

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
        if (!IsOwner) return;
        rb.velocity = movement.normalized * moveSpeed; // 이동 속도에 맞게 Rigidbody2D에 속도 적용

        // 카메라가 플레이어를 따라가도록 설정
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 0f, -10f);
        }
    }
}