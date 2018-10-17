﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    float moveSpeed = 5;
    public GameObject Curser;
    public GameObject Bullet;
    public Text ammoText;
    public Vector3 CurserVec;
    public int ammo = 6;
    private bool paused;

    // Use this for initialization
    void Start () {

        CurserVec = new Vector3(this.transform.position.x + 10, this.transform.position.y - 0.5f, this.transform.position.z);
        Instantiate(Curser, CurserVec, transform.rotation);
        //ammoText = 

	}
	
	// Update is called once per frame
	void Update () {

        ammoText.text = ammo.ToString();

        Movement();
        CurserControll();
        Shoot();

        if (this.transform.rotation.x != 0 && this.transform.rotation.z != 0) {

            

        }

    }

    void Movement() {

        float moveH = Input.GetAxis("Horizontal1") * Time.deltaTime;
        float moveV = Input.GetAxis("Vertical1") * Time.deltaTime;

        this.transform.Translate((moveH * moveSpeed), 0, (moveV * moveSpeed));

    }

    void CurserControll() {

        int layerMask = 1 << 9;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        float moveH2 = Input.GetAxis("Horizontal1-2") * Time.deltaTime;
        float moveV2 = Input.GetAxis("Vertical1-2") * Time.deltaTime;

        GameObject.Find("Curser(Clone)").transform.Translate((moveH2 * moveSpeed), 0, (moveV2 * moveSpeed));

        if (Physics.Raycast(ray, out hit, 100, layerMask)) {

            Debug.DrawLine(ray.origin, hit.point, Color.red);
            Debug.DrawLine(GameObject.Find("Player").transform.position, hit.point, Color.blue, 2);
            GameObject.Find("Curser(Clone)").transform.position = hit.point;
            this.transform.LookAt(GameObject.Find("Curser(Clone)").transform);

        }

    }

    void Shoot() {

        float thrust = 25;

        if (Input.GetButtonDown("Shoot1") && ammo > 0 && paused == false) {

            ammo--;
            Instantiate(Bullet, GameObject.Find("Player").transform);
            GameObject.Find("Player").transform.Find("Bullet(Clone)").transform.SetParent(GameObject.Find("WorldPoint").transform);

            GameObject.Find("WorldPoint").transform.Find("Bullet(Clone)").name = "Bullet" + GameObject.Find("WorldPoint").transform.Find("Bullet(Clone)").transform.GetSiblingIndex().ToString();

            GameObject.Find("WorldPoint").transform.Find("Bullet" + (GameObject.Find("WorldPoint").transform.childCount - 1)).transform.position = new Vector3(GameObject.Find("Player").transform.position.x + 1, GameObject.Find("Player").transform.position.y, GameObject.Find("Player").transform.position.z);
            GameObject.Find("WorldPoint").transform.Find("Bullet" + (GameObject.Find("WorldPoint").transform.childCount - 1)).GetComponent<Rigidbody>().AddForce(transform.forward * thrust, ForceMode.Impulse);
            
        }

        if (ammo == 0) {

            StartCoroutine(ReloadTimer());

        }

    }

    IEnumerator ReloadTimer() {

        while (ammo < 6)
        {

            paused = true;
            ammo++;
            yield return new WaitForSecondsRealtime(0.5f);

        }

        paused = false;

    }
 
}
