using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //int passCount = 0; 
    public float thrust = 25;

    public PlayerController playerOne;
    public PlayerController playerTwo;

    public GameObject playerObj;

    void Awake()
    {

        playerOne = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerTwo = GameObject.Find("Player2").GetComponent<PlayerController>();

    }

    // Use this for initialization
    void Start () {

        StartCoroutine(TimerDelete());
        this.GetComponent<Rigidbody>().AddForce(transform.forward * thrust, ForceMode.Impulse);

	}
	
	// Update is called once per frame
	void Update () {
		
        

	}

    IEnumerator TimerDelete() {

        yield return new WaitForSeconds(2);

        if (playerOne._bullets.Contains(this.gameObject)) {

            playerOne._bullets.Remove(this.gameObject);

        }
        else if (playerTwo._bullets.Contains(this.gameObject)) {

            playerTwo._bullets.Remove(this.gameObject);

        }

        Destroy(this.gameObject);

    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.collider.tag == "DELETEWALL") {

            if (playerOne._bullets.Contains(this.gameObject)) {

                playerOne._bullets.Remove(this.gameObject);

            }
            else if (playerTwo._bullets.Contains(this.gameObject)) {

                playerTwo._bullets.Remove(this.gameObject);

            }

            Destroy(this.gameObject);

        }

    }

}
