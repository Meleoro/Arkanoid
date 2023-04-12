using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.ShaderGraph.Internal;


public class DangerZone : MonoBehaviour
{
    [SerializeField] private Material materialColorChange;
    [SerializeField] private Color originalColor;
    [SerializeField] private Color alertColor;
    
    [ColorUsage(true, true)]
    public Color originalColorHDR;
    [ColorUsage(true, true)]
    public Color alertColorHDR;

    public List<GameObject> objectsInZone = new List<GameObject>();
    
    private bool dangerOn;
    private float timerDanger;
    private bool currentlyRed;
    

    private void Update()
    {
        if (objectsInZone.Count > 0)
        {
            timerDanger -= Time.deltaTime;
            
            if(!currentlyRed)
                ChangeToAlert();
            
            else if(currentlyRed)
                ChangeToNormal();


            if (timerDanger < 0)
            {
                timerDanger = 0.5f;

                currentlyRed = !currentlyRed;
            }
        }

        else
        {
            if(timerDanger > 0)
                timerDanger -= Time.deltaTime;
            
            ChangeToNormal();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            objectsInZone.Add(other.gameObject);

            other.GetComponent<Brick>().scriptDangerZone = this;
        }
    }



    private void ChangeToAlert()
    {
        materialColorChange.color = Color.Lerp(alertColor, originalColor, timerDanger * 2);
        materialColorChange.SetColor("_EmissionColor", Color.Lerp(alertColorHDR, originalColorHDR, timerDanger * 2));
    }

    private void ChangeToNormal()
    {
        materialColorChange.color = Color.Lerp(originalColor, alertColor, timerDanger * 2);
        materialColorChange.SetColor("_EmissionColor", Color.Lerp(originalColorHDR, alertColorHDR, timerDanger * 2));
    }
}
