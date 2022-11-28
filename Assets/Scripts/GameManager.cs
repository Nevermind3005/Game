using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; set; }

    public Health playerHealth = new(3);
    public int coinValue = 0;

    public long startTime;
    
    void Awake()
    {
        if (gameManager is not null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
        startTime = DateTimeOffset.Now.ToUnixTimeSeconds();
    }

    public long GetLevelTime()
    {
        return DateTimeOffset.Now.ToUnixTimeSeconds() - startTime;
    }

}
