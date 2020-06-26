using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotation;
    //[SerializeField]
    //private GameObject Game_Menu;

    GameObject GameMenu_UI;
    GameMenuLoc1 GameMenu_UI_Script;

    private Rigidbody rb;

    void Awake()
    {
        GameMenu_UI = GameObject.Find("GameMenu");

        GameMenu_UI_Script = GameMenu_UI.GetComponent<GameMenuLoc1>();

        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {   

    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.transform.tag == "Ground")
        {
            rb.isKinematic = true;
        }
        CharacterLoc1 character = collider.GetComponent<CharacterLoc1>();
        if (character)
        {
            character.CoinsCollecting++;
            GameMenu_UI_Script.CharacterCoinsCollecting();
            Destroy(gameObject, 0f);
        }
    }
}
