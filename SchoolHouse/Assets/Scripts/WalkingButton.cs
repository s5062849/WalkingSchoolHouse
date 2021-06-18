using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkingButton : MonoBehaviour
{
    public GameObject ContentManager;

    public WalkingDisplayManager displayManager;

    //public GameObject contextTitle, contextImageGrid, contextImageShow;

    public string displaySting;

   


    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name.Contains("PlayBackground") && other.name.Contains("Index"))
        {
            ContentManager.GetComponent<WalkingContentManager>().StartExperience();
            displayManager.currentState = WalkingDisplayManager.showState.Title;
            
        }
        
        if (gameObject.name.Contains("ImageBackground") && other.name.Contains("Index"))
        {
           
            //displayManager.previousState = WalkingDisplayManager.showState.Title;
            displayManager.currentState = WalkingDisplayManager.showState.Heading;
        }

        if (gameObject.name.Contains("ImagesUI") && other.name.Contains("Index"))
        {
            
           // displayManager.previousState = WalkingDisplayManager.showState.Heading;
            displayManager.currentState = WalkingDisplayManager.showState.Subheading;
            displayManager.displayImage(gameObject.GetComponent<Image>(), displaySting);
            

        }
        if (gameObject.name.Contains("BackButton") && other.name.Contains("Index"))
        {
            displayManager.backButton();
        }


    }



}
