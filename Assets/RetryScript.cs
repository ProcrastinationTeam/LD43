using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryScript : MonoBehaviour {

    public Text countdownText;
    int countdownCounter = 10;
    float count = 0;
    bool stopCountdown = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!stopCountdown)
        {
            count += Time.deltaTime;
            if (count > 1)
            {
                count = 0;
                countdownCounter--;
                if (countdownCounter == 0)
                {
                    SceneManager.LoadScene(2);
                }
                countdownText.text = countdownCounter.ToString();
            }
        }
    }

    public void ChooseNO()
    {
        SceneManager.LoadScene(0);

        //Application.Quit()
    }

    public void ChooseYES()
    {
        stopCountdown = true;
        SceneManager.LoadScene(2);

    }
}
