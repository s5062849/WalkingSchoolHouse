using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;

public class ContentManager : MonoBehaviour
{

    private int MenuOption1, MenuOption2;

    private int CurrentSelected;

    public Text MenuOptionText1, MenuOptionText2;

    public Text T1ImageText, T1TimelineText, T2ImageText, T2VideoText, T2TimelineText, T3ImageText, T3VideoText, T3ArchiveText, T3TimelineText;

    public TextMeshProUGUI T1ImageNumberText, T2ImageNumberText, T3ImageNumberText, T2VideoNumberText, T3VideoNumberText, T3ArchiveNumberText, T1PageText, T2PageText, T3PageText;

    public Text CurrentSelectedText;

    public Text PlayPauseText;
    private bool videoPlaying = false;

    private int CurrentT1Image, CurrentT1Timeline, CurrentT2Image, CurrentT2Video, CurrentT2Timeline, CurrentT3Image, CurrentT3Video, CurrentT3Archive, CurrentT3Timeline;

    public GameObject[] T1Images, T2Images, T3Images;

    public GameObject[] T3Archive;

    public GameObject[] T2Videos, T3Videos;

    private VideoPlayer MyVideoPlayer;

    public Transform[] Menu1Positions;
    public Transform[] T1MenuPositions;
    public Transform[] T2MenuPositions;
    public Transform[] T3MenuPositions;

    public GameObject selectionRing1, T1SelectionRing, T2SelectionRing, T3SelectionRing;

    public bool menuVisible = false;

    public GameObject Menu1, Menu2, Menu3;

    public GameObject T1Options, T2Options, T3Options;

    public GameObject T1ImageHolder, T1TimelineHolder, T2ImageHolder, T2VideoHolder, T2TimelineHolder, T3ImageHolder, T3VideoHolder, T3ArchiveHolder, T3TimelineHolder;

    public GameObject FloatingContent;

    public GameObject FloatingT1ImageHolder, FloatingT1TimelineHolder, FloatingT2ImageHolder, FloatingT2VideoHolder, FloatingT2TimelineHolder, FloatingT3ImageHolder, FloatingT3VideoHolder, FloatingT3ArchiveHolder, FloatingT3TimelineHolder;

    public GameObject T1Timeline, T2Timeline, T3Timeline;
    public GameObject TimelinePos, TimelineOriginalPos;

    public float menuSpeed;

    public Fader fader;

    public GameObject MainRoom, T1Room, T2Room, T3Room;

    public GameObject PanelIns1, PanelIns2, PanelIns3, PanelIns4, PanelIns5, PanelIns6, PanelIns7;

    // Narrative
    private bool NarrativeVisible = false;

    public GameObject NarrativePos, NarrativeOriginalPos;

    public GameObject NarrativeHolder;

    private int CurrentT1Page, CurrentT2Page, CurrentT3Page;

    public GameObject T1Narrative, T2Narrative, T3Narrative;
    public GameObject T1NarrativeDesk, T2NarrativeDesk, T3NarrativeDesk;

    private bool T1Playing = false, T2Playing = false, T3Playing = false, MenuPlaying = true;

    private bool T1Done = false, T2Done = false, T3Done = false;

    public Image T1Image, T2Image, T3Image;

    private bool FadeBack = false;

    public GameObject FinalText;

    public bool ExperienceFinished = false;

    public AudioClip T1Music, T2Music, T3Music;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSelected = 1;

        MenuOption1 = 1;
        MenuOption2 = 1;

