using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using Mono.Data.Sqlite;

public class Menu : MonoBehaviour
{
    private string Target_Name = "MainMenu_Target";

    private Vector3 Cam_Default_Pos;
    public Vector2 target;
    private Vector2 position;

    private GameObject Button;
    private GameObject character;

    private GameObject ChangeLevel;

    private GameObject MainCanvas;

    private GameObject CanvasChangeLevel;
    private GameObject AuthorizationMenu;
    private GameObject MainMenu;

    private GameObject AuthorizationTargetCam;
    private GameObject cam;
    private GameObject LevelTargetCam;

    private GameObject CanvasAuth;
    private GameObject CanvasReg;

    private GameObject TargetDownAuth;
    private GameObject TargetMiddleAuth;
    private GameObject TargetUpAuth;

    private GameObject Level_Name;
    private GameObject Level_Image;


    GameObject ButtonP;
    GameObject part;
    GameObject UpTarget;
    GameObject MiddleTarget;
    GameObject DownTarget;
    GameObject ButtonPartTarget;
    GameObject ButtonPartTarget2;

    bool Change_Part = false;
    bool InRegistration = false;
    float StepPart;

    GameObject RegLogin;
    GameObject RegPassword;
    GameObject RegPasswordRepeat;
    GameObject AuthLogin;
    GameObject AuthPassword;

    GameObject NickName;
    GameObject NickName_placeholder;
    GameObject NickName_text;
    GameObject NickName_checkmark;

    GameObject User;

    GameObject ErrorMessage;

    GameObject MessageNickName;
    GameObject MessageAccept;
    GameObject Alert;

    string Nickname;

    bool ButtonCCU = false;
    bool NickNameNull = false;

    GameObject CanvasSettings;
    float timer = 1f;

    public AudioSource aSource;

    DataBase db = new DataBase();

    public void Awake()
    {
        MainCanvas = GameObject.Find("The_Main_Canvas_Menu_");

        AuthorizationMenu = MainCanvas.transform.GetChild(0).gameObject;
        MainMenu = MainCanvas.transform.GetChild(1).gameObject;
        CanvasChangeLevel = MainCanvas.transform.GetChild(2).gameObject;

        CanvasChangeLevel.SetActive(true);
        AuthorizationMenu.SetActive(true);
        MainMenu.SetActive(true);

        NickName = MainMenu.transform.GetChild(2).transform.GetChild(5).gameObject;
        NickName_placeholder = NickName.transform.GetChild(0).gameObject;
        NickName_text = NickName.transform.GetChild(1).gameObject;
        NickName_checkmark = NickName.transform.GetChild(2).gameObject;

        NickName_checkmark.SetActive(false);

        ButtonPartTarget2 = GameObject.Find("ButtonPartTarget2");

        ChangeLevel = GameObject.Find("ChangeLevel");
        TargetUpAuth = GameObject.Find("TargetUpAuth");
        TargetMiddleAuth = GameObject.Find("TargetMiddleAuth");
        TargetDownAuth = GameObject.Find("TargetDownAuth");
        target = GameObject.Find("MainMenu_Target").transform.position;
        Cam_Default_Pos = GameObject.Find("MainCamera").transform.position;
        character = GameObject.Find("Character");
        cam = GameObject.Find("MainCamera");

        CanvasAuth = AuthorizationMenu.transform.GetChild(0).transform.GetChild(0).gameObject;
        CanvasReg = AuthorizationMenu.transform.GetChild(0).transform.GetChild(1).gameObject;

        AuthLogin = CanvasAuth.transform.GetChild(0).transform.GetChild(0).gameObject;
        AuthPassword = CanvasAuth.transform.GetChild(0).transform.GetChild(1).gameObject;

        RegLogin = CanvasReg.transform.GetChild(0).transform.GetChild(0).gameObject;
        RegPassword = CanvasReg.transform.GetChild(0).transform.GetChild(1).gameObject;
        RegPasswordRepeat = CanvasReg.transform.GetChild(0).transform.GetChild(2).gameObject;

        User = AuthorizationMenu.transform.GetChild(1).gameObject;

        LevelTargetCam = GameObject.Find("CamTargetLevel");
        AuthorizationTargetCam = GameObject.Find("CamTargetAuthorization");
        Level_Name = GameObject.Find("Level_Name");
        Level_Image = GameObject.Find("Level_Image");
        ButtonP = GameObject.Find("ButtonPart");

        ErrorMessage = MainMenu.transform.GetChild(3).gameObject;

        CanvasSettings = cam.gameObject.transform.GetChild(0).gameObject;

        CanvasSettings.SetActive(false);


        character.GetComponent<Character>().enabled = false;

        CanvasChangeLevel.SetActive(false);
        AuthorizationMenu.SetActive(false);
    }

