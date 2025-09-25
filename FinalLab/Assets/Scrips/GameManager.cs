using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton
    [Header("Player Lives")]
    public int lives = 3;
    public Image[] lifeIcons;
    public Sprite lifeOn;
    public Sprite lifeOff;

    [Header("Score System")]
    public int score = 0;
    public TMP_Text scoreText;

    [Header("Panel")]
    public GameObject GameOverPanel;
    public TMP_Text finalscoreTextGameOver;
    public GameObject GameWinPanel;
    public TMP_Text finalscoreTextGameWin;

    [Header("Respawn Settings")]
    public Transform playerSpawnPoint;
    public GameObject playerPrefab;

    [Header("Pause Settings")]
    public GameObject PausePanel;
    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {      
        if (GameOverPanel != null) GameOverPanel.SetActive(false);
        if (GameWinPanel != null) GameWinPanel.SetActive(false);
        if (PausePanel != null) PausePanel.SetActive(false);

        SpawnPlayer();
    }
    void Update()
    {
        UpdateLivesUI();
        UpdateScoreUI();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ContinueGame();
            else
                PauseGame();
        }
    }
    // Gọi khi mất 1 mạng
    public void LoseLife()
    {
        lives--;
        UpdateLivesUI();

        if(lives > 0)
        {
            // Respawn sau 2 giây, trong lúc đó background vẫn chạy
            StartCoroutine(RespawnPlayer());
        }
        else
        {
            GameOver();
        }    
    }
    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2f); // delay 2 giây
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }
    // Cập nhật icon mạng sống
    void UpdateLivesUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (i < lives)
                lifeIcons[i].sprite = lifeOn;
            else
                lifeIcons[i].sprite = lifeOff;
        }
    }
    void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }
    // Gọi khi tiêu diệt enemy, miniboss, finalboss
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }
    void UpdateScoreUI()
    {
        if(scoreText  != null)
            scoreText.text = "0" + score;
    }
    public void GameOver()
    {
        if(GameOverPanel  != null)
        {
            GameOverPanel.SetActive(true);
            if (finalscoreTextGameOver != null)
                finalscoreTextGameOver.text = "Score: " + score;
        }
        Time.timeScale = 0f; //dung game
    }
    public void GameWin()
    {
        if(GameWinPanel != null)
        {
            GameWinPanel.SetActive(true);
            if (finalscoreTextGameWin != null)
                finalscoreTextGameWin.text = "Score: " + score;
        }
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }       
    public void PauseGame()
    {
        if(PausePanel != null)
            PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }    
    public void ContinueGame()
    {
        if(PausePanel != null)
            PausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }       
}