        ResetValues();
    }

    // Update is called once per frame
    void Update()
    {
        // If T1 timeline - move to right position
        if (CurrentSelected == 3 && MenuOption1 == 1 && MenuOption2 == 2)
        {
            T1Timeline.transform.position = Vector3.Lerp(T1Timeline.transform.position, TimelinePos.transform.position, menuSpeed * Time.deltaTime);         
        }

        // If T2 timeline - move to right position
        if (CurrentSelected == 3 && MenuOption1 == 2 && MenuOption2 == 3)
        {
            T2Timeline.transform.position = Vector3.Lerp(T2Timeline.transform.position, TimelinePos.transform.position, menuSpeed * Time.deltaTime);
        }

        // If T3 timeline - move to right position
        if (CurrentSelected == 3 && MenuOption1 == 3 && MenuOption2 == 4)
        {
            T3Timeline.transform.position = Vector3.Lerp(T3Timeline.transform.position, TimelinePos.transform.position, menuSpeed * Time.deltaTime);
        }

        if (NarrativeVisible == true)
        {
            //Narrative T1 Move
            if (MenuOption1 == 1)
            {
                T1Narrative.transform.position = Vector3.Lerp(T1Narrative.transform.position, NarrativePos.transform.position, menuSpeed * Time.deltaTime);
            }

            //Narrative T2 Move
            if (MenuOption1 == 2)
            {
                T2Narrative.transform.position = Vector3.Lerp(T2Narrative.transform.position, NarrativePos.transform.position, menuSpeed * Time.deltaTime);
            }

            //Narrative T3 Move
            if (MenuOption1 == 3)
            {
                T3Narrative.transform.position = Vector3.Lerp(T3Narrative.transform.position, NarrativePos.transform.position, menuSpeed * Time.deltaTime);
            }
        }
    }

    public void UpdateMenuOption()
    {
        MenuOptionText1.text = MenuOption1.ToString() + ".";
        MenuOptionText2.text = MenuOption2.ToString() + ".";

        T1ImageNumberText.text = CurrentT1Image.ToString() + "/" + T1Images.Length.ToString();
        T1TimelineText.text = CurrentT1Timeline.ToString();

        T2ImageNumberText.text = CurrentT2Image.ToString() + "/" + T2Images.Length.ToString();
        T2VideoNumberText.text = CurrentT2Video.ToString() + "/" + T2Videos.Length.ToString();
        T2TimelineText.text = CurrentT2Timeline.ToString();

        T3ImageNumberText.text = CurrentT3Image.ToString() + "/" + T3Images.Length.ToString();
        T3VideoNumberText.text = CurrentT3Video.ToString() + "/" + T3Videos.Length.ToString();
        T3ArchiveNumberText.text = CurrentT3Archive.ToString() + "/" + T3Archive.Length.ToString();
        T3TimelineText.text = CurrentT3Timeline.ToString();

        CurrentSelectedText.text = CurrentSelected.ToString();

        T1PageText.text = CurrentT1Page.ToString() + "/" + "4";
        T2PageText.text = CurrentT2Page.ToString() + "/" + "5";
        T3PageText.text = CurrentT3Page.ToString() + "/" + "4";

        if (videoPlaying == true)
        {
            PlayPauseText.text = "PLAY";
        }
        else
        {
            PlayPauseText.text = "PAUSE";
        }

        if (CurrentSelected == 1 && menuVisible == true && NarrativeVisible == false)
        {
            MenuOptionText1.color = Color.green;
            MenuOptionText2.color = Color.red;
        }

        if (CurrentSelected == 2 && NarrativeVisible == false)
        {
            MenuOptionText1.color = Color.red;
            MenuOptionText2.color = Color.green;
        }

        if (CurrentSelected == 3 && NarrativeVisible == false)
        {
            MenuOptionText1.color = Color.red;
            MenuOptionText2.color = Color.red;
        }

        if (NarrativeVisible == true || FadeBack == true)
        {
            Invoke("ShowCorrectContent", 3.0f);
        }
        else
        {
            ShowCorrectContent();
        }

    }

    public void IncreaseCurrentSelected()
    {
        if (menuVisible == true && NarrativeVisible == false)
        {
            if (CurrentSelected == 1)
            {
                CurrentSelected = CurrentSelected + 1;

                if (MenuOption1 == 1)
                {
                    //start T1
                    fader.FadeToBlack();
                    NarrativeVisible = true;

                    //Stop music
                    if (MenuPlaying == true)
                    {
                        //FindObjectOfType<AudioController>().StopMusic("StartMenuMusic");
                        //FindObjectOfType<AudioController>().StopMusic("BackMenuMusic");

                        SoundManager.instance.PlayTrack(T1Music);
                        T1Playing = true;

                        MenuPlaying = false;
                    }
                    Invoke("SetUpT1", 3.0f);
                }

                if (MenuOption1 == 2)
                {
                    //start T2
                    fader.FadeToBlack();
                    NarrativeVisible = true;

                    //Stop music
                    if (MenuPlaying == true)
                    {
                        //FindObjectOfType<AudioController>().StopMusic("StartMenuMusic");
                        //FindObjectOfType<AudioController>().StopMusic("BackMenuMusic");

                        SoundManager.instance.PlayTrack(T2Music);
                        T2Playing = true;

                        MenuPlaying = false;
                    }

                    Invoke("SetUpT2", 3.0f);
                }

                if (MenuOption1 == 3)
                {
                    //start T3
                    fader.FadeToBlack();
                    NarrativeVisible = true;

                    //Stop music
                    if (MenuPlaying == true)
                    {
                        //FindObjectOfType<AudioController>().StopMusic("StartMenuMusic");
                        //FindObjectOfType<AudioController>().StopMusic("BackMenuMusic");

                        SoundManager.instance.PlayTrack(T3Music);
                        T3Playing = true;

                        MenuPlaying = false;
                    }

                    Invoke("SetUpT3", 3.0f);
                }
            }
            else if (CurrentSelected == 2)
            {
                CurrentSelected = CurrentSelected + 1;
            }
            else if (CurrentSelected == 3)
            {
                // if T2 Videos
                if (MenuOption1 == 2)
                {
                    // videos
                    if (MenuOption2 == 2)
                    {
                        CurrentSelected = CurrentSelected + 1;
                        PlayVideo();
                    }
                }

                // if T3 Videos
                if (MenuOption1 == 3)
                {
                    // videos
                    if (MenuOption2 == 2)
                    {
                        CurrentSelected = CurrentSelected + 1;
                        PlayVideo();
                    }
                }
            }

            UpdateMenuOption();
        }
        else
        {
            if (MenuOption1 == 1)
            {
                NarrativeVisible = false;
                T1Narrative.SetActive(false);
                T1NarrativeDesk.SetActive(false);
                NarrativeHolder.SetActive(false);
            }

            if (MenuOption1 == 2)
            {
                NarrativeVisible = false;
                T2Narrative.SetActive(false);
                T2NarrativeDesk.SetActive(false);
                NarrativeHolder.SetActive(false);
            }

            if (MenuOption1 == 3)
            {
                NarrativeVisible = false;
                T3Narrative.SetActive(false);
                T3NarrativeDesk.SetActive(false);
                NarrativeHolder.SetActive(false);
            }

            UpdateMenuOption();
        }
    }

    public void DecreaseCurrentSelected()
    {
        if (menuVisible == true)
        {
            //pause any videos
            if (videoPlaying == true)
            {
                CurrentSelected = CurrentSelected - 1;
                PauseVideo();
            }
            else if(CurrentSelected > 1)
            {
                CurrentSelected = CurrentSelected - 1;
                MenuOption2 = 1;

                ResetValues();
            }

            // returning to main selection
            if (CurrentSelected == 1)
            {
                if (MenuOption1 == 1)
                {
                    fader.FadeToBlack();
                    NarrativeVisible = false;

                    //Stop music
                    if (T1Playing == true)
                    {
                        //FindObjectOfType<AudioController>().StopMusic("T1MenuMusic");
                        SoundManager.instance.ReturnToMenuMusic();

                        T1Playing = false;
                    }

                    Invoke("SetUpMain", 3.0f);
                    //T1 DONE
                    T1Done = true;
                    FadeBack = true;
                    Invoke("UpdatePanel", 3);
                }

                if (MenuOption1 == 2)
                {
                    fader.FadeToBlack();
                    NarrativeVisible = false;

                    //Stop music
                    if (T2Playing == true)
                    {
                        //FindObjectOfType<AudioController>().StopMusic("T2MenuMusic");
                        SoundManager.instance.ReturnToMenuMusic();

                        T2Playing = false;
                    }

                    Invoke("SetUpMain", 3.0f);
                    //T2 DONE
                    T2Done = true;
                    FadeBack = true;
                    Invoke("UpdatePanel", 3);
                }

                if (MenuOption1 == 3)
                {
                    fader.FadeToBlack();
                    NarrativeVisible = false;

                    //Stop music
                    if (T3Playing == true)
                    {
                        //FindObjectOfType<AudioController>().StopMusic("T3MenuMusic");
                        SoundManager.instance.ReturnToMenuMusic();

                        T3Playing = false;
                    }

                    Invoke("SetUpMain", 3.0f);
                    //T3 DONE
                    T3Done = true;
                    FadeBack = true;
                    Invoke("UpdatePanel", 3);
                }
            }

            UpdateMenuOption();

            NarrativeVisible = false;
        }
    }

    public void IncreaseMenuOption()
    {
        if (menuVisible == true && NarrativeVisible == false)
        {

            // T1 T2 T3
            if (CurrentSelected == 1 && MenuOption1 < 3)
            {
                MenuOption1 = MenuOption1 + 1;
            }

            // CHOOSE CONTENT WITHIN SECTION

            // if T1
            if (CurrentSelected == 2 && MenuOption1 == 1 && MenuOption2 < 2)
            {
                MenuOption2 = MenuOption2 + 1;
            }

            // if T2
            if (CurrentSelected == 2 && MenuOption1 == 2 && MenuOption2 < 3)
            {
                MenuOption2 = MenuOption2 + 1;
            }

            // if T3
            if (CurrentSelected == 2 && MenuOption1 == 3 && MenuOption2 < 4)
            {
                MenuOption2 = MenuOption2 + 1;
            }

            // CURRENT MEDIA
            if (CurrentSelected == 3)
            {
                // if T1
                if (MenuOption1 == 1)
                {
                    // images
                    if (MenuOption2 == 1 && CurrentT1Image < T1Images.Length)
                    {
                        CurrentT1Image++;
                    }

                    // timeline
                    if (MenuOption2 == 2 && CurrentT1Timeline < 14)
                    {
                        CurrentT1Timeline++;
                        TimelinePos.transform.localPosition -= new Vector3(1.25f, 0, 0);
                    }
                }

                // if T2
                if (MenuOption1 == 2)
                {
                    // images
                    if (MenuOption2 == 1 && CurrentT2Image < T2Images.Length)
                    {
                        CurrentT2Image++;
                    }

                    // videos
                    if (MenuOption2 == 2 && CurrentT2Video < T2Videos.Length)
                    {
                        CurrentT2Video++;
                    }

                    // timeline
                    if (MenuOption2 == 3 && CurrentT2Timeline < 18)
                    {
                        CurrentT2Timeline++;
                        TimelinePos.transform.localPosition -= new Vector3(1.25f, 0, 0);
                    }
                }

                // if T3
                if (MenuOption1 == 3)
                {
                    // images
                    if (MenuOption2 == 1 && CurrentT3Image < T3Images.Length)
                    {
                        CurrentT3Image++;
                    }

                    // videos
                    if (MenuOption2 == 2 && CurrentT3Video < T3Videos.Length)
                    {
                        CurrentT3Video++;
                    }

                    // archive
                    if (MenuOption2 == 3 && CurrentT3Archive < T3Archive.Length)
                    {
                        CurrentT3Archive++;
                    }

                    // timeline
                    if (MenuOption2 == 4 && CurrentT3Timeline < 26)
                    {
                        CurrentT3Timeline++;
                        TimelinePos.transform.localPosition -= new Vector3(1.25f, 0, 0);
                    }
                }
            }

            UpdateMenuOption();
        }

        if (NarrativeVisible == true)
        {
            if (MenuOption1 == 1 && CurrentT1Page < 4)
            {
                CurrentT1Page++;
                NarrativePos.transform.localPosition -= new Vector3(2.00f, 0, 0);
            }

            if (MenuOption1 == 2 && CurrentT2Page < 5)
            {
                CurrentT2Page++;
                NarrativePos.transform.localPosition -= new Vector3(2.00f, 0, 0);
            }

            if (MenuOption1 == 3 && CurrentT3Page < 4)
            {
                CurrentT3Page++;
                NarrativePos.transform.localPosition -= new Vector3(2.00f, 0, 0);
            }

            UpdateMenuOption();
        }

    }

    public void DecreaseMenuOption()
    {
        if (menuVisible == true && NarrativeVisible == false)
        {
            // T1 T2 T3
            if (CurrentSelected == 1 && MenuOption1 > 1)
            {
                MenuOption1 = MenuOption1 - 1;
            }

            // IMAGES VIDEO DOCUMENTS
            if (CurrentSelected == 2 && MenuOption2 > 1)
            {
                MenuOption2 = MenuOption2 - 1;
            }

            // CHOOSE CONTENT WITHIN SECTION
            if (CurrentSelected == 3)
            {
                // if T1
                if (MenuOption1 == 1)
                {
                    // images
                    if (MenuOption2 == 1 && CurrentT1Image > 1)
                    {
                        CurrentT1Image--;
                    }

                    // timeline
                    if (MenuOption2 == 2 && CurrentT1Timeline > 1)
                    {
                        CurrentT1Timeline--;
                        TimelinePos.transform.localPosition += new Vector3(1.25f, 0, 0);
                    }
                }

                // if T2
                if (MenuOption1 == 2)
                {
                    // images
                    if (MenuOption2 == 1 && CurrentT2Image > 1)
                    {
                        CurrentT2Image--;
                    }

                    // videos
                    if (MenuOption2 == 2 && CurrentT2Video > 1)
                    {
                        CurrentT2Video--;
                    }

                    // timeline
                    if (MenuOption2 == 3 && CurrentT2Timeline > 1)
                    {
                        CurrentT2Timeline--;
                        TimelinePos.transform.localPosition += new Vector3(1.25f, 0, 0);
                    }
                }

                // if T3
                if (MenuOption1 == 3)
                {
                    // images
                    if (MenuOption2 == 1 && CurrentT3Image > 1)
                    {
                        CurrentT3Image--;
                    }

                    // videos
                    if (MenuOption2 == 2 && CurrentT3Video > 1)
                    {
                        CurrentT3Video--;
                    }

                    // archive
                    if (MenuOption2 == 3 && CurrentT3Archive > 1)
                    {
                        CurrentT3Archive--;
                    }

                    // timeline
                    if (MenuOption2 == 4 && CurrentT3Timeline > 1)
                    {
                        CurrentT3Timeline--;
                        TimelinePos.transform.localPosition += new Vector3(1.25f, 0, 0);
                    }
                }
            }

            UpdateMenuOption();
        }

        if (NarrativeVisible == true)
        {

            if (MenuOption1 == 1 && CurrentT1Page > 1)
            {
                CurrentT1Page--;
                NarrativePos.transform.localPosition += new Vector3(2.00f, 0, 0);
            }

            if (MenuOption1 == 2 && CurrentT1Page > 2)
            {
                CurrentT2Page--;
                NarrativePos.transform.localPosition += new Vector3(2.00f, 0, 0);
            }

            if (MenuOption1 == 3 && CurrentT1Page > 3)
            {
                CurrentT3Page--;
                NarrativePos.transform.localPosition += new Vector3(2.00f, 0, 0);
            }

            UpdateMenuOption();
        }
    }

    private void ShowCorrectContent()
    {
        FadeBack = false;

        if (menuVisible == true)
        {
            // First menu selection
            if (CurrentSelected == 1)
            {
                Menu1.SetActive(true);
                selectionRing1.SetActive(true);
                HideAllInstructions();

                if (ExperienceFinished == false)
                {
                    PanelIns6.SetActive(true);
                }
                else
                {
                    Menu1.SetActive(false);
                    selectionRing1.SetActive(false);
                    PanelIns6.SetActive(false);
                }

                if (MenuOption1 == 1)
                {
                    selectionRing1.transform.position = Menu1Positions[0].position;
                }

                if (MenuOption1 == 2)
                {
                    selectionRing1.transform.position = Menu1Positions[1].position;
                }

                if (MenuOption1 == 3)
                {
                    selectionRing1.transform.position = Menu1Positions[2].position;
                }
            }
            else
            {
                Menu1.SetActive(false);
                selectionRing1.SetActive(false);
            }

            // Second menu selection
            if (CurrentSelected == 2 && NarrativeVisible == false)
            {
                Menu2.SetActive(true);

                HideAllInstructions();
                PanelIns5.SetActive(true);

                // Show correct options
                // T1 Options
                if (MenuOption1 == 1)
                {
                    T1Options.SetActive(true);
                    T1SelectionRing.SetActive(true);

                    // Move cursor
                    if (MenuOption2 == 1)
                    {
                        T1SelectionRing.transform.position = T1MenuPositions[0].position;
                    }

                    if (MenuOption2 == 2)
                    {
                        T1SelectionRing.transform.position = T1MenuPositions[1].position;
                    }
                }
                else
                {
                    T1Options.SetActive(false);
                }

                // T2 Options
                if (MenuOption1 == 2)
                {
                    T2Options.SetActive(true);
                    T2SelectionRing.SetActive(true);

                    // Move cursor
                    if (MenuOption2 == 1)
                    {
                        T2SelectionRing.transform.position = T2MenuPositions[0].position;
                    }

                    if (MenuOption2 == 2)
                    {
                        T2SelectionRing.transform.position = T2MenuPositions[1].position;
                    }

                    if (MenuOption2 == 3)
                    {
                        T2SelectionRing.transform.position = T2MenuPositions[2].position;
                    }
                }
                else
                {
                    T2Options.SetActive(false);
                }

                // T3 Options
                if (MenuOption1 == 3)
                {
                    T3Options.SetActive(true);
                    T3SelectionRing.SetActive(true);

                    // Move cursor
                    if (MenuOption2 == 1)
                    {
                        T3SelectionRing.transform.position = T3MenuPositions[0].position;
                    }

                    if (MenuOption2 == 2)
                    {
                        T3SelectionRing.transform.position = T3MenuPositions[1].position;
                    }

                    if (MenuOption2 == 3)
                    {
                        T3SelectionRing.transform.position = T3MenuPositions[2].position;
                    }

                    if (MenuOption2 == 4)
                    {
                        T3SelectionRing.transform.position = T3MenuPositions[3].position;
                    }
                }
                else
                {
                    T3Options.SetActive(false);
                }
            }
            else
            {
                Menu2.SetActive(false);
                T1SelectionRing.SetActive(false);
                T2SelectionRing.SetActive(false);
                T3SelectionRing.SetActive(false);
            }

            // Third menu selection
            if (CurrentSelected > 2)
            {

                Menu3.SetActive(true);

                HideAllInstructions();
                PanelIns2.SetActive(true);

                FloatingContent.SetActive(true);

                // T1

                // T1 Images
                if (MenuOption1 == 1 && MenuOption2 == 1)
                {
                    T1ImageHolder.SetActive(true);
                    FloatingT1ImageHolder.SetActive(true);

                    for (int i = 0; i < T1Images.Length; i++)
                    {
                        if (i == CurrentT1Image - 1)
                        {
                            T1Images[i].SetActive(true);
                        }
                        else
                        {
                            T1Images[i].SetActive(false);
                        }
                    }
                }
                else
                {
                    T1ImageHolder.SetActive(false);
                    FloatingT1ImageHolder.SetActive(false);
                }

                // T1 Timeline
                if (MenuOption1 == 1 && MenuOption2 == 2)
                {
                    T1TimelineHolder.SetActive(true);
                    FloatingT1TimelineHolder.SetActive(true);
                }
                else
                {
                    T1TimelineHolder.SetActive(false);
                    FloatingT1TimelineHolder.SetActive(false);
                }

                // T2

                // T2 Images
                if (MenuOption1 == 2 && MenuOption2 == 1)
                {
                    T2ImageHolder.SetActive(true);
                    FloatingT2ImageHolder.SetActive(true);

                    for (int i = 0; i < T2Images.Length; i++)
                    {
                        if (i == CurrentT2Image - 1)
                        {
                            T2Images[i].SetActive(true);
                        }
                        else
                        {
                            T2Images[i].SetActive(false);
                        }
                    }
                }
                else
                {
                    T2ImageHolder.SetActive(false);
                    FloatingT2ImageHolder.SetActive(false);
                }

                // T2 Videos
                if (MenuOption1 == 2 && MenuOption2 == 2)
                {
                    T2VideoHolder.SetActive(true);
                    FloatingT2VideoHolder.SetActive(true);

                    HideAllInstructions();
                    PanelIns4.SetActive(true);

                    for (int i = 0; i < T2Videos.Length; i++)
                    {
                        if (i == CurrentT2Video - 1)
                        {
                            T2Videos[i].SetActive(true);
                        }
                        else
                        {
                            T2Videos[i].SetActive(false);
                        }
                    }
                }
                else
                {
                    T2VideoHolder.SetActive(false);
                    FloatingT2VideoHolder.SetActive(false);
                }

                // T2 Timeline
                if (MenuOption1 == 2 && MenuOption2 == 3)
                {
                    T2TimelineHolder.SetActive(true);
                    FloatingT2TimelineHolder.SetActive(true);
                }
                else
                {
                    T2TimelineHolder.SetActive(false);
                    FloatingT2TimelineHolder.SetActive(false);
                }

                // T3

                // T3 Images
                if (MenuOption1 == 3 && MenuOption2 == 1)
                {
                    T3ImageHolder.SetActive(true);
                    FloatingT3ImageHolder.SetActive(true);

                    for (int i = 0; i < T3Images.Length; i++)
                    {
                        if (i == CurrentT3Image - 1)
                        {
                            T3Images[i].SetActive(true);
                        }
                        else
                        {
                            T3Images[i].SetActive(false);
                        }
                    }
                }
                else
                {
                    T3ImageHolder.SetActive(false);
                    FloatingT3ImageHolder.SetActive(false);
                }

                // T3 Videos
                if (MenuOption1 == 3 && MenuOption2 == 2)
                {
                    T3VideoHolder.SetActive(true);
                    FloatingT3VideoHolder.SetActive(true);

                    HideAllInstructions();
                    PanelIns4.SetActive(true);

                    for (int i = 0; i < T3Videos.Length; i++)
                    {
                        if (i == CurrentT3Video - 1)
                        {
                            T3Videos[i].SetActive(true);
                        }
                        else
                        {
                            T3Videos[i].SetActive(false);
                        }
                    }
                }
                else
                {
                    T3VideoHolder.SetActive(false);
                    FloatingT3VideoHolder.SetActive(false);
                }

                // T3 Archive
                if (MenuOption1 == 3 && MenuOption2 == 3)
                {
                    T3ArchiveHolder.SetActive(true);
                    FloatingT3ArchiveHolder.SetActive(true);

                    for (int i = 0; i < T3Archive.Length; i++)
                    {
                        if (i == CurrentT3Archive - 1)
                        {
                            T3Archive[i].SetActive(true);
                        }
                        else
                        {
                            T3Archive[i].SetActive(false);
                        }
                    }
                }
                else
                {
                    T3ArchiveHolder.SetActive(false);
                    FloatingT3ArchiveHolder.SetActive(false);
                }

                // T3 Timeline
                if (MenuOption1 == 3 && MenuOption2 == 4)
                {
                    T3TimelineHolder.SetActive(true);
                    FloatingT3TimelineHolder.SetActive(true);
                }
                else
                {
                    T3TimelineHolder.SetActive(false);
                    FloatingT3TimelineHolder.SetActive(false);
                }

            }
            else
            {
                Menu3.SetActive(false);
                FloatingContent.SetActive(false);
            }
        }
    }

    private void PlayVideo()
    {
        // if T2 Videos
        if (MenuOption1 == 2)
        {
            // videos
            if (MenuOption2 == 2)
            {
                if (videoPlaying == false)
                {
                    //play video
                    videoPlaying = true;
                    T2Videos[CurrentT2Video - 1].GetComponent<HideCoverImage>().HideImage();
                    MyVideoPlayer = T2Videos[CurrentT2Video - 1].GetComponent<VideoPlayer>();
                    MyVideoPlayer.Play();

                    //stop background music
                    if (T2Playing == true)
                    {
                        SoundManager.instance.PauseTrack();
                        //FindObjectOfType<AudioController>().StopMusic("T2MenuMusic");
                        T2Playing = false;
                    }
                }
            }
        }

        // if T3 Videos
        if (MenuOption1 == 3)
        {
            // videos
            if (MenuOption2 == 2)
            {
                if (videoPlaying == false)
                {
                    //play video
                    videoPlaying = true;
                    T3Videos[CurrentT3Video - 1].GetComponent<HideCoverImage>().HideImage();
                    MyVideoPlayer = T3Videos[CurrentT3Video - 1].GetComponent<VideoPlayer>();
                    MyVideoPlayer.Play();

                    //stop background music
                    if (T3Playing == true)
                    {
                        SoundManager.instance.PauseTrack();
                        //FindObjectOfType<AudioController>().StopMusic("T3MenuMusic");
                        T3Playing = false;
                    }
                }
            }
        }

    }

    private void PauseVideo()
    {
        // if T2 Videos
        if (MenuOption1 == 2)
        {
            // videos
            if (MenuOption2 == 2)
            {
                if (videoPlaying == true)
                {
                    //pause video
                    videoPlaying = false;
                    MyVideoPlayer.Pause();

                    //play background music
                    if (T2Playing == false)
                    {
                        SoundManager.instance.UnPauseTrack();
                        //FindObjectOfType<AudioController>().PlayMusic("T2MenuMusic");
                        T2Playing = true;
                    }
                }
            }
        }

        // if T3 Videos
        if (MenuOption1 == 3)
        {
            // videos
            if (MenuOption2 == 2)
            {
                if (videoPlaying == true)
                {
                    //play video
                    videoPlaying = false;
                    MyVideoPlayer.Pause();

                    //play background music
                    if (T3Playing == false)
                    {
                        SoundManager.instance.UnPauseTrack();
                        //FindObjectOfType<AudioController>().PlayMusic("T3MenuMusic");
                        T3Playing = true;
                    }
                }
            }
        }
    }

    private void SetUpT1()
    {
        HideAllInstructions();
        PanelIns3.SetActive(true);

        T1Narrative.SetActive(true);
        T1NarrativeDesk.SetActive(true);
        NarrativeHolder.SetActive(true);
        MainRoom.SetActive(false);
        T1Room.SetActive(true);
    }

    private void SetUpT2()
    {
        HideAllInstructions();
        PanelIns3.SetActive(true);

        T2Narrative.SetActive(true);
        T2NarrativeDesk.SetActive(true);
        NarrativeHolder.SetActive(true);
        MainRoom.SetActive(false);
        T2Room.SetActive(true);
    }

    private void SetUpT3()
    {
        HideAllInstructions();
        PanelIns3.SetActive(true);

        T3Narrative.SetActive(true);
        T3NarrativeDesk.SetActive(true);
        NarrativeHolder.SetActive(true);
        MainRoom.SetActive(false);
        T3Room.SetActive(true);
    }

    private void SetUpMain()
    {
        HideAllInstructions();
        PanelIns6.SetActive(true);

        T1Narrative.SetActive(false);
        T2Narrative.SetActive(false);
        T3Narrative.SetActive(false);

        T1NarrativeDesk.SetActive(false);
        T2NarrativeDesk.SetActive(false);
        T3NarrativeDesk.SetActive(false);

        NarrativeHolder.SetActive(false);

        T1Room.SetActive(false);
        T2Room.SetActive(false);
        T3Room.SetActive(false);

        MainRoom.SetActive(true);

        MenuPlaying = true;
    }

    private void ResetValues()
    {
        CurrentT1Image = 1;
        CurrentT1Timeline = 1;

        CurrentT2Image = 1;
        CurrentT2Video = 1;
        CurrentT2Timeline = 1;

        CurrentT3Image = 1;
        CurrentT3Video = 1;
        CurrentT3Archive = 1;
        CurrentT3Timeline = 1;

        CurrentT1Page = 1;
        CurrentT2Page = 1;
        CurrentT3Page = 1;

        TimelinePos.transform.position = TimelineOriginalPos.transform.position;

        NarrativePos.transform.position = NarrativeOriginalPos.transform.position;

    }

    private void UpdatePanel()
    {
        if (T1Done == true)
        {
            T1Image.color = new Color32(0, 255, 0, 128);
        }

        if (T2Done == true)
        {
            T2Image.color = new Color32(0, 255, 0, 128);
        }

        if (T3Done == true)
        {
            T3Image.color = new Color32(0, 255, 0, 128);
        }

        if (T1Done == true && T2Done == true && T3Done == true)
        {
            FinalText.SetActive(true);
            ExperienceFinished = true;

            HideAllInstructions();
            PanelIns7.SetActive(true);
        }
    }

    public void BackHome()
    {
        if (ExperienceFinished == true)
        {
            if (T1Playing == true)
            {
                SoundManager.instance.StopAndResetTrack();
                T1Playing = false;
            }

            if (T2Playing == true)
            {
                SoundManager.instance.StopAndResetTrack();
                T2Playing = false;
            }

            if (T3Playing == true)
            {
                SoundManager.instance.StopAndResetTrack();
                T3Playing = false;
            }

            if (MenuPlaying == true)
            {
                SoundManager.instance.StopAndResetTrack();
                MenuPlaying = false;
            }

            SceneManager.LoadScene("Menu");
        }
    }

    private void HideAllInstructions()
    {
        PanelIns1.SetActive(false);
        PanelIns2.SetActive(false);
        PanelIns3.SetActive(false);
        PanelIns4.SetActive(false);
        PanelIns5.SetActive(false);
        PanelIns6.SetActive(false);
    }

}
