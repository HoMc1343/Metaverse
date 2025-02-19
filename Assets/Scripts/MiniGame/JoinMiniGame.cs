using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinMiniGame : MonoBehaviour
{
    public GameObject miniGameButton;  // 미니게임 입장 버튼
    public TextMeshProUGUI buttonText;  // 버튼 텍스트 (선택 사항)

    private void Start()
    {
        // 미니게임 버튼 초기 비활성화
        miniGameButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 구역에 들어오면 버튼 활성화
        if (other.CompareTag("Player"))
        {
            miniGameButton.SetActive(true);
            if (buttonText != null)
            {
                buttonText.text = "Start";  // 버튼 텍스트 변경 (선택 사항)
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 구역을 벗어나면 버튼 비활성화
        if (other.CompareTag("Player"))
        {
            miniGameButton.SetActive(false);
        }
    }

    // 미니게임 입장 버튼 클릭 시 호출되는 함수
    public void OnJoinMiniGameButtonClick()
    {
        // 미니게임 씬 로드
        SceneManager.LoadScene("BallSpawnerScene");
    }

    // 취소 버튼 클릭 시 호출되는 함수
    public void OnCancelButtonClick()
    {
        // 버튼 비활성화
        miniGameButton.SetActive(false);
    }
}
