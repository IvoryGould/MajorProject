using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : MonoBehaviour {

    public PlayerController playerOne;
    public PlayerController playerTwo;

    public GameObject playerObj;

    void Awake() {

        playerOne = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerTwo = GameObject.Find("Player2").GetComponent<PlayerController>();

    }

    // Use this for initialization
    void Start () {

        

    }
	
	// Update is called once per frame
	void Update () {



	}

    private void OnCollisionEnter(Collision collision) {

        playerObj = collision.gameObject;

        if (collision.collider.tag == "P1" || collision.collider.tag == "P2") {

            PickUp();

        }

    }

    void PickUp() {



        if (playerObj.tag == "P1") {

            if (playerOne.hasPickUp == false && playerOne.shotgunActive == false) {

                playerOne.hasPickUp = true;
                playerOne.RapidBuff();
                Destroy(this.gameObject);

            }


        } else if(playerObj.tag == "P2") {

            if (playerTwo.hasPickUp == false && playerTwo.shotgunActive == false) {

                playerTwo.hasPickUp = true;
                playerTwo.RapidBuff();
                Destroy(this.gameObject);

            }



        }

    }

}
