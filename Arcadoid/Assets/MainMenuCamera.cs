using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class MainMenuCamera : MonoBehaviour
{
    private bool isShaking;

    [SerializeField] private float forceShake;
    private Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.localPosition;
    }


    void Update()
    {
        if (!isShaking)
        {
            Vector3 newPos = originalPos + new Vector3(Random.Range(-forceShake, forceShake), Random.Range(-forceShake, forceShake),
                0);
            
            isShaking = true;
            transform.DOLocalMove(newPos, 1.2f).SetEase(Ease.InOutSine).OnComplete(()
                => isShaking = false);
        }
    }
}
