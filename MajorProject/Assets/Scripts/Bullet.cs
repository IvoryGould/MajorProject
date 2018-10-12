using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    int passCount = 0;

	// Use this for initialization
	void Start () {

        StartCoroutine(TimerDelete());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator TimerDelete() {

        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DELETEWALL" && passCount < 2) {

            passCount++;

        }

        if (other.gameObject.tag == "DELETEWALL" && passCount == 2) {

            Destroy(this.gameObject);

        }
    }
}
