using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingPanel;
    void Start()
    {
        // Khi mở scene MainMenu thì phát nhạc menu
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMenuMusic();
        }
    }
    void Update()
    {
       
        // Nhấn ESC thì thoát Setting Panel
        if (settingPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSetting();
        }
    }
    public void StartGame()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGameMusic();
        }
        SceneManager.LoadScene("GameScene");   
    }   
    public void QuitGame()
    {
        Application.Quit();
    }    
    public void OpenSetting()
    {
        mainMenuPanel.SetActive(false);
        settingPanel.SetActive(true);
    }   
    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        mainMenuPanel.SetActive(true);      
    }    
}
