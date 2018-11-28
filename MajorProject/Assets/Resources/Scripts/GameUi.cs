using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUi : MonoBehaviour {

    public PlayerController playerOne;
    public PlayerController playerTwo;

    public Text roundEndText;
    public Text countDown;
    int countNum = 3;

    public GameObject gameOverPanel;

    void Awake() {

        playerOne = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerTwo = GameObject.Find("Player2").GetComponent<PlayerController>();
        roundEndText = GameObject.Find("GameOverText").GetComponent<Text>();
        countDown = GameObject.Find("CountDownText").GetComponent<Text>();

        gameOverPanel = GameObject.Find("GameOverPanel");

    }

    // Use this for initialization
    void Start () {

        gameOverPanel.SetActive(false);
        Time.timeScale = 0.0f;

        countDown.text = "" + countNum;

        StartCoroutine(CountDown());

	}
	
	// Update is called once per frame
	void Update () {

        if (playerOne.health == 0) {

            roundEndText.text = "Player 2 Won";

        }

        if (playerTwo.health == 0) {

            roundEndText.text = "Player 1 Won";

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

        yield return new WaitForSecondsRealtime(1);
        countNum -= 1;
        countDown.text = "" + countNum;

    }


}
