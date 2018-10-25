using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Movement() {

        Timer();
        //this.transform.Translate();

    }

    IEnumerator Timer() {

        yield return new WaitForSeconds(5);

    }
}
