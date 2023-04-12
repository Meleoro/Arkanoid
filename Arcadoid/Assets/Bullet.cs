using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1000;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 dir = Vector3.up;
    
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    
    void FixedUpdate()
    {
        rb.velocity = dir * (speed * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            collision.gameObject.GetComponent<Brick>().LoseHealth(LevelManager.Instance.currentStrength + 1, false);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("UpWall"))
        {
            Destroy(gameObject);
        }
    }
}
