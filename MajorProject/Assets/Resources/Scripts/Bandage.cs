using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandage : MonoBehaviour
{

    public PlayerController playerOne;
    public PlayerController playerTwo;

    public GameObject playerObj;

    void Awake()
    {

        playerOne = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerTwo = GameObject.Find("Player2").GetComponent<PlayerController>();

    }
    
    void OnTriggerEnter(Collider collision) {

        playerObj = collision.gameObject;

        if (collision.tag == "P1" || collision.tag == "P2")
        {

            PickUp();

        }

    }

    void PickUp() {

        Destroy(this.gameObject);

        if (playerObj.tag == "P1" && playerOne.health != 6) {

            playerOne.health += 1;
            playerOne.BandageFeedback();
            playerOne.l_Health[playerOne._healthIdx].enabled = true;
            playerOne._healthIdx += 1;

        }
        else if (playerObj.tag == "P2" && playerTwo.health != 6) {

            playerTwo.health += 1;
            playerTwo.BandageFeedback();
            playerTwo.l_Health[playerTwo._healthIdx].enabled = true;
            playerTwo._healthIdx += 1;

        }

    }

}