using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourelle : MonoBehaviour
{
    public bool canShoot;
    [SerializeField] private GameObject bullet;
    
    [SerializeField] private Vector3 directionBullet;
    
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
                GameObject newBullet = Instantiate(bullet, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                newBullet.GetComponent<Bullet>().dir = directionBullet;
            }
        }
    }
    
}
