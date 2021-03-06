﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Missile;
    int cooldownTimer = 0;
    int missileCooldownTimer = 0;
    public int fireCooldown = 2;
    public int missileFireCooldown = 50;
    public int missileAmount = 5;
    public int missileMax = 5;
    public bool ammoRegeneration = true;
    public GameObject[] BulletFirePoints;
    public GameObject[] MissileFirePoints;
    void Start()
    {
     InvokeRepeating("Cooldown", 0, 0.1f);   
     InvokeRepeating("AmmoRegeneration",0 ,10);
    }

    
    void Update()
    {
        if (cooldownTimer <= 0)
        {
            if (Input.GetKey("mouse 0"))
            {
                foreach (GameObject firePoint in BulletFirePoints)
                {
                Instantiate(Bullet, firePoint.transform.position, firePoint.transform.rotation);
                }
                cooldownTimer = fireCooldown;
                
            }
        }
        if (missileCooldownTimer <= 0)
        {
            if (Input.GetKey("space"))
            {
                if (missileAmount > 0)
                {
                    foreach (GameObject firePoint in MissileFirePoints)
                    {
                    Instantiate(Missile,firePoint.transform.position, firePoint.transform.rotation);
                    missileAmount--;
                    }
                    missileCooldownTimer = missileFireCooldown;                }
            }
        }
    }
    void Cooldown ()
    {
        cooldownTimer--;
        missileCooldownTimer--;
    }
    void AmmoRegeneration()
    {
        if (ammoRegeneration)
        {
            if (missileAmount < missileMax)
            {
                missileAmount++;
            }
        }
    }
    
}
