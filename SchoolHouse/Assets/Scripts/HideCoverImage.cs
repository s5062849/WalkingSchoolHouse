using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCoverImage : MonoBehaviour
{

    public GameObject CoverImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideImage()
    {
        CoverImage.SetActive(false);
    }
}
