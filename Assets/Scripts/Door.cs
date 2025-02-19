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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedDoorSprite;
    }

    void Update()
    {
        // 플레이어가 근처에 있고 'E' 키를 눌렀을 때 문 열고 닫기
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                CloseDoor();  // 문이 열려있으면 닫기
            }
            else
            {
                OpenDoor();  // 문이 닫혀있으면 열기
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 문에 근접했을 때
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;  // 플레이어가 문 근처에 있다고 표시
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 문을 떠났을 때
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;  // 플레이어가 문을 떠났다고 표시
        }
    }

    void OpenDoor()
    {
        if (!isOpen)
        {
            spriteRenderer.sprite = openDoorSprite;  // 열린 문 이미지로 변경
            isOpen = true;
        }
    }

    void CloseDoor()
    {
        if (isOpen)
        {
            spriteRenderer.sprite = closedDoorSprite;  // 닫힌 문 이미지로 변경
            isOpen = false;
        }
    }
}