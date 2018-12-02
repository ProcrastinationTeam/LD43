using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public enum GameMod
    {
        stealthMode,
    }

    public PlayerManagerScript playerManager;
    SantaController santaController;
    Transform playerTransform;
    bool isRunning = false;
    bool stealthMode = false;
    GameMod currentGameMod;



	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        currentGameMod = GameMod.stealthMode;
        isRunning = true;
        SceneManager.sceneLoaded += OnSceneLo;
    }

    void OnSceneLo(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);

        var lvl = GameObject.Find("Grid");
        if (lvl != null)
        {
            playerTransform = GameObject.Find("Player").GetComponent<Transform>();
            //santaController = GameObject.Find("Player").GetComponent<SantaController>();

            var fireplaces = GameObject.Find("Fireplaces");

            var fireplacesTransform = fireplaces.GetComponentsInChildren<Transform>();

            playerTransform.position = new Vector3(fireplacesTransform[1].position.x, fireplacesTransform[1].position.y, fireplacesTransform[1].position.z);

         

        }
    }

    void Awake()
    {
     
    }
	
	// Update is called once per frame
	void Update () {
		//if(playerManager.currentFuel <= 0)
  //      {
  //          Debug.Log("GAME OVER");
  //          isRunning = false;
  //          StartCoroutine(LoadEndScreen());

  //      }

        if(isRunning)
        {
            switch (currentGameMod)
            {
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

    public void InitPlayerPos()
    {

    }
    

}
