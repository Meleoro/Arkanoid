using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreText2;
    private int currentPoints;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        else
            Destroy(gameObject);
    }


    public void AddPoints(int points)
    {
        currentPoints += points;

        if (currentPoints < 1000)
        {
            scoreText.text = "00" + currentPoints;
        }
        
        else if (currentPoints < 10000)
        {
            scoreText.text = "0" + currentPoints;
        }

        else
        {
            scoreText.text = currentPoints.ToString();
        }

        scoreText2.text = "Score : " + currentPoints;

        scoreText.transform.DOScale(Vector3.one, 0);
        scoreText.transform.DOScale(Vector3.one * 1.2f,0.15f).OnComplete(() => scoreText.transform.DOScale(Vector3.one,0.15f));
    }
}
