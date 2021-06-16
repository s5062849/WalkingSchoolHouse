using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{

    public OVRScreenFade ovrsf;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeToBlack()
    {
        ovrsf.FadeOut();
        Invoke("FadeToNormal", 3.0f);
    }

    private void FadeToNormal()
    {
        ovrsf.FadeIn();
    }
}
