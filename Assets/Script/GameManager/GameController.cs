using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public List<PlantUnit> plantUnit = new List<PlantUnit>();
    public List<Zombie> zombies = new List<Zombie>();
    public List<Transform> spawnZombies = new List<Transform>();

    public SunElement sunPrefab;
    public Bullet bulletPrefab;
    public SnowBullet snowBulletPrefab;
    public SquareElement elementGrid;
    public Transform startMapGenPositon;
    public PlantCardManager plantCards;

    public LevelSO levelSO;
    public TMP_Text sunText;

    public PlantType idSpawn;

    public int startSun;
    public int sunAmount = 0;
    public float spacingX = 1.5f;
    public float spacingY = 1.5f;
    private int totalZombiesToSpawn;
    private int zombiesSpawned;
    private bool speedUp;

    public GameObject loseScreen;
    public GameObject winScreen; 
    public GameObject pauseGame;
    
    public Slider volumeSlider;
    public Slider progressBar;

    [SerializeField] private AudioSource themeSound;
    [SerializeField] private AudioSource loseSound;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource zomSound;

    public Button pauseBtn;
    public Button resumeBtn;
    public Button quitBtn;
    public Button winBtn;
    public Button loseBtn;
    public Button speedBtn;

    void Start()
    {
        AddSun(startSun);
        instance = this;
        StartCoroutine(SpawnZombie());
        GenMap();
        StartCoroutine(SpawnSun());
        themeSound.Play();
        Invoke("PlayZomSound", 2.5f);
        pauseBtn.onClick.AddListener(Pause);
        speedBtn.onClick.AddListener(SpeedGame);
    }
    public void SpeedGame()
    {
        if(speedUp==false)
        {
            Time.timeScale = 1f;
            speedUp = true;
        }
        else
        {
            Time.timeScale = 3f;
            speedUp = false;
        }
    }
    public void Pause()
    {
        StopSound();
        zomSound.Stop();
        pauseGame.gameObject.SetActive(true);
        Time.timeScale = 0f;
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        volumeSlider.value = AudioListener.volume;
        resumeBtn.onClick.AddListener(Resume);
        quitBtn.onClick.AddListener(QuitLV);
    }
    public void Resume()
    {
        pauseGame.gameObject.SetActive(false);
        Time.timeScale = 1f;
        themeSound.Play();
    }
    void PlayZomSound()
    {
        zomSound.Play();
    }
    void StopSound()
    {
        themeSound.Stop();
    }
    public void QuitLV()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public void GameWin()
    {

        int nextLevel = GameData.LEVEL_CHOOSING + 1;
        int highestLvl = PlayerPrefs.GetInt(GameData.KEY_LEVELHIGHEST, 0);
            if(nextLevel > highestLvl)
        {
            PlayerPrefs.SetInt(GameData.KEY_LEVELHIGHEST, nextLevel);
        }
        GameData.openLV = true;
        SceneManager.LoadScene("MainMenu");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ShowEndScreen(loseScreen, loseSound);
            loseBtn.onClick.AddListener(RestartLV);
        }
    }
    public void RestartLV()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void AddSun(int amt)
    {
        sunAmount += amt;
        UpdateSunText();    
    }
    public void DeductSun(int amt)
    {
        if(sunAmount>=amt)
        {
            sunAmount -= amt;
            UpdateSunText();
        }
        else
        {
            return;
        }
    }
    void UpdateSunText()
    {
        sunText.text = "" + sunAmount;
    }
    public void GenMap ()
    {
        Vector2 spawnLocation = startMapGenPositon.position;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
              SquareElement newSquare = Instantiate(elementGrid, spawnLocation, Quaternion.identity);
                newSquare.SetId(i,j);
                spawnLocation.x += spacingX;
            }
            spawnLocation.y -= spacingY;
            spawnLocation.x = startMapGenPositon.position.x;
        }
    }
    public IEnumerator SpawnZombie ()
    {
        int lvl = GameData.LEVEL_CHOOSING;
        LevelData currentLevelData = levelSO.zombieQuantities[lvl];
        totalZombiesToSpawn = levelSO.zombieQuantities[lvl].GetTotalZombie();
        foreach (var zombieQuantity in currentLevelData.zombies)
        {
            ZombieType zombieType = zombieQuantity.type;
            int quantity = zombieQuantity.quantity;

            for (int i = 0; i < quantity; i++)
            {
                int randomRow = Random.Range(0, 5);
                yield return new WaitForSeconds(Random.Range(5, 13));
                Instantiate(GetZombie(zombieType), spawnZombies[randomRow].position, Quaternion.identity);
                zombiesSpawned++;
                UpdateProgressBar();
               
            }
        }

        if (zombiesSpawned >= totalZombiesToSpawn)
        {
            yield return new WaitUntil(() => GameObject.FindWithTag("Enemy") == null);
            ShowEndScreen(winScreen, winSound);
            winBtn.onClick.AddListener(GameWin);
        }

    }
   
    void UpdateProgressBar()
    {
        float progress = (float)zombiesSpawned / totalZombiesToSpawn;
        progressBar.value = progress;
    }

    public IEnumerator SpawnSun ()
    {
        while(true)
        {
            int randomTime = Random.Range(3, 10);
            yield return new WaitForSeconds(randomTime);
            float randomX = Random.Range(-4.7f, 6.3f);
            SunElement newSun =  Instantiate(sunPrefab, new Vector2(randomX,6), Quaternion.identity);
            newSun.isFalling = true;
        }
    }
    public Zombie GetZombie(ZombieType id)
    {
        for (int i = 0; i < zombies.Count; i++)
        {
            if (zombies[i].zombieId == id )
            {
                return zombies[i];
            }
        }
        return null;
    }
    public void ChangeIdSpawn(PlantType id)
    {
        idSpawn = id;
    }
    
    public PlantUnit GetUnit(PlantType type)
    {
        for (int i = 0; i < plantUnit.Count; i++)
        {
            if (plantUnit[i].plantType == type)
            {
                return plantUnit[i];
            }
        }
        return null;
    }
    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    private void ShowEndScreen(GameObject endScreen, AudioSource endSound)
    {
        StopAllCoroutines();
        StopSound();
        endScreen.SetActive(true);
        endSound.Play();
    }
}