using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP_Obstacle : MonoBehaviour
{
    private void Start()
    {
        SetCollider();
    }

    private void SetCollider()
    {
        if(gameObject.CompareTag("Spike"))
        {
            gameObject.AddComponent<TriangleCollider>();
        }
    }
}
