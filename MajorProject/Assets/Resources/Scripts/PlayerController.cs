using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5;
    public float rotSpeed = 10;
    public float fireRateMultiplyer = 0.5f;
    public float rapidFireModifier = 0.25f;

    public AudioSource audioSource;
    public AudioClip revShot;
    public AudioClip shotGun;
    public AudioClip reload;

    public GameObject Curser;
    public GameObject Bullet;
    public GameObject gameOverPanel;
    public GameObject musselFlash;

    public Transform p1gunMid;
    public Transform p1gunLeft;
    public Transform p1gunRight;

    public Transform p2gunMid;
    public Transform p2gunLeft;
    public Transform p2gunRight;

    public Vector3 bulletOffsetMid;
    public Vector3 bulletOffsetLeft;
    public Vector3 bulletOffsetRight;

    public Animator animator;

    public List<Image> l_Health;
    public int _healthIdx;
    public List<Image> l_Ammo;
    public int _ammoIdx;

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

    bool dash = true;

    public List<GameObject> _bullets;

    // Use this for initialization
    void Start () {

        if (this.gameObject.tag == "P1") {

            bulletOffsetMid = p1gunMid.position;
            bulletOffsetLeft = p1gunLeft.position;
            bulletOffsetRight = p1gunRight.position;

            audioSource = this.gameObject.GetComponent<AudioSource>();

            _bullets = new List<GameObject>();
            l_Health = new List<Image>();
            l_Ammo = new List<Image>();

            musselFlash = GameObject.Find("P1MusselFlash");
            this.musselFlash.SetActive(false);

            for (int i = 0; i < 6; i++) {

                l_Health.Add(GameObject.Find("P1Health" + i).GetComponent<Image>());
                l_Ammo.Add(GameObject.Find("P1Ammo" + i).GetComponent<Image>());

            }

            this._healthIdx = l_Health.Count;
            this._ammoIdx = l_Ammo.Count;

            playerCanvas = GameObject.Find("CanvasP1");

        } else if (this.gameObject.tag == "P2") {

            bulletOffsetMid = p2gunMid.position;
            bulletOffsetLeft = p2gunLeft.position;
            bulletOffsetRight = p2gunRight.position;

            audioSource = this.gameObject.GetComponent<AudioSource>();

            _bullets = new List<GameObject>();
            l_Health = new List<Image>();
            l_Ammo = new List<Image>();

            musselFlash = GameObject.Find("P2MusselFlash");
            this.musselFlash.SetActive(false);

            for (int i = 0; i < 6; i++)
            {

                l_Health.Add(GameObject.Find("P2Health" + i).GetComponent<Image>());
                l_Ammo.Add(GameObject.Find("P2Ammo" + i).GetComponent<Image>());

            }

            this._healthIdx = l_Health.Count;
            this._ammoIdx = l_Ammo.Count;

            playerCanvas = GameObject.Find("CanvasP2");

        }

        gameOverPanel = GameObject.Find("Canvas").transform.Find("GameOverPanel").gameObject;

        animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        playerCanvas.GetComponent<RectTransform>().position = new Vector3(this.transform.position.x + 2, this.transform.position.y + 3, this.transform.position.z);


        _bulletRotation = this.transform.rotation;
        _bulletRotationRight = this.transform.rotation;

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

                this.l_Health[_healthIdx - 1].enabled = false;
                _healthIdx -= 1;

                playerCanvas.transform.Find("Image").GetComponent<Image>().enabled = true;
                StartCoroutine(OofWait());
                
                Death();

            }
            
        }

        if (collision.collider.tag == "DATRAINKILLEDMEH") {

            health -= 3;
            this.l_Health[_healthIdx - 1].enabled = false;
            _healthIdx -= 1;
            this.l_Health[_healthIdx - 1].enabled = false;
            _healthIdx -= 1;
            this.l_Health[_healthIdx - 1].enabled = false;
            _healthIdx -= 1;
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



        //DASH
        if (Input.GetButtonDown("Dash" + this.gameObject.tag) && dash == true) {

            this.transform.Translate(Vector3.forward * 5, Space.Self);

            StartCoroutine(DashCoolDown());

        }

        animator.SetFloat("moving", moveH);
        animator.SetFloat("movingH", moveV);

        if (moveV == 0 && moveH == 0)
        {

            animator.SetBool("idleing", true);

        }
        else {

            animator.SetBool("idleing", false);

        }

    }

    void Rotation() {

        //float moveH2 = Input.GetAxis("Horizontal" + this.gameObject.tag + "-2") * Time.deltaTime;
        //float moveV2 = Input.GetAxis("Vertical" + this.gameObject.tag + "-2") * Time.deltaTime;

        //this.transform.Rotate(0, moveH2*rotSpeed, 0);


        //Directional joystick rotation
        Vector3 input = new Vector3(Input.GetAxis("Vertical" + this.gameObject.tag + "-2"), 0, Input.GetAxis("Horizontal" + this.gameObject.tag + "-2"));

        if (input != Vector3.zero) {

            transform.forward = input;

        }

    }


    void Shoot() {

        if (Input.GetButtonDown("Shoot" + this.gameObject.tag) && ammo > 0 && isReloading == false && revPaused == false && revolverActive == true) {

            animator.SetTrigger("Shoot");

            this.musselFlash.SetActive(true);
            StartCoroutine(MusselWait());

            this.audioSource.PlayOneShot(revShot, 0.02f);

            ammo--;

            this.l_Ammo[_ammoIdx - 1].enabled = false;
            _ammoIdx -= 1;

            this._bullets.Add(Instantiate(Bullet, bulletOffsetMid, _bulletRotation));

            StartCoroutine(FireRate(fireRateMultiplyer));

        }

    }

    IEnumerator MusselWait() {

        yield return new WaitForSecondsRealtime(0.1f);
        this.musselFlash.SetActive(false);

    }

    IEnumerator DashCoolDown() {

        dash = false;
        yield return new WaitForSecondsRealtime(3);
        dash = true;


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

            this.audioSource.PlayOneShot(reload, 1);

            this.l_Ammo[_ammoIdx].enabled = true;
            _ammoIdx += 1;

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
        foreach (Image image in l_Ammo)
        {

            image.enabled = true;

        }
        _ammoIdx = l_Ammo.Count;

        fireRateMultiplyer -= rapidFireModifier;

    }

    public void ShotgunShoot() {



        if (ammo < 6 && dontDo == false) {

            ammo = 6;
            foreach (Image image in l_Ammo) {

                image.enabled = true;

            }
            _ammoIdx = l_Ammo.Count;
            dontDo = true;

        }

        if (Input.GetButtonDown("Shoot" + this.gameObject.tag) && ammo > 0 && isReloading == false && shotPaused == false && shotgunActive == true) {

            animator.SetTrigger("Shoot");

            this.musselFlash.SetActive(true);
            StartCoroutine(MusselWait());

            this.audioSource.PlayOneShot(shotGun, 0.5f);

            ammo -= 3;

            this.l_Ammo[_ammoIdx - 1].enabled = false;
            _ammoIdx -= 1;
            this.l_Ammo[_ammoIdx - 1].enabled = false;
            _ammoIdx -= 1;
            this.l_Ammo[_ammoIdx - 1].enabled = false;
            _ammoIdx -= 1;

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
