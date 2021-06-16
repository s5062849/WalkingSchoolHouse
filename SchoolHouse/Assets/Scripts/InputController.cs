using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{

    public Transform Oculus_ReferencePosition;
    public Transform Oculus2_ReferencePosition;

    public WallBuilder _wallBuilder;
    public DeskBuilder _deskBuilder;
    public ChairBuilder _chairBuilder;

    private Transform ReferencePosition;

    private int BuildStage;

    public GameObject InstructionCanvas;

    public GameObject Ins1, Ins2, Ins3, Ins4, Ins5, Ins6, Ins7, Ins8, Ins9, Ins10, Ins11, Ins12, Ins13, Ins14, Ins15, Ins16;

    public GameObject Wall, Desk, Chair, Button1, Button2, Button3, Button4;

    public GameObject AttachReference, GlobalReference;

    public GameObject LaserPointer;

    public GameObject MainMenu;

    public Button LoadButton;

    public GameObject ContentHolder;

    // Wall values
    private float WallYrot;
    private Vector3 WallPosition;

    // Desk values
    private float DeskYrot;
    private Vector3 DeskPosition;
    private Vector3 DeskHeight;
    private Vector3 DeskRight;
    private Vector3 DeskLeft;
    private Vector3 DeskBack;

    // Button values
    private Vector3 Button1Pos;
    private Vector3 Button2Pos;
    private Vector3 Button3Pos;
    private Vector3 Button4Pos;

    // Chair values
    private float ChairYrot;
    private Vector3 ChairPosition;
    private Vector3 ChairHeight;
    private Vector3 ChairRight;
    private Vector3 ChairLeft;
    private Vector3 ChairBack;
    private Vector3 ChairBackHeight;

    // Used to see if user is building room
    private bool Building = false;

    // Start is called before the first frame update
    void Start()
    {
        ReturnReferencePosition();

        //Hide instruction canvas
        InstructionCanvas.SetActive(false);

        if (PlayerPrefs.GetInt("IsSavedData") == 1)
        {
            LoadButton.interactable = true;
        }
        else
        {
            LoadButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            // Wall position
            if (BuildStage == 0 && Building == true)
            {
                Wall.SetActive(true);

                WallYrot = ReturnReferencePosition().transform.eulerAngles.y;

                WallPosition = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);

                _wallBuilder.UpdateWallPosition(WallPosition, WallYrot);

                // Save values
                PlayerPrefs.SetFloat("WallPositionX", WallPosition.x);
                PlayerPrefs.SetFloat("WallPositionY", WallPosition.y);
                PlayerPrefs.SetFloat("WallPositionZ", WallPosition.z);

                PlayerPrefs.SetFloat("WallRotationY", WallYrot);

                // Change instructions
                Ins1.SetActive(false);
                Ins2.SetActive(true);
            }

            // Desk position
            if (BuildStage == 1 && Building == true)
            {
                Desk.SetActive(true);

                DeskYrot = ReturnReferencePosition().transform.eulerAngles.y;

                DeskPosition = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);

                _deskBuilder.UpdateDeskPosition(DeskPosition, DeskYrot);

                // Save values
                PlayerPrefs.SetFloat("DeskPositionX", DeskPosition.x);
                PlayerPrefs.SetFloat("DeskPositionY", DeskPosition.y);
                PlayerPrefs.SetFloat("DeskPositionZ", DeskPosition.z);

                PlayerPrefs.SetFloat("DeskRotationY", DeskYrot);

                // Change instructions
                Ins2.SetActive(false);
                Ins3.SetActive(true);
            }

            // Desk height
            if (BuildStage == 2 && Building == true)
            {
                DeskHeight = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);

                _deskBuilder.UpdateDeskHeight(DeskHeight);

                // Save values
                PlayerPrefs.SetFloat("DeskHeightX", DeskHeight.x);
                PlayerPrefs.SetFloat("DeskHeightY", DeskHeight.y);
                PlayerPrefs.SetFloat("DeskHeightZ", DeskHeight.z);

                // Change instructions
                Ins3.SetActive(false);
                Ins4.SetActive(true);
            }


            // Desk right
            if (BuildStage == 3 && Building == true)
            {
                DeskRight = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);

                _deskBuilder.UpdateDeskRight(DeskRight);

                // Save values
                PlayerPrefs.SetFloat("DeskRightX", DeskRight.x);
                PlayerPrefs.SetFloat("DeskRightY", DeskRight.y);
                PlayerPrefs.SetFloat("DeskRightZ", DeskRight.z);

                // Change instructions
                Ins4.SetActive(false);
                Ins5.SetActive(true);
            }

            // Desk left
            if (BuildStage == 4 && Building == true)
            {
                DeskLeft = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);

                _deskBuilder.UpdateDeskLeft(DeskLeft);

                // Save values
                PlayerPrefs.SetFloat("DeskLeftX", DeskLeft.x);
                PlayerPrefs.SetFloat("DeskLeftY", DeskLeft.y);
                PlayerPrefs.SetFloat("DeskLeftZ", DeskLeft.z);

                ContentHolder.transform.position = AttachReference.transform.position;

                // Change instructions
                Ins5.SetActive(false);
                Ins6.SetActive(true);
            }

            // Desk back
            if (BuildStage == 5 && Building == true)
            {
                DeskBack = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);

                _deskBuilder.UpdateDeskBack(DeskBack);

                // Save values
                PlayerPrefs.SetFloat("DeskBackX", DeskBack.x);
                PlayerPrefs.SetFloat("DeskBackY", DeskBack.y);
                PlayerPrefs.SetFloat("DeskBackZ", DeskBack.z);

                // Change instructions
                Ins6.SetActive(false);
                Ins7.SetActive(true);
            }

            // Button 1 
            if (BuildStage == 6 && Building == true)
            {
                Button1.SetActive(true);

                Button1Pos = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);
                _deskBuilder.UpdateButton1(Button1Pos);

                // Save values
                PlayerPrefs.SetFloat("Button1X", Button1Pos.x);
                PlayerPrefs.SetFloat("Button1Y", Button1Pos.y);
                PlayerPrefs.SetFloat("Button1Z", Button1Pos.z);

                // Change instructions
                Ins7.SetActive(false);
                Ins8.SetActive(true);
            }

            // Button 2
            if (BuildStage == 7 && Building == true)
            {
                Button2.SetActive(true);

                Button2Pos = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);
                _deskBuilder.UpdateButton2(Button2Pos);

                // Save values
                PlayerPrefs.SetFloat("Button2X", Button2Pos.x);
                PlayerPrefs.SetFloat("Button2Y", Button2Pos.y);
                PlayerPrefs.SetFloat("Button2Z", Button2Pos.z);

                // Change instructions
                Ins8.SetActive(false);
                Ins9.SetActive(true);
            }

            // Button 3
            if (BuildStage == 8 && Building == true)
            {
                Button3.SetActive(true);

                Button3Pos = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);
                _deskBuilder.UpdateButton3(Button3Pos);

                // Save values
                PlayerPrefs.SetFloat("Button3X", Button3Pos.x);
                PlayerPrefs.SetFloat("Button3Y", Button3Pos.y);
                PlayerPrefs.SetFloat("Button3Z", Button3Pos.z);

                // Change instructions
                Ins9.SetActive(false);
                Ins10.SetActive(true);
            }

            // Button 4
            if (BuildStage == 9 && Building == true)
            {
                Button4.SetActive(true);

                Button4Pos = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);
                _deskBuilder.UpdateButton4(Button4Pos);

                // Save values
                PlayerPrefs.SetFloat("Button4X", Button4Pos.x);
                PlayerPrefs.SetFloat("Button4Y", Button4Pos.y);
                PlayerPrefs.SetFloat("Button4Z", Button4Pos.z);

                // Change instructions
                Ins10.SetActive(false);
                Ins11.SetActive(true);
            }

            // Chair position
            if (BuildStage == 10 && Building == true)
            {
                Chair.SetActive(true);

                ChairYrot = ReturnReferencePosition().transform.eulerAngles.y;

                ChairPosition = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);

                _chairBuilder.UpdateChairPosition(ChairPosition, ChairYrot);

                // Save values
                PlayerPrefs.SetFloat("ChairPositionX", ChairPosition.x);
                PlayerPrefs.SetFloat("ChairPositionY", ChairPosition.y);
                PlayerPrefs.SetFloat("ChairPositionZ", ChairPosition.z);

                PlayerPrefs.SetFloat("ChairRotationY", ChairYrot);

                // Change instructions
                Ins11.SetActive(false);
                Ins12.SetActive(true);
            }

            // Chair height
            if (BuildStage == 11 && Building == true)
            {
                ChairHeight = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);

                _chairBuilder.UpdateChairHeight(ChairHeight);

                // Save values
                PlayerPrefs.SetFloat("ChairHeightX", ChairHeight.x);
                PlayerPrefs.SetFloat("ChairHeightY", ChairHeight.y);
                PlayerPrefs.SetFloat("ChairHeightZ", ChairHeight.z);

                // Change instructions
                Ins12.SetActive(false);
                Ins13.SetActive(true);
            }

            // Chair right
            if (BuildStage == 12 && Building == true)
            {
                ChairRight = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);

                _chairBuilder.UpdateChairRight(ChairRight);

                // Save values
                PlayerPrefs.SetFloat("ChairRightX", ChairRight.x);
                PlayerPrefs.SetFloat("ChairRightY", ChairRight.y);
                PlayerPrefs.SetFloat("ChairRightZ", ChairRight.z);

                // Change instructions
                Ins13.SetActive(false);
                Ins14.SetActive(true);
            }

            // Chair left
            if (BuildStage == 13 && Building == true)
            {
                ChairLeft = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);

                _chairBuilder.UpdateChairLeft(ChairLeft);

                // Save values
                PlayerPrefs.SetFloat("ChairLeftX", ChairLeft.x);
                PlayerPrefs.SetFloat("ChairLeftY", ChairLeft.y);
                PlayerPrefs.SetFloat("ChairLeftZ", ChairLeft.z);

                // Change instructions
                Ins14.SetActive(false);
                Ins15.SetActive(true);
            }

            // Chair back
            if (BuildStage == 14 && Building == true)
            {
                ChairBack = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);

                _chairBuilder.UpdateChairBack(ChairBack);

                // Save values
                PlayerPrefs.SetFloat("ChairBackX", ChairBack.x);
                PlayerPrefs.SetFloat("ChairBackY", ChairBack.y);
                PlayerPrefs.SetFloat("ChairBackZ", ChairBack.z);

                // Change instructions
                Ins15.SetActive(false);
                Ins16.SetActive(true);
            }

            // Chair back height
            if (BuildStage == 15 && Building == true)
            {
                ChairBackHeight = new Vector3(ReturnReferencePosition().transform.position.x, ReturnReferencePosition().transform.position.y, ReturnReferencePosition().transform.position.z);

                _chairBuilder.UpdateChairBackHeight(ChairBackHeight);

                // Save values
                PlayerPrefs.SetFloat("ChairBackHeightX", ChairBackHeight.x);
                PlayerPrefs.SetFloat("ChairBackHeightY", ChairBackHeight.y);
                PlayerPrefs.SetFloat("ChairBackHeightZ", ChairBackHeight.z);

                // Change instructions
                Ins16.SetActive(false);
                InstructionCanvas.SetActive(false);

                // Save object positions
                CombineScene();

                PlayerPrefs.SetInt("IsSavedData", 1);

                LaserPointer.SetActive(true);
            }

            if (BuildStage > 15 && Building == true)
            {
                ResetGlobalPosition();
                LaserPointer.SetActive(true);
            }

            if (Building == true)
            {
                BuildStage++;
            }
        }
    }

    // Return correct reference position depending on what model Quest is being used
    public Transform ReturnReferencePosition()
    {
        // If Oculus Quest 2
        if (OVRPlugin.GetSystemHeadsetType() == OVRPlugin.SystemHeadset.Oculus_Quest_2)
        {
            ReferencePosition = Oculus2_ReferencePosition;
        }

        // If Oculus Quest
        if (OVRPlugin.GetSystemHeadsetType() == OVRPlugin.SystemHeadset.Oculus_Quest)
        {
            ReferencePosition = Oculus_ReferencePosition;
        }

        // If Oculus Quest 2 Link
        if (OVRPlugin.GetSystemHeadsetType() == OVRPlugin.SystemHeadset.Oculus_Link_Quest_2)
        {
            ReferencePosition = Oculus2_ReferencePosition;
        }

        // If Oculus Quest Link
        if (OVRPlugin.GetSystemHeadsetType() == OVRPlugin.SystemHeadset.Oculus_Link_Quest)
        {
            ReferencePosition = Oculus_ReferencePosition;
        }

        return ReferencePosition;
    }

    private void CombineScene()
    {
        // Set reset position
        GlobalReference.transform.position = AttachReference.transform.position;

        GlobalReference.transform.rotation = AttachReference.transform.rotation;

        // Make objects child of global reference position
        Wall.transform.parent = GlobalReference.transform;
        Desk.transform.parent = GlobalReference.transform;
        Chair.transform.parent = GlobalReference.transform;
        Button1.transform.parent = GlobalReference.transform;
        Button2.transform.parent = GlobalReference.transform;
        Button3.transform.parent = GlobalReference.transform;
        Button4.transform.parent = GlobalReference.transform;
    }

    private void ResetGlobalPosition()
    {
        // Change global position
        GlobalReference.transform.position = new Vector3(ReturnReferencePosition().position.x, AttachReference.transform.position.y, ReturnReferencePosition().position.z);

        // Define wall rotation value
        Vector3 GlobalRotation = new Vector3(0, ReturnReferencePosition().eulerAngles.y, 0);

        // Change wall rotation
        GlobalReference.transform.rotation = Quaternion.Euler(GlobalRotation);
    }

    // Build room selection
    public void SetBuilding()
    {
        Building = true;

        InstructionCanvas.SetActive(true);

        LaserPointer.SetActive(false);

        MainMenu.SetActive(false);
    }

    // Load building from save
    public void LoadBuilding()
    {
        MainMenu.SetActive(false);
        LaserPointer.SetActive(false);

        // Step 1 - Wall position and rotation

        WallYrot = PlayerPrefs.GetFloat("WallRotationY", 0);

        WallPosition = new Vector3(PlayerPrefs.GetFloat("WallPositionX", 0), PlayerPrefs.GetFloat("WallPositionY", 0), PlayerPrefs.GetFloat("WallPositionZ", 0));

        _wallBuilder.UpdateWallPosition(WallPosition, WallYrot);

        // Step 2 - Desk position and rotation

        DeskYrot = PlayerPrefs.GetFloat("DeskRotationY", 0);

        DeskPosition = new Vector3(PlayerPrefs.GetFloat("DeskPositionX", 0), PlayerPrefs.GetFloat("DeskPositionY", 0), PlayerPrefs.GetFloat("DeskPositionZ", 0));

        _deskBuilder.UpdateDeskPosition(DeskPosition, DeskYrot);

        // Step 3 - Desk height

        DeskHeight = new Vector3(PlayerPrefs.GetFloat("DeskHeightX", 0), PlayerPrefs.GetFloat("DeskHeightY", 0), PlayerPrefs.GetFloat("DeskHeightZ", 0));

        _deskBuilder.UpdateDeskHeight(DeskHeight);

        // Step 4 - Desk right

        DeskRight = new Vector3(PlayerPrefs.GetFloat("DeskRightX", 0), PlayerPrefs.GetFloat("DeskRightY", 0), PlayerPrefs.GetFloat("DeskRightZ", 0));

        _deskBuilder.UpdateDeskRight(DeskRight);

        // Step 5 - Desk left

        DeskLeft = new Vector3(PlayerPrefs.GetFloat("DeskLeftX", 0), PlayerPrefs.GetFloat("DeskLeftY", 0), PlayerPrefs.GetFloat("DeskLeftZ", 0));

        _deskBuilder.UpdateDeskLeft(DeskLeft);

        ContentHolder.transform.position = AttachReference.transform.position;

        // Step 6 - Desk back

        DeskBack = new Vector3(PlayerPrefs.GetFloat("DeskBackX", 0), PlayerPrefs.GetFloat("DeskBackY", 0), PlayerPrefs.GetFloat("DeskBackZ", 0));

        _deskBuilder.UpdateDeskBack(DeskBack);

        // Step 7 - Button 1 position

        Button1Pos = new Vector3(PlayerPrefs.GetFloat("Button1X"), PlayerPrefs.GetFloat("Button1Y"), PlayerPrefs.GetFloat("Button1Z"));

        _deskBuilder.UpdateButton1(Button1Pos);

        // Step 8 - Button 2 position

        Button2Pos = new Vector3(PlayerPrefs.GetFloat("Button2X"), PlayerPrefs.GetFloat("Button2Y"), PlayerPrefs.GetFloat("Button2Z"));

        _deskBuilder.UpdateButton2(Button2Pos);

        // Step 9 - Button 3 position

        Button3Pos = new Vector3(PlayerPrefs.GetFloat("Button3X"), PlayerPrefs.GetFloat("Button3Y"), PlayerPrefs.GetFloat("Button3Z"));

        _deskBuilder.UpdateButton3(Button3Pos);

        // Step 10 - Button 4 position

        Button4Pos = new Vector3(PlayerPrefs.GetFloat("Button4X"), PlayerPrefs.GetFloat("Button4Y"), PlayerPrefs.GetFloat("Button4Z"));

        _deskBuilder.UpdateButton4(Button4Pos);

        // Step 11 - Chair position and rotation

        ChairYrot = PlayerPrefs.GetFloat("ChairRotationY", 0);

        ChairPosition = new Vector3(PlayerPrefs.GetFloat("ChairPositionX", 0), PlayerPrefs.GetFloat("ChairPositionY", 0), PlayerPrefs.GetFloat("ChairPositionZ", 0));

        _chairBuilder.UpdateChairPosition(ChairPosition, ChairYrot);

        // Step 12 - Chair height

        ChairHeight = new Vector3(PlayerPrefs.GetFloat("ChairHeightX", 0), PlayerPrefs.GetFloat("ChairHeightY", 0), PlayerPrefs.GetFloat("ChairHeightZ", 0));

        _chairBuilder.UpdateChairHeight(ChairHeight);

        // Step 13 - Chair right

        ChairRight = new Vector3(PlayerPrefs.GetFloat("ChairRightX", 0), PlayerPrefs.GetFloat("ChairRightY", 0), PlayerPrefs.GetFloat("ChairRightZ", 0));

        _chairBuilder.UpdateChairRight(ChairRight);

        // Step 14 - Chair left

        ChairLeft = new Vector3(PlayerPrefs.GetFloat("ChairLeftX", 0), PlayerPrefs.GetFloat("ChairLeftY", 0), PlayerPrefs.GetFloat("ChairLeftZ", 0));

        _chairBuilder.UpdateChairLeft(ChairLeft);

        // Step 15 - Chair back

        ChairBack = new Vector3(PlayerPrefs.GetFloat("ChairBackX", 0), PlayerPrefs.GetFloat("ChairBackY", 0), PlayerPrefs.GetFloat("ChairBackZ", 0));

        _chairBuilder.UpdateChairBack(ChairBack);

        // Step 16 - Chair back height

        ChairBackHeight = new Vector3(PlayerPrefs.GetFloat("ChairBackHeightX", 0), PlayerPrefs.GetFloat("ChairBackHeightY", 0), PlayerPrefs.GetFloat("ChairBackHeightZ", 0));

        _chairBuilder.UpdateChairBackHeight(ChairBackHeight);

        // Make objects child of global reference position
        CombineScene();

        // Next click will realign position
        BuildStage = 16;

        Building = true;
    }
}
