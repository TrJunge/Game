using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersServerReceive : MonoBehaviour
{
    public static GameObject Character;
    Vector3 position;
    string nickname;
    private void Awake()
    {
        Character = Resources.Load("CharacterOnline (R)") as GameObject;
    }
    public void Start()
    {
        CreateCharacter();
        Connection.Connect();
        //Connection.Send(character.transform.position.ToString());

    }


    void FixedUpdate()
    {
        if (Connection.position!="")
        {
            position = DeserializeVector3Array(Connection.position);
            //position_result = new Vector3(float.Parse(position_value[0]));
            Connection.position.Split(',')[3] = "";
            //Connection.Receive();
            
            //Connection.Send("ss ");
            //Debug.Log("CH pos " + Character.transform.position.ToString());
            //position = Character.transform.position;
            //Debug.Log("Pos " + position);
        }
    }

    public void CreateCharacter()
    {
        Instantiate(Character);
    }

    public static Vector3 DeserializeVector3Array(string aData)
    {
        string[] values = aData.Split(',');
        Vector3 result = new Vector3();
        result = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        return result;
    }
}
