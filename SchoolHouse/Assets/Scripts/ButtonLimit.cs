using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLimit : MonoBehaviour
{

    [SerializeField]
    private GameObject maxPos, minPos;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= minPos.transform.position.y)
        {
            transform.position = minPos.transform.position;
        }

        if (transform.position.y <= maxPos.transform.position.y)
        {
            transform.position = maxPos.transform.position;
        }
    }
}
