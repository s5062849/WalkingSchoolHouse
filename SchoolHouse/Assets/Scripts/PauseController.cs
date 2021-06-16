using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{

    public GameObject PauseMenu;

    private bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            ShowOrHideMenu();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.Y) && pause == true)
        {
            Home();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.X) && pause == true)
        {
            Quit();
        }
    }

    void ShowOrHideMenu()
    {
        if(pause == false)
        {
            PauseMenu.SetActive(true);
            pause = true;
        }
        else
        {
            PauseMenu.SetActive(false);
            pause = false;
        }
    }

    void Quit()
    {
        Application.Quit();
    }

    void Home()
    {
        SceneManager.LoadScene("Menu");

        SoundManager.instance.StopAndResetTrack();
    }
}
