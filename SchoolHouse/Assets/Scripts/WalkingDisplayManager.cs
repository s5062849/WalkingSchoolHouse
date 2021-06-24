using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WalkingDisplayManager : MonoBehaviour
{
    public enum showState
    {
        Title,
       Heading,
       Subheading
    }

    public WalkingContentManager contentManager;

    public GameObject imageCloseUp, textCloseUp,imageToShowGO, textToShow, imageGridToShow, contextTitle, selector;


    public showState currentState, previousState;



    private void Update()
    {

        switch (currentState)
        {
            case showState.Title:
                showTitle();
                break;
            case showState.Heading:
                showImageGrid();
                break;
            case showState.Subheading:
                showImageCloseUp();
                break;

            default:
                //do nothing
                break;
        }


        
    }



    public void displayImage (Image imageToShow, string stringToShow )
    {
        
        imageToShowGO.GetComponent<Image>().sprite = imageToShow.sprite;
        textToShow.GetComponent<TextMeshProUGUI>().text = stringToShow;

    }

    public void PlayGame()
    {
        contentManager.StartExperience();
    }


    public void checkHighlightedObject(GameObject highlightedObject)
    {
        
        if(highlightedObject.tag.Contains("PlayButton"))
        {
            PlayGame();
        }



    }



    public void showTitle()
    {
        
        contextTitle.SetActive(true);

        imageGridToShow.SetActive(false);
    }

    public void showImageGrid ()
    {
        imageGridToShow.SetActive(true);
        previousState = showState.Title;

        contextTitle.SetActive(false);
        imageCloseUp.SetActive(false);
        textCloseUp.SetActive(false);

    }

   

    public void showImageCloseUp()
    {
        imageCloseUp.SetActive(true);
        textCloseUp.SetActive(true);
        previousState = showState.Heading;

        imageGridToShow.SetActive(false);
    }


    public void backButton()
    {
        currentState = previousState;

    }

}
