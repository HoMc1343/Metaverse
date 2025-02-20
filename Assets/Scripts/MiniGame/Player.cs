using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private MiniGameManager miniGameManager;
    
    private void Start()
    {
        miniGameManager = FindObjectOfType<MiniGameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball")) // 공에 닿았을 때
        {
            FindObjectOfType<MiniGameUI>().GameOver();
            FindObjectOfType<TimeKeeping>().EndGame();
        }
    }
}