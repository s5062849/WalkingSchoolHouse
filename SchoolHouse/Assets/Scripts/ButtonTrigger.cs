using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ButtonTrigger : MonoBehaviour
{
    MasterTelemetrySystem MST;

    private void Start()
    {
        MST = GameObject.FindGameObjectWithTag("telemetry").GetComponent<MasterTelemetrySystem>();
    }


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
            MST.AddLine(gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button")
        {
            pressedInProgress = false;

            StartCoroutine("PauseClick");
        }
    }

    private IEnumerator PauseClick()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        Debug.Log("Started Cotoutine");
        
        yield return new WaitForSeconds(0.5f);

        Debug.Log("End Cotoutine");
        this.GetComponent<BoxCollider>().enabled = true;

    }


}
