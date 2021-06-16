using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairBuilder : MonoBehaviour
{

    public Transform RightRef, LeftRef, BackRef, UpRef;

    public GameObject ChairTop;

    public GameObject ChairBack;

    public GameObject ChairSurface;

    public GameObject ChairBackSurface;

    private float DirectionLengthRight, DirectionLengthLeft, DirectionLengthBack, DirectionLengthUp;

    private float RightAngle;
    private float LeftAngle;
    private float BackAngle;
    private float UpAngle;

    private float AddedLengthRight, AddedLengthLeft, AddedLengthBack, AddedLengthUp;

    public GameObject Leg1, Leg2, Leg3, Leg4;

    private Renderer ChairMaterialRenderer, ChairBackMaterialRenderer;

    // Start is called before the first frame update
    void Start()
    {
        ChairMaterialRenderer = ChairSurface.GetComponent<Renderer>();
        ChairBackMaterialRenderer = ChairBackSurface.GetComponent<Renderer>();

        PlaceLegs();
    }

    // Change position and rotation of chair
    public void UpdateChairPosition(Vector3 PositionReference, float Yrotation)
    {
        // Change chair position
        transform.position = new Vector3(PositionReference.x, PositionReference.y, PositionReference.z);

        // Define chair rotation value
        Vector3 ChairRotation = new Vector3(0, Yrotation, 0);

        // Change chair rotation
        transform.rotation = Quaternion.Euler(ChairRotation);

        PlaceLegs();
    }

    // Change chair height
    public void UpdateChairHeight(Vector3 PositionReference)
    {
        // Change chair height only changing Y value
        transform.position = new Vector3(transform.position.x, PositionReference.y, transform.position.z);

        PlaceLegs();
    }

    public void UpdateChairRight(Vector3 PositionReference)
    {
        // Get Vector from the controller reference position to the centre of the chair
        Vector3 DirectionRight = new Vector3(PositionReference.x, 0, PositionReference.z) - new Vector3(ChairTop.transform.position.x, 0, ChairTop.transform.position.z);

        // Calculate length of Vector
        DirectionLengthRight = DirectionRight.magnitude;

        // Get Vector in straight line from the right of the chair
        Vector3 RightDirection = new Vector3(RightRef.transform.position.x, 0, RightRef.transform.position.z) - new Vector3(ChairTop.transform.position.x, 0, ChairTop.transform.position.z);

        // Calculate angle between both Vectors
        RightAngle = Vector3.Angle(RightDirection, DirectionRight);

        // Calculate added length needed
        AddedLengthRight = DirectionLengthRight * Mathf.Cos((RightAngle * Mathf.Deg2Rad));

        // Add required length
        ChairSurface.transform.localScale = new Vector3((AddedLengthRight * 2f) + 0.5f, ChairSurface.transform.localScale.y, ChairSurface.transform.localScale.z);

        ChairBackSurface.transform.localScale = new Vector3((AddedLengthRight * 2f) + 0.5f, ChairBackSurface.transform.localScale.y, ChairBackSurface.transform.localScale.z);

        // Move chair into position after length is added
        ChairSurface.transform.localPosition += new Vector3((AddedLengthRight / 2) - 0.125f, 0, 0);

        ChairBackSurface.transform.localPosition += new Vector3((AddedLengthRight / 2) - 0.125f, 0, 0);

        UpdateTexture();
        PlaceLegs();
    }

    public void UpdateChairLeft(Vector3 PositionReference)
    {
        // Get Vector from the controller reference position to the centre of the chair
        Vector3 DirectionLeft = new Vector3(PositionReference.x, 0, PositionReference.z) - new Vector3(ChairTop.transform.position.x, 0, ChairTop.transform.position.z);

        // Calculate length of Vector
        DirectionLengthLeft = DirectionLeft.magnitude;

        // Get Vector in straight line from the left of the chair
        Vector3 LeftDirection = new Vector3(LeftRef.transform.position.x, 0, LeftRef.transform.position.z) - new Vector3(ChairTop.transform.position.x, 0, ChairTop.transform.position.z);

        // Calculate angle between both Vectors
        LeftAngle = Vector3.Angle(LeftDirection, DirectionLeft);

        // Calculate added length needed
        AddedLengthLeft = DirectionLengthLeft * Mathf.Cos((LeftAngle * Mathf.Deg2Rad));

        // Add required length
        ChairSurface.transform.localScale = new Vector3((AddedLengthLeft * 2) + (AddedLengthRight * 2), ChairSurface.transform.localScale.y, ChairSurface.transform.localScale.z);

        ChairBackSurface.transform.localScale = new Vector3((AddedLengthLeft * 2) + (AddedLengthRight * 2), ChairBackSurface.transform.localScale.y, ChairBackSurface.transform.localScale.z);

        // Move chair into position after length is added
        ChairSurface.transform.localPosition -= new Vector3((AddedLengthLeft / 2) - 0.125f, 0, 0);

        ChairBackSurface.transform.localPosition -= new Vector3((AddedLengthLeft / 2) - 0.125f, 0, 0);

        UpdateTexture();
        PlaceLegs();
    }

    public void UpdateChairBack(Vector3 PositionReference)
    {
        // Get Vector from the controller reference position to the centre of the chair
        Vector3 DirectionBack = new Vector3(PositionReference.x, 0, PositionReference.z) - new Vector3(ChairTop.transform.position.x, 0, ChairTop.transform.position.z);

        // Calculate length of Vector
        DirectionLengthBack = DirectionBack.magnitude;

        // Get Vector in straight line from the back of the chair
        Vector3 BackDirection = new Vector3(BackRef.transform.position.x, 0, BackRef.transform.position.z) - new Vector3(ChairTop.transform.position.x, 0, ChairTop.transform.position.z);

        // Calculate angle between both Vectors
        BackAngle = Vector3.Angle(BackDirection, DirectionBack);

        // Calculate added length needed
        AddedLengthBack = DirectionLengthBack * Mathf.Cos((BackAngle * Mathf.Deg2Rad));

        // Add required length
        ChairSurface.transform.localScale = new Vector3(ChairSurface.transform.localScale.x, ChairSurface.transform.localScale.y, (AddedLengthBack * 2f) + 0.5f);

        // Move chair into position after length is added
        ChairSurface.transform.localPosition += new Vector3(0, 0, (AddedLengthBack / 2) - 0.125f);

        ChairBack.transform.localPosition = new Vector3(0, 0, ChairSurface.transform.localScale.z / 2);

        UpdateTexture();
        PlaceLegs();
    }

    public void UpdateChairBackHeight(Vector3 PositionReference)
    {
        // Get Vector from the controller reference position to the centre of the back of the chair
        Vector3 DirectionUp = new Vector3(0, PositionReference.y, 0) - new Vector3(0, ChairBackSurface.transform.position.y, 0);

        // Calculate length of Vector
        DirectionLengthUp = DirectionUp.magnitude;

        // Get Vector in straight line from straight above the chair
        Vector3 UpDirection = new Vector3(0, UpRef.transform.position.y, 0) - new Vector3(0, ChairBackSurface.transform.position.y, 0);

        // Calculate angle between both Vectors
        UpAngle = Vector3.Angle(UpDirection, DirectionUp);

        // Calculate added length needed
        AddedLengthUp = DirectionLengthUp * Mathf.Cos((UpAngle * Mathf.Deg2Rad));

        // Add required length
        ChairBackSurface.transform.localScale = new Vector3(ChairBackSurface.transform.localScale.x, ChairBackSurface.transform.localScale.y, (AddedLengthUp * 2) + 0.5f);

        // Move chair into position after length is added
        ChairBackSurface.transform.localPosition += new Vector3(0, (AddedLengthUp / 2) - 0.125f, 0);

        UpdateTexture();
        PlaceLegs();
    }

    // Update the texture applied to the chair
    private void UpdateTexture()
    {
        ChairMaterialRenderer.material.SetTextureScale("_MainTex", new Vector2(ChairSurface.transform.localScale.x * 4, ChairSurface.transform.localScale.z * 4));
        ChairBackMaterialRenderer.material.SetTextureScale("_MainTex", new Vector2(ChairBackSurface.transform.localScale.x * 4, ChairBackSurface.transform.localScale.y * 4));
    }

    // Place the legs of the desk in position regarding new chair size/position
    private void PlaceLegs()
    {
        Leg1.transform.localPosition = new Vector3((ChairSurface.transform.localPosition.x - (ChairSurface.transform.localScale.x / 4)) + 0.05f, ChairTop.transform.localPosition.y, (ChairSurface.transform.localPosition.z - (ChairSurface.transform.localScale.z / 4)) + 0.05f);
        Leg2.transform.localPosition = new Vector3((ChairSurface.transform.localPosition.x - (ChairSurface.transform.localScale.x / 4)) + 0.05f, ChairTop.transform.localPosition.y, (ChairSurface.transform.localPosition.z + (ChairSurface.transform.localScale.z / 4)) - 0.05f);
        Leg3.transform.localPosition = new Vector3((ChairSurface.transform.localPosition.x + (ChairSurface.transform.localScale.x / 4)) - 0.05f, ChairTop.transform.localPosition.y, (ChairSurface.transform.localPosition.z - (ChairSurface.transform.localScale.z / 4)) + 0.05f);
        Leg4.transform.localPosition = new Vector3((ChairSurface.transform.localPosition.x + (ChairSurface.transform.localScale.x / 4)) - 0.05f, ChairTop.transform.localPosition.y, (ChairSurface.transform.localPosition.z + (ChairSurface.transform.localScale.z / 4)) - 0.05f);
    }
}
