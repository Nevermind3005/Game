using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnFall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            GameManager.gameManager.playerHealth.Kill();
            col.gameObject.GetComponent<PlayerBehaviour>().ShowHp();
            col.gameObject.GetComponent<PlayerBehaviour>().KillDontFollow();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
