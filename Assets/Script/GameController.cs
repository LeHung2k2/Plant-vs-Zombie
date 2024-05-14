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
    public TMP_Text sunText;
    public int startSun;
    public int sunAmount = 0;
    public static GameController instance;
    public SunElement sunPrefab;
    public List<PlantUnit> plantUnit = new List<PlantUnit>();
    public List<Zombie> zombies = new List<Zombie>();
    public Bullet bulletPrefab;
    public SnowBullet snowBulletPrefab;
    public SquareElement elementGrid;
    public Transform startMapGenPositon;
    public List<Transform> spawnZombies = new List<Transform>();
    public LevelSO levelSO;
    public float spacingX = 1.5f;
    public float spacingY = 1.5f;
    public PlantType idSpawn;
    public Slider progressBar;
    private int totalZombiesToSpawn;
    private int zombiesSpawned;
    public PlantCardManager plantCards;
    public GameObject loseScreen;
    public GameObject winScreen;
    private int currentLevelIndex;

    [SerializeField] private AudioSource themeSound;
    [SerializeField] private AudioSource loseSound;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource zomSound;
  
    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt(currentLevelIndex.ToString(), 0);
        Debug.Log("Current Level  = " + currentLevelIndex);
    }

    void Start()
    {
        AddSun(startSun);
        instance = this;
        StartCoroutine(SpawnZombie());
        GenMap();
        StartCoroutine(SpawnSun());
        themeSound.Play();
        Invoke("PlayZomSound", 2.5f);
    }

    void PlayZomSound()
    {
        zomSound.Play();
    }
    void StopSound()
    {
        themeSound.Stop();
    }
    
    public void GameWin()
    {
        PlayerPrefs.SetInt(currentLevelIndex.ToString(),currentLevelIndex+1);
        SceneManager.LoadScene("Gameplay");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            loseScreen.gameObject.SetActive(true);
            StopSound();
            StopAllCoroutines();
            loseSound.Play(); ;
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
        
        int lvl = currentLevelIndex;
        Debug.Log("lvl " + lvl);
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

            winScreen.gameObject.SetActive(true);
            StopSound();
            StopAllCoroutines();
            winSound.Play();;
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
}

