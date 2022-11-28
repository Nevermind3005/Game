using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    public static MainManager Instance { get; set; }
    public int allCoinValue;
    public int coinValue;
    public long levelTime;

    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }

        allCoinValue = 0;
        levelTime = 0;
        coinValue = 0;
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetCoinValue(int coinValue)
    {
        this.coinValue = coinValue;
        allCoinValue += coinValue;
    }
}
