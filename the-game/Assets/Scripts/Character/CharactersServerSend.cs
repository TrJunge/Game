using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersServerSend : MonoBehaviour
{
    GameObject Character;
    Vector3 position;
    string nickname;

    DataBase db = new DataBase();

    private void Awake()
    {
        Character = gameObject;
    }

    void Start()
    {
        nickname = db.ExecuteQueryWithAnswer($"SELECT nickname FROM Player where active like 'true'");
        Connection.Connect();
        Connection.Send("");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (position != Character.transform.position)
        {
            Connection.Send(Character.transform.position.ToString()+","+nickname+"pos");
            //Connection.Send("ss ");
            //Debug.Log("CH pos " + Character.transform.position.ToString());
            position = Character.transform.position;
            //Debug.Log("Pos " + position);
        }
        //InvokeRepeating("SendPosition",0,);
        ////Debug.Log(Time.deltaTime);
    }

    void SendPosition()
    {

    }
}
