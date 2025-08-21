using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton

    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    private int score = 0;
    private bool isGameOver = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();    
    }
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        if(scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    public int GetScore()
    {
        return score;
    }
    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true); // Hien panel GameOver
        finalScoreText.text = "Score: " + score;
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
