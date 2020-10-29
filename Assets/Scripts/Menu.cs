using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject submenu;
    public GameObject controlsSubmenu;
    public GameObject mainMenu;
    public GameObject creditsSubmenu;

    public void GoToLevel()
    {
        Debug.Log("Se llamo a la funcion");
        SceneManager.LoadScene("Level1");
    }

    public void Controls()
    {
        controlsSubmenu.SetActive(true);
    }

    public void ExitControls()
    {
        controlsSubmenu.SetActive(false);
    }

    public void Credits()
    {
        creditsSubmenu.SetActive(true);
    }

    public void ExitCredits()
    {
        creditsSubmenu.SetActive(false);
    }

    public void ShowSubMenu()
    {
        submenu.SetActive(true);
    }

    public void HideSubMenu()
    {
        submenu.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    public void ExitGame()
    {
        Debug.Log("Sali del juego");
        Application.Quit();
    }
}
