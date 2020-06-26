using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

    GameObject CategoryPanel;
    GameObject SettingsByCategory;

    GameObject SettingsSound;

    void Start()
    {
        CategoryPanel = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;


        SettingsByCategory = gameObject.transform.GetChild(0).transform.GetChild(1).gameObject;

        SettingsSound = SettingsByCategory.transform.GetChild(0).gameObject;
    }

}
