using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartMenuController : MonoBehaviour
{
    // Start Menu specific
    public void OnStartClick()
    {
        SceneManager.LoadScene("DeckBuilder");
    }

    public void OnTutorialClick()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnCreditsClick()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    // Tutorial Menu Specific
    public TextMeshProUGUI phase_Text;
    public Sprite[] refImage;
    public Image targetImage;

    public void OnNextClick()
    {
        phase_Text.text = "Phase 2: In-Level " +
            "\n- On entering the level, player moves using the AWSD key to go left, up, down, and right." +
            "\n- You are able to attack with any of the keys (U, I, O, P, J)." +
            "\n- Each button will use one of the cards matching the same position of the card with that of the button." +
            "\n- Ex: Pressing 'U' will use the 1st card (far left).";
        
        targetImage.sprite = refImage[1];
    }

    public void OnPreviousClick()
    {
        phase_Text.text = "Phase 1: Deck Builder" +
            "\n- A total of 20 cards will appear, of them use the mouse to click and select 20." +
            "\n- Each card has a unique effect (in-development)." +
            "\n- Once all 15 are selected the level can be started." +
            "\n- Each card is used alongside an attack.";

        targetImage.sprite = refImage[0];
    }

    public void OnMenuClick()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
