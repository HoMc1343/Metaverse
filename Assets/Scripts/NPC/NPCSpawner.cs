using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class NPCSpawner : MonoBehaviour
{
    public GameObject[] npcPrefabs; // NPC 프리팹 설정
    public Transform[] spawnPoints; // 여러 개의 스폰 위치

    void Start()
    {
        NetworkManager.Singleton.OnServerStarted += SpawnNPCs; // 호스트 시작 후 실행
    }

    private void SpawnNPCs()
    {
        if (!NetworkManager.Singleton.IsHost) return; // 호스트 실행

        if (npcPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogError("NPCSpawner: NPC 프리팹 또는 스폰 포인트가 설정되지 않았습니다!");
            return;
        }

        List<Transform> availableSpawns = new List<Transform>(spawnPoints);

        foreach (GameObject npcPrefab in npcPrefabs)
        {
            if (availableSpawns.Count == 0) break; // 스폰 위치가 부족하면 중단

            int randomIndex = Random.Range(0, availableSpawns.Count);
            Transform spawnPoint = availableSpawns[randomIndex];
            availableSpawns.RemoveAt(randomIndex); // 선택한 위치 제거 (중복 방지)

            GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
            npc.GetComponent<NetworkObject>().Spawn(); // 네트워크에서 동기화
        }
    }
}
