using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;


public class MasterTelemetrySystem : MonoBehaviour
{
    public string ID;
    public string currentDate;
    public string currentTime;
    public string fileName;
    public Transform headTransform;


    public TelemetryInitialData telementryInitialData;
    public TelementryRunTimeData telementryRuntimeData;


    public GameObject contentHolder, narrativeHolder, hubRoom;
    public GameObject[] rooms;

    public GameObject[] selectionRings;
  

    public static GameObject instance = null;

    public TextMeshProUGUI debugText;
    //public TextMeshPro debugText;

    public ContentManager contentManager;


    //makes sure that this is the only object that exsits
    private void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(gameObject);

        }else if (instance != this.gameObject)
        {
            Destroy(this.gameObject);
        }
    }

    //gets called when new scene is loaded
    public void sceneStart()
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            contentHolder = GameObject.FindWithTag("contentHolder");
            // debugText = GameObject.FindGameObjectWithTag("debugText").GetComponent<TextMeshProUGUI>();
            narrativeHolder = GameObject.FindWithTag("narrativeHolder");
            narrativeHolder.SetActive(false);

            hubRoom = GameObject.FindWithTag("HubRoom");

            rooms = GameObject.FindGameObjectsWithTag("Room");
            foreach(GameObject go in rooms)
            {
                go.SetActive(false);
            }

            selectionRings = GameObject.FindGameObjectsWithTag("SelectionRing");
            foreach (GameObject go in selectionRings)
            {
                go.SetActive(false);
            }

            contentManager = GameObject.FindGameObjectWithTag("contentManager").GetComponent<ContentManager>();
        }
       
        
       

        
    }




    // This is the one time data of the sessions and player information saved at the start
    [System.Serializable]
    public class TelemetryInitialData
    {
        public string ParticipentID;
        //public string SessionID;
        public string Date;
        public string StartTime;


    }

    //variables that will change at runtime
    [System.Serializable]
    public class TelementryRunTimeData
    {
        public string actionTimeCode;
        public string buttonPressedID;
        public string experienceMode;
        public string mediaType;
        public string mediaShown;
        public string context;
        public string deskMenuSelection;
    }


    //creates the Json file when called. Gives a random ID and pushes initial data to the Json file.
    public void CreateFile()
    {
        //create the random ID name
        System.Random random = new System.Random();

        char letter1 = (char)random.Next('A', 'Z');
        char letter2 = (char)random.Next('A', 'Z');
        char num1 = (char)random.Next('0', '9');
        char num2 = (char)random.Next('0', '9');
        char num3 = (char)random.Next('0', '9');
        char num4 = (char)random.Next('0', '9');

        ID = ("Participent_" + letter1.ToString() + letter2.ToString() + num1.ToString() + num2.ToString() + num3.ToString() + num4.ToString());

        //assign the current date and time
        currentDate = System.DateTime.Now.ToShortDateString(); 
        currentTime = System.DateTime.Now.ToLongTimeString(); 

        //make date and time compatible with file names
        currentDate = currentDate.Replace("/", "-");
        currentTime = currentTime.Replace(":", "-");

        //assign the start initial data
        telementryInitialData.ParticipentID = ID;
        telementryInitialData.Date = currentDate;
        telementryInitialData.StartTime = currentTime;

        //create the file name with the date and time
        fileName = ID + "_" + currentDate + "_" + currentTime;

        //creates a temp string that contains all the data to be pushed
        string lineToPush;

        lineToPush = JsonUtility.ToJson(telementryInitialData, true);

        //saves the file to a directory descirbed in the function
        saveFile(lineToPush);

    }


    public void AddLine(string buttonPressed)
    {
        string lineToPush;
        //initialise the data to be pushed
        telementryRuntimeData = new TelementryRunTimeData();

        //add the time the action happend
        telementryRuntimeData.actionTimeCode = DateTime.Now.ToString("HH:mm:ss.ff");

        //add the button pressed
        telementryRuntimeData.buttonPressedID = buttonPressed;


        //what experience are they in, Desk or Walking
        telementryRuntimeData.experienceMode = SceneManager.GetActiveScene().name;



        //This gets the game object of the content that this shown
        GameObject shownMedia;
        shownMedia = GetShownMedia();
        telementryRuntimeData.mediaShown = "";
        telementryRuntimeData.mediaType = "";

        if(shownMedia != null)
        {
            if(contentHolder.activeSelf || narrativeHolder.activeSelf)
            {
                switch (shownMedia.tag)
                {
                    case "Image":
                        telementryRuntimeData.mediaShown = shownMedia.GetComponent<Image>().sprite.name;
                        telementryRuntimeData.mediaType = "Image";
                        break;

                    case "Video":
                        telementryRuntimeData.mediaShown = shownMedia.GetComponent<VideoPlayer>().clip.name;
                        telementryRuntimeData.mediaType = "Video";
                        break;

                    case "Panel":
                        telementryRuntimeData.mediaShown = shownMedia.name;
                        telementryRuntimeData.mediaType = "Timeline";
                        break;
                    case "Introduction":
                        telementryRuntimeData.mediaShown = shownMedia.name;
                        telementryRuntimeData.mediaType = shownMedia.tag;
                        break;

                    default:
                        Debug.LogError("No Tag Found");
                        break;

                }

            } 
            else
            {
                telementryRuntimeData.mediaShown = "";
                telementryRuntimeData.mediaType = "";
               

            } 
          
            
        }
       
       
        //foreach loop that outputs what context the player is in currently
        if(!hubRoom.activeSelf)
        {
            foreach (GameObject go in rooms)
            {
                if (go.activeSelf)
                {
                    telementryRuntimeData.context = go.name;
                }
            }
        }
        else { telementryRuntimeData.context = hubRoom.name; }

        //depending on what menu options are visible, this will set the telemetry runtime data field deskMenuSelection as the correct thing that is hovered over
        int ringPosNumber = ringPosition();
        if(buttonPressed != "")
        {
            if (contentManager.Menu1.activeInHierarchy)
            {
                if (ringPosNumber == 0)
                {
                    telementryRuntimeData.deskMenuSelection = "Kosovo in Former Yugoslavia";
                }
                else if (ringPosNumber == 1)
                {
                    telementryRuntimeData.deskMenuSelection = "Civil Resistance";
                }
                else if (ringPosNumber == 2)
                {
                    telementryRuntimeData.deskMenuSelection = "School House";
                }
            }
            else if (contentManager.T1Options.activeInHierarchy)
            {
                if (ringPosNumber == 0)
                {
                    telementryRuntimeData.deskMenuSelection = "Images";
                }
                else if (ringPosNumber == 1)
                {
                    telementryRuntimeData.deskMenuSelection = "Timeline";
                }
            }
            else if (contentManager.T2Options.activeInHierarchy)
            {
                if (ringPosNumber == 0)
                {
                    telementryRuntimeData.deskMenuSelection = "Images";
                }
                else if (ringPosNumber == 1)
                {
                    telementryRuntimeData.deskMenuSelection = "Videos";
                }
                else if (ringPosNumber == 2)
                {
                    telementryRuntimeData.deskMenuSelection = "Timeline";
                }
            }
            else if (contentManager.T3Options.activeInHierarchy)
            {
                if (ringPosNumber == 0)
                {
                    telementryRuntimeData.deskMenuSelection = "Images";
                }
                else if (ringPosNumber == 1)
                {
                    telementryRuntimeData.deskMenuSelection = "Videos";
                }
                else if (ringPosNumber == 2)
                {
                    telementryRuntimeData.deskMenuSelection = "Archive";
                }
                else if (ringPosNumber == 3)
                {
                    telementryRuntimeData.deskMenuSelection = "Timeline";
                }
            }
            //if there is no content that is highlighted, set this to 0
            else
            {
                telementryRuntimeData.deskMenuSelection = "";
            }
        }
        else if(buttonPressed == "")
        {
            telementryRuntimeData.deskMenuSelection = "";
        }




        //pushes the data to a json file
        lineToPush = JsonUtility.ToJson(telementryRuntimeData, true);

        //saves the file
        saveFile(lineToPush);

    }


    //method that returns the game object that is shown
   public GameObject GetShownMedia()
    {
        //goes through each child within the content holder and if it is active (ie being shown to the player) then retun the gamObject
        if(narrativeHolder.gameObject.activeSelf)
        {
            foreach(Transform child in narrativeHolder.transform)
            {
                if(child.gameObject.activeSelf)
                {
                    return child.gameObject;
                }
            }
        }
        else
        {
            foreach (Transform child in contentHolder.transform)
            {
                if (child.gameObject.activeSelf)
                {

                    switch (child.tag)
                    {
                        case "Image":
                            return child.GetComponentInChildren<Image>().gameObject;

                        case "Video":
                            return child.GetComponentInChildren<VideoPlayer>().gameObject;

                        case "Panel":
                            return child.gameObject;
                        default:
                            return null;

                    }
                }
            }
        }
        return null;

    }

    //method that returns the position of the active selection ring
    public int ringPosition()
    {
        
       foreach(GameObject go in selectionRings)
        {
            if(go.activeInHierarchy)
            {
                switch(go.name)
                {
                    case "Menu1SelectionRing":
                        for(int i = 0; i < contentManager.Menu1Positions.Length; i++)
                        {
                            if (go.transform.position == contentManager.Menu1Positions[i].position)
                                return i;
                        }
                        break;
                    case "T1SelectionRing":
                        for (int i = 0; i < contentManager.T1MenuPositions.Length; i++)
                        {
                            if (go.transform.position == contentManager.T1MenuPositions[i].position)
                                return i;
                        }
                        break;
                    case "T2SelectionRing":
                        for (int i = 0; i < contentManager.T2MenuPositions.Length; i++)
                        {
                            if (go.transform.position == contentManager.T2MenuPositions[i].position)
                                return i;
                        }
                        break;
                    case "T3SelectionRing":
                        for (int i = 0; i < contentManager.T3MenuPositions.Length; i++)
                        {
                            if (go.transform.position == contentManager.T3MenuPositions[i].position)
                                return i;
                        }
                        break;
                    default:
                        Debug.LogError("No selection ring active");
                        return 10;

                }
            }
            else
            {
               // return 11;
            }
        }
        return 10;
    }


   


    //saves the file, takes in the json string to be added to the file
    public void saveFile(string toSave)
    {
        string fileType = fileName + ".json";

        //if the path does not exsist, it will create a new file. If it does it will update/overwrite the file
        string path = Path.Combine(Application.persistentDataPath, fileType);
           


       /* if (SceneManager.GetActiveScene().name != "Menu")
        {
            debugText.text = "Path : " + path;
            Debug.Log("Path: " + path);
        }*/
           

        StreamWriter SW = new StreamWriter(path, true);
        SW.WriteLine(toSave);
        SW.Close();
        

    }


}
