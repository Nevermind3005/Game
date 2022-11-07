using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private void DamagePlayer()
    {
        GameManager.gameManager.playerHealth.Demage(1);
    }
}
