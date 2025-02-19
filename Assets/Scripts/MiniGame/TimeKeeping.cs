using System.Collections;
using UnityEngine;
using TMPro; // TextMeshPro 사용

public class TimeKeeping : MonoBehaviour
{
    public TextMeshProUGUI timeText; // 현재 시간
    public TextMeshProUGUI bestTimeText; // 최고 기록
    private float elapsedTime = 0f; // 경과 시간
    private bool isGameActive = true; // 게임 진행 여부

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