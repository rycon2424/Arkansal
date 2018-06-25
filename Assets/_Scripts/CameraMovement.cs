using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;

	[Range(1.0f, 15.0f)]
	public float sensitivity = 5f;
	[Range(0.1f, 1.0f)]
	public float smoothing = 0.5f;

	[Range(1.0f, 90f)]
	public float yMinClamp = 60f;
	[Range(0.1f, 90f)]
	public float yMaxClamp = 60f;

	public float camCorrectionSpeed;

	GameObject player;
	GameObject focusPoint;
	GameObject recoveryPoint;

	bool canReturnCam;

	RaycastHit hit;

	void Start () {
		StartCoroutine (CamCorrectionDelay ());
		player = GameObject.FindObjectOfType<PlayerScript> ().gameObject;
		focusPoint = GameObject.FindObjectOfType<FocusPointTag>().gameObject;
		recoveryPoint = GameObject.FindObjectOfType<RecoveryPointTag> ().gameObject;
	}
	
	void Update () {
		LookAt ();
		Yrotator ();
		WalkCheck ();
	}

	void LookAt(){
		//Looks just above the player
		transform.LookAt (focusPoint.transform.position);	
	}

	void Yrotator(){
		//The input
		var md = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		//Calculations for the rotation
		md = Vector2.Scale (md, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp (smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp (smoothV.y, md.y, 1f / smoothing);
		mouseLook += smoothV;
		mouseLook.y = Mathf.Clamp (mouseLook.y, -yMinClamp, yMaxClamp);

		//Rotates the cam and player
		focusPoint.transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		player.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, player.transform.up);
	}

	void WalkCheck(){
		Ray myRay = new Ray (transform.position, -(transform.position - new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z)));
		Debug.DrawRay(transform.position, -(transform.position - new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z)));

		if (Physics.Raycast(myRay, out hit)) {
			if (hit.collider.gameObject.GetComponent<PlayerScript> () == null) {
				transform.position = Vector3.MoveTowards (transform.position, focusPoint.transform.position, camCorrectionSpeed);
				StartCoroutine (CamCorrectionDelay());
			} else {
				if (canReturnCam == true) {
					transform.position = Vector3.MoveTowards (transform.position, recoveryPoint.transform.position, camCorrectionSpeed);
				}
			}
		}
	}

	IEnumerator CamCorrectionDelay(){
		canReturnCam = false;
		yield return new WaitForSeconds (1f);
		canReturnCam = true;
	}
}
