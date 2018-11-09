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

        if (this.transform.position == new Vector3(0, 1, 30))
        {

            StartCoroutine(Timer());

        }

    }

    void Movement() {

        this.transform.Translate(0, 0 , (-moveSpeed * Time.deltaTime));

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
