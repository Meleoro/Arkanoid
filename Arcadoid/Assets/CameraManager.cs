using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    private bool isShaking;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(gameObject);
    }



    public void DoCameraShake(float shakeDuration, float shakeAmplitude)
    {
        if (!isShaking && !LevelManager.Instance.isLevelingUp)
        {
            isShaking = true;
            transform.DOShakePosition(shakeDuration, shakeAmplitude).OnComplete(() => isShaking = false);
        }
    }
}
