using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePlane : MonoBehaviour
{
    GameObject ScoreProgress_UI;
    GameObject Character;
    GameObject TheEnd;
    GameObject Finish_obj;
    GameObject GameMenu_UI;
    GameObject ScoreMenu_UI;

    FinishMiddle ScriptFinish;

    Text LevelProgress_Text;
    Text ScoreProgress_UI_Text;
    Text GameMenu_UI_CoinsColected_UI_Text;
    Text ScoreMenu_UI_Money_UI_Text;

    Image FillLevelProgress_image;
    float maxDistance;
    float distance;

    void Awake()
    {
        GameMenu_UI = GameObject.Find("GameMenu");
        Finish_obj = GameObject.Find("FinishMiddle");
        TheEnd = GameObject.Find("TheEnd");
        Character = GameObject.Find("Character");
        ScoreProgress_UI = GameObject.Find("ScoreProgress");
        ScoreMenu_UI = GameObject.Find("ScoreMenu");

        ScriptFinish = Finish_obj.GetComponent<FinishMiddle>();

        GameMenu_UI_CoinsColected_UI_Text = GameMenu_UI.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        ScoreMenu_UI_Money_UI_Text = ScoreMenu_UI.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
        LevelProgress_Text = ScoreMenu_UI.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        ScoreProgress_UI_Text = ScoreProgress_UI.GetComponent<Text>();

        FillLevelProgress_image = ScoreMenu_UI.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
    }

    public void Finished()
    {
        maxDistance = getDistance();
    }

    private void FixedUpdate()
    {
        if (Character.transform.position.z <= TheEnd.transform.position.z && ScriptFinish.CharacterFinished == true)
        {
            distance = maxDistance - getDistance();
            ScoreProgress();

        }
    }

    private void ScoreProgress()
    {
        ScoreProgress_UI_Text.text = "Score " + (int)distance;
    }

    float getDistance()
    {
        return Vector3.Distance(Character.transform.position, TheEnd.transform.position);

    }

    void LevelProgress()
    {
        int best_score = int.Parse(DataBaseLoc1.ExecuteQueryWithAnswer($"Select best_score From Player where current = 1"));
        if ((int)distance > best_score)
        {
            DataBaseLoc1.ExecuteQueryWithoutAnswer($"Update Player Set best_score = {(int)distance} Where current = 1");
        }
        ScoreMenu_UI.SetActive(true);
        ScoreMenu_UI_Money_UI_Text.text = GameMenu_UI_CoinsColected_UI_Text.text;
        GameMenu_UI.SetActive(false);
        LevelProgress_Text.text = (int)distance + "/500";
        FillLevelProgress_image.fillAmount = distance / 500;
        ScoreProgress_UI.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        CharacterLoc1 character = collider.GetComponent<CharacterLoc1>();
        if (character)
        {
            Invoke("LevelProgress", 0.5f);
            character.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
