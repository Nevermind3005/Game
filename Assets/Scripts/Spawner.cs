using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col is not null)
        {
            if (col.tag == "Player")
            {
                Instantiate(prefab, spawnPoint.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    
    void OnDrawGizmos()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
        Gizmos.color = new Color(0, 1, 0, 1f);
        Gizmos.DrawLine(spawnPoint.position, spawnPoint.position + new Vector3(0, 0.1f, 0));
    }
}
