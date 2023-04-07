using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 dir = Vector3.up;

    private bool isPowerful;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        rb.velocity = dir * (speed * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PaddleMover>().timerPowershot > 0)
            {
                isPowerful = true;
                
                other.gameObject.GetComponent<PaddleMover>().EffetsTirPuissant();
            }
            
            PaddleBounce(other.transform, other.contacts[0].point);
        }

        else
        {
            if (other.gameObject.CompareTag("Brick"))
            {
                other.gameObject.GetComponent<Brick>().DestroyBrick();

                if (!isPowerful)
                {
                    dir = Vector3.Reflect(dir, other.contacts[0].normal).normalized;
                    dir.z = 0;
                }
            }
            else
            {
                dir = Vector3.Reflect(dir, other.contacts[0].normal).normalized;
                dir.z = 0;

                isPowerful = false;
            }
        }
    }


    public void PaddleBounce(Transform paddle, Vector3 point)
    {
        float localPosX = paddle.InverseTransformPoint(point).x;
        float lerpPercent = Mathf.Clamp(localPosX + 0.5f, 0, 1);
        float angle = Mathf.Lerp(-75, 75, lerpPercent);

        dir = new Vector3(
            Mathf.Sin(Mathf.Deg2Rad * angle),
            Mathf.Cos(Mathf.Deg2Rad * angle),
            0);
    }
}
