using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingInputController : MonoBehaviour
{

    public Transform Oculus_ReferencePosition;
    public Transform Oculus2_ReferencePosition;

    public WallBuilder _wallBuilder;

    private Transform ReferencePosition;

    private int BuildStage;

    public GameObject InstructionCanvas;

    public GameObject Wall;

    public GameObject LaserPointer;

    public GameObject MainMenu;

    [SerializeField] AudioController AC;

    // Wall values
    private float WallYrot;
    private Vector3 WallPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            // Wall position
            if (BuildStage == 0)
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

                // Hide instructions
                InstructionCanvas.SetActive(false);

                AC.PlayMusic("StartMenuMusic"); 

                BuildStage += 1;

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

    // Build room selection
    public void SetBuilding()
    {
        InstructionCanvas.SetActive(true);

        LaserPointer.SetActive(false);

        MainMenu.SetActive(false);
    }
}
