using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelmentryInitialise : MonoBehaviour
{

    MasterTelemetrySystem MTS;


    // Start is called before the first frame update
    void Awake()
    {
        MTS = GameObject.FindWithTag("telemetry").GetComponent<MasterTelemetrySystem>();

        MTS.sceneStart();
        Debug.Log(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Debug.Log("Run CreateFile");
            MTS.CreateFile();
        }
        
    }

   
}