    public void Start()
    {
            Nickname = db.ExecuteQueryWithAnswer($"SELECT nickname FROM Player where active like 'true'");
            NickName_placeholder.GetComponent<Text>().text = Nickname;
        string user = Convert.ToString(db.ExecuteQueryWithAnswer($"SELECT login FROM Player where active like 'true'"));
        User.GetComponent<Text>().text = "user: " + user;
    }

    public void Exit()
    {
        ButtonCCU = false;
        MessageNickName.SetActive(false);
    }

    [Obsolete]
    public void Update()
    {
        Time.timeScale = timer;
        if (Time.timeScale == 1f)
        {
            if (InRegistration == true)
            {
                StepPart = 20.0f * Time.deltaTime;
                CanvasAuth.transform.position = Vector3.MoveTowards(CanvasAuth.transform.position, TargetUpAuth.transform.position, StepPart);
                CanvasReg.transform.position = Vector3.MoveTowards(CanvasReg.transform.position, TargetMiddleAuth.transform.position, StepPart);
                //CanvasAuthorization.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).GetComponent<Text>().text = "";
                //CanvasAuthorization.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Text>().text = "";
            }

            if (Change_Part == true)
            {
                StepPart = 10.0f * Time.deltaTime;
                if (ButtonP.transform.localScale == new Vector3(1, -1, 1))
                {
                    ButtonP.transform.position = Vector3.MoveTowards(ButtonP.transform.position, ButtonPartTarget2.transform.position, StepPart);
                    ChangeLevel.transform.position = Vector3.MoveTowards(ChangeLevel.transform.position, UpTarget.transform.position, StepPart);
                    part.transform.position = Vector3.MoveTowards(part.transform.position, MiddleTarget.transform.position, StepPart);
                    if (part.transform.position == MiddleTarget.transform.position && ChangeLevel.transform.position == UpTarget.transform.position)
                    {
                        Change_Part = false;
                    }
                }
                else
                {
                    ButtonP.transform.position = Vector3.MoveTowards(ButtonP.transform.position, ButtonPartTarget.transform.position, StepPart);
                    ChangeLevel.transform.position = Vector3.MoveTowards(ChangeLevel.transform.position, MiddleTarget.transform.position, StepPart);
                    part.transform.position = Vector3.MoveTowards(part.transform.position, DownTarget.transform.position, StepPart);
                    if (part.transform.position == DownTarget.transform.position && ChangeLevel.transform.position == MiddleTarget.transform.position)
                    {
                        Change_Part = false;
                    }
                }
            }
            if (Input.GetKey("escape")) Application.Quit();
            if (Target_Name == "MainMenu_Target") MainMenuTarget();
            if (Target_Name == "LevelChange_Target") LevelChangeTarget();
            if (Target_Name == "Authorization_Target") AuthorizationTarget();
            Next();
        }
    }

    public void CheckmarkNick()
    {
        string old_Nickname = NickName_placeholder.GetComponent<Text>().text;
        Nickname = NickName_text.GetComponent<Text>().text;

         int CountUsers = Convert.ToInt32(db.ExecuteQueryWithAnswer($"SELECT count(rowid) FROM Player where nickname like '" + Nickname + "'"));
        if (CountUsers == 0)
        {
            db.ExecuteQueryWithAnswer($"Update Player set nickname = '" + Nickname + "' where active like 'true'");
            NickName_placeholder.GetComponent<Text>().text = Nickname;
            NickName_text.GetComponent<Text>().text = Nickname;
        }
        else
        {
            NickName_checkmark.SetActive(false);
            ErrorMessage.SetActive(true);
            NickName.GetComponent<InputField>().text = old_Nickname;
            NickName_placeholder.GetComponent<Text>().text = old_Nickname;
            NickName_text.GetComponent<Text>().text = old_Nickname;
            ErrorMessage.transform.SetParent(MainMenu.transform);
            ErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Player with that nickname already exists";
        }
        NickName_checkmark.SetActive(false);
    }

    public void ChangeNickName()
    {
        if (NickName_text.GetComponent<Text>().text != "")
        {
            NickName_checkmark.SetActive(true);
        }
    }

    void Next()
    {
        target = GameObject.Find(Target_Name).transform.position;
        character.transform.position = Vector2.MoveTowards(character.transform.position, target, 0.9F);
        Cam();
    }

