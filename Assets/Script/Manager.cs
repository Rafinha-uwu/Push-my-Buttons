using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{


    public bool First = true;



    public bool Menu = false;

    public bool Cal1 = false;
    public bool Cal2 = false;
    public float Caln = 0;

    public float P1 = 0;
    public float P2 = 0;

    public bool TimerOn = false;
    public float Ctime = 0f;
    public float Stime = 90f;

    public float Round = 1;
    public bool Rounds = false;


    public bool RandomAC = false;
    public bool AC = false;

    public bool MJ = false;
    public bool Butts = false;
    public bool Sequ = false;


    public bool ST1 = false;
    public bool ST2 = false;
    public float STn = 0;

    public bool Love = false;
    public bool EndBut = true;

    public float ACTION = 0;

    public float A1 = 0;
    public float A2 = 0;
    public float A3 = 0;
    public float A4 = 0;
    public float A5 = 0;
    public float A6 = 0;
    public float A7 = 0;
    public float A8 = 0;
    public float A9 = 0;

    public Image uiImage;

    public Image Rou;
    public Image Rou2;

    public bool AT1 = false;
    public bool AT2 = false;
    public float ATn = 0;

    public bool Again = false;

    public bool Die = false;

    public bool Perguntas = false;

    public bool Once = true;

    public bool Tutu = true;

    public bool Last = false;


    //Sequencias

    public string S1 = "0";
    public string S2 = "0";

    //Sev

    ServerScript_2 Server2;
    public GameObject Sev2;

    ServerScript Server;
    public GameObject Sev;


    public TMP_Text textComponent;
    List<string> remainingOptions = new List<string>();
    string[] normalOptions = new string[]
    {
       "What is my favorite day of the week?",
"What did I have for dinner yesterday?",
"What is my favorite drink?",
"What color is my toothbrush?",
"What is my favorite highlighter color?",
"What is my least favorite movie?",
"What is the thing I quote the most?",
"What is my favorite word?",
"What is my most useless talent?",
"What do I wash first when I take a shower?",
"What time do I take a shower?",
"What do I do before going to bed?",
"What is the first thing I do when I wake up?",
"What is my favorite day of the year?",
"What do I usually do on Sundays?",
"What is my favorite planet?",
"What book/game/series/movie do I recommend the most?",
"What was my first pet's name?",
"When is my birthday?"
    };

    string[] loveOptions = new string[]
    {
        "Would you still love me if I were a worm?",
"What is my favorite day of the week?",
"What did we eat on our first date?",
"What was my worst pick-up line?",
"What is my love language?",
"What color is my toothbrush?",
"What is my favorite superpower?",
"What is my biggest obsession?",
"What quality do I like most in you?",
"What do I like to cook the most?",
"What is my favorite food?",
"What is my favorite animated movie?",
"What book/game/series/movie do I recommend the most?",
"What was my first pet's name?",
"When is my birthday?",
"What is my favorite word?",
"What is my most useless talent?",
"What is the thing I quote the most?",
"What hobbies would I like to try?",
"What is my favorite type of chocolate?"
    };

    public float X = 0;

    public AudioSource MenuS;
    public AudioSource Timer;
    public AudioSource R1;
    public AudioSource R2;
    public AudioSource R3;
    public AudioSource Points;
    public AudioSource Pergunt;
    public AudioSource pop;
    public AudioSource Final;
    public AudioSource Sequence;
    public AudioSource Betweens;
    public AudioSource Acoes;
    public AudioSource CountD;
    public AudioSource Lob;
    public AudioSource Wheel;

    // Start is called before the first frame update
    void Start()
    {
        Stime = 90f;
        Ctime = Stime;

        Server = Sev.GetComponent<ServerScript>();
        Server2 = Sev2.GetComponent<ServerScript_2>();
        Tutu = false;
    }

    void SetOptions()
    {
        if (Love)
        {
            remainingOptions.AddRange(loveOptions);
        }
        else
        {
            remainingOptions.AddRange(normalOptions);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Other

        //Cal
        if (Caln >= 2)
        {
            Invoke("CAL", 0.5f);

        }

        //ST
        if (STn >= 2)
        {
            GameObject.Find("UI/Game/Rounds/Count").GetComponent<Animator>().Play("321");
            Invoke("Count", 3f);

            STn = 0;
            ST1 = false;
            ST2 = false;

            Betweens.Stop();
            CountD.Play();

            switch (Round)
            {
                case 1:
                    R1.Play();
                    break;
                case 2:
                    R2.Play();
                    break;
                case 3:
                    R3.Play();
                    break;
            }

        }

        //AT
        if (ATn >= 2)
        {

            Invoke("AcEnd", .5f);
            ATn = 0;
            AT1 = false;
            AT2 = false;
        }

        //Time
        if (TimerOn)
        {
            if (Ctime >= 0.01f)
            {
                Ctime -= 1 * Time.deltaTime;
            }
            if (Ctime < 10f)
            {

                GameObject.Find("UI/Game/Rounds/Timer/NT1").GetComponent<TMP_Text>().color =
                ColorUtility.TryParseHtmlString("#FF0000", out Color newCol) ? newCol :
                GameObject.Find("UI/Game/Rounds/Timer/NT1").GetComponent<TMP_Text>().color; ;


            }
            if (Ctime < 0.1f)
            {
                GameObject.Find("UI/Game/Rounds/Timer/NT1").GetComponent<TMP_Text>().color =
                ColorUtility.TryParseHtmlString("#FFFFFF", out Color newCol) ? newCol :
                GameObject.Find("UI/Game/Rounds/Timer/NT1").GetComponent<TMP_Text>().color; ;
                if (Rounds == true) { Invoke("Tim1", 0.1f); }
                else { if (Once) { Invoke("GO", 0f); } }
            }

            if (Rounds) GameObject.Find("UI/Game/Rounds/Timer/NT1").GetComponent<TMP_Text>().SetText(Ctime.ToString("F0"));
            else { GameObject.Find("UI/Game/Rounds/Ações/Timer/NT1").GetComponent<TMP_Text>().SetText(Ctime.ToString("F0")); }



        }

        //Menu

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (Menu == false) { Invoke("MenOn", 0); }
            else { Invoke("MenOFF", 0); }
            pop.Play();
        }


        //Action

        if (ACTION != 0 && ACTION != A1 && ACTION != A2 && ACTION != A3 && ACTION != A4 && ACTION != A5 && ACTION != A6 && ACTION != A7 && ACTION != A8 && ACTION != A9)
        {
            if (A1 == 0) { A1 = ACTION; if (A1 == 5) { A2 = 52; } if (A1 == 7) { A2 = 72; } if (A1 == 8) { A2 = 82; } if (A1 == 9) { A2 = 92; } }
            else if (A2 == 0) { A2 = ACTION; if (A2 == 5) { A3 = 52; } if (A2 == 7) { A3 = 72; } if (A2 == 8) { A3 = 82; } if (A2 == 9) { A3 = 92; } }
            else if (A3 == 0) { A3 = ACTION; if (A3 == 5) { A4 = 52; } if (A3 == 7) { A4 = 72; } if (A3 == 8) { A4 = 82; } if (A3 == 9) { A4 = 92; } }
            else if (A4 == 0) { A4 = ACTION; if (A4 == 5) { A5 = 52; } if (A4 == 7) { A5 = 72; } if (A4 == 8) { A5 = 82; } if (A4 == 9) { A5 = 92; } }
            else if (A5 == 0) { A5 = ACTION; if (A5 == 5) { A6 = 52; } if (A5 == 7) { A6 = 72; } if (A5 == 8) { A6 = 82; } if (A5 == 9) { A6 = 92; } }
            else if (A6 == 0) { A6 = ACTION; if (A6 == 5) { A7 = 52; } if (A6 == 7) { A7 = 72; } if (A6 == 8) { A7 = 82; } if (A6 == 9) { A7 = 92; } }
            else if (A7 == 0) { A7 = ACTION; if (A7 == 5) { A8 = 52; } if (A7 == 7) { A8 = 72; } if (A7 == 8) { A8 = 82; } if (A7 == 9) { A8 = 92; } }
            else if (A8 == 0) { A8 = ACTION; if (A8 == 5) { A9 = 52; } if (A8 == 7) { A9 = 72; } if (A8 == 8) { A9 = 82; } if (A8 == 9) { A9 = 92; } }
            else if (A9 == 0) { A9 = ACTION; }

        }


        //Seq
        switch (S2)
        {
            //E
            case "121":

                P2 = P2 + 3; S2 = "0"; ACTION = 1; Sequence.Play();
                break;

            case "324":

                P2 = P2 + 3; S2 = "0"; ACTION = 2; Sequence.Play();
                break;

            case "223":

                P2 = P2 + 3; S2 = "0"; ACTION = 3; Sequence.Play();
                break;
            //M
            case "1532":

                P2 = P2 + 6; S2 = "0"; ACTION = 4; Sequence.Play();
                break;

            case "5432":

                P2 = P2 + 6; S2 = "0"; ACTION = 5; Sequence.Play();
                break;

            case "4143":

                P2 = P2 + 6; S2 = "0"; ACTION = 6; Sequence.Play();
                break;
            //D

            case "4135":

                P2 = P2 + 12; S2 = "0"; ACTION = 7; Sequence.Play();
                break;

            case "3112":

                P2 = P2 + 12; S2 = "0"; ACTION = 8; Sequence.Play();
                break;

            case "2435":

                P2 = P2 + 12; S2 = "0"; ACTION = 9; Sequence.Play();
                break;


        }

        switch (S1)
        {
            //E
            case "121":

                P1 = P1 + 3; S1 = "0"; ACTION = 1; Sequence.Play();
                break;

            case "324":

                P1 = P1 + 3; S1 = "0"; ACTION = 2; Sequence.Play();
                break;

            case "223":

                P1 = P1 + 3; S1 = "0"; ACTION = 3; Sequence.Play();
                break;
            //M
            case "1532":

                P1 = P1 + 6; S1 = "0"; ACTION = 4; Sequence.Play();
                break;

            case "5432":

                P1 = P1 + 6; S1 = "0"; ACTION = 5; Sequence.Play();
                break;

            case "4143":

                P1 = P1 + 6; S1 = "0"; ACTION = 6; Sequence.Play();
                break;
            //D

            case "4135":

                P1 = P1 + 12; S1 = "0"; ACTION = 7; Sequence.Play();
                break;

            case "3112":

                P1 = P1 + 12; S1 = "0"; ACTION = 8; Sequence.Play();
                break;

            case "2435":

                P1 = P1 + 12; S1 = "0"; ACTION = 9; Sequence.Play();
                break;


        }

        if (Input.GetMouseButtonDown(0))
        {
            pop.Play();
        }

        //Reader

        if (Server2.data.Trim() == "00" || Keyboard.current.digit1Key.wasPressedThisFrame)//00 RED
        {

            GameObject.Find("UI/Game/Rounds/Timer/1").GetComponent<Animator>().Play("1");

            pop.Play();
            //Cal 
            if (Cal1)
            {
                GameObject.Find("UI/Intro/CAL/Cal1").SetActive(false);
                GameObject.Find("UI/Intro/CAL/Cal12").SetActive(true);


                GameObject.Find("UI/Intro/CAL/Cal1 (1)").SetActive(false);
                GameObject.Find("UI/Intro/CAL/Cal12 (1)").SetActive(true); ;

                Caln++;
                Cal1 = false;
            }

            //MJ

            if (MJ)
            {
                Invoke("Rom", 0.2f);
            }

            //tutu 
            if (Tutu)
            {
                Invoke("TUT", 0);
            }

            //Butts

            if (Butts)
            {
                Invoke("H", 0.2f);
            }


            //Sequ

            if (Sequ)
            {
                Invoke("Seq", 0f);
            }


            //Count
            if (ST1)
            {
                STn++;
                ST1 = false;
                Invoke("SeqT", 1);
            }

            //AT
            if (AT1)
            {
                ATn++;
                AT1 = false;
                Invoke("ATT", .4f);
            }




            //Perg

            if (Perguntas)
            {
                if (X > 0)
                {
                    StartCoroutine(SpinAndStop());
                    Perguntas = false;
                    Invoke("Perg2", 6);
                }
                else
                {
                    Perguntas = false;
                    GameObject.Find("UI/Game/PER").SetActive(false);
                    GameObject.Find("UI/Game/Reset").SetActive(true);
                    Last = true;
                }
            }

            //Points

            if (Rounds)
            {
                P2++;

                //Seq

                switch (S2)
                {
                    case "4":
                        S2 = "41";
                        break;
                    case "3":
                        S2 = "31";
                        break;
                    case "31":
                        S2 = "311";
                        break;
                    case "0":
                        S2 = "1";
                        break;
                    case "12":
                        S2 = "121";
                        break;
                    default:
                        S2 = "1";
                        break;
                }
            }
            Server2.data = "";
        }

        if (Server2.data.Trim() == "01" || Keyboard.current.digit2Key.wasPressedThisFrame) //01 Yellow
        {
            GameObject.Find("UI/Game/Rounds/Timer/1").GetComponent<Animator>().Play("2");

            pop.Play();
            //MJ

            if (MJ)
            {
                Invoke("Classic", 0.2f);
            }


            //Butts

            if (Butts)
            {
                Invoke("M", 0.2f);
            }

            //Points

            if (Rounds)
            {
                P2++;

                //Seq

                switch (S2)
                {
                    case "311":
                        S2 = "3112";
                        break;
                    case "0":
                        S2 = "2";
                        break;
                    case "1":
                        S2 = "12";
                        break;
                    case "3":
                        S2 = "32";
                        break;
                    case "2":
                        S2 = "22";
                        break;
                    case "153":
                        S2 = "1532";
                        break;
                    case "543":
                        S2 = "5432";
                        break;
                    default:
                        S2 = "2";
                        break;
                }
            }
            Server2.data = "";
        }
        if (Server2.data.Trim() == "10" || Keyboard.current.digit3Key.wasPressedThisFrame) //02 Green
        {

            GameObject.Find("UI/Game/Rounds/Timer/1").GetComponent<Animator>().Play("3");
            //First
            pop.Play();
            if (First)
            {
                Invoke("Startt", 0.2f);
            }

            //Butts

            if (Butts)
            {
                Invoke("E", 0.2f);
            }


            if (Again)
            {
                Invoke("ButEnd", 0.2f);
            }

            //Points

            if (Rounds)
            {
                P2++;

                //Seq

                switch (S2)
                {
                    case "41":
                        S2 = "413";
                        break;
                    case "0":
                        S2 = "3";
                        break;
                    case "24":
                        S2 = "243";
                        break;
                    case "22":
                        S2 = "223";
                        break;
                    case "15":
                        S2 = "153";
                        break;
                    case "54":
                        S2 = "543";
                        break;
                    case "414":
                        S2 = "4143";
                        break;
                    default:
                        S2 = "3";
                        break;
                }
            }
            Server2.data = "";
        }
        if (Server2.data.Trim() == "11" || Keyboard.current.digit4Key.wasPressedThisFrame) //10 Orange
        {
            GameObject.Find("UI/Game/Rounds/Timer/1").GetComponent<Animator>().Play("4");

            pop.Play();
            if (Die || Last)
            {
                Invoke("Died", 0.2f);
            }

            //Points

            if (Rounds)
            {
                P2++;

                //Seq

                switch (S2)
                {
                    case "0":
                        S2 = "4";
                        break;
                    case "2":
                        S2 = "24";
                        break;
                    case "32":
                        S2 = "324";
                        break;
                    case "5":
                        S2 = "54";
                        break;
                    case "41":
                        S2 = "414";
                        break;
                    default:
                        S2 = "4";
                        break;
                }
            }
            Server2.data = "";
        }
        if (Server2.data.Trim() == "20" || Keyboard.current.digit5Key.wasPressedThisFrame) //11 Blue
        {
            GameObject.Find("UI/Game/Rounds/Timer/1").GetComponent<Animator>().Play("5");
            pop.Play();
            //Points

            if (Rounds)
            {
                P2++;

                //Seq

                switch (S2)
                {
                    case "413":
                        S2 = "4135";
                        break;
                    case "243":
                        S2 = "2435";
                        break;
                    case "1":
                        S2 = "15";
                        break;
                    case "0":
                        S2 = "5";
                        break;
                    default:
                        S2 = "5";
                        break;
                }
            }

            //AC

            if (Love)
            {
                if (AC == true)
                {
                    Invoke("AcStart", 0); AC = false; if (A3 == 0) { A1 = 0; A2 = 11; A3 = 12; }
                    else if (A1 != 0) { uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A1); A1 = 0; }
                }
                if (RandomAC == true)
                {

                    if (A2 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A2); A2 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A3 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A3); A3 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A4 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A4); A4 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A5 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A5); A5 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A6 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A6); A6 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A7 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A7); A7 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A8 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A8); A8 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A9 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A9); A9 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else { Invoke("AcB", 0); }

                }


            }
            else
            {

                if (AC == true) { Invoke("AcStart", 0); AC = false; if (A3 == 0) { A1 = 0; A2 = 11; A3 = 12; } else if (A1 != 0) { uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A1); A1 = 0; } }
                if (RandomAC == true)
                {

                    if (A2 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A2); A2 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A3 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A3); A3 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A4 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A4); A4 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A5 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A5); A5 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A6 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A6); A6 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A7 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A7); A7 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A8 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A8); A8 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A9 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A9); A9 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else { Invoke("AcB", 0); }


                }
            }

            Server2.data = "";
        }


        // P2


        if (Server.data.Trim() == "00" || Keyboard.current.qKey.wasPressedThisFrame) //00 RED
        {

            GameObject.Find("UI/Game/Rounds/Timer/1").GetComponent<Animator>().Play("1");
            pop.Play();

            //Cal
            if (Cal2)
            {
                GameObject.Find("UI/Intro/CAL/Cal2").SetActive(false);
                GameObject.Find("UI/Intro/CAL/Cal22").SetActive(true);

                GameObject.Find("UI/Intro/CAL/Cal1 (2)").SetActive(false);
                GameObject.Find("UI/Intro/CAL/Cal22 (1)").SetActive(true);
                Caln++;
                Cal2 = false;
            }

            //MJ 

            if (MJ)
            {
                Invoke("Rom", 0.2f);
            }

            //tutu 
            if (Tutu)
            {
                Invoke("TUT", 0);
            }
            //Butts

            if (Butts)
            {
                Invoke("H", 0.2f);
            }


            //Sequ

            if (Sequ)
            {
                Invoke("Seq", 0f);
            }

            //Count
            if (ST2)
            {
                STn++;
                ST2 = false;

                Invoke("SeqT", 1);
            }

            //AT
            if (AT2)
            {
                ATn++;
                AT2 = false;
                Invoke("ATT", .4f);
            }



            //Perg

            if (Perguntas)
            {
                if (X > 0)
                {
                    StartCoroutine(SpinAndStop());
                    Perguntas = false;
                    Invoke("Perg2", 6);
                }
                else
                {
                    Perguntas = false;
                    GameObject.Find("UI/Game/PER").SetActive(false);
                    GameObject.Find("UI/Game/Reset").SetActive(true);
                    Last = true;
                }
            }




            //Points

            if (Rounds)
            {
                P1++;

                //Seq

                switch (S1)
                {
                    case "4":
                        S1 = "41";
                        break;
                    case "3":
                        S1 = "31";
                        break;
                    case "31":
                        S1 = "311";
                        break;
                    case "0":
                        S1 = "1";
                        break;
                    case "12":
                        S1 = "121";
                        break;
                    default:
                        S1 = "1";
                        break;
                }
            }
            Server.data = "";
        }

        if (Server.data.Trim() == "01" || Keyboard.current.wKey.wasPressedThisFrame) //01 Yellow
        {
            GameObject.Find("UI/Game/Rounds/Timer/1").GetComponent<Animator>().Play("2");
            pop.Play();
            //MJ

            if (MJ)
            {
                Invoke("Classic", 0.2f);
            }

            //Butts

            if (Butts)
            {
                Invoke("M", 0.2f);
            }

            //Points

            if (Rounds)
            {
                P1++;


                switch (S1)
                {
                    case "311":
                        S1 = "3112";
                        break;
                    case "0":
                        S1 = "2";
                        break;
                    case "1":
                        S1 = "12";
                        break;
                    case "3":
                        S1 = "32";
                        break;
                    case "2":
                        S1 = "22";
                        break;
                    case "153":
                        S1 = "1532";
                        break;
                    case "543":
                        S1 = "5432";
                        break;
                    default:
                        S1 = "2";
                        break;
                }
            }
            Server.data = "";
        }
        if (Server.data.Trim() == "10" || Keyboard.current.eKey.wasPressedThisFrame) //02 Green
        {

            GameObject.Find("UI/Game/Rounds/Timer/1").GetComponent<Animator>().Play("3");
            pop.Play();
            //First

            if (First)
            {
                Invoke("Startt", 0.2f);
            }

            //Butts

            if (Butts)
            {
                Invoke("E", 0.2f);
            }

            if (Again)
            {
                Invoke("ButEnd", 0.2f);
            }


            //Points

            if (Rounds)
            {
                P1++;

                switch (S1)
                {
                    case "41":
                        S1 = "413";
                        break;
                    case "0":
                        S1 = "3";
                        break;
                    case "24":
                        S1 = "243";
                        break;
                    case "22":
                        S1 = "223";
                        break;
                    case "15":
                        S1 = "153";
                        break;
                    case "54":
                        S1 = "543";
                        break;
                    case "414":
                        S1 = "4143";
                        break;
                    default:
                        S1 = "3";
                        break;
                }
            }
            Server.data = "";
        }
        if (Server.data.Trim() == "11" || Keyboard.current.rKey.wasPressedThisFrame) //10 Orange
        {
            GameObject.Find("UI/Game/Rounds/Timer/1").GetComponent<Animator>().Play("4");
            pop.Play();
            if (Die || Last)
            {
                Invoke("Died", 0.2f);
            }


            //Points

            if (Rounds)
            {
                P1++;

                switch (S1)
                {
                    case "0":
                        S1 = "4";
                        break;
                    case "2":
                        S1 = "24";
                        break;
                    case "32":
                        S1 = "324";
                        break;
                    case "5":
                        S1 = "54";
                        break;
                    case "41":
                        S1 = "414";
                        break;
                    default:
                        S1 = "4";
                        break;
                }
            }
            Server.data = "";
        }
        if (Server.data.Trim() == "20" || Keyboard.current.tKey.wasPressedThisFrame) //11 Blue
        {
            GameObject.Find("UI/Game/Rounds/Timer/1").GetComponent<Animator>().Play("5");
            pop.Play();
            //Points

            if (Rounds)
            {
                P1++;

                switch (S1)
                {
                    case "413":
                        S1 = "4135";
                        break;
                    case "243":
                        S1 = "2435";
                        break;
                    case "1":
                        S1 = "15";
                        break;
                    case "0":
                        S1 = "5";
                        break;
                    default:
                        S1 = "5";
                        break;
                }
            }

            //AC

            if (Love)
            {
                if (AC == true) { Invoke("AcStart", 0); AC = false; if (A3 == 0) { A1 = 0; A2 = 11; A3 = 12; } else if (A1 != 0) { uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A1); A1 = 0; } }
                if (RandomAC == true)
                {

                    if (A2 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A2); A2 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A3 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A3); A3 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A4 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A4); A4 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A5 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A5); A5 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A6 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A6); A6 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A7 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A7); A7 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A8 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A8); A8 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A9 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_casal/" + A9); A9 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else { Invoke("AcB", 0); }

                }
            }
            else
            {

                if (AC == true) { Invoke("AcStart", 0); AC = false; if (A3 == 0) { A1 = 0; A2 = 11; A3 = 12; } else if (A1 != 0) { uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A1); A1 = 0; } }
                if (RandomAC == true)
                {

                    if (A2 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A2); A2 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A3 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A3); A3 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A4 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A4); A4 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A5 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A5); A5 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A6 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A6); A6 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A7 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A7); A7 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A8 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A8); A8 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else if (A9 != 0)
                    {
                        uiImage.sprite = Resources.Load<Sprite>("acoes_amigos/" + A9); A9 = 0; RandomAC = false;
                        Invoke("RA", 1f);
                    }
                    else { Invoke("AcB", 0); }
                    RandomAC = false;
                    Invoke("RA", 1f);



                }
            }

            Server.data = "";
        }



    }

    public void Startt()
    {
        GameObject.Find("UI/Intro/Start").SetActive(false);
        GameObject.Find("UI/Intro/CAL").SetActive(true);

        First = false;
        Cal1 = true;
        Cal2 = true;
        Caln = 0;
    }
    public void CAL()
    {
        Once = true;
        Round = 0;
        GameObject.Find("UI/Intro/CAL").SetActive(false);
        GameObject.Find("UI/Intro/MJ").SetActive(true);
        Caln = 0;
        MJ = true;

    }
    public void Classic()
    {
        GameObject.Find("UI/Intro/MJ").SetActive(false);
        StartCoroutine(Tuts());
        Love = false;
        SetOptions();
        MJ = false;
        Tutu = true;
        MenuS.Stop();
        Betweens.Play();
    }
    public void Rom()
    {
        GameObject.Find("UI/Intro/MJ").SetActive(false);
        StartCoroutine(Tuts());
        Love = true;
        SetOptions();
        MJ = false;
        Tutu = true;
        MenuS.Stop();
        Betweens.Play();
    }


    public void TUT()
    {
        if (Tutu)
        {
            GameObject.Find("UI/Intro/TUT").SetActive(false);
            GameObject.Find("UI/Intro/TUT").SetActive(false);
            GameObject.Find("UI/Intro/Butts").SetActive(true);

            Butts = true;
            Tutu = false;
        }
    }

    public void E()
    {
        GameObject.Find("UI/Intro/Butts/Dif").SetActive(false);
        GameObject.Find("UI/Intro/Butts/E").SetActive(true);

        Butts = false;
        Sequ = true;

    }

    public void M()
    {
        GameObject.Find("UI/Intro/Butts/Dif").SetActive(false);
        GameObject.Find("UI/Intro/Butts/M").SetActive(true);

        Butts = false;
        Sequ = true;

    }

    public void H()
    {
        GameObject.Find("UI/Intro/Butts/Dif").SetActive(false);
        GameObject.Find("UI/Intro/Butts/D").SetActive(true);

        Butts = false;
        Sequ = true;

    }


    public void Seq()
    {
        GameObject.Find("UI/Intro/Butts").SetActive(false);
        GameObject.Find("UI/Game/Rounds/Count").SetActive(true);

        ST1 = true;
        ST2 = true;
        STn = 0;
        Sequ = false;

        switch (Round)
        {

            case 0:
                Round = 1;
                GameObject.Find("UI/Game/Rounds/Timer/RoundI").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Butts/round_1");
                GameObject.Find("UI/Game/Rounds/Timer/RoundI2").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Butts/round_1");
                break;
            case 1:
                Round = 2;
                GameObject.Find("UI/Game/Rounds/Timer/RoundI").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Butts/round_2");
                GameObject.Find("UI/Game/Rounds/Timer/RoundI2").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Butts/round_2");
                break;
            case 2:
                Round = 3;
                GameObject.Find("UI/Game/Rounds/Timer/RoundI").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Butts/round_3");
                GameObject.Find("UI/Game/Rounds/Timer/RoundI2").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Butts/round_3");
                break;
        }
    }

    public void SeqT()
    {
        if (STn > 0)
        {
            ST1 = true;
            ST2 = true;
            STn = 0;
        }
    }

    public void ATT()
    {
        if (ATn > 0)
        {
            AT1 = true;
            AT2 = true;
            ATn = 0;
        }
    }

    public void Count()
    {
        GameObject.Find("UI/Game/Rounds/Count").SetActive(false);
        GameObject.Find("UI/Game/Rounds/Timer").SetActive(true);

        TimerOn = true;

        Timer.Play();

        switch (Round)
        {
            case 1:
                Ctime = 90;
                break;
            case 2:
                Ctime = 60;
                break;
            case 3:
                Ctime = 30;
                break;
        }



        Rounds = true;
    }

    public void Tim1()
    {
        Timer.Stop();
        Points.Play();

        GameObject.Find("UI/Game/Rounds/Timer").SetActive(false);
        GameObject.Find("UI/Game/Rounds/End").SetActive(true);
        TimerOn = false;
        Rounds = false;
        if (Round < 3)
        {
            GameObject.Find("UI/Game/Rounds/End/P1").GetComponent<TMP_Text>().SetText(P1.ToString("F0"));
            GameObject.Find("UI/Game/Rounds/End/P2").GetComponent<TMP_Text>().SetText(P2.ToString("F0"));
        }
        else
        {

            GameObject.Find("UI/Game/Rounds/End/P1").GetComponent<TMP_Text>().SetText("???");
            GameObject.Find("UI/Game/Rounds/End/P2").GetComponent<TMP_Text>().SetText("???");

        }
        Invoke("Ac1", 5);


    }
    public void Ac1()
    {

        GameObject.Find("UI/Game/Rounds/End").SetActive(false);
        GameObject.Find("UI/Game/Rounds/Ações").SetActive(true);

        AC = true;
        R1.Stop();
        R2.Stop();
        R3.Stop();
        Acoes.Play();
    }

    public void AcStart()
    {

        GameObject.Find("UI/Game/Rounds/Ações/Start").SetActive(false);
        GameObject.Find("UI/Game/Rounds/Ações/Timer").SetActive(true);

        RandomAC = true;
        TimerOn = true;
        switch (Round)
        {
            case 1:
                Ctime = 30;
                break;
            case 2:
                Ctime = 25;
                break;
            case 3:
                Ctime = 20;
                break;
        }

    }

    public void RA()
    {
        RandomAC = true;
    }

    public void GO()
    {
        Acoes.Stop();
        Lob.Play();

        Once = false;

        GameObject.Find("UI/Game/Rounds/Ações/Timer").SetActive(false);
        GameObject.Find("UI/Game/Rounds/Ações/GameO").SetActive(true);

        Die = true;
        RandomAC = false;
    }

    public void Died()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
         
    }
    public void AcB()
    {

        GameObject.Find("UI/Game/Rounds/Ações/Timer/Image").SetActive(false);
        GameObject.Find("UI/Game/Rounds/Ações/Timer/Next").SetActive(false);
        GameObject.Find("UI/Game/Rounds/Ações/Timer/2But").SetActive(true);

        RandomAC = false;

        AT1 = true;
        AT2 = true;
        ATn = 0;


    }

    public void AcEnd()
    {
        if (Round < 3)
        {
            GameObject.Find("UI/Game/Rounds/Ações/Timer/2But").SetActive(false);
            GameObject.Find("UI/Game/Rounds/Ações/Timer").SetActive(false);
            GameObject.Find("UI/Game/Rounds/Ações/Timer/Image").SetActive(true);
            GameObject.Find("UI/Game/Rounds/Ações/Timer/Next").SetActive(true);
            GameObject.Find("UI/Game/Rounds/Ações/End").SetActive(true);

            Acoes.Stop();
            Betweens.Play();

            ATn = 0;
            Again = true;

            TimerOn = false;
            EndBut = true;

            AT1 = false;
            AT2 = false;
        }
        else
        {
            GameObject.Find("UI/Game/Rounds/Ações/Timer/2But").SetActive(false);
            GameObject.Find("UI/Game/Rounds/Ações/Timer").SetActive(false);
            GameObject.Find("UI/Game/Rounds/Ações/Timer/Image").SetActive(true);
            GameObject.Find("UI/Game/Rounds/Ações/Timer/Next").SetActive(true);

            if (P1 > P2)
            {
                GameObject.Find("UI/Game/RES/P1W").SetActive(true);

                GameObject.Find("UI/Game/RES/P1W/Poi1").GetComponent<TMP_Text>().SetText(P1.ToString("F0"));
                GameObject.Find("UI/Game/RES/P1W/Poi2").GetComponent<TMP_Text>().SetText(P2.ToString("F0"));

                X = (P2 * 10) / P1;
                X = 10 - X;
            }
            else
            {
                GameObject.Find("UI/Game/RES/P2W").SetActive(true);

                GameObject.Find("UI/Game/RES/P2W/Poi1").GetComponent<TMP_Text>().SetText(P1.ToString("F0"));
                GameObject.Find("UI/Game/RES/P2W/Poi2").GetComponent<TMP_Text>().SetText(P2.ToString("F0"));

                X = (P1 * 10) / P2;
                X = 10 - X;
            }

            AT1 = false;
            AT2 = false;
            TimerOn = false;

            Acoes.Stop();
            Final.Play();

            Invoke("Perg", 7);

        }
    }

    public void ButEnd()
    {

        GameObject.Find("UI/Game/Rounds/Ações/End").SetActive(false);
        GameObject.Find("UI/Game/Rounds/Ações").SetActive(false);
        GameObject.Find("UI/Game/Rounds/Ações/Start").SetActive(true);

        GameObject.Find("UI/Intro/Butts").SetActive(true);
        GameObject.Find("UI/Intro/Butts/Dif").SetActive(true);
        GameObject.Find("UI/Intro/Butts/E").SetActive(false);
        GameObject.Find("UI/Intro/Butts/M").SetActive(false);
        GameObject.Find("UI/Intro/Butts/D").SetActive(false);
        Butts = true;
        Again = false;
        A1 = 0;
        ACTION = 0;

        RandomAC = false;


    }

    public void Perg()
    {

        Pergunt.Play();



        GameObject.Find("UI/Game/RES/P1W").SetActive(false);
        GameObject.Find("UI/Game/RES/P2W").SetActive(false);
        GameObject.Find("UI/Game/PER").SetActive(true);

        Invoke("Perg2", 6);
        StartCoroutine(SpinAndStop());
    }

    public void Perg2()
    {
        X--;
        Perguntas = true;
    }


    IEnumerator SpinAndStop()
    {
        GameObject.Find("UI/Game/PER/Image").GetComponent<Animator>().Play("Move");
        GameObject.Find("UI/Game/PER/Image (2)").GetComponent<Animator>().Play("Move");
        Invoke("seg4", 4);


        Wheel.Play();

        float spinDuration = 4f;
        float spinSpeed = 360f / spinDuration;

        float spinTime = 0f;
        bool spinning = true;

        while (spinning)
        {
            float angle = spinTime * spinSpeed;
            int index = Mathf.FloorToInt(angle / 360f * remainingOptions.Count);

            textComponent.text = remainingOptions[index];


            spinTime += Time.deltaTime;

            if (spinTime >= spinDuration)
            {
                spinning = false;
            }

            yield return null;
        }

        int stopIndex = Random.Range(0, remainingOptions.Count);
        textComponent.text = remainingOptions[stopIndex];

        remainingOptions.RemoveAt(stopIndex);

    }

    public void seg4()
    {
        Points.Play();
        GameObject.Find("UI/Game/PER/Image").GetComponent<Animator>().Play("Stop");
        GameObject.Find("UI/Game/PER/Image (2)").GetComponent<Animator>().Play("Stop");

    }


    public void MenOn()
    {

        GameObject.Find("UI/Menu/UI").SetActive(true);
        Menu = true;

    }

    public void MenOFF()
    {

        GameObject.Find("UI/Menu/UI").SetActive(false);
        Menu = false;
    }

    public void Exit()
    {

        Application.Quit();

    }


    public IEnumerator Tuts()
    {
        GameObject.Find("UI/Intro/TUT").SetActive(true);
        yield return new WaitForSeconds(8f);
        GameObject.Find("UI/Intro/TUT/Image").SetActive(false);
        GameObject.Find("UI/Intro/TUT/Image (1)").SetActive(true);
        yield return new WaitForSeconds(8f);
        GameObject.Find("UI/Intro/TUT/Image (1)").SetActive(false);
        GameObject.Find("UI/Intro/TUT/Image (2)").SetActive(true);
        yield return new WaitForSeconds(8f);
        Invoke("TUT", 0);

    }
}
