using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterLoc1 : MonoBehaviour
{
    public int BestScore;
    public int TotalMoney;
    public int lvl;


    private Rigidbody rb;

    [SerializeField]
    float Speed = 10f;
    [SerializeField]
    float AddSpeed = 0.0f;
    [SerializeField]
    float DownForce = 8;
    [SerializeField]
    float UpForce = 0;
    public bool CheckGround;
    [SerializeField]
    public int CoinsCollecting = 0;
    [SerializeField]
    public int TraveledDistance = 0;

    bool CheckDrag = false;
    bool mCheckDrag = false;
    bool CheckSpaseDown;
    Vector3 move;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (CheckDrag == true)
        {
            //rb.drag = Mathf.Abs(Mathf.Lerp(0.4f, 1f,  Time.fixedDeltaTime * 2f));
            //CheckDrag = false;
        }
        Const_Movement();
        Controller_Movement();
    }

    void Const_Movement()
    {
        move = new Vector3(0f, 0,Speed);
        rb.AddForce(move, ForceMode.Acceleration);
    }

    private void Controller_Movement()
    {
        if (CheckGround == true)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space))
            {
                CheckSpaseDown = true;
                //GravityStrength();
            }
            else
            {
                CheckSpaseDown = false;
            }
        }

        if (CheckGround == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GravityStrength();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                CheckSpaseDown = true;
                GravityStrength();
            }
            else
            {
                    //CheckSpaseDown = false;
                    CheckSpaseDown = false;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //CheckSpaseDown = true;
                CheckSpaseDown = false;
            }
        }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
              
                if (CheckGround == false)
                {
                    if ((touch.phase == TouchPhase.Began) || (touch.phase == TouchPhase.Moved) || (touch.phase == TouchPhase.Stationary))
                    {
                        GravityStrength();
                    }
                }
            }   
    }

    private void GravityStrength()
    {
        if (CheckGround == false & DownForce > -20)
        {

            Vector2 movement = new Vector3(0f, DownForce);
            rb.AddForce(movement, ForceMode.Force);

            //rb.AddForce(movement, ForceMode.Impulse);
            //DownForce += -1f;
            //FORCE
            DownForce += -0.8f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            //DownForce = -0.3f;
            //FORCE
            DownForce = -8f;
            CheckGround = true;
            CheckDrag = false;
            CancelInvoke("GravityStrength");
            Invoke("Stop",0.3f);
            //StartCoroutine(Stop());
            if(CheckSpaseDown == true)
            {
                rb.drag = 0;
                //rb.mass = 0.05f;
                EnterGroundForce();
            }
            else
            {
                //rb.isKinematic = true;
                rb.drag = 0.4f;
                //rb.mass = 2;
                //Invoke("isDrag", 0.2f) ;
                //rb.isKinematic = false;
            }
        }
    }

    void Stop()
    {
        if (CheckGround == false)
        {
            Debug.Log(1);
            
            //CheckDrag = true;
            //rb.drag = 0.4f;
            //Physics.gravity = new Vector3(0,-15.00f,0);
            //Physics.gravity = new Vector3(0, -9.81f, 0);
            //rb.mass = 100;
            //var time = Time.deltaTime * 0.9f;
            //rb.drag = Mathf.Lerp(1.0f, 0.4f, Time.deltaTime * 10f);
        }
    }


    void EnterGroundForce()
    {
        //movement = new Vector2(0f, -10f);
        Vector2 movement = new Vector3(0f, -30f);
        rb.AddForce(movement, ForceMode.Force);
        //rb.AddForce(movement, ForceMode.VelocityChange);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            CheckGround = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            CheckGround = true;
            if (CheckSpaseDown == true)
            {
                rb.drag = 0;
                //rb.mass = 0.05f;
            }
            else
            {
                //rb.isKinematic = true;
                //rb.drag = 0.4f;
                // Invoke("isDrag", 1f);
                //rb.isKinematic = false;
            }

        }
    }

    void isDrag()
    {
        if(CheckSpaseDown == false)
        {
            rb.drag = 0.4f;
            //rb.mass = 2;
        }
        //rb.drag = 1f;

        //while(rb.drag !<= 0.4f)
        //{
        //}
    }
}