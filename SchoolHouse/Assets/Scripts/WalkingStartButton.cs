using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingStartButton : MonoBehaviour
{
    public GameObject ContentManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OVRHand>() != null)
        {
            ContentManager.GetComponent<WalkingContentManager>().StartExperience();
        }
    }

/*
    private void OnCollisionEnter(Collision collision)
    {
        if (other.GetComponent<OVRHand>() != null)
        {
            ContentManager.GetComponent<WalkingContentManager>().StartExperience();
        }
    }*/

}
