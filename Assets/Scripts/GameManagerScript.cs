using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public enum GameMod
    {
        shmupMode,
        stealthMode,
    }


    public PlayerManagerScript playerManager;
    bool isRunning = false;
    bool shmupMode = false;
    bool stealthMode = false;
    GameMod currentGameMod;



	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        currentGameMod = GameMod.shmupMode;
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
            switch (currentGameMod)
            {
                case GameMod.shmupMode:

                    break;
                case GameMod.stealthMode:
                    break;
                default:
                    break;
            }
        }
	}

    public void SetCurrentMode(GameMod gm)
    {
        currentGameMod = gm;
    }

    IEnumerator LoadEndScreen()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("RetryScene");
    }

    

}
