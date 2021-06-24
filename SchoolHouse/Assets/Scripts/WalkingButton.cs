using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkingButton : MonoBehaviour
{
    

    public WalkingDisplayManager displayManager;

    //public GameObject contextTitle, contextImageGrid, contextImageShow;

    public string displaySting;

    public GameObject selected;


    

    private void OnTriggerEnter(Collider other)
    {
        //If the player hits the select button
        if (gameObject.name.Contains("SelectButtonTrigger") && other.name.Contains("SelectButtonActivator"))
        {
            
            displayManager.checkHighlightedObject(selected);

            //displayManager.currentState = WalkingDisplayManager.showState.Title;
            
        }

        //If the player hits the right button
        if (gameObject.name.Contains("RightButtonTrigger") && other.name.Contains("RightButtonActivator"))
        {
           
            //displayManager.previousState = WalkingDisplayManager.showState.Title;
            //displayManager.currentState = WalkingDisplayManager.showState.Heading;
        }

        //if the player hits the left button
        if (gameObject.name.Contains("LeftButtonTrigger") && other.name.Contains("LeftButtonActivator"))
        {
            
          
            //displayManager.currentState = WalkingDisplayManager.showState.Subheading;
            //displayManager.displayImage(gameObject.GetComponent<Image>(), displaySting);
            

        }

        //if the player hits the back button
        if (gameObject.name.Contains("BackButtonTrigger") && other.name.Contains("BackButtonActivator"))
        {
            //displayManager.backButton();
        }


    }



}
