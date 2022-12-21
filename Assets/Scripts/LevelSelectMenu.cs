using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCounter;

    private void Start()
    {
        coinCounter.text = "Coins: " + MainManager.Instance.allCoinValue;
    }
}
