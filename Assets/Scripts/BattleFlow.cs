using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleFlow : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public PlayerHealth playerHealth;
    //public GameObject bgMusic;

    public void Start()
    {
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
        playerHealth.onDead += OnGameOver;

    }

    private void Update()
    {
        if (Enemyhealth.LivingEnemyCount <= 0)
        {
            OnGameWin();
        }
    }

    private void OnGameWin()
    {
        gameWinUI.SetActive(true);
        //bgMusic.SetActive(false);
        playerHealth.gameObject.SetActive(false);

    }

    private void OnGameOver()
    {
        gameOverUI.SetActive(true);
        //bgMusic.SetActive(false);
    }

    public void ReturnToMainMenu() => SceneManager.LoadScene("MainMenu");
}
