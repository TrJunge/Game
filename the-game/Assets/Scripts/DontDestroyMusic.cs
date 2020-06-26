using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{

    void Awake()
    {
        GameObject[] objs_dont_destroy = GameObject.FindGameObjectsWithTag("Music");

        if (objs_dont_destroy.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
