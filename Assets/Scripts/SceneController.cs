using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        // 싱글톤 패턴 적용 (씬 전환 시 중복 방지)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 특정 씬으로 이동하는 함수
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // 시간 정지 해제
        SceneManager.LoadScene(sceneName);
    }

    // 미니게임 씬 로드
    public void LoadMiniGame()
    {
        LoadScene("MiniGameScene"); // 미니게임 씬 이름에 맞게 변경
    }

    // 메인 씬으로 이동
    public void LoadMainScene()
    {
        LoadScene("MainScene"); // 메인 씬 이름에 맞게 변경
    }
}
