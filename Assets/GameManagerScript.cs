using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public PlayerManagerScript playerManager;
    bool isRunning = false;


	// Use this for initialization
	void Start () {
        isRunning = true;

    }
	
	// Update is called once per frame
	void Update () {
		if(playerManager.currentFuel <= 0)
        {
            Debug.Log("GAME OVER");
            isRunning = false;
            StartCoroutine(LoadEndScreen());

        }

        if(isRunning)
        {

        }
	}

    IEnumerator LoadEndScreen()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("RetryScene");
    }



}
