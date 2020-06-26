using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewCharacter : MonoBehaviour
{
    public static GameObject character;
    private SurpriseSave surprise;
    //private Connector con;
    private void Awake()
    {
        character = Resources.Load("Character") as GameObject;
        //    character = Resources.Load<Character>("Character2");
    }
    public void Start()
    {
        CreateCharacter();
        Connection.Connect();
        Connection.Send(character.transform.position.ToString());
        //Character copyCharacter = Instantiate(character);

    }

   
    void Update()
    {

    }
    public void CreateCharacter()
    {
        Instantiate(character);
        //Character newCharacter = (Character)Instantiate(character, new Vector3(3, 1, 0), Quaternion.identity);
        //Instantiate(character[0], new Vector3(3, 1, 0), Quaternion.identity);

    }
}
