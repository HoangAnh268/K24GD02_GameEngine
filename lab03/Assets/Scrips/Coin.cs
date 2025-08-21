using UnityEngine;

public class Coin : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(10);
            Destroy(gameObject);
        }
    }

}
