using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingContentManager : MonoBehaviour
{

    public GameObject T1Content, T2Content, T3Content, T4Content, StartInstructions;
    public GameObject DefualtRoom, T1Room, T2Room, T3Room;

    public void StartExperience()
    {
        DefualtRoom.SetActive(false);
        T1Content.SetActive(true);
        T1Room.SetActive(true);
        StartInstructions.SetActive(false);
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Section1")
        {
           // T1Content.SetActive(true);
            //T1Room.SetActive(true);
        }

        if (other.tag == "Section2")
        {
            T2Content.SetActive(true);
            T2Room.SetActive(true);
        }

        if (other.tag == "Section3")
        {
            T3Content.SetActive(true);
            T3Room.SetActive(true);
        }

        if (other.tag == "Section4")
        {
            T4Content.SetActive(true);
            T1Room.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Section1")
        {
            T1Content.SetActive(false);
            T1Room.SetActive(false);
        }

        if (other.tag == "Section2")
        {
            T2Content.SetActive(false);
            T2Room.SetActive(false);
        }

        if (other.tag == "Section3")
        {
            T3Content.SetActive(false);
            T3Room.SetActive(false);
        }

        if (other.tag == "Section4")
        {
            T4Content.SetActive(false);
            T1Room.SetActive(false);
        }
    }
}
