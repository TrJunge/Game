using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuLoc1 : MonoBehaviour
{
    GameObject GameMenu_UI;
    GameObject ScoreMenu_UI;
    GameObject ShopMenu_UI;
    GameObject InventoryMenu_UI;
    GameObject Character;

    ScoreMenu ScoreMenu_UI_Script;

    string material;
    CharacterLoc1 character;
    GameMenu gm_script;
    Material SphereMaterial;



    void Awake()
    {
        gm_script = gameObject.transform.root.GetComponent<GameMenu>();
        GameMenu_UI = GameObject.Find("GameMenu");
        ScoreMenu_UI = GameObject.Find("ScoreMenu");
        ShopMenu_UI = GameObject.Find("ShopMenu");
        InventoryMenu_UI = GameObject.Find("InventoryMenu");
        Character = GameObject.Find("Character");

        ScoreMenu_UI_Script = ScoreMenu_UI.GetComponent<ScoreMenu>();

        character = Character.GetComponent<CharacterLoc1>();
        gm_script.isLocattion = true;
    }

    void Start()
    {
        gm_script.ispuse = true;
        //material = DataBaseLoc1.ExecuteQueryWithAnswer($"SELECT Shop.material FROM Shop INNER JOIN Inventory on Shop.object_id = Inventory.id_object INNER JOIN Player on Player.id = Inventory.id_player WHERE Player.current = 1 and Inventory.selected = 1");

        //Character.GetComponent<Renderer>().material = Resources.Load<Material>(material);

        gameObject.transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = DataBaseLoc1.ExecuteQueryWithAnswer($"SELECT nick From Player Where current = 1");
        gameObject.transform.GetChild(1).transform.GetComponent<Text>().text = "Best Score "+DataBaseLoc1.ExecuteQueryWithAnswer($"SELECT best_score From Player Where current = 1");
        gameObject.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = DataBaseLoc1.ExecuteQueryWithAnswer($"SELECT total_money From Player Where current = 1");
        ScoreMenu_UI_Script.Money = int.Parse(gameObject.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text);
        ScoreMenu_UI.SetActive(false);
        //Debug.Log(gm.isLocattion);
        //Time.timeScale = 0f;
    }

    void Update()
    {
      
    }

    public void ButtonShop()
    {
        gameObject.SetActive(false);
        ShopMenu_UI.SetActive(true);
        Character.SetActive(false);
    }

    public void ButtonInventory()
    {
        gameObject.SetActive(false);
        InventoryMenu_UI.SetActive(true);
        Character.SetActive(false);
    }

    public void ButtonStart()
    {
        gm_script.timer = 1f;
        gm_script.ispuse = false;
        //string text = gameObject.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text;
        string ph = gameObject.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text; ;
        string sourse = "";
        //Debug.Log(text);

        //if (text == "")
        //{
        //    sourse = ph;

        //}
        //else
        //{
        //    sourse = text;
        //}

        //Debug.Log(sourse);
        //gameObject.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = gameObject.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text;
        //Debug.Log(DataBase.ExecuteQueryWithAnswer($"SELECT count(*) From Player Where nick like '{gameObject.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text}'"));
        if ("0" == DataBaseLoc1.ExecuteQueryWithAnswer($"SELECT count(*) From Player Where nick like '{sourse}'"))
        {
            DataBaseLoc1.ExecuteQueryWithoutAnswer($"Insert into Player (nick, total_money, best_score, lvl, current) values ('{sourse}',0,0,1,1)");
        }
        else
        {
            DataBaseLoc1.ExecuteQueryWithoutAnswer($"UPDATE Player SET current = 0");
            DataBaseLoc1.ExecuteQueryWithoutAnswer($"Update Player Set current = 1 Where nick like '{sourse}'");
        }
        gameObject.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = DataBaseLoc1.ExecuteQueryWithAnswer($"SELECT total_money From Player Where current = 1");
        ScoreMenu_UI_Script.Money = int.Parse(gameObject.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text);
        //Time.timeScale = 1;
        //gm.ispuse = false;
        //gm.isLocattion = true;
        gameObject.SetActive(false);
        ScoreMenu_UI.SetActive(false);
        GameMenu_UI.SetActive(true);
        

    }
}
