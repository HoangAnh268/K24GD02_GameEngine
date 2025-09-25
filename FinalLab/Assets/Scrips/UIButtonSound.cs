using UnityEngine;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour
{
    public AudioClip clickSound;
    
    void Awake()
    {       
        // Tìm tất cả button trong Canvas này
        Button[] buttons = GetComponentsInChildren<Button>(true);
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(PlayClickSound);
        }
    }  
    public void PlayClickSound()
    {
        if(clickSound != null) 
            AudioManager.Instance.PlaySFX(clickSound);
        
    }
}
