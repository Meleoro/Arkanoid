using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class BallManager : MonoBehaviour
{
    public static BallManager Instance;

    [SerializeField] private GameObject ball;
    [SerializeField] private Transform paddlePos;
    [SerializeField] private BallLauncher _ballLauncher;

    [HideInInspector] public int currentMax;
    [HideInInspector] public int currentBallStock;

    [Header("References")]
    [SerializeField] private List<Image> balls;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(gameObject);
    }



    private void Start()
    {
        currentBallStock = 1;
        currentMax = 1;

        ActualiseBalls();
    }




    private void Update()
    {
        transform.position = paddlePos.position + new Vector3(0, paddlePos.localScale.y * 0.5f, 0);
        
        if (Input.GetKeyDown(KeyCode.Q) && currentBallStock > 0)
        {
            SpawnNewBall();
        }
    }


    public void SpawnNewBall()
    {
        _ballLauncher.ballTransform = Instantiate(ball, transform.position, Quaternion.identity, transform).transform;

        currentBallStock -= 1;
        ActualiseBalls();
    }

    public void DestroyBall()
    {
        currentBallStock += 1;
    }


    public void ActualiseBalls()
    {
        for(int k = 0; k < balls.Count; k++)
        {
            if (k + 1 <= currentMax)
            {
                balls[k].enabled = true;
            }
            else
            {
                balls[k].enabled = false;
            }

            if (k + 1 <= currentBallStock)
            {
                balls[k].DOFade(1f, 0);
            }
            else
            {
                balls[k].DOFade(0.5f, 0);
            }
        }
    }
}


[Serializable]
public class Ball
{
    public Image ballImage;

    public bool canBeUse;
    public bool isUsed;
}
