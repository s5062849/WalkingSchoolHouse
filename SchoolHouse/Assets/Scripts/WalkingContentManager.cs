using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingContentManager : MonoBehaviour
{

    public GameObject T1Content, T2Content, T3Content, T4Content, StartInstructions;
    public GameObject DefualtRoom, T1Room, T2Room, T3Room;
    public GameObject T1Trigger;
    [SerializeField] AudioController audioController;
    private bool ExperienceStart = false;


    

    public void StartExperience()
    {
        DefualtRoom.SetActive(false);
        T1Content.SetActive(true);
        T1Room.SetActive(true);
        StartInstructions.SetActive(false);
        ExperienceStart = true;

        audioController.PlayMusic("T1MenuMusic");

        audioController.StopMusic("StartMenuMusic");
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Section1" && ExperienceStart)
        {
            T1Content.SetActive(true);
            T1Room.SetActive(true);

            audioController.PlayMusic("T1MenuMusic");

        }

        if (other.tag == "Section2")
        {
            T2Content.SetActive(true);
            T2Room.SetActive(true);

            audioController.PlayMusic("T2MenuMusic");

            
        }

        if (other.tag == "Section3")
        {
            T3Content.SetActive(true);
            T3Room.SetActive(true);

            audioController.PlayMusic("T3MenuMusic");

            
        }

        if (other.tag == "Section4")
        {
            T4Content.SetActive(true);
            T1Room.SetActive(true);

            audioController.PlayMusic("BackMenuMusic");

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Section1")
        {
            T1Content.SetActive(false);
            T1Room.SetActive(false);

            audioController.StopMusic("T1MenuMusic");
        }

        if (other.tag == "Section2")
        {
            T2Content.SetActive(false);
            T2Room.SetActive(false);
            audioController.StopMusic("T2MenuMusic");
        }

        if (other.tag == "Section3")
        {
            T3Content.SetActive(false);
            T3Room.SetActive(false);
            audioController.StopMusic("T3MenuMusic");
        }

        if (other.tag == "Section4")
        {
            T4Content.SetActive(false);
            T1Room.SetActive(false);
            audioController.StopMusic("BackMenuMusic");
        }
    }
}
