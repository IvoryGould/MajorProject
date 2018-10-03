using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float moveSpeed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Movement();

    }

    void Movement() {

        float moveH = Input.GetAxis("Horizontal") * Time.deltaTime;
        float moveV = Input.GetAxis("Vertical") * Time.deltaTime;

        this.transform.Translate((moveV * moveSpeed), 0, (moveH * moveSpeed));

    }
}
