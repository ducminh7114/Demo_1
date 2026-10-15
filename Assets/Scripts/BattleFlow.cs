using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleFlow : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public PlayerHealth playerHealth;
    [SerializeField] public string sceneName;
    private bool gameEnd = false;
    private bool hasEnemy = false;
    //public GameObject bgMusic;

    public void Start()
    {
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
        playerHealth.onDead += OnGameOver;

    }

    private void Update()
    {
        if (gameEnd)
        {
            return;
        }

        if (Enemyhealth.LivingEnemyCount > 0)
        {
            hasEnemy = true;
        }

        if (hasEnemy && Enemyhealth.LivingEnemyCount <= 0)
        {
            gameEnd = true;
            Debug.Log("All enemy deleted");
            OnGameWin();
        }
    }

    private void OnGameWin()
    {
        gameWinUI.SetActive(true);
        //bgMusic.SetActive(false);
        playerHealth.gameObject.SetActive(false);
        Debug.Log("Game win UI appeared");

    }

    private void OnGameOver()
    {
        gameOverUI.SetActive(true);
        //bgMusic.SetActive(false);
        Debug.Log("Game over UI appeared");
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Scene name is: '" + sceneName + "'");

        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("Scene name is empty! Check the Inspector.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
