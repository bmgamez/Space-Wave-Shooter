﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissileTargeting : MonoBehaviour
{
    TargetingDistance targ;
    UIArrows uIArrows;
    Rigidbody rb;
    public GameObject targeter;
    public GameObject explosion;
    public float fuse = 10f;
    public int missileVelocity;
    public int rotationSpeed;
    public int maxTargetingRotationDifference = 90;
    int positivemaxTargetingRotation;
    int negativemaxTargetingRotation;
    GameObject currentTarget;
    void Start()
    {
        positivemaxTargetingRotation = 180 + maxTargetingRotationDifference;
        negativemaxTargetingRotation = 180 - maxTargetingRotationDifference;
        Invoke("Explode", fuse);
        uIArrows = GameObject.Find("UI Camera").GetComponent<UIArrows>();
        rb = GetComponent<Rigidbody>();
        InvokeRepeating ("MissileTargetFinding", 0, 0.1f);
        targ = uIArrows.GetClosestToCenter();
        currentTarget = targ.ForTargeting;
        Vector3 position = transform.position;
        targeter.transform.LookAt(currentTarget.transform);
        Vector3 missileEulerRotation = transform.rotation.eulerAngles;
        Vector3 targeterEulerRotation = targeter.transform.rotation.eulerAngles;
        Vector3 rotationDifference = new Vector3 (Mathf.Abs(targeterEulerRotation.x - missileEulerRotation.x),Mathf.Abs(targeterEulerRotation.y - missileEulerRotation.y),Mathf.Abs(targeterEulerRotation.z - missileEulerRotation.z)); 
        if (rotationDifference.x < negativemaxTargetingRotation | rotationDifference.x > positivemaxTargetingRotation)
        {
            if (rotationDifference.y < negativemaxTargetingRotation | rotationDifference.y > positivemaxTargetingRotation)
            {
            }
            else
            {
                currentTarget = null;
            }
        }
        else
        {
            currentTarget = null;
        }
    }

    void Update()
    {
        rb.velocity = transform.forward * missileVelocity;
        float predictedImpactTime = (Vector3.Distance(currentTarget.transform.position, transform.position) / (rb.velocity.x + rb.velocity.y + rb.velocity.z));
        Vector3 currentTargetVelocity = currentTarget.GetComponent<Rigidbody>().velocity;
        float targetPredictionx = currentTarget.transform.position.x + (currentTargetVelocity.x * predictedImpactTime);
        float targetPredictiony = currentTarget.transform.position.y + (currentTargetVelocity.y * predictedImpactTime);
        float targetPredictionz = currentTarget.transform.position.z + (currentTargetVelocity.z * predictedImpactTime);
        Vector3 targetPrediction = new Vector3 (targetPredictionx, targetPredictiony, targetPredictionz);
        Quaternion targetRotation = Quaternion.LookRotation (targetPrediction - transform.position);
        float str = Mathf.Min (rotationSpeed * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, str);

    }
    void MissileTargetFinding ()
    {
        
        targeter.transform.LookAt(currentTarget.transform);
        Vector3 missileEulerRotation = transform.rotation.eulerAngles;
        Vector3 targeterEulerRotation = targeter.transform.rotation.eulerAngles;
        Vector3 rotationDifference = new Vector3 (Mathf.Abs(targeterEulerRotation.x - missileEulerRotation.x),Mathf.Abs(targeterEulerRotation.y - missileEulerRotation.y),Mathf.Abs(targeterEulerRotation.z - missileEulerRotation.z)); 
        if (rotationDifference.x < negativemaxTargetingRotation | rotationDifference.x > positivemaxTargetingRotation)
        {
            if (rotationDifference.y < negativemaxTargetingRotation | rotationDifference.y > positivemaxTargetingRotation)
            {
                
            }
            else
            {
                currentTarget = null;
            }
        }
        else
        {
            currentTarget = null;
        }
        
    }
    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.tag != "Bullet")
        {
            Instantiate (explosion, transform.position, Quaternion.identity);
            Destroy (gameObject);
        }
    }
    void Explode ()
    {
        Instantiate (explosion, transform.position, Quaternion.identity);
        Destroy (gameObject);
    }
}
