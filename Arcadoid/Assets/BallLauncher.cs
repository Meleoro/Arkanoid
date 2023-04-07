using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public Transform ballTransform;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(ballTransform != null)
                LaunchBall();
        }
    }


    public void LaunchBall()
    {
        ballTransform.GetComponent<Rigidbody>().isKinematic = false;

        ballTransform.parent = null;
    }
}
