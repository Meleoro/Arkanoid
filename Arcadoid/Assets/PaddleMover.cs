using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PaddleMover : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float maxLateralDistance = 10;

    [Header("PowerShot")] 
    public float margeErreurTirPuissant;
    [HideInInspector] public float timerPowershot;
    [HideInInspector] public float timerBallTouch;
    [HideInInspector] public BallMovement currentBall;
    [SerializeField] private ParticleSystem effetPowerShot;


    [Header("References")] 
    public Volume volumePowerShot;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && timerPowershot <= 0)
        {
            timerPowershot = margeErreurTirPuissant;
        }

        if (timerPowershot > 0)
        {
            timerPowershot -= Time.deltaTime;
        }

        if(timerBallTouch > 0)
        {
            timerBallTouch -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                currentBall.ActivatePowerShot();
                EffetsTirPuissant();
            }
        }

        if (volumePowerShot.weight > 0)
        {
            volumePowerShot.weight -= Time.deltaTime * 2;
        }
    }


    void FixedUpdate()
    {
        MovePaddle();
    }


    public void MovePaddle()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector3((direction * speed) / Time.timeScale, 0, 0);
    }

    public void EffetsTirPuissant()
    {
        effetPowerShot.Play();
        
        volumePowerShot.weight = 1;
    }
}
