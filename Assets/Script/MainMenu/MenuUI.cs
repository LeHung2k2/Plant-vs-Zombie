using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public static MenuUI instance;

    public RectTransform levelPanel;
    public RectTransform resetScreen;
    public Button playBtn;
    public Button resetBtn;
    public Button confirmBtn;
    public Button refuseBtn;
    public Button exitBtn;

    private AudioSource audioSource;
    public AudioClip buttonClickSound;
    void Start()
    {

        audioSource = gameObject.AddComponent<AudioSource>();
        levelPanel.gameObject.SetActive(false);
        instance = this;
        playBtn.onClick.AddListener(() =>
        {
            PlayButtonClickSound();
            OpenLevel();
        });
        resetBtn.onClick.AddListener(() =>
        {
            PlayButtonClickSound();
            ResetScreen();
        });
        exitBtn.onClick.AddListener(QuitGame);

        confirmBtn.onClick.AddListener(() =>
        {
            PlayButtonClickSound();
            ResetGame();
        });
        refuseBtn.onClick.AddListener(() =>
        {
            PlayButtonClickSound();
            ResumeGame();
        });
        if (GameData.openLV == true)
        {
            OpenLevel();
            GameData.openLV = false;
        }
    }
    private void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
    private void QuitGame()
    {
        Application.Quit();
    }
    private void OpenLevel()
    {
        levelPanel.gameObject.SetActive(true);
    }
    private void ResetScreen()
    {
        resetScreen.gameObject.SetActive(true);
        confirmBtn.onClick.AddListener(ResetGame);
        refuseBtn.onClick.AddListener(ResumeGame);
    }
    private void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        GameData.LEVEL_CHOOSING = 0;
        resetScreen.gameObject.SetActive(false);
    }
    private void ResumeGame()
    {
        resetScreen.gameObject.SetActive(false);
    }
}
