using System.Collections;
using TMPro;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public string npcName; // NPC 이름
    public string[] dialogue; // 대사 목록
    public float detectionRange = 2f; // 감지 범위
    public float flipRange = 4f; // NPC가 방향을 바꿀 범위
    public bool isMovable = false; // true일 때 이동 추가
    private bool isInteracting = false; // 대화 중 여부
    private int dialogueIndex = 0;
    private Transform player;
    private GameObject speechBubble;
    private TextMeshProUGUI speechText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Canvas 아래에서 SpeechBubble 찾기
        Transform canvasTransform = transform.Find("Canvas");
        if (canvasTransform != null)
        {
            speechBubble = canvasTransform.Find("SpeechBubble")?.gameObject;
        }

        if (speechBubble != null)
        {
            speechText = speechBubble.GetComponentInChildren<TextMeshProUGUI>();
            speechBubble.SetActive(false);
        }
        else
        {
            Debug.LogError($"{gameObject.name}: SpeechBubble을 찾을 수 없습니다! Canvas 구조를 확인하세요.");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        bool isPlayerNearby = distance <= detectionRange;

        // 플레이어를 바라보도록 방향 조정
        FlipDirection();

        // 대사 넘기기
        if (isPlayerNearby)
        {
            if (!speechBubble.activeSelf) speechBubble.SetActive(true);
            UpdateSpeechBubblePosition();

            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1))
            {
                if (!isInteracting)
                {
                    StartDialogue();
                }
                else
                {
                    AdvanceDialogue();
                }
            }
        }
        else
        {
            if (speechBubble.activeSelf) speechBubble.SetActive(false);
            isInteracting = false;
            dialogueIndex = 0;
        }
    }

    private void StartDialogue() // 대사
    {
        if (dialogue.Length == 0) return;

        isInteracting = true;
        dialogueIndex = 0;
        speechText.text = $"{dialogue[dialogueIndex]}";
    }

    private void AdvanceDialogue() // 대사
    {
        dialogueIndex++;

        if (dialogueIndex < dialogue.Length)
        {
            speechText.text = $"{dialogue[dialogueIndex]}";
        }
        else
        {
            speechBubble.SetActive(false);
            isInteracting = false;
            dialogueIndex = 0;
        }
    }

    private void FlipDirection()
    {
        if (player != null)
        {
            // 여러 명의 플레이어 찾기
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            GameObject closestPlayer = null;
            float closestDistance = float.MaxValue; // 초기값을 큰 값으로 설정

            // 모든 플레이어에 대해 가장 가까운 플레이어 찾기
            foreach (GameObject p in players)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, p.transform.position);

                // 범위 내의 플레이어 중 가장 가까운 플레이어를 찾음
                if (distanceToPlayer <= flipRange && distanceToPlayer < closestDistance)
                {
                    closestDistance = distanceToPlayer;
                    closestPlayer = p;
                }
            }

            if (closestPlayer != null)
            {
                // 가장 가까운 플레이어의 위치에 따라 방향 전환
                bool isPlayerOnLeft = closestPlayer.transform.position.x < transform.position.x;

                // 플레이어가 왼쪽에 있을 때
                if (isPlayerOnLeft && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
                }
                // 플레이어가 오른쪽에 있을 때
                else if (!isPlayerOnLeft && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
                }
            }
        }
    }

    private void UpdateSpeechBubblePosition()
    {
        speechBubble.transform.position = new Vector3(
        transform.position.x, transform.position.y + 1.5f, transform.position.z);
    }
}