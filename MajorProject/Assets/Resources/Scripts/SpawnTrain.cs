using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrain : MonoBehaviour {

    public GameObject train;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Spawn();

	}

    void Spawn() {

        if (GameObject.Find("Train(Clone)") == false) {

            Instantiate(train, new Vector3(0, 1, 30), Quaternion.identity);

        }
    }
}
