using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour {

    public PlayerController playerOne;
    public PlayerController playerTwo;

    public GameObject playerObj;

    void Awake() {

        playerOne = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerTwo = GameObject.Find("Player2").GetComponent<PlayerController>();

    }

    // Use this for initialization
    void Start() {



    }

    // Update is called once per frame
    void Update() {



    }

    private void OnCollisionEnter(Collision collision) {

        playerObj = collision.gameObject;

        if (collision.collider.tag == "P1" || collision.collider.tag == "P2") {

            PickUp();

        }

    }

    IEnumerator Wait() {

        yield return new WaitForSeconds(1);

    }

    void PickUp() {



        if (playerObj.tag == "P1") {

            if (playerOne.hasPickUp == false) {

                playerOne.hasPickUp = true;
                playerOne.revolverActive = false;
                playerOne.shotgunActive = true;
                playerOne.runShotgun = true;
                Destroy(this.gameObject);

            }
        }
        else if (playerObj.tag == "P2") {

            if (playerTwo.hasPickUp == false) {

                playerTwo.hasPickUp = true;
                playerTwo.revolverActive = false;
                playerTwo.shotgunActive = true;
                playerTwo.runShotgun = true;
                Destroy(this.gameObject);

            }
        }
    }
}
