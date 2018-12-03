using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public Text childrenCounter;
    public Text announcementText;
    public Text useText;
  
    float elapsed = 0;
    SantaController santaController;
    IEnumerator hideTextCoroutine;

    string SnatchThemAll = "QUICK!\nSNATCH'EM ALL WITHOUT GETTING CAUGHT!";
    string BringThemAll = "NOOOOO!\nBRING ME ALL THE CHILDREN!";
    string BringHimBack = "SNEAKY BASTARD!\nBRING HIM BACK TO A FIREPLACE!";
    string GoodJobContinue = "DELICIOUS!\nCONTINUE, BRING ME MORE!";
    string GoToFireplace = "MISSION ACCOMPLISHED!\nEXIT AT THE NEAREST FIREPLACE!";

    string useString = "X to use";

    // Use this for initialization
    void Start () {
        santaController = GameObject.FindGameObjectWithTag("Player").GetComponent<SantaController>();

        hideTextCoroutine = HideBigText();
        StartCoroutine(hideTextCoroutine);
    }
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;

        UpdateChildrenKidnapedText();
    }

    void UpdateChildrenKidnapedText()
    {
        childrenCounter.text = santaController.numberOfChildrenKidnaped + " / " + santaController.numberOfChildrenBeds;
    }

    IEnumerator HideBigText()
    {
        yield return new WaitForSeconds(3);
        announcementText.enabled = false;
    }

    public void OnSantaSnatched()
    {
        StopCoroutine(hideTextCoroutine);
        announcementText.text = BringHimBack;
        announcementText.enabled = true;
        hideTextCoroutine = HideBigText();
        StartCoroutine(hideTextCoroutine);
    }

    public void OnSantaTriesToExitWithoutChild()
    {
        StopCoroutine(hideTextCoroutine);
        announcementText.text = BringThemAll;
        announcementText.enabled = true;
        hideTextCoroutine = HideBigText();
        StartCoroutine(hideTextCoroutine);
    }

    public void OnSantaReleased()
    {
        if(santaController.numberOfChildrenKidnaped == santaController.numberOfChildrenBeds)
        {
            // SI DERNIER
            StopCoroutine(hideTextCoroutine);
            announcementText.text = GoToFireplace;
            announcementText.enabled = true;
            hideTextCoroutine = HideBigText();
            //StartCoroutine(hideTextCoroutine);
        } else
        {
            // S'IL EN RESTE
            StopCoroutine(hideTextCoroutine);
            announcementText.text = GoodJobContinue;
            announcementText.enabled = true;
            hideTextCoroutine = HideBigText();
            StartCoroutine(hideTextCoroutine);
        }
    }

    public void DisplayUseText(bool display)
    {
        useText.enabled = display;
    }
}
