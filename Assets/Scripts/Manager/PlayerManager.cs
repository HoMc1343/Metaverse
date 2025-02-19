using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float sprintSpeed = 8f; // 달리기 속도
    public float jumpForce = 5f; // 점프
    private float currentSpeed; // 현재 속도
    private Rigidbody2D rb; 
    private Vector2 movement;
    private Camera mainCamera;

    void Start()
    {
        if (!IsOwner) Destroy(this); // 본인 캐릭터만 조작 가능
        rb = GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;

        currentSpeed = moveSpeed;
    }

    void Update()
    {
        if (!IsOwner) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Shift 키 속도 변경
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed; // 속도 증가
        }
        else
        {
            currentSpeed = moveSpeed; // 기본 속도로 복구
        }

        // 좌우 이동 방향에 따라 플레이어 회전
        if (movement.x > 0) // 오른쪽으로 이동
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0); // 오른쪽을 바라봄
        }
        else if (movement.x < 0) // 왼쪽으로 이동
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0); // 왼쪽을 바라봄
        }
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