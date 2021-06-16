using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // First scene
    private bool StartSceneDone = false;

    public GameObject MainContentDesk, MainContentFloating;

    public GameObject selectionRing1;

    public ContentManager CM;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveStartScene()
    {
        if (StartSceneDone == false)
        {
            StartSceneDone = true;

            MainContentDesk.SetActive(true);
            MainContentFloating.SetActive(true);
            selectionRing1.SetActive(true);
            CM.menuVisible = true;
            CM.UpdateMenuOption();
        }       
    }
}
