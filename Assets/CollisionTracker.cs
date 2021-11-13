using System;
using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    public GameObject go;

    public int count;
    
    private const int ObstacleLayer = 8;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ObstacleLayer)
        {
            count++;
            go = other.gameObject;
        }
    }
}
