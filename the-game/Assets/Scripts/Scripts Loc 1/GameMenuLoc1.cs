using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuLoc1 : MonoBehaviour
{
    GameObject CoinsColected_UI;
    GameObject Character;
    GameObject ScoreProgress_UI;
    CharacterLoc1 ScriptCharacter;
    GameObject Death_UI;
    int CountCoins = 0;
    GameMenu gm_script;


    float LaterCharacterPosition;

    void Awake()
    {

        CoinsColected_UI = GameObject.Find("CoinsColected");
        Character = GameObject.Find("Character");
        ScoreProgress_UI = GameObject.Find("ScoreProgress");
        Death_UI = gameObject.transform.GetChild(4).gameObject;
        ScriptCharacter = Character.GetComponent<CharacterLoc1>();
        gm_script = gameObject.transform.root.GetComponent<GameMenu>();
    }

    void Start()
    {


        Death_UI.SetActive(false);

        gameObject.SetActive(false);


    }

    void FixedUpdate()
    {
        //Debug.Log("Ch"+ Character.gameObject.transform.position.z);
        LaterCharacterPosition = Character.gameObject.transform.position.z;
        Invoke("SaveLaterCharacterPosition", 1);

    }

    void SaveLaterCharacterPosition()
    {
        if (LaterCharacterPosition > Character.gameObject.transform.position.z) Death();
        //Debug.Log("Lch"+LaterCharacterPosition);
    }

    void Death()
    {
        gm_script.ispuse = true;
        Death_UI.SetActive(true);
        //Time.timeScale = 0;
        gm_script.timer = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CharacterCoinsCollecting()
    {
        CountCoins = ScriptCharacter.CoinsCollecting;
        CoinsColected_UI.transform.GetChild(0).GetComponent<Text>().text = CountCoins.ToString();

    }

    public void ButtonBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

