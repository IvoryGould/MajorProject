using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrain : MonoBehaviour {

    public GameObject train;

    void Awake() {
        
        

    }

    // Use this for initialization
    void Start () {

        

    }
	
	// Update is called once per frame
	void Update () {

        Spawn();

	}

    void Spawn() {

        if (GameObject.Find("Train1") == false) {

            Instantiate(train, new Vector3(0, 3, 40), Quaternion.identity);
            GameObject.Find("Train(Clone)").name = "Train1";
            
        }

        if (GameObject.Find("Train2") == false) {

            Instantiate(train, new Vector3(30, 3, 0), Quaternion.identity);
            GameObject.Find("Train(Clone)").name = "Train2";

        }
    }
}
