
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
	}

    void Movement()
    {
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * sideways * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.W))
            {

            }
        }

        transform.Translate(Vector3.forward * translation);
        transform.Translate(Vector3.right * rotation);
    }

}
