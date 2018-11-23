using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5;
    public float rotSpeed = 10;
    public float fireRateMultiplyer = 0.5f;
    public float rapidFireModifier = 0.25f;

    public GameObject Curser;
    public GameObject Bullet;
    public GameObject gameOverPanel;

    public Transform p1gunMid;
    public Transform p1gunLeft;
    public Transform p1gunRight;

    public Transform p2gunMid;
    public Transform p2gunLeft;
    public Transform p2gunRight;

    public Text ammoText;
    public Text healthText;

    public Vector3 bulletOffsetMid;
    public Vector3 bulletOffsetLeft;
    public Vector3 bulletOffsetRight;

    public Animator animator;

    public GameObject playerCanvas;

    Quaternion _bulletRotation;
    Quaternion _bulletRotationRight;


    public int ammo = 6;
    public int health = 6;
    int currentBulletIdx;

    public bool isReloading = false;

    private bool revPaused;
    private bool shotPaused;

    public bool revolverActive = true;
    public bool shotgunActive = false;
    public bool runShotgun = false;
    public bool dontDo = false;
    public bool hasPickUp = false;
    public bool rapidFireActive = false;

    public List<GameObject> _bullets;

    // Use this for initialization
    void Start () {

        if (this.gameObject.tag == "P1") {

            bulletOffsetMid = p1gunMid.position;
            bulletOffsetLeft = p1gunLeft.position;
            bulletOffsetRight = p1gunRight.position;

            _bullets = new List<GameObject>();

            playerCanvas = GameObject.Find("CanvasP1");

        } else if (this.gameObject.tag == "P2") {

            bulletOffsetMid = p2gunMid.position;
            bulletOffsetLeft = p2gunLeft.position;
            bulletOffsetRight = p2gunRight.position;

            _bullets = new List<GameObject>();

            playerCanvas = GameObject.Find("CanvasP2");

        }

        ammoText = GameObject.Find(this.gameObject.tag + "ammo").GetComponent<Text>();
        healthText = GameObject.Find(this.gameObject.tag + "life").GetComponent<Text>();
        gameOverPanel = GameObject.Find("Canvas").transform.Find("GameOverPanel").gameObject;

        animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        playerCanvas.GetComponent<RectTransform>().position = new Vector3(this.transform.position.x + 2, this.transform.position.y + 3, this.transform.position.z);


        _bulletRotation = this.transform.rotation;
        _bulletRotationRight = this.transform.rotation;

        ammoText.text = ammo.ToString();
        healthText.text = health.ToString();

        Movement();

        Rotation();

        Shoot();

        BulletTrailColour();

        if (this.gameObject.tag == "P1")
        {

            bulletOffsetMid = p1gunMid.position;
            bulletOffsetLeft = p1gunLeft.position;
            bulletOffsetRight = p1gunRight.position;

        }
        else if (this.gameObject.tag == "P2")
        {

            bulletOffsetMid = p2gunMid.position;
            bulletOffsetLeft = p2gunLeft.position;
            bulletOffsetRight = p2gunRight.position;

        }

        if (runShotgun == true) {

            ShotgunShoot();

        }

        if (ammo == 0)
        {

            StartCoroutine(ReloadTimer());

        }

    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.collider.tag == "Bullet") {

            Destroy(collision.gameObject);

            if (this._bullets.Contains(collision.collider.gameObject) == false) {

                health -= 1;

                playerCanvas.transform.Find("Image").GetComponent<Image>().enabled = true;
                StartCoroutine(OofWait());
                
                Death();

            }
            
        }

        if (collision.collider.tag == "DATRAINKILLEDMEH") {

            health -= 3;
            Death();

        }

    }

    void Movement() {

        float moveHD = Input.GetAxis("Horizontal" + this.gameObject.tag + "D") * Time.deltaTime;
        float moveVD = Input.GetAxis("Vertical" + this.gameObject.tag + "D") * Time.deltaTime;

        float moveH = Input.GetAxis("Horizontal" + this.gameObject.tag) * Time.deltaTime;
        float moveV = Input.GetAxis("Vertical" + this.gameObject.tag) * Time.deltaTime;

        this.transform.Translate(Vector3.forward * (moveHD * moveSpeed), Space.World);
        this.transform.Translate(Vector3.right * (moveVD * moveSpeed), Space.World);

        this.transform.Translate(Vector3.forward * (moveH * moveSpeed), Space.World);
        this.transform.Translate(Vector3.right * (moveV * moveSpeed), Space.World);

        animator.SetFloat("moving", moveH);
        animator.SetFloat("movingH", moveV);

        //if (moveH != 0 && moveV != 0) {

        //    animator.SetBool("Moving", true);

        //} else {

        //    animator.SetBool("Moving", false);

        //}

        if (moveV == 0 && moveH == 0)
        {

            animator.SetBool("idleing", true);

        }
        else {

            animator.SetBool("idleing", false);

        }

    }

    void Rotation() {

        float moveH2 = Input.GetAxis("Horizontal" + this.gameObject.tag + "-2") * Time.deltaTime;
        //float moveV2 = Input.GetAxis("Vertical" + this.gameObject.tag + "-2") * Time.deltaTime;

        this.transform.Rotate(0, moveH2*rotSpeed, 0);

    }

    void Shoot() {

        if (Input.GetButtonDown("Shoot" + this.gameObject.tag) && ammo > 0 && isReloading == false && revPaused == false && revolverActive == true) {

            animator.SetTrigger("Shoot");

            ammo--;

            this._bullets.Add(Instantiate(Bullet, bulletOffsetMid, _bulletRotation));

            StartCoroutine(FireRate(fireRateMultiplyer));

        }

    }

    IEnumerator FireRate(float rate)
    {
        if(isReloading == false) {

            revPaused = true;
            shotPaused = true;
            yield return new WaitForSecondsRealtime(rate);
            shotPaused = false;
            revPaused = false;

        }

    }

    IEnumerator ReloadTimer() {

        isReloading = true;

        

        this.shotgunActive = false;
        this.runShotgun = false;
        this.revolverActive = true;
        this.dontDo = false;
        this.rapidFireActive = false;
        this.hasPickUp = false;

        while (ammo < 6) {

            ammo++;

            animator.SetTrigger("Reload");

            yield return new WaitForSecondsRealtime(0.5f);

        }

        fireRateMultiplyer = 0.5f;
        _bullets.Clear();

        isReloading = false;

    }

    IEnumerator WaitShotgun() {


        yield return new WaitForSeconds(0.1f);


    }

    IEnumerator OofWait() {

        yield return new WaitForSecondsRealtime(0.2f);
        playerCanvas.transform.Find("Image").GetComponent<Image>().enabled = false;

    }

    void Death() {

        if (health <= 0) {

            Time.timeScale = 0.0f;
            gameOverPanel.SetActive(true);

        }

    }

    public void RapidBuff() {

        rapidFireActive = true;

        ammo = 6;

        fireRateMultiplyer -= rapidFireModifier;

    }

    public void ShotgunShoot() {



        if (ammo < 6 && dontDo == false) {

            ammo = 6;
            dontDo = true;

        }

        if (Input.GetButtonDown("Shoot" + this.gameObject.tag) && ammo > 0 && isReloading == false && shotPaused == false && shotgunActive == true) {

            animator.SetTrigger("Shoot");

            ammo -= 3;

            this._bullets.Add(Instantiate(Bullet, bulletOffsetMid, _bulletRotation));
            this._bullets.Add(Instantiate(Bullet, bulletOffsetLeft, _bulletRotation *= Quaternion.Euler(0, -10, 0)));
            this._bullets.Add(Instantiate(Bullet, bulletOffsetRight, _bulletRotationRight *= Quaternion.Euler(0, 10, 0)));

            StartCoroutine(FireRate(fireRateMultiplyer));

        }

    }

    void BulletTrailColour() {

        if (this.gameObject.tag == "P1") {

            foreach (GameObject bullet in this._bullets) {

                Bullet.GetComponent<TrailRenderer>().material = Resources.Load("Materials/P2Red") as Material;

            }

        }
        else if (this.gameObject.tag == "P2") {

            foreach (GameObject bullet in this._bullets) {

                Bullet.GetComponent<TrailRenderer>().material = Resources.Load("Materials/P1Blue") as Material;

            }

        }

    }
    
}
