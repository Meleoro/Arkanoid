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

    [SerializeField] private List<float> reloadTimers = new List<float>();

    [Header("References")]
    [SerializeField] private List<Ball> balls;


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
            for (int i = 0; i < balls.Count; i++)
            {
                if (!balls[i].isUsed && balls[i].canBeUse)
                {
                    SpawnNewBall(i);
                }
            }
        }

        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i].reload)
            {
                if (balls[i].timerReload > 0)
                {
                    balls[i].timerReload -= Time.deltaTime;

                    balls[i].ballImage.fillAmount = 1 - reloadTimers[i];
                }
            
                else if (balls[i].canBeUse)
                {
                    balls[i].isUsed = false;
                    balls[currentBallStock - 1].reload = false;
                }
            }
        }
    }


    public void SpawnNewBall(int indexBall)
    {
        _ballLauncher.ballTransform = Instantiate(ball, transform.position, Quaternion.identity, transform).transform;

        balls[indexBall].isUsed = true;
        
        currentBallStock -= 1;
        ActualiseBalls();
    }

    public void DestroyBall()
    {
        currentBallStock += 1;

        balls[currentBallStock - 1].timerReload = 1;
        balls[currentBallStock - 1].reload = true;
        
        ActualiseBalls();
    }


    public void ActualiseBalls()
    {
        for(int k = 0; k < balls.Count; k++)
        {
            if (k + 1 <= currentMax)
            {
                balls[k].ballImage.enabled = true;
            }
            else
            {
                balls[k].ballImage.enabled = false;
            }

            if (k + 1 <= currentBallStock)
            {
                balls[k].ballImage.enabled = true;
            }
            else
            {
                balls[k].ballImage.enabled = false;
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
    public float timerReload;
    public bool reload;
}
