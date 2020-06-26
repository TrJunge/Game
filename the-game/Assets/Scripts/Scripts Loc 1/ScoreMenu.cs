using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    GameObject GameMenu_UI;

    public int Money;

    Text GameMenu_UI_CoinsColected_UI_Text;
    Text ScoreMenu_UI_Money_UI_Text;
    private void Awake()
    {
        GameMenu_UI = GameObject.Find("GameMenu");

        GameMenu_UI_CoinsColected_UI_Text = GameMenu_UI.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        ScoreMenu_UI_Money_UI_Text = gameObject.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
    }

    void ButtonNext()
    {
        Money = Money + int.Parse(ScoreMenu_UI_Money_UI_Text.text);
        DataBaseLoc1.ExecuteQueryWithoutAnswer($"Update Player Set total_money = \'"+ Money +"\' Where current = 1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
