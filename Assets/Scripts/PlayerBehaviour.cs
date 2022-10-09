using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void DemagePlayer()
    {
        GameManager.gameManager.playerHealth.Demage(1);
    }
}
