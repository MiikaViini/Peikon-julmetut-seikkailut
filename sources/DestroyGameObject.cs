using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, 1.5f);
    }
}