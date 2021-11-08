using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3 moveDir;     //direction        
    public float moveSpeed;      
    
    private float standFor = 15.0f; 

    void Start()
    {
        Destroy(gameObject, standFor);
    }

    void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.back * moveDir.x * (moveSpeed * 20) * Time.deltaTime);
    }
}
