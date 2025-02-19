using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Authentication;
using Unity.Networking.Transport.Relay;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using System.Threading.Tasks;
using UnityEngine;
using System;

public class RelayManager : MonoBehaviour
{
    async void Start()
    {
        try
        {
            // Unity Services 초기화
            await UnityServices.InitializeAsync();

            // 인증 상태 확인 및 로그인
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            // Relay 할당 생성 (Relay에 연결)
            var allocation = await RelayService.Instance.CreateAllocationAsync(4, "ap-northeast-2");
            Debug.Log("Relay allocation created: " + allocation);
        }
        catch (Exception e)
        {
            // Debug.LogError("Error: " + e.Message);
        }
    }
    public async Task<string> CreateRelay()
    {
        await UnityServices.InitializeAsync(); // Unity 서비스 초기화
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(4); // 최대 4명 지원
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        RelayServerData relayData = new RelayServerData(allocation, "dtls"); // Relay 데이터 설정
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayData);

        NetworkManager.Singleton.StartHost(); // 호스트 시작
        return joinCode;
    }

    public async void JoinRelay(string joinCode)
    {
        await UnityServices.InitializeAsync(); // Unity 서비스 초기화
        JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        RelayServerData relayData = new RelayServerData(joinAllocation, "dtls");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayData);

        NetworkManager.Singleton.StartClient(); // 클라이언트 시작
    }
}
