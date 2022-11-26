using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCounter;
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Coin"))
        {
            Destroy(collider2D.gameObject);
            GameManager.gameManager.coinValue += collider2D.GetComponentInParent<CoinBehaviour>().value;
            coinCounter.text = "Coins: " + GameManager.gameManager.coinValue;
        }
    }
}
