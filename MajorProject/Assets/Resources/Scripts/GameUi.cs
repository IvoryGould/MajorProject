using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUi : MonoBehaviour {

    public PlayerController playerOne;
    public PlayerController playerTwo;

    public Image roundEndText;
    public Sprite blueWins;
    public Sprite redWins;
    public Text countDown;
    int countNum = 3;

    public GameObject gameOverPanel;

    void Awake() {

        playerOne = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerTwo = GameObject.Find("Player2").GetComponent<PlayerController>();
        roundEndText = GameObject.Find("GameOverPanel").GetComponent<Image>();
        countDown = GameObject.Find("CountDownText").GetComponent<Text>();

        gameOverPanel = GameObject.Find("GameOverPanel");

    }

    // Use this for initialization
    void Start () {

        gameOverPanel.SetActive(false);

        countDown.text = "" + countNum;

        StartCoroutine(CountDown());

	}
	
	// Update is called once per frame
	void Update () {

        if (playerOne.health == 0) {

            roundEndText.sprite = redWins;
            Time.timeScale = 0.0f;


        }

        if (playerTwo.health == 0) {

            roundEndText.sprite = blueWins;
            Time.timeScale = 0.0f;


        }

    }

    public void PlayAgain() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;

    }

    public void ReturnMenu() {

        SceneManager.LoadScene("MainMenu");

    }

    IEnumerator CountDown() {

        Time.timeScale = 0;

        while (countNum != 0) {

            yield return new WaitForSecondsRealtime(1);
            countNum -= 1;
            countDown.text = "" + countNum;

        }

        Time.timeScale = 1.0f;
        countDown.enabled = false;

    }


}
