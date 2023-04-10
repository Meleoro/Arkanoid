using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 dir = Vector3.up;

    [HideInInspector] public bool isPowerful;

    private List<BoxCollider> disabledBoxColliders = new List<BoxCollider>();

    [Header("Powershot")]
    [SerializeField] private float powershotSpeed;

    [Header("Autres")]
    private float currentSpeed;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentSpeed = speed;
    }



    void FixedUpdate()
    {
        rb.velocity = dir * (currentSpeed * Time.deltaTime);
    }


    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PaddleMover paddleScript = other.gameObject.GetComponent<PaddleMover>();

            if (paddleScript.timerPowershot > 0)
            {
                ActivatePowerShot();
                
                paddleScript.EffetsTirPuissant();
            }
            else
            {
                paddleScript.timerBallTouch = paddleScript.margeErreurTirPuissant;
                paddleScript.currentBall = this;
            }
            
            PaddleBounce(other.transform, other.contacts[0].point);
        }

        else
        {
            if (other.gameObject.CompareTag("Brick"))
            {
                if (!isPowerful)
                {
                    other.gameObject.GetComponent<Brick>().LoseHealth(1, false);

                    Vector3 newDirection = Vector3.Reflect(dir, other.contacts[0].normal);
                    float hypothenuse = (newDirection.normalized + other.contacts[0].normal).magnitude;

                    if (hypothenuse > Mathf.Sqrt(2))
                    {
                        dir = newDirection;
                    }

                    dir.z = 0;
                }
                else
                {
                    if (other.gameObject.GetComponent<Brick>().health > 1)
                        disabledBoxColliders.Add(other.gameObject.GetComponent<BoxCollider>());

                    other.gameObject.GetComponent<Brick>().LoseHealth(1, true);
                }
            }

            else
            {
                Vector3 newDirection = Vector3.Reflect(dir, other.contacts[0].normal);
                float hypothenuse = (newDirection.normalized + other.contacts[0].normal).magnitude;

                if (hypothenuse > Mathf.Sqrt(2))
                {
                    dir = newDirection;
                }

                dir.z = 0;

                if (other.gameObject.CompareTag("UpWall"))
                {
                    isPowerful = false;
                    currentSpeed = speed;

                    for (int i = 0; i < disabledBoxColliders.Count; i++)
                    {
                        disabledBoxColliders[i].enabled = true;
                    }

                    disabledBoxColliders.Clear();
                }
            }
        }
    }



    public void ActivatePowerShot()
    {
        isPowerful = true;
        currentSpeed = powershotSpeed;
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
