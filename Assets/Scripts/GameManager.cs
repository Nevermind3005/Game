using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public Health playerHealth = new(3);
    public int coinValue = 0;

    public long startTime;
    
    void Awake()
    {

        gameManager = this;
        
        startTime = DateTimeOffset.Now.ToUnixTimeSeconds();
    }

    public long GetLevelTime()
    {
        return DateTimeOffset.Now.ToUnixTimeSeconds() - startTime;
    }

}
