using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadSitting()
    {
        SceneManager.LoadScene("Desk");
    }

    public void LoadWalking()
    {
        SceneManager.LoadScene("Walking");
        Debug.Log("Load walking experience");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
