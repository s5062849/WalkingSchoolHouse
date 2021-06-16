using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingStartButton : MonoBehaviour
{
    public GameObject ContentManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("_CapsuleCollider"))
        {
            ContentManager.GetComponent<WalkingContentManager>().StartExperience();
        }
        Debug.Log(other.name);
    }



}
