using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxNoCam : MonoBehaviour
{

    [SerializeField] private GameObject camera;
    [SerializeField] private float effect;

    private float startPos;
    private float length;
    
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    
    //TODO rewrite needed
    void Update()
    {
        var temp = (camera.transform.position.x * (1 - effect));
        var distance = (camera.transform.position.x * effect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
        camera.transform.position += Vector3.right * Time.deltaTime;
    }
}
