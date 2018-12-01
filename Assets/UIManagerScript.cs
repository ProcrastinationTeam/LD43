using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour {

    public Image starsLine;
    public List<Sprite> starsSprites;

    public Text pointCounter;

    public Slider fuelSlider;
    public Slider giftSlider;

    public PlayerManagerScript playerManagerScript;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //if(playerManagerScript.currentGiftCount != giftSlider.value)
        //{
        //    giftSlider.value = playerManagerScript.currentGiftCount;
        //}

        //if (playerManagerScript.currentFuel != fuelSlider.value)
        //{
        //    fuelSlider.value = playerManagerScript.currentFuel;
        //}

        UpdateGiftSlider(playerManagerScript.currentGiftCount);
        UpdateFuelSlider(playerManagerScript.currentFuel);
        UpdateScore(playerManagerScript.score);
        UpdateWantedIndicator(playerManagerScript.wantedIndicator);

    }

    public void UpdateWantedIndicator(int value)
    {
        starsLine.sprite = starsSprites[value];
    }

    public void UpdateScore(int value)
    {
        pointCounter.text = value.ToString();
    }

    public void UpdateFuelSlider(float value)
    {
        fuelSlider.value = value;
    }

    public void UpdateGiftSlider(float value)
    {
        giftSlider.value = value;
    }

    


}
