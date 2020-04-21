using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
    public GameObject[] EscMenu;

    public void ButtonPause()
    {
        EscMenu[0].SetActive(true);
        Time.timeScale = 0f;
        EscMenu[1].SetActive(false);
    }
    public void ButtonEndMenu()
    {
        EscMenu[0].SetActive(false);
        Time.timeScale = 1f;
        EscMenu[1].SetActive(true);
    }
    public void ButtonStart()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    } 
    public void ButtonQuit()
    {
        Application.Quit();
        Debug.Log("quitting game");
        Time.timeScale = 1f;
    }    
    public void ButtonSave()
    {
        Debug.Log("SAVE"); 
    }
}

