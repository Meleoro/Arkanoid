using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourelle : MonoBehaviour
{
    public bool canShoot;
    [SerializeField] private GameObject bullet;
    
    [SerializeField] private float cooldownShot;
    [SerializeField] private float startOffsetCooldown;

    private float timerShoot;
    

    private void Start()
    {
        timerShoot = startOffsetCooldown;
    }


    private void Update()
    {
        timerShoot -= Time.deltaTime;

        if (timerShoot < 0)
        {
            timerShoot = cooldownShot;

            if (canShoot)
            {
                Instantiate(bullet, transform.position, Quaternion.identity, transform);
            }
        }
    }
    
}
