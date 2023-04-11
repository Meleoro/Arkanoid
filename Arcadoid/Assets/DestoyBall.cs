using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
            BallManager.Instance.DestroyBall();
        }
    }
}
