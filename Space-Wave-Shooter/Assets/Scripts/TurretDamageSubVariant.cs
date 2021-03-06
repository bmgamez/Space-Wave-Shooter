﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDamageSubVariant : MonoBehaviour
{

    public GameObject[] parts;
    public int currentHp = 50;
    public GameObject explosion;
    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            currentHp = currentHp - 10;
            Debug.Log("TurretHp" + currentHp);
            if (currentHp <= 0)
            {
                foreach (GameObject part in parts)
                {
                    Instantiate (explosion, transform.position, Quaternion.identity);
                    Destroy (part);
                }
            }
        }
        if (col.gameObject.tag == "Missile")
        {
            currentHp = currentHp - 50;
            Debug.Log("TurretHp" + currentHp);
            if (currentHp <= 0)
            {
                foreach (GameObject part in parts)
                {
                    Instantiate (explosion, transform.position, Quaternion.identity);
                    Destroy (part);
                }
            }
        }
    }
}
