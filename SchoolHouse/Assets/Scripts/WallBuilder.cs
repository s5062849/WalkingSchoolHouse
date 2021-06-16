using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallBuilder : MonoBehaviour
{
        // Change position and rotation of wall
        public void UpdateWallPosition(Vector3 PositionReference, float Yrotation)
    {
        // Change wall position
        transform.position = new Vector3(PositionReference.x, 0, PositionReference.z);

        // Define wall rotation value
        Vector3 WallRotation = new Vector3(0, Yrotation, 0);

        // Change wall rotation
        transform.rotation = Quaternion.Euler(WallRotation);
    }
}
