﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //int passCount = 0; 
    public float thrust = 25;

    // Use this for initialization
    void Start () {

        StartCoroutine(TimerDelete());
        this.GetComponent<Rigidbody>().AddForce(transform.forward * thrust, ForceMode.Impulse);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator TimerDelete() {

        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);

    }

}
