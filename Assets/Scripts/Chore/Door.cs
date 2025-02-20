using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite openDoorSprite;
    public Sprite closedDoorSprite;

    private SpriteRenderer spriteRenderer;
    private bool isOpen = false;
    private bool isPlayerNearby = false;

    private BoxCollider2D collisionCollider; // 플레이어 충돌 방지용
    private BoxCollider2D triggerCollider;   // 플레이어 감지용 (Trigger)

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 2개의 BoxCollider2D 가져오기
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        if (colliders.Length >= 2)
        {
            collisionCollider = colliders[0]; // 충돌 방지용
            triggerCollider = colliders[1];   // 감지용
            triggerCollider.isTrigger = true; // 감지용 Collider는 Trigger 설정
        }

        spriteRenderer.sprite = closedDoorSprite;
        collisionCollider.enabled = true; // 문이 닫혀있으므로 충돌 활성화
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    void OpenDoor()
    {
        if (!isOpen)
        {
            spriteRenderer.sprite = openDoorSprite;
            isOpen = true;
            collisionCollider.enabled = false; // 충돌 비활성화 (플레이어 통과 가능)
        }
    }

    void CloseDoor()
    {
        if (isOpen)
        {
            spriteRenderer.sprite = closedDoorSprite;
            isOpen = false;
            collisionCollider.enabled = true; // 충돌 활성화 (플레이어 통과 불가능)
        }
    }
}