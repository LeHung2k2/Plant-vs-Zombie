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
    void Start()
    {
        levelPanel.gameObject.SetActive(false);
        instance = this;
        playBtn.onClick.AddListener(OpenLevel);
        resetBtn.onClick.AddListener(ResetScreen);
        if (GameData.openLV==true)
        {
            OpenLevel();
            GameData.openLV = false;
        }
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
