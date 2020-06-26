using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishMiddle : MonoBehaviour
{
    GameObject ScoreProgress_UI;
    GameObject ScorePlane;

    ScorePlane ScriptScore;
    
    CharacterLoc1 character;

    public bool CharacterFinished = false;
    
    void Awake()
    {
        ScorePlane = GameObject.Find("ScorePlane");
        ScoreProgress_UI = GameObject.Find("ScoreProgress");
        ScriptScore = ScorePlane.GetComponent<ScorePlane>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        character = collider.GetComponent<CharacterLoc1>();
        if (character)
        {
            ScriptScore.Finished();
            ScoreProgress_UI.SetActive(true);
            CharacterFinished = true;
            //character.TotalMoney = character.CoinsCollecting;
            //Vector3 movement = new Vector3(0f, 6f, 2f);
            //character.GetComponent<Rigidbody>().AddForce(movement, ForceMode.Impulse);
        }
    }
}
