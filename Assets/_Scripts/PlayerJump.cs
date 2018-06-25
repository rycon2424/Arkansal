using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public bool isFalling = false;
    public float jump;

    public Rigidbody rb;

    void FixedUpdate()
    {
        //JUMPSCRIPT
        if (Input.GetKey(KeyCode.Space) && isFalling == false)
        {
            rb.velocity = new Vector3(0f, jump, 0f);
            isFalling = true;
        }
    }

    void OnTriggerStay()
    {
        isFalling = false;
    }
}
