using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    public float moveSpeed = 5;
    float timerNum;
    public float timerMax = 20;

    void Awake() {



    }

    // Use this for initialization
    void Start () {

        timerNum = Random.Range(6, timerMax);

    }
	
	// Update is called once per frame
	void Update () {

        if (this.name == "Train1" && this.transform.position == new Vector3(0, 3, 40))
        {

            this.transform.rotation = Quaternion.Euler(0, 180, 0);
            StartCoroutine(Timer());

        }
        else if (this.name == "Train2" && this.transform.position == new Vector3(30, 3, 0))
        {

            this.transform.rotation = Quaternion.Euler(0, -90, 0);
            StartCoroutine(Timer());

        }

    }

    void Movement() {

        this.transform.Translate(0, 0 , (moveSpeed * Time.deltaTime));

    }

    void DeleteTrain() {

        Destroy(this.gameObject);

    }

    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "TrainDeleter") {

            DeleteTrain();

        }

    }

    IEnumerator Timer() {

        yield return new WaitForSeconds(timerNum);
        Movement();

    }


}
