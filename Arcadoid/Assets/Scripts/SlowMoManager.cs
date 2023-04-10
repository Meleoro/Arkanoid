using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SlowMoManager : MonoBehaviour
{
    [Header("SlowMo")] 
    [SerializeField] private Image chargeBar;
    [SerializeField] private float speedUseSlowMo;
    [SerializeField] private float speedTransitionSlowMo;
    [SerializeField] [Range(0, 1)] private float strengthSlowMo;
    private float currentCharge;

    [Header("Effets")] 
    [SerializeField] private Camera _camera;
    [SerializeField] private Volume slowMoEffect;
    private float originalPosZ;


    private void Start()
    {
        originalPosZ = _camera.transform.position.z;
    }


    private void Update()
    {
        /*if (Input.GetKey(KeyCode.Mouse1) && currentCharge > 0)
        {
            SlowMoTime();
        }
        else
        {
            ExitSlowMo();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddCharge();
        }
        
        chargeBar.fillAmount = currentCharge;*/
    }


    public void AddCharge()
    {
        if (currentCharge < 1)
        {
            currentCharge += 0.1f;
        }
        else
        {
            currentCharge = 1;
        }
    }
    

    public void SlowMoTime()
    {
        // Slow Mo
        currentCharge -= (Time.deltaTime * speedUseSlowMo) / Time.timeScale;

        float wantedSlowMoStrength = Mathf.Lerp(Time.timeScale, strengthSlowMo, Time.fixedDeltaTime * speedTransitionSlowMo);
        
        Time.timeScale = wantedSlowMoStrength;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        
        
        // Effets
        float effectStrength = strengthSlowMo / Time.timeScale;
        
        DoSlowMoEffect(effectStrength);
    }

    
    public void ExitSlowMo()
    {
        // Slow Mo
        float wantedSlowMoStrength = Mathf.Lerp(Time.timeScale, 1, Time.fixedDeltaTime * speedTransitionSlowMo);
        
        Time.timeScale = wantedSlowMoStrength;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        
        
        // Effets
        float effectStrength = strengthSlowMo / Time.timeScale - strengthSlowMo / 1;
        
        DoSlowMoEffect(effectStrength);
    }
    

    
    public void DoSlowMoEffect(float effectStrength)
    {
        slowMoEffect.weight = effectStrength;

        _camera.transform.position = new Vector3(
            _camera.transform.position.x,
            _camera.transform.position.y,
            Mathf.Lerp(originalPosZ, originalPosZ - 1, effectStrength));
    }
}
