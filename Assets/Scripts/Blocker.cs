using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    [SerializeField] GameObject blocker;
    [SerializeField] private float movespeed = 10;
    private bool playeratbutton = false;
    private float deltaTime = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playeratbutton) ;
        {
            Debug.Log("E has been pressed");
            blocker.transform.position = blocker.transform.position + (Vector3.up * movespeed) * Time.deltaTime;
            if (transform.position.y < deltaTime)
            {
                movespeed = 0;
            }

        }

             void OnTriggerEnter(Collider other)
            {
                if (other.CompareTag("Player"))
                {
                    Debug.Log("Player has entered the trigger zone.");

                    playeratbutton = true;
                }
            }

             void OnTriggerExit(Collider other)
            {
                if (other.CompareTag("Player"))
                {
                    Debug.Log("Player has exited the trigger zone.");

                    playeratbutton = false;
                }
            }
    }
}
