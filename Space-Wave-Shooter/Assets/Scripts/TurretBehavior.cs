﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{   
    public GameObject Player;
    public GameObject Bullet;
    public float radius = 5;
    public GameObject[] FirePoints;

    float cooldownTimer = 0f;
    public float FireCooldown = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Cooldown",0,0.1f);
    }

    // Update is called once per frame
    void Update()
    {   
        float distance = Vector3.Distance(transform.position,Player.transform.position);
        if (distance <= radius) {
            Fire();
        }
    }

    void Fire() {
        if (cooldownTimer <= 0) {
            foreach(GameObject point in FirePoints) {
                Instantiate(Bullet, point.transform.position + (point.transform.forward * 20), point.transform.rotation);
                cooldownTimer = FireCooldown;
            }
        }
    }

    void Cooldown() {
        cooldownTimer--;
    }
}
