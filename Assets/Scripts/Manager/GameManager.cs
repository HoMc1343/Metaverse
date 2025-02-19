using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void SpawnPlayer()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            GameObject player = Instantiate(playerPrefab);
            player.GetComponent<NetworkObject>().Spawn();
        }
    }
}