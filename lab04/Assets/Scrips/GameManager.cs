using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Singleton

    public TextMeshProUGUI scoreText;
    //public GameObject gameOverPanel;
    //public TextMeshProUGUI finalScoreText;

    private int score = 0;  
    
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Update()
    {
        
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "0" + score;
    }
}
