using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform paddlePos;
    [SerializeField] private BallLauncher _ballLauncher;

    
    private void Update()
    {
        transform.position = paddlePos.position + new Vector3(0, paddlePos.localScale.y * 0.5f, 0);
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnNewBall();
        }
    }


    public void SpawnNewBall()
    {
        _ballLauncher.ballTransform = Instantiate(ball, transform.position, Quaternion.identity, transform).transform;
    }
}
