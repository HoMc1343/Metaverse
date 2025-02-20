using System.Collections;
using UnityEngine;
using TMPro;

public class TimeKeeping : MonoBehaviour
{
    public TextMeshProUGUI timeText; // 현재 시간
    public TextMeshProUGUI bestTimeText; // 최고 기록
    private float elapsedTime = 0f; // 경과 시간
    private bool isGameActive = true; // 게임 진행 여부

    public Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance; // Inventory의 싱글톤 인스턴스를 가져옴
    }

    void Update()
    {
        if (isGameActive)
        {
            elapsedTime += Time.deltaTime;
            timeText.text = elapsedTime.ToString("F2"); // 소수 두 자리
        }
    }

    public void EndGame()
    {
        isGameActive = false;
        SaveBestTime();

        int earnedGold = Mathf.FloorToInt(elapsedTime);

        if (inventory != null)
        {
            inventory.AddGold(earnedGold);
            Debug.Log("미니게임 종료! 획득한 골드: " + earnedGold);
        }
        else
        {
            Debug.LogError("EndGame에서 Inventory를 찾을 수 없습니다!");
        }
    }

    private void SaveBestTime()
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        if (elapsedTime > bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", elapsedTime);
            PlayerPrefs.Save();
        }
        bestTimeText.text = "Best: " + PlayerPrefs.GetFloat("BestTime", 0f).ToString("F2");
    }
}