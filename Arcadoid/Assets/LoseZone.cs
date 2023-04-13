using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LoseZone : MonoBehaviour
{
    private bool doOnce;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick") && !doOnce)
        {
            doOnce = true;
            PauseManager.Instance.canPause = false;
            StartCoroutine(LoseGame(other.transform.position));
        }
    }

    public IEnumerator LoseGame(Vector3 posBrick)
    {
        Time.timeScale = 0;
        
        Vector3 direction = CameraManager.Instance.transform.position - posBrick;

        CameraManager.Instance.transform.DOMove(CameraManager.Instance.transform.position - direction * 0.5f, 0.5f).SetUpdate(true);
        
        yield return new WaitForSecondsRealtime(1);
        
        CameraManager.Instance.transform.DOMove(new Vector3(7.8f, 1, -207.7f), 2f).SetUpdate(true);
        
    }
}
