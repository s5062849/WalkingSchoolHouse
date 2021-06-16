using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ButtonTrigger : MonoBehaviour
{

    [SerializeField]
    private UnityEvent onButtonPressed;

    private bool pressedInProgress = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button" && pressedInProgress == false)
        {
            pressedInProgress = true;
            onButtonPressed?.Invoke();
            Debug.Log("Button pressed");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button")
        {
            pressedInProgress = false;
        }
    }

}
