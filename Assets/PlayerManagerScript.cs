using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour {

    int maxWantedValue = 10;
    public int wantedIndicator = 0;

    int maxGift = 100;
    public int currentGiftCount = 50;

    public float fuelPerSecond = 2.0f;

    float maxFuel = 100.0f;
    public float currentFuel = 100.0f;

    float timerForFuel = 0.0f;

    public int score = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        ConsumeFuel(2 * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            IncreaseWantedIndicator(1);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            DecreaseWantedIndicator(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void IncreaseWantedIndicator(int value)
    {
        if( wantedIndicator + value > maxWantedValue)
        {
            wantedIndicator = maxWantedValue;
        }
        else
        {
            wantedIndicator += value;
        }

    }

    public void DecreaseWantedIndicator(int value)
    {
        if (wantedIndicator - value < 0)
        {
            wantedIndicator = 0;
        }
        else
        {
            wantedIndicator -= value;
        }
  
    }

    public void IncreaseScore(int value)
    {
        score += value;
    }

    public void Shoot()
    {
        if(currentGiftCount - 1 < 0 )
        {
            currentGiftCount = 0;            
        }
        else
        {
            currentGiftCount -= 1;
        }

    }

    public void ConsumeFuel(float value)
    {
        if (currentFuel - value < 0.0f)
        {
            currentFuel = 0.0f;
        }
        else
        {
            currentFuel -= value;
        }
        
    }

    public void AddGift(int value)
    {
        if(currentGiftCount + value > 100)
        {
            currentGiftCount = 100;
        }
        else
        {
            currentGiftCount += value;
        }
    }

}
