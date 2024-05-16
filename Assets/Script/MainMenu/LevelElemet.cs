
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelElemet : MonoBehaviour
{
    public Button lockBtn;
    public int level;
    public TextMeshProUGUI lvTxt;
    private Button lvBtn;
    private void Start()
    {
        lvBtn = GetComponent<Button>();
        lvTxt.text =( level +1 ).ToString();
        lvBtn.onClick.AddListener(OnGame);
    }
    private void OnGame()
    {
        GameData.LEVEL_CHOOSING = level;
        SceneManager.LoadScene("GamePlay");
    }
    public void UnLock()
    {
        lockBtn.gameObject.SetActive(false);
        
    }
    public void Lock()
    {
        lockBtn.gameObject.SetActive(true);
    }
}
