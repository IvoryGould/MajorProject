﻿using System.Collections;
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

    private void OnCollisionEnter(Collision collision) {

        playerObj = collision.gameObject;

        if (collision.collider.tag == "P1" || collision.collider.tag == "P2")
        {

            PickUp();

        }

    }

    void PickUp() {

        Destroy(this.gameObject);

        if (playerObj.tag == "P1") {

            playerOne.health += 1;

        }
        else if (playerObj.tag == "P2") {

            playerTwo.health += 1;

        }

    }

}