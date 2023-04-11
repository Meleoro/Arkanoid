using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    [Header("Strength")]
    [SerializeField] private List<Image> barresStrength = new List<Image>();
    
    [Header("Speed")]
    [SerializeField] private List<float> speedPerLevel = new List<float>();
    [SerializeField] private List<Image> barresSpeed = new List<Image>();
    
    [Header("Width")]
    [SerializeField] private List<float> widthPerLevel = new List<float>();
    [SerializeField] private List<Image> barresWidth = new List<Image>();

    [SerializeField] private PaddleMover paddle;


    private void Start()
    {
        Initialise();
        ApplyStats();
    }



    private void Initialise()
    {
        for (int i = 0; i < barresStrength.Count; i++)
        {
            barresStrength[i].enabled = false;
        }
        
        for (int i = 0; i < barresSpeed.Count; i++)
        {
            barresSpeed[i].enabled = false;
        }
        
        for (int i = 0; i < barresWidth.Count; i++)
        {
            barresWidth[i].enabled = false;
        }
    }
    
    
    public void ApplyStats()
    {
        int currentStrength = LevelManager.Instance.currentStrength;
        int currentSpeedLvl = LevelManager.Instance.currentSpeed;
        int currentWidthLvl = LevelManager.Instance.currentWidth;

        // On applique les stats
        paddle.speed = speedPerLevel[currentSpeedLvl];
        paddle.transform.localScale = new Vector3(widthPerLevel[currentWidthLvl], paddle.transform.localScale.y,
            paddle.transform.localScale.z);
        
        // On les affiche dans l'UI
        for (int i = 0; i < currentStrength + 1; i++)
        {
            barresStrength[i].enabled = true;
        }
        
        for (int i = 0; i < currentSpeedLvl + 1; i++)
        {
            barresSpeed[i].enabled = true;
        }
        
        for (int i = 0; i < currentWidthLvl + 1; i++)
        {
            barresWidth[i].enabled = true;
        }
    }
}
