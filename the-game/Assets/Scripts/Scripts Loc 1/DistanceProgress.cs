using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceProgress : MonoBehaviour
{

    [SerializeField] Transform Character;
    [SerializeField] Transform Finish;
    [SerializeField]
    //GameObject ProgressBar;
    Image ProgressBar;

    float maxDistance;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {

        maxDistance = getDistance();
        //Debug.Log("maxDs "+maxDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (Character.position.z <= maxDistance && Character.position.z <= Finish.position.z)
        {
            float distance = 1 - (getDistance() / maxDistance);
            //Debug.Log("ds " + distance);
            setProgress(distance);
        }   
    }

    float getDistance()
    {
        return Vector3.Distance(Character.position, Finish.position);

    }

    void setProgress(float p)
    {
        //ProgressBar.GetComponent<Image>().;
        ProgressBar.fillAmount = p;
        //Debug.Log("p" + p);
    }
}
