using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_ : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    float Speed = 7f;
    [SerializeField]
    float AddSpeed = 0.0f;
    [SerializeField]
    float DownForce = 0;
    [SerializeField]
    float UpForce = 0;
    public bool CheckGround;
    float Gravity;
    //[SerializeField]
    //public bool Gravity = true;
    //float GravityForce = -9.81f;
    [SerializeField]
    public int CoinsCollecting = 0;
    [SerializeField]
    public int TraveledDistance = 0;

    bool isPressed = false;
    Vector3 movement;

    void Start()
    {
        Gravity = Physics.gravity.y;
        rb = GetComponent<Rigidbody>();


    }
    void FixedUpdate()
    {
        Const_Movement();
        Controller_Movement();
    }

    private void Const_Movement()
    {
        movement = new Vector3(0f, 0f, Speed);
        //rb.AddForce(movement, ForceMode.Acceleration);
        //rb.
        //rb.AddTorque(-rb.angularVelocity, ForceMode.VelocityChange);
        rb.AddForce(movement, ForceMode.Acceleration);
    }
    private void Controller_Movement()
    {
        if (CheckGround == true)
        {
            DownForce = 0;
            isPressed = false;
            if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKey(KeyCode.Space)))
            {
                isPressed = true;
                //rb.AddForce(movement, ForceMode.Impulse);
                //rb.mass = 4;
                //AddSpeed = Mathf.Lerp(AddSpeed, 0.4f, Time.deltaTime * ((0.4f * 80) / 100));
            }
            else
            {

                //rb.mass = 1;
                //AddSpeed = Mathf.Lerp(AddSpeed, 0, Time.deltaTime * ((0.2f * 80) / 100));
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
            }
            //rb.mass = 1;
            //Vector3 movement = new Vector3(0f, 0f, AddSpeed);
            //rb.AddForce(movement, ForceMode.Impulse);
        }

        if (CheckGround == false)
        {
            isPressed = false;
            if (Input.GetKey(KeyCode.Space))
            {
                Invoke("GravityStrength", 0.05f);
                isPressed = true;
                //rb.mass = 24;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPressed = true;
                //rb.mass = 24;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {

            }
        }
    }

    private void GravityStrength()
    {
        if (CheckGround == false)
        {
            //DownForce = -0.5f;
            //DownForce = Mathf.Lerp(DownForce, -1, Time.deltaTime * 2f);
            DownForce = Mathf.Lerp(-10, -50, Time.deltaTime * 10f);
            Vector3 movement = new Vector3(0f, DownForce, 0);
            //rb.AddForce(movement, ForceMode.Impulse);
            rb.AddForce(movement, ForceMode.Acceleration);
        }
    }

    private void Reset()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            if (isPressed == false)
            {
                rb.AddForce(new Vector3(0, 0, 0), ForceMode.Acceleration);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            CheckGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            CheckGround = false;


        }
    }

    private void CkGround_ForceUp()
    {
   

       
    }
}