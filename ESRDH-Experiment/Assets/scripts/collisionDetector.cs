using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetector : MonoBehaviour
{

    public bool CollisionDetected = false;
    
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collision detect");
        CollisionDetected = true;
        GetComponent<Renderer>().material.color = Color.yellow;
    }
}
