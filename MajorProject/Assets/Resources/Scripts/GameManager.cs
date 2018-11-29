using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {



    //List<PlayerController> players = new List<PlayerController>();

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {

        Object.DontDestroyOnLoad(this.gameObject);

        if (Input.GetKeyDown("escape")) {

            Application.Quit();

        }

	}

    public void Play() {

        SceneManager.LoadScene("Level-1");
        Time.timeScale = 1;

    }

    public void Quit() {

        Application.Quit();

    }

}
