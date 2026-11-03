
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleFlow : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameOverUI;
    public GameObject gameWinUI;

    [Header("Player")]
    public PlayerHealth playerHealth;

    [Header("Scene")]
    [SerializeField] public string sceneName;

    [Header("Boss Settings")]
    public bool enableBoss = true;
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    private GameObject spawnedBoss;

    private bool gameEnd = false;
    private bool hasEnemy = false;
    private bool bossSpawned = false;

    private void Start()
    {
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);

        playerHealth.onDead += OnGameOver;
    }

    private void Update()
    {
        if (gameEnd)
            return;

        // Check normal enemies
        if (Enemyhealth.LivingEnemyCount > 0)
        {
            hasEnemy = true;
        }

        // All normal enemies defeated
        if (hasEnemy && Enemyhealth.LivingEnemyCount <= 0)
        {
            if (enableBoss && !bossSpawned)
            {
                SpawnBoss();
            }
            else if (!enableBoss && !bossSpawned)
            {
                gameEnd = true;
                OnGameWin();
            }
        }
    }

    private void SpawnBoss()
    {
        if (bossPrefab == null)
        {
            Debug.LogError("Boss Prefab is not assigned!");
            return;
        }

        bossSpawned = true;

        Vector3 spawnPosition = bossSpawnPoint != null
            ? bossSpawnPoint.position
            : Vector3.zero;

        spawnedBoss = Instantiate(
            bossPrefab,
            spawnPosition,
            Quaternion.identity
        );

        // Subscribe to boss death
        Health bossHealth = spawnedBoss.GetComponent<Health>();

        if (bossHealth != null)
        {
            bossHealth.onDead += OnBossDefeated;
        }
        else
        {
            Debug.LogError("Boss prefab has no Health component!");
        }

        Debug.Log("BOSS SPAWNED!");
    }

    private void OnBossDefeated()
    {
        if (gameEnd)
            return;

        gameEnd = true;

        Debug.Log("BOSS DEFEATED!");

        OnGameWin();
    }

    private void OnGameWin()
    {
        gameWinUI.SetActive(true);

        playerHealth.gameObject.SetActive(false);

        Debug.Log("Game Win!");
    }

    private void OnGameOver()
    {
        if (gameEnd)
            return;

        gameEnd = true;

        gameOverUI.SetActive(true);

        Debug.Log("Game Over!");
    }

    public void ReturnToMainMenu()
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("Scene name is empty!");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.onDead -= OnGameOver;

        if (spawnedBoss != null)
        {
            Health bossHealth = spawnedBoss.GetComponent<Health>();

            if (bossHealth != null)
                bossHealth.onDead -= OnBossDefeated;
        }
    }
}
