using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskBuilder : MonoBehaviour
{

    public Transform RightRef, LeftRef, BackRef;

    public GameObject DeskTop;

    public GameObject DeskSurface;

    private float DirectionLengthRight, DirectionLengthLeft, DirectionLengthBack;

    private float RightAngle;
    private float LeftAngle;
    private float BackAngle;

    private float AddedLengthRight, AddedLengthLeft, AddedLengthBack;

    public GameObject Leg1, Leg2, Leg3, Leg4;

    private Renderer DeskMaterialRenderer;

    public GameObject Button1, Button2, Button3, Button4;

    // Start is called before the first frame update
    void Start()
    {
        DeskMaterialRenderer = DeskSurface.GetComponent<Renderer>();
        PlaceLegs();
    }

    // Change position and rotation of desk
    public void UpdateDeskPosition(Vector3 PositionReference, float Yrotation)
    {
        // Change desk position
        transform.position = new Vector3(PositionReference.x, PositionReference.y, PositionReference.z);

        // Define desk rotation value
        Vector3 DeskRotation = new Vector3(0, Yrotation, 0);

        // Change desk rotation
        transform.rotation = Quaternion.Euler(DeskRotation);

        PlaceLegs();
    }

    // Change desk height
    public void UpdateDeskHeight(Vector3 PositionReference)
    {
        // Change desk height only changing Y value
        transform.position = new Vector3(transform.position.x, PositionReference.y, transform.position.z);

        PlaceLegs();
    }

    public void UpdateDeskRight(Vector3 PositionReference)
    {
        // Get Vector from the controller reference position to the centre of the table
        Vector3 DirectionRight = new Vector3(PositionReference.x, 0, PositionReference.z) - new Vector3(DeskTop.transform.position.x, 0, DeskTop.transform.position.z);

        // Calculate length of Vector
        DirectionLengthRight = DirectionRight.magnitude;

        // Get Vector in straight line from the right of the table
        Vector3 RightDirection = new Vector3(RightRef.transform.position.x, 0, RightRef.transform.position.z) - new Vector3(DeskTop.transform.position.x, 0, DeskTop.transform.position.z);

        // Calculate angle between both Vectors
        RightAngle = Vector3.Angle(RightDirection, DirectionRight);

        // Calculate added length needed
        AddedLengthRight = DirectionLengthRight * Mathf.Cos((RightAngle * Mathf.Deg2Rad));

        // Add required length
        DeskSurface.transform.localScale = new Vector3(AddedLengthRight + 0.5f, DeskSurface.transform.localScale.y, DeskSurface.transform.localScale.z);

        // Move table into position after length is added
        DeskSurface.transform.localPosition += new Vector3((AddedLengthRight / 2) - 0.25f, 0, 0);

        UpdateTexture();
        PlaceLegs();
    }

    public void UpdateDeskLeft(Vector3 PositionReference)
    {
        // Get Vector from the controller reference position to the centre of the table
        Vector3 DirectionLeft = new Vector3(PositionReference.x, 0, PositionReference.z) - new Vector3(DeskTop.transform.position.x, 0, DeskTop.transform.position.z);

        // Calculate length of Vector
        DirectionLengthLeft = DirectionLeft.magnitude;

        // Get Vector in straight line from the left of the table
        Vector3 LeftDirection = new Vector3(LeftRef.transform.position.x, 0, LeftRef.transform.position.z) - new Vector3(DeskTop.transform.position.x, 0, DeskTop.transform.position.z);

        // Calculate angle between both Vectors
        LeftAngle = Vector3.Angle(LeftDirection, DirectionLeft);

        // Calculate added length needed
        AddedLengthLeft = DirectionLengthLeft * Mathf.Cos((LeftAngle * Mathf.Deg2Rad));

        // Add required length
        DeskSurface.transform.localScale = new Vector3(AddedLengthLeft + AddedLengthRight, DeskSurface.transform.localScale.y, DeskSurface.transform.localScale.z);

        // Move table into position after length is added
        DeskSurface.transform.localPosition -= new Vector3((AddedLengthLeft / 2) - 0.25f, 0, 0);

        UpdateTexture();
        PlaceLegs();
    }

    public void UpdateDeskBack(Vector3 PositionReference)
    {
        // Get Vector from the controller reference position to the centre of the table
        Vector3 DirectionBack = new Vector3(PositionReference.x, 0, PositionReference.z) - new Vector3(DeskTop.transform.position.x, 0, DeskTop.transform.position.z);

        // Calculate length of Vector
        DirectionLengthBack = DirectionBack.magnitude;

        // Get Vector in straight line from the back of the table
        Vector3 BackDirection = new Vector3(BackRef.transform.position.x, 0, BackRef.transform.position.z) - new Vector3(DeskTop.transform.position.x, 0, DeskTop.transform.position.z);

        // Calculate angle between both Vectors
        BackAngle = Vector3.Angle(BackDirection, DirectionBack);

        // Calculate added length needed
        AddedLengthBack = DirectionLengthBack * Mathf.Cos((BackAngle * Mathf.Deg2Rad));

        // Add required length
        DeskSurface.transform.localScale = new Vector3(DeskSurface.transform.localScale.x, DeskSurface.transform.localScale.y, AddedLengthBack + 0.5f);

        // Move table into position after length is added
        DeskSurface.transform.localPosition += new Vector3(0, 0, (AddedLengthBack / 2) - 0.25f);

        UpdateTexture();
        PlaceLegs();
    }

    public void UpdateButton1(Vector3 PositionReference)
    {
        Button1.transform.position = new Vector3(PositionReference.x, transform.position.y, PositionReference.z);
        Button1.transform.rotation = transform.rotation;
    }

    public void UpdateButton2(Vector3 PositionReference)
    {
        Button2.transform.position = new Vector3(PositionReference.x, transform.position.y, PositionReference.z);
        Button2.transform.rotation = transform.rotation;
    }

    public void UpdateButton3(Vector3 PositionReference)
    {
        Button3.transform.position = new Vector3(PositionReference.x, transform.position.y, PositionReference.z);
        Button3.transform.rotation = transform.rotation;
    }

    public void UpdateButton4(Vector3 PositionReference)
    {
        Button4.transform.position = new Vector3(PositionReference.x, transform.position.y, PositionReference.z);
        Button4.transform.rotation = transform.rotation;
    }

    // Update the texture applied to the desk
    private void UpdateTexture()
    {
        DeskMaterialRenderer.material.SetTextureScale("_MainTex", new Vector2(DeskSurface.transform.localScale.x * 4f, DeskSurface.transform.localScale.z * 4f));
    }

    // Place the legs of the desk in position regarding new desk size/position
    private void PlaceLegs()
    {
        Leg1.transform.localPosition = new Vector3((DeskSurface.transform.localPosition.x - (DeskSurface.transform.localScale.x / 2)) + 0.05f, DeskTop.transform.localPosition.y, (DeskSurface.transform.localPosition.z - (DeskSurface.transform.localScale.z / 2)) + 0.05f);
        Leg2.transform.localPosition = new Vector3((DeskSurface.transform.localPosition.x - (DeskSurface.transform.localScale.x / 2)) + 0.05f, DeskTop.transform.localPosition.y, (DeskSurface.transform.localPosition.z + (DeskSurface.transform.localScale.z / 2)) - 0.05f);
        Leg3.transform.localPosition = new Vector3((DeskSurface.transform.localPosition.x + (DeskSurface.transform.localScale.x / 2)) - 0.05f, DeskTop.transform.localPosition.y, (DeskSurface.transform.localPosition.z - (DeskSurface.transform.localScale.z / 2)) + 0.05f);
        Leg4.transform.localPosition = new Vector3((DeskSurface.transform.localPosition.x + (DeskSurface.transform.localScale.x / 2)) - 0.05f, DeskTop.transform.localPosition.y, (DeskSurface.transform.localPosition.z + (DeskSurface.transform.localScale.z / 2)) - 0.05f);
    }
}
