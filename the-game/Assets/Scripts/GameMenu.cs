using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public float timer;
    public bool ispuse = false;

    GameObject CanvasGameMenu;
    GameObject MenuGame;
    GameObject Character;
    GameObject MenuButton;

    public bool isLocattion = false;

    bool ButtonMenuClicked = false;

    GameObject CanvasSettings;
    //GameObject ButtonStart;


    private void Awake()
    {
        CanvasSettings = this.gameObject.transform.GetChild(2).gameObject;
        //ButtonStart = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        CanvasSettings.SetActive(false);
        //timer = 1f;
    }

    void Start()
    {
        MenuButton = GameObject.Find("ButtonMenu");
        CanvasGameMenu = gameObject.transform.GetChild(1).gameObject;
        Character = GameObject.Find("Character");
        CanvasGameMenu.GetComponent<Canvas>().worldCamera = gameObject.GetComponent<Camera>();
        MenuGame = gameObject.transform.GetChild(1).transform.GetChild(1).gameObject;
        MenuGame.SetActive(false);

        if (isLocattion == true)
        {
            timer = 0;
            //Time.timeScale = 0f;
        }
        else
        {
            timer = 1;
            //Time.timeScale = 1f;
        }

    }

    void Update()
    {    
        Time.timeScale = timer;
    }

    public void ButtonSettingsOpen()
    {
        CanvasSettings.SetActive(true);
        //timer = 0;
        Time.timeScale = 0f;
    }

    public void ButtonSettingsClose()
    {
        CanvasSettings.SetActive(false);
    }

    public void Continue()
    {
        //Time.timeScale = 1f;
        if (ispuse == true)
        {
            timer = 0f;
        }
        else
        {
            timer = 1f;
        }

        //ButtonStart.SetActive(true);
        //        //Character.SetActive(true);
        MenuButton.SetActive(true);
        MenuGame.SetActive(false);

        //ispuse = false;
        //    Update();
    }

    public void BackMenu()
    {
        timer = 1f;
        //Time.timeScale = 1f;
        Cursor.visible = true;
        ispuse = false;
        SceneManager.LoadScene("Scene/Menu");
    }

    public void ButtonMenu()
    {
        timer = 0;
        //ButtonStart.SetActive(false);
        //Time.timeScale = 0f;
        MenuButton.SetActive(false);
        //        //Character.SetActive(false);
        MenuGame.SetActive(true);
        //       //Cursor.visible = true;
        //ButtonMenuClicked = true;
        //   Update();
    }
}

