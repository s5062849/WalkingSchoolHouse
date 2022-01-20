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


    public GameObject contentHolder;
    public GameObject playerCam;

    public static GameObject instance = null;

    public TextMeshProUGUI debugText;
    //public TextMeshPro debugText;

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
        }
       
        playerCam = GameObject.FindWithTag("MainCamera");
       

        
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
        public float headXcoord;
        public float headYcoord;
        public float headZcoord;
        public string experienceMode;
        public string mediaType;
        public string mediaShown;
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
        telementryRuntimeData.actionTimeCode = System.DateTime.Now.ToString("HH:mm:ss.fff");

        //the current head position ##### CAN BE DELETED LATER ######
        telementryRuntimeData.headXcoord = playerCam.transform.position.x;
        telementryRuntimeData.headYcoord = playerCam.transform.position.y;
        telementryRuntimeData.headZcoord = playerCam.transform.position.z;

        //add the button pressed
        telementryRuntimeData.buttonPressedID = buttonPressed;


        //what experience are they in, Desk or Walking
        telementryRuntimeData.experienceMode = SceneManager.GetActiveScene().name;



        //This gets the game object of the content that this shown
        GameObject shownMedia = GetShownMedia();
        if(shownMedia != null)
        {
            Debug.Log(shownMedia.name);
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

                default:
                    Debug.LogError("No Tag Found");
                    break;

            }
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
        foreach(Transform child in contentHolder.transform)
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

        return null;



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
