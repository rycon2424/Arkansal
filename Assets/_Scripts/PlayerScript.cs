
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Rigidbody rb;
    public float speed;
    public float sideways;
    public Animator anim;
    
	void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        Movement();
        Shooting();
        Animation();
	}

    void Movement()
    {
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * sideways * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 8f;
            }

            else
            {
                speed = 2;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            speed = 2;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                sideways = 6f;
            }
            else
            {
                sideways = 2;
            }
        }

        transform.Translate(Vector3.forward * translation);
        transform.Translate(Vector3.right * rotation);
    }

    void Shooting()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetInteger("State", 6);
        }
    }

    void Animation()
    {

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetInteger("State", 2);
            }
            else
            {
                anim.SetInteger("State", 1);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetInteger("State", 7);
            }
            else
            {
                anim.SetInteger("State", 3);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetInteger("State", 8);
            }
            else
            {
                anim.SetInteger("State", 4);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetInteger("State", 5);
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetInteger("State", 0);
        }

    }

}
