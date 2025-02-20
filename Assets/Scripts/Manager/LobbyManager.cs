using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using System.Threading.Tasks;

public class LobbyManager : MonoBehaviour
{
    private Lobby currentLobby;

    public async Task CreateLobby(string lobbyName, int maxPlayers)
    {
        CreateLobbyOptions options = new CreateLobbyOptions { IsPrivate = false };
        currentLobby = await Lobbies.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
    }

    public async void JoinLobby(string lobbyId)
    {
        currentLobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId);
    }
}