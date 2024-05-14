using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject quitGame;
   public void QuitGame()
    {
        quitGame.gameObject.SetActive(true);
    }
    public void Cancel()
    {
        quitGame.gameObject.SetActive(false);
    }
    public void Confirm()
    {
        Application.Quit();
    }
    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }
    public void ReturnSence()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GameScreen()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
