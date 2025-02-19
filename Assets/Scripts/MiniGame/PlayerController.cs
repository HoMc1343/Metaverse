using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어의 이동 속도
    private Rigidbody2D rb; // 2D 리지드바디 컴포넌트

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 리지드바디 컴포넌트 가져오기
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal"); // 좌우 입력 받기
        Vector2 movement = new Vector2(moveInput * moveSpeed, rb.velocity.y); // 수평 이동 벡터 생성
        rb.velocity = movement; // 리지드바디의 속도 설정
    }
}
