using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CreditMenuController : MonoBehaviour
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
    public int i;

    public void OnNextClick()
    {
        // Change image to next one
        targetImage.sprite = refImage[i+1];
        i++;
    }

    public void OnPreviousClick()
    {
        // Change image to previous one
        if (i > 0)
        {
            targetImage.sprite = refImage[i-1];
            i--;
        }
        
    }

    public void OnMenuClick()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
