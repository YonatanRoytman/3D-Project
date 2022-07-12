using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainManu : MonoBehaviour
{
    public Button toc;
    public Text credits;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void CreditLoad()
    {
        credits.gameObject.SetActive(true);
        toc.gameObject.SetActive(true);
    }
    public void TurnOffCredits()
    {
        credits.gameObject.SetActive(false);
        toc.gameObject.SetActive(false);
    }
}
