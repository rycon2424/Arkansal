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

	GameObject player;
	GameObject focusPoint;

	void Start () {
		player = GameObject.FindObjectOfType<PlayerScript> ().gameObject;
		focusPoint = GameObject.FindObjectOfType<FocusPointTag>().gameObject;
	}
	
	void Update () {
		LookAt ();
		Yrotator ();
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
		mouseLook.y = Mathf.Clamp (mouseLook.y, -60f, 60f);

		//Rotates the cam and player
		focusPoint.transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		player.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, player.transform.up);
	}
}
