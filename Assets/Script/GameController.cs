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
    public int sunAmount=0;
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
    public bool isLose=false;
    public GameObject winScreen;
    public bool isWin=false;
    public void GameOver()
    {
        isLose = true;
        loseScreen.gameObject.SetActive(true);
    }
    public void GameWin()
    {
        isWin = true;
        winScreen.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameOver();
        }
    }
 
    public void RestartLV()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Start()
    {
        AddSun(startSun);
        instance = this;
        StartCoroutine(SpawnZombie());
        GenMap();
        StartCoroutine(SpawnSun());
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
              SquareElement newSquare =   Instantiate(elementGrid, spawnLocation, Quaternion.identity);
                newSquare.SetId(i,j);
                spawnLocation.x += spacingX;
            }

            spawnLocation.y -= spacingY;
            spawnLocation.x = startMapGenPositon.position.x;

        }
    }

    public IEnumerator SpawnZombie ()
    {
        int lvl = 0;
        totalZombiesToSpawn = levelSO.zombieQuantities[lvl].GetTotalZombie();

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5,13));
            int randomRow = Random.Range(0, 5);
            Instantiate(GetZombie(ZombieType.Zombie), spawnZombies[randomRow].position, Quaternion.identity);
            zombiesSpawned++;

            UpdateProgressBar();

            if (zombiesSpawned >= totalZombiesToSpawn)
            {
                yield return new WaitUntil(() => GameObject.FindWithTag("Enemy") == null);
                
                    GameWin();
            }
            


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

