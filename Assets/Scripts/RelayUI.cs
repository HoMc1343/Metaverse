using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;

public class RelayUI : MonoBehaviour
{
    public RelayManager relayManager; // RelayManager 참조
    public Button hostButton; // Host 버튼
    public Button joinButton; // Join 버튼
    public TMP_InputField joinCodeInput; // Join Code 입력 필드
    public TextMeshProUGUI joinCodeText; // Join Code 표시 텍스트

    void Awake()
    {
        DontDestroyOnLoad(gameObject);  // RelayUI 오브젝트가 씬 전환 중에도 유지되도록
    }

    void Start()
    {
        hostButton.onClick.AddListener(async () =>
        {
            // Relay 생성 (호스트 역할)
            string code = await relayManager.CreateRelay();
            
            // Join Code를 표시
            if (joinCodeText != null)
            {
                joinCodeText.text = "Code: " + code; // Join Code 표시
            }

            // 호스트 버튼 비활성화 (사라지게)
            hostButton.gameObject.SetActive(false);

            // 조인 버튼도 비활성화
            joinButton.gameObject.SetActive(false);

            // 입력 필드 비활성화
            joinCodeInput.gameObject.SetActive(false);
        });

        joinButton.onClick.AddListener(() =>
        {
            // Relay에 참여 (조인 역할)
            relayManager.JoinRelay(joinCodeInput.text);

            // 조인 버튼 비활성화 (사라지게)
            joinButton.gameObject.SetActive(false);

            // 호스트 버튼도 비활성화
            hostButton.gameObject.SetActive(false);

            // 입력 필드 비활성화
            joinCodeInput.gameObject.SetActive(false);
        });
    }
}