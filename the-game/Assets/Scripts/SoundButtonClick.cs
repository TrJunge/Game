using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtonClick : MonoBehaviour
{
    public AudioSource aSource;

    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void StartPlaying()
    {
        aSource.Play();
        //Destroy(this.gameObject);
        //DontDestroyOnLoad(this.gameObject);
    }

    public void StopPlaying()
    {
        aSource.Stop();
    }
}
