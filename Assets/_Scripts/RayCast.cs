using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour {

    public GameObject cameraPos;
    [Range(1, 200)]
    public float fireRange;
    RaycastHit hit;

    [Header("Weapon")]
    public bool fireCooldown = false;
    public float fireRate;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray myRay = new Ray(cameraPos.transform.position, cameraPos.transform.TransformDirection(Vector3.forward));

        Debug.DrawRay(cameraPos.transform.position, cameraPos.transform.TransformDirection(Vector3.forward) * fireRange);


        if (Input.GetMouseButton(0) && Physics.Raycast(myRay, out hit, fireRange) && fireCooldown == false)
        {
            if (hit.collider.CompareTag("NormalHit")) 
            {
                Debug.Log("Hit");
            }

            if (hit.collider.CompareTag("Weakness"))
            {
                Debug.Log("headshot");
            }
            fireCooldown = true;
            StartCoroutine(FireRateCld());
        }
    }

    IEnumerator FireRateCld()
    {
        yield return new WaitForSeconds(fireRate);
        fireCooldown = false;
    }
}
