using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaveSpawner;
using UnityEngine.UI;

public class WaveSpawnerController : MonoBehaviour
{

    public Wave[] waves;

    private static int aliveEnemies;

    public Transform spawnPoint;
    
    private static int currentWaveIndex;

    public Text countdownText;

    private static string counddownTextStr = "До следующей волны: ";
    
    [Header("Spawn Attributes")]
    public float countdown;

    //time between waves
    public float spawnCooldown;

    //Time between spawning enemy in one wave
    public float internalCountdown;

    public Text currentWaveNumberText;

    private static string waveStr = "Wave: ";

    private static WaveSpawnerController instance;

    public static WaveSpawnerController getInstance()
    {
        return instance;
    }

    public void DecreaseAliveEnemiesCount()
    {
        aliveEnemies--;
    }

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    
    void Start()
    {
        currentWaveIndex = 0;
        UpdateWaveNumberOnUI();
    }

    void Update()
    {
        if (!GameController.IsGameEnded)
        {
            if (aliveEnemies > 0)
            {
                if (IsCountdownUIShown())
                {
                    HideCountdownUI();
                }
                return;
            }
            else
            {
                if (!IsCountdownUIShown())
                {
                    ShowCountdownUI();
                }
            }

            if (currentWaveIndex == waves.Length)
            {
                HideCountdownUI();
                GameController.getInstance().WinLevel();
            }
            
            if (countdown <= 0.0f)
            {
                StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                countdown = spawnCooldown;
              
                UpdateWaveNumberOnUI();
            }

            countdown -= Time.deltaTime;

            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            countdownText.text = counddownTextStr + string.Format("{0:00.00}", countdown);
            
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        aliveEnemies = wave.enemiesCount;
        for (int i = 0; i < wave.enemiesCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(internalCountdown);
        }

        currentWaveIndex++;
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void UpdateWaveNumberOnUI()
    {
        int waveIndexForUi = currentWaveIndex + 1;
        currentWaveNumberText.text = waveStr + waveIndexForUi + "/" + waves.Length;
    }

    bool IsCountdownUIShown()
    {
        return countdownText.gameObject.activeSelf;
    }
    
    void HideCountdownUI()
    {
        countdownText.gameObject.SetActive(false);
    }

    void ShowCountdownUI()
    {
        countdownText.gameObject.SetActive(true);
    }
    
    public void GetWaveReward()
    {
        PlayerController.getInstance().RecieveMoney(waves[currentWaveIndex-1].waveReward);
    }

    public int GetAliveEnemies()
    {
        return aliveEnemies;
    }
    
}
