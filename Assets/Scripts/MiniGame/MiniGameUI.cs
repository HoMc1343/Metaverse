using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameUI : MonoBehaviour
{
    public GameObject gameOverUI; // 게임 오버 UI (Start, Exit 버튼)

    private void Start()
    {
        gameOverUI.SetActive(false); // 시작할 때는 UI 비활성화
    }

    public void GameOver()
    {
        Time.timeScale = 0f; // 게임 멈추기
        gameOverUI.SetActive(true); // 게임 오버 UI 활성화
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // 게임 다시 시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }

    public void ExitToMain()
    {
        Time.timeScale = 1f; // 시간 멈춤 해제
        SceneManager.LoadScene("MainScene"); // 메인 씬으로 이동
    }
}