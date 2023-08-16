using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ImpactTester : MonoBehaviour
{
    //We want this script to handle collison and trigger dectection
    //When our object impacts another, we want to PRINT TEXT


    //First, collisions....
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ouch!");


        //Destroy the ground
      Destroy(collision.gameObject);
    }


    //Next, triggers

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ouch, !I have Triggered something.");
    }

}