using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public Text childrenCounter;
    public Text GoToFireplaceText;
    public Text snatchThemAllText;
    public Text BringHimBackText;
    public Text GoodJobContinueText;
    public Text BringAtLeastOneToExitText;

    float elapsed = 0;

    SantaController santaController;

    // Use this for initialization
    void Start () {
        santaController = GameObject.FindGameObjectWithTag("Player").GetComponent<SantaController>();
    }
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;

        UpdateChildrenKidnapedText();

        StartCoroutine(HideSnatchThemAllText());
    }

    IEnumerator HideSnatchThemAllText()
    {
        yield return new WaitForSeconds(3);
        snatchThemAllText.enabled = false;
    }

    void UpdateChildrenKidnapedText()
    {
        childrenCounter.text = santaController.numberOfChildrenKidnaped + " / " + santaController.numberOfChildrenBeds;
    }

    public void OnSantaSnatched()
    {
        BringHimBackText.enabled = true;
        StartCoroutine(HideBringHimBackText());
    }

    IEnumerator HideBringHimBackText()
    {
        yield return new WaitForSeconds(3);
        BringHimBackText.enabled = false;
    }

    public void OnSantaReleased()
    {
        if(santaController.numberOfChildrenKidnaped == santaController.numberOfChildrenBeds)
        {
            // SI DERNIER
            GoToFireplaceText.enabled = true;
        } else
        {
            // S'IL EN RESTE
            GoodJobContinueText.enabled = true;
            StartCoroutine(HideGoodJobContinueText());
        }
    }

    IEnumerator HideGoodJobContinueText()
    {
        yield return new WaitForSeconds(2);
        GoodJobContinueText.enabled = false;
    }

    public void OnSantaTriesToExitWithoutChild()
    {
        BringAtLeastOneToExitText.enabled = true;
        StartCoroutine(HideBringAtLeastOneToExitText());
    }

    IEnumerator HideBringAtLeastOneToExitText()
    {
        yield return new WaitForSeconds(2);
        BringAtLeastOneToExitText.enabled = false;
    }
}
