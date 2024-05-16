using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class MenuManager : MonoBehaviour
{
    public Button exitBtn;
    public List<LevelElemet> lvls = new List<LevelElemet> ();

    private void Start()
    {
        exitBtn.onClick.AddListener(OnExit);
       
    }

    private void OnEnable()
    {
        int highestlv = PlayerPrefs.GetInt(GameData.KEY_LEVELHIGHEST, GameData.LEVEL_CHOOSING);
        for(int i = 0;i< lvls.Count; i++)
        {
            if (i <= highestlv)
            {
                lvls[i].UnLock();
            }
            else lvls[i].Lock();
        }
    }
    private void OnExit()
    {
       this.gameObject.SetActive(false);
    }

    public void GameScreen()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
