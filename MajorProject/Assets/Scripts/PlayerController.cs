using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5;
    public float rotSpeed = 10;
    public float thrust = 25;

    public GameObject Curser;
    public GameObject Bullet;
    public GameObject gameOverPanel;

    public Text ammoText;
    public Text healthText;

    public Vector3 CurserVec;

    public int ammo = 6;
    public int health = 6;

    private bool paused;

    // Use this for initialization
    void Start () {

        if (this.gameObject.tag == "P1") {

            CurserVec = new Vector3(this.transform.position.x + 10, this.transform.position.y - 0.5f, this.transform.position.z);

        } else if (this.gameObject.tag == "P2") {

            CurserVec = new Vector3(this.transform.position.x - 10, this.transform.position.y - 0.5f, this.transform.position.z);

        }
        Instantiate(Curser, CurserVec, transform.rotation, this.transform);
        CurserBS();
        ammoText = GameObject.Find(this.gameObject.tag + "ammo").GetComponent<Text>();
        healthText = GameObject.Find(this.gameObject.tag + "life").GetComponent<Text>();
        gameOverPanel = GameObject.Find("Canvas").transform.Find("GameOverPanel").gameObject;

	}
	
	// Update is called once per frame
	void Update () {

        ammoText.text = ammo.ToString();
        healthText.text = health.ToString();

        Movement();
        CurserControll();
        Shoot();

        if (this.transform.rotation.x != 0 && this.transform.rotation.z != 0) {

            

        }

    }

    void Movement() {

        float moveH = Input.GetAxis("Horizontal" + this.gameObject.tag) * Time.deltaTime;
        float moveV = Input.GetAxis("Vertical" + this.gameObject.tag) * Time.deltaTime;

        this.transform.Translate((moveV * moveSpeed), 0, (moveH * moveSpeed));

    }

    void CurserControll() {

        //int layerMask = 1 << 9;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        float moveH2 = Input.GetAxis("Horizontal" + this.gameObject.tag + "-2") * Time.deltaTime;
        float moveV2 = Input.GetAxis("Vertical" + this.gameObject.tag + "-2") * Time.deltaTime;

        GameObject.Find("Curser" + this.gameObject.tag).transform.Translate((moveH2 * rotSpeed), 0, (moveV2 * rotSpeed));

        this.transform.LookAt(GameObject.Find("Curser" + this.gameObject.tag).transform);

        /*if (Physics.Raycast(ray, out hit, 100, layerMask)) {

            Debug.DrawLine(ray.origin, hit.point, Color.red);
            Debug.DrawLine(this.transform.position, hit.point, Color.blue, 2);
            GameObject.Find("Curser" + this.gameObject.tag).transform.position = hit.point;
            this.transform.LookAt(GameObject.Find("Curser" + this.gameObject.tag).transform);

        }*/

    }

    void Shoot() {

        

        if (Input.GetButtonDown("Shoot" + this.gameObject.tag) && ammo > 0 && paused == false) {

            ammo--;
            Instantiate(Bullet, this.transform);
            this.transform.Find("Bullet(Clone)").transform.SetParent(GameObject.Find("WorldPoint").transform);

            GameObject.Find("WorldPoint").transform.Find("Bullet(Clone)").name = "Bullet" + GameObject.Find("WorldPoint").transform.Find("Bullet(Clone)").transform.GetSiblingIndex().ToString();

            GameObject.Find("WorldPoint").transform.Find("Bullet" + (GameObject.Find("WorldPoint").transform.childCount - 1)).transform.position = this.gameObject.transform.GetChild(0).transform.position;
            GameObject.Find("WorldPoint").transform.Find("Bullet" + (GameObject.Find("WorldPoint").transform.childCount - 1)).GetComponent<Rigidbody>().AddForce(transform.forward * thrust, ForceMode.Impulse);
            
        }

        if (ammo == 0) {

            StartCoroutine(ReloadTimer());

        }

    }

    IEnumerator ReloadTimer() {

        while (ammo < 6) {

            paused = true;
            ammo++;
            yield return new WaitForSecondsRealtime(0.5f);

        }

        paused = false;

    }

    void CurserBS() {

        this.transform.Find("Curser(Clone)").name = "Curser" + this.gameObject.tag;
        this.transform.Find("Curser" + this.gameObject.tag).transform.SetParent(GameObject.Find("WorldPoint").transform);

    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.collider.tag == "Bullet") {

            health--;
            Destroy(collision.gameObject);
            Death();

        }

        if (collision.collider.tag == "DATRAINKILLEDMEH") {

            health = 0;
            Death();

        }

    }

    void Death() {

        if (health == 0) {

            Time.timeScale = 0.0f;
            gameOverPanel.SetActive(true);

        }

    }

}
