using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour {

    public float timer;
    
	void Start ()
    {
        Destroy(this.gameObject, timer);
	}

}
