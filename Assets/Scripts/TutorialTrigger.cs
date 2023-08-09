using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//use instantiate 
//use addforce for direction
public class TutorialTrigger : MonoBehaviour
{
    //When the player character enters this zone
    //display the toutorial text
    [SerializeField] TextMeshPro tutorialText;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ouch, !I have Triggered something.");

        //If the player character triggers this method, do the thing
        if (other.CompareTag("Player"))

        {
            //Show the tutorial text
            tutorialText.enabled = true;
        }
    }
}