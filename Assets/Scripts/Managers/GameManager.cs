using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public WaypointManager _waypointManager;
    private SceneHandler sceneHandler;
    private UIManager _uiManager;
    private SaveHandler saveHandler;
    private PauseManager pauseManager;
    public SettingsManager _settingsManager { get; private set; }
    [SerializeField]private int health;
    public int points;

    [Header("Wave Data")]

    public WaveData[] waves;
    [SerializeField] private WaveData curWave;
    [HideInInspector] public List<Enemy> curEnemies;
    private float spawnDelay;
    private float spawnCooldown;
    private int curEnemiesSpawned;
    private int enemyIndex;
    public bool isPlaying;
    public int waveIndex;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _waypointManager = GetComponent<WaypointManager>();
        _uiManager = GetComponent<UIManager>();
        sceneHandler = GetComponent<SceneHandler>();
        saveHandler = GetComponent<SaveHandler>();
        _settingsManager = GetComponent<SettingsManager>();
    }

    private void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        waveIndex = -1;
    }

    private void Update()
    {
        SpawnEnemies();

        _uiManager.waveButton.SetActive(!isPlaying);
        _uiManager.waveContainer.SetActive(isPlaying);

        if (isPlaying)
        {
            if(curEnemies.Count <= 0)
            {
                if(enemyIndex >= curWave.enemies.Length)
                {
                    isPlaying = false;
                    
                }
            }
        }
        
        if (curEnemies.Count <= 0 && waveIndex >= waves.Length - 1 && !isPlaying)
        {
            GameWin();
        }
        
    }
    private void SpawnEnemies()
    {
        if (spawnCooldown > 0)
        {
            spawnCooldown -= Time.deltaTime;
        }
        if (isPlaying)
        {
            if (spawnCooldown <= 0)
            {
                if (enemyIndex >= curWave.enemies.Length)
                {                    
                    return;
                }
                spawnCooldown = spawnDelay;
                Enemy e = Instantiate(curWave.enemies[enemyIndex], _waypointManager.waypoints[0].transform.position, Quaternion.identity).GetComponent<Enemy>();
                curEnemies.Add(e);
                curEnemiesSpawned++;
                if (curEnemiesSpawned >= curWave.enemyCounts[enemyIndex])
                {
                    enemyIndex++;
                    curEnemiesSpawned = 0;
                }
            }
        }
    }
    public void NewWave(int waveIndex)
    {
        curWave = waves[waveIndex];
        this.waveIndex = waveIndex;
        spawnDelay = curWave.spawnDelay;
        curEnemiesSpawned = 0;
        enemyIndex = 0;
        isPlaying = true;
        curEnemies = new List<Enemy>();
        _uiManager.updateUI();

    }
    public void NewWave(WaveData waveData)
    {
        curWave = waveData;
        for(int i = 0; i < waves.Length; i++) {
            if (waves[i] == curWave) {
                waveIndex = i;
            }
        }
        spawnDelay = curWave.spawnDelay;
        curEnemiesSpawned = 0;
        enemyIndex = 0;
        isPlaying = true;
        curEnemies = new List<Enemy>();
        _uiManager.updateUI();

    }
    public void NewWave()
    {
        waveIndex++;
        curWave = waves[waveIndex];
        spawnDelay = curWave.spawnDelay;
        enemyIndex = 0;
        curEnemiesSpawned = 0;
        isPlaying = true;
        curEnemies = new List<Enemy>();
        _uiManager.updateUI();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        DeathCheck();
        _uiManager.updateUI();
    }

    public void GainHealth(int amount)
    {
        health += amount;
        if(health >= 100)
        {
            health = 100;
        }
        _uiManager.updateUI();
    }

    public void DeathCheck()
    {
        if(health <= 0)
        {
            print("Dead");
            GameOver();
        }
    }

    public void AddPoints(int amount)
    {
        points += amount;
        _uiManager.updateUI();

    }

    public void RemovePoints(int amount) 
    {
        points -= amount;
        _uiManager.updateUI();

    }

    public int getHealth()
    {
        return health;
    }

    private void GameOver()
    {
        sceneHandler.LoadScene("GameOver");
    }

    private void GameWin()
    {
        saveHandler.PopulateSave();
        sceneHandler.LoadScene("WinScreen");
    }

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        print("inPause");

        pauseManager.TogglePause();
    }

}