    public void LevelChange()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && (Level_Name.GetComponent<Text>().text != "LOCATION 1 PART 1"))
        {
            Level_Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("BigStar");
        }
    }
    public void OKerrorMessage()
    {
        ErrorMessage.SetActive(false);

    }

    public void Button_Start()
    {
        StartCoroutine(ButtonButton_StartWaitForSec());
    }

    IEnumerator ButtonButton_StartWaitForSec()
    {
        yield return new WaitForSeconds(0.3f);
        var Loc = GameObject.Find("Level_Name");
        var Loc_Text = Loc.GetComponent<Text>().text;
        Level_Name = GameObject.Find("Level_Name");
        string Path = Level_Name.GetComponent<Text>().text;
        SceneManager.LoadScene("Scene/" + Loc_Text + "/" + Path.Replace("\n", " "));
    }


    public void ButtonSettingsOpen()
    {
        CanvasSettings.SetActive(true);
        timer = 0;
    }

    public void ButtonSettingsClose()
    {
        CanvasSettings.SetActive(false);
        timer = 1;
    }

    public void ButtonTraining()
    {
        StartCoroutine(ButtonTrainingWaitForSec());
    }

    IEnumerator ButtonTrainingWaitForSec()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Scene/Training");
    }

    public void ButtonQuit()
    {
        StartCoroutine(ButtonQuitWaitForSec());
    }

    IEnumerator ButtonQuitWaitForSec()
    {
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
    }


    public void Click_Button_Part(GameObject butt)
    {
        StartCoroutine(ButtonClick_Button_PartWaitForSec(butt));
    }

    IEnumerator ButtonClick_Button_PartWaitForSec(GameObject butt)
    {
        yield return new WaitForSeconds(0.3f);
        var Level = GameObject.Find("Level_Name");
        var Level_Text = Level.GetComponent<Text>().text;
        string Path = butt.GetComponentInChildren<Text>().text;
        SceneManager.LoadScene("Scene/" + Level_Text + "/" + Path.Replace("\n", " "));
    }

    public void ServerScene()
    {
        SceneManager.LoadScene("Server");
    }

    public void ButtonRegInReg()
    {
        string login = RegLogin.GetComponent<InputField>().text;
        string password = RegPassword.GetComponent<InputField>().text;
        string passwordRepeat = RegPasswordRepeat.GetComponent<InputField>().text;
        if (password != "" || passwordRepeat != "" || login != "")
        {
            if (password == passwordRepeat)
            {
                int CountUsers = Convert.ToInt32(db.ExecuteQueryWithAnswer($"SELECT count(rowid) FROM Player where login like '" + login + "'"));
                if (CountUsers == 0)
                {
                    int id = Convert.ToInt32(db.ExecuteQueryWithAnswer($"SELECT max(id_player) FROM Player"));
                    Nickname = "Player" + (id + 1);

                    db.ExecuteQueryWithAnswer($"UPDATE Player Set active = 'false' where active like 'true'");
                    db.ExecuteQueryWithAnswer($"Insert into Player (nickname, login, password, active) values ('{Nickname}','{login}','{password}','true')");

                    Nickname = Convert.ToString(db.ExecuteQueryWithAnswer($"SELECT nickname FROM Player where active like 'true'"));
                    NickName.GetComponent<InputField>().text = Nickname;
                    NickName_placeholder.GetComponent<Text>().text = Nickname;
                    NickName_text.GetComponent<Text>().text = Nickname;

                    string user = Convert.ToString(db.ExecuteQueryWithAnswer($"SELECT login FROM Player where active like 'true'"));
                    User.GetComponent<Text>().text = "user: " + user;
                }
                else
                {
                    ErrorMessage.SetActive(true);
                    ErrorMessage.transform.SetParent(AuthorizationMenu.transform);
                    ErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Player with this e-mail already exists";
                }
            }
            else
            {
                ErrorMessage.SetActive(true);
                ErrorMessage.transform.SetParent(AuthorizationMenu.transform);
                ErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Password mismatch";
            }
            RegLogin.GetComponent<InputField>().text = "";
            RegPassword.GetComponent<InputField>().text = "";
            RegPasswordRepeat.GetComponent<InputField>().text = "";
        }
        else
        {
            ErrorMessage.SetActive(true);
            ErrorMessage.transform.SetParent(AuthorizationMenu.transform);
            ErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "There are no compleated field";
        }
    }

    public void ButtonAutorization()
    {
        string login = AuthLogin.GetComponent<InputField>().text;
        string password = AuthPassword.GetComponent<InputField>().text;
        int Count = Convert.ToInt32(db.ExecuteQueryWithAnswer($"SELECT count(rowid) FROM Player where login like '{login}' and password like '{password}'"));
        if (Count == 1)
        {
            db.ExecuteQueryWithAnswer($"UPDATE Player Set active = 'false' where active like 'true'");
            db.ExecuteQueryWithAnswer($"UPDATE Player SET active = 'true' where login like '{login}' and password like '{password}'");

            Nickname = Convert.ToString(db.ExecuteQueryWithAnswer($"SELECT nickname FROM Player where active like 'true'"));
            NickName.GetComponent<InputField>().text = Nickname;
            NickName_placeholder.GetComponent<Text>().text = Nickname;
            NickName_text.GetComponent<Text>().text = Nickname;

            string user = Convert.ToString(db.ExecuteQueryWithAnswer($"SELECT login FROM Player where active like 'true'"));
            User.GetComponent<Text>().text = "user: " + user;

            SceneManager.LoadScene("Scene/Server/Mode_menu");
        }
        else
        {
            ErrorMessage.SetActive(true);
            ErrorMessage.transform.SetParent(AuthorizationMenu.transform, false);
            ErrorMessage.transform.GetChild(0).GetComponent<Text>().text = "Login or password entered incorrectly";
        }
        AuthLogin.GetComponent<InputField>().text = "";
        AuthPassword.GetComponent<InputField>().text = "";
    }

    public void ButtonReg()
    {
        TargetUpAuth = GameObject.Find("TargetUpAuth");
        TargetMiddleAuth = GameObject.Find("TargetMiddleAuth");
        TargetDownAuth = GameObject.Find("TargetDownAuth");
        InRegistration = true;
        Invoke("Update",0);
    }

    public void Button_Part()
    {
        ButtonPartTarget = GameObject.Find("ButtonPartTarget");
        ButtonPartTarget2 = GameObject.Find("ButtonPartTarget2");
        ButtonP = GameObject.Find("ButtonPart");
        part = GameObject.Find("Part");
        CanvasChangeLevel= GameObject.Find("Canvas_LevelChange");
        UpTarget = GameObject.Find("UpTarget");
        MiddleTarget = GameObject.Find("MiddleTarget");
        DownTarget = GameObject.Find("DownTarget");
        Invoke("Part", 0.05f); 
    }


    public void Part()
    {

        //Debug.Log(1);
        if (ButtonP.transform.localScale == new Vector3(1, 1, 1))
        {
            ButtonP.transform.localScale = new Vector3(1, -1, 1);
            Change_Part = true;
            Invoke("Update", 0.1f);
        }
        else
        {
            ButtonP.transform.localScale = new Vector3(1, 1, 1);
            Change_Part = true;
            Invoke("Update", 0.1f);
        }
    }

    string ss = "s";
    void AuthorizationTarget()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CanvasAuth.transform.position = TargetMiddleAuth.transform.position;
            CanvasReg.transform.position = TargetDownAuth.transform.position;
            Target_Name = "MainMenu_Target";
            character.transform.localScale = new Vector2(-1, 1);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //CanvasAuthorization.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text = "ddd";
            Target_Name = "Authorization_Target";
            character.transform.localScale = new Vector2(1, 1);
        }
    }

    void MainMenuTarget()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Target_Name = "LevelChange_Target";
            character.transform.localScale = new Vector2(-1, 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Target_Name = "Authorization_Target";
            character.transform.localScale = new Vector2(1, 1);
        }
    }

    void LevelChangeTarget()
    {
        //LevelChange();
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Target_Name = "LevelChange_Target";
            character.transform.localScale = new Vector2(-1, 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Target_Name = "MainMenu_Target";
            character.transform.localScale = new Vector2(1, 1);
        }
    }

    void Cam()
    {
        if (Target_Name == "LevelChange_Target" && cam.transform.position != LevelTargetCam.transform.position)
        {
            InRegistration = false;
            cam.transform.position = LevelTargetCam.transform.position;
            MainMenu.SetActive(false);
            CanvasChangeLevel.SetActive(true);
            AuthorizationMenu.SetActive(false);
        }
        if (Target_Name == "Authorization_Target" && cam.transform.position != AuthorizationTargetCam.transform.position)
        {
            cam.transform.position = AuthorizationTargetCam.transform.position;
            MainMenu.SetActive(false);
            CanvasChangeLevel.SetActive(false);
            AuthorizationMenu.SetActive(true);
        }
        if (Target_Name == "MainMenu_Target" && cam.transform.position != Cam_Default_Pos)
        {
            InRegistration = false;
            cam.transform.position = Cam_Default_Pos;
            MainMenu.SetActive(true);
            CanvasChangeLevel.SetActive(false);
            AuthorizationMenu.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        var ch = character.GetComponent<Character>();
        Character ch_coll = coll.GetComponent<Character>();
        if (ch_coll)
        {
            ch.MyScene = "MainMenu";
        }
    }
}
