using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    void Start()
    {
        //saving the initial background position
        startPos = transform.position;

        //Using the BoxCollider component size
        //to calculate the mid point of the background
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;

    }

    
    void Update()
    {
        //Reset background position to simulate infinite movement
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }

    }
}
