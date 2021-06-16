using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInputManager : MonoBehaviour
{

    public Text CurrentSelectionText;

    private int CurrentSelection;

    public GameObject s1, s2, s3, s4, s5, s6;
    public GameObject sb1, sb2, sb3, sb4, sb5, sb6;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSelection = 1;
        UpdateSelectionText();
    }

    private void UpdateSelectionText()
    {
        CurrentSelectionText.text = CurrentSelection.ToString();

        if (CurrentSelection == 1)
        {
            s1.SetActive(true);
            sb1.SetActive(true);
        }
        else
        {
            s1.SetActive(false);
            sb1.SetActive(false);
        }

        if (CurrentSelection == 2)
        {
            s2.SetActive(true);
            sb2.SetActive(true);
        }
        else
        {
            s2.SetActive(false);
            sb2.SetActive(false);
        }

        if (CurrentSelection == 3)
        {
            s3.SetActive(true);
            sb3.SetActive(true);
        }
        else
        {
            s3.SetActive(false);
            sb3.SetActive(false);
        }

        if (CurrentSelection == 4)
        {
            s4.SetActive(true);
            sb4.SetActive(true);
        }
        else
        {
            s4.SetActive(false);
            sb4.SetActive(false);
        }

        if (CurrentSelection == 5)
        {
            s5.SetActive(true);
            sb5.SetActive(true);
        }
        else
        {
            s5.SetActive(false);
            sb5.SetActive(false);
        }

        if (CurrentSelection == 6)
        {
            s6.SetActive(true);
            sb6.SetActive(true);
        }
        else
        {
            s6.SetActive(false);
            sb6.SetActive(false);
        }
    }

    public void IncreaseSelection()
    {
        CurrentSelection = CurrentSelection + 1;

        if (CurrentSelection > 6)
        {
            CurrentSelection = 1;
        }

        UpdateSelectionText();
    }

    public void DecreaseSelection()
    {
        CurrentSelection = CurrentSelection - 1;

        if (CurrentSelection < 1)
        {
            CurrentSelection = 6;
        }

        UpdateSelectionText();
    }

}
