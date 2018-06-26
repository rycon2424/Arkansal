
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Rigidbody rb;
    public Animator anim;
    public CapsuleCollider slideBoxCollider;

    [Header("MovementFloats")]
    public float speed;
    public float sideways;
    public float reloadWalkSpeed;

    [Header("PlayerStates")]
    public bool sprinting;
    public bool reloading = false;

    [Header("Weapon")]
    public GameObject muzzleFlash;
    public Transform ammoClip;
    public GameObject playerHand;


    void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        Reload();
        Movement();
        Shooting();
        Animation();
        Sliding();
    }

    void Movement()
    {
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * sideways * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprinting = true;
        }
        else
        {
            sprinting = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!Input.GetKey(KeyCode.S))
                {
                    speed = 8f;
                }
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
                if (!Input.GetKey(KeyCode.S))
                {
                    sideways = 6f;
                }
                else
                {
                    sideways = 2;
                }
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
        if (Input.GetMouseButton(0) && !sprinting && !reloading)
          {
            muzzleFlash.SetActive(true);
                anim.SetBool("Firing", true);
                if (Input.GetKey(KeyCode.A))
                {
                    anim.SetBool("WalkFiring", false);
                    anim.SetBool("Firing", true);
                    anim.SetInteger("State", 3);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    anim.SetBool("WalkFiring", false);
                    anim.SetBool("Firing", true);
                    anim.SetInteger("State", 4);
                }
                if (Input.GetKey(KeyCode.W))
                {
                    anim.SetBool("WalkFiring", true);
                    anim.SetBool("Firing", false);
                }
                else
                {
                    anim.SetBool("WalkFiring", false);
                    anim.SetInteger("State", 9);
                }

          }
        else
        {
            muzzleFlash.SetActive(false);
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Firing", false);
            anim.SetBool("WalkFiring", false);
        }
    }

    void Reload()
    {
        if (reloading)
        {
            speed = reloadWalkSpeed;
            sideways = reloadWalkSpeed;
            anim.SetBool("Reload", true);
        }
        if (Input.GetKeyDown(KeyCode.R) && !reloading && !sprinting)
        {
            reloading = true;
            StartCoroutine(ReloadTime());
        }
    }

    IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(ammoClip, playerHand.transform.position, playerHand.transform.rotation);
        yield return new WaitForSeconds(1.7f);
        reloading = false;
        anim.SetBool("Reload", false);
    }

    void Sliding()
    {

        if (sprinting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(SlideHitbox());
                slideBoxCollider.enabled = false;
                anim.SetInteger("State", 11);
            }
        }

    }

    IEnumerator SlideHitbox()
    {
        yield return new WaitForSeconds(1.4f);
        slideBoxCollider.enabled = true;
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

        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W)
            && !Input.GetKey(KeyCode.D) && !Input.GetMouseButton(0))
        {
            anim.SetInteger("State", 0);
        }

    }

}