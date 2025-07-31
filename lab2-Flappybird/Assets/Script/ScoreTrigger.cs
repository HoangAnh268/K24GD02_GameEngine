using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public AudioClip scoreSound;
    private AudioSource audioSource;
    private void Start()
    {
        // Tìm AudioSource từ player để dùng chung
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            audioSource = player.GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.instance.AddScore();
            if (scoreSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(scoreSound);
            }
        }
    }
}
