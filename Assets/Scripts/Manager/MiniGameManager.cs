using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // 씬 전환을 위한 네임스페이스
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public GameObject miniGameUI;  // 미니게임 시작/나가기 버튼을 위한 UI
    public string mainSceneName = "MainScene";  // 원래 씬 이름

    private bool isInMiniGame = false;

    void Start()
    {
        miniGameUI.SetActive(false);  // 미니게임 UI는 시작 시 비활성화
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 플레이어가 트리거 영역에 도달하면
        {
            miniGameUI.SetActive(true);  // 버튼 UI 활성화
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 플레이어가 트리거 영역을 벗어나면
        {
            miniGameUI.SetActive(false);  // 버튼 UI 비활성화
        }
    }

    public void StartMiniGame()
    {
        isInMiniGame = true;
        miniGameUI.SetActive(false);  // 버튼 UI 비활성화
        SceneManager.LoadScene("MiniGameScene");  // 미니게임 씬으로 전환
    }

    public void ExitMiniGame()
    {
        SceneManager.LoadScene(mainSceneName);  // 원래 씬으로 돌아가기
    }
    public void GameOver()
    {
        // MiniGameUI의 GameOver() 메서드 호출
        FindObjectOfType<MiniGameUI>().GameOver();
        FindObjectOfType<TimeKeeping>().EndGame();
    }
}
