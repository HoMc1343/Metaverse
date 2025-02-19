using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
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
    public void LoadSceneNetwork(string sceneName)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    // 특정 씬으로 이동하는 함수
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
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

    public void OnHostButtonClicked()
    {
        // 호스트 관련 로직 처리 후 메인 씬으로 이동
        LoadMainScene();
    }

    // 조인 버튼 클릭 시 호출되는 함수
    public void OnJoinButtonClicked()
    {
        // 조인 관련 로직 처리 후 메인 씬으로 이동
        LoadMainScene();
    }
}
