using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (this.transform.position.y == 1) {

            StartCoroutine(Despawn());

        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Despawn()
    {

        yield return new WaitForSecondsRealtime(10);
        Destroy(this.gameObject);

    }

}
