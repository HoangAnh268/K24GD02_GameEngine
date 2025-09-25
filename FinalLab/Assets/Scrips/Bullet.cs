using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    // Thêm biến callback để báo lại cho Player
    public Action onDestroyed;
      
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (transform.position.x > Camera.main.orthographicSize + 3)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("FinalBoss"))
        {
            Boss fb = collision.GetComponent<Boss>();
            if (fb != null)
            {
                fb.TakeDamege(30);
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("MiniBoss"))
        {
            Destroy(gameObject);
        }    
    }
    private void OnDestroy()
    {
        if(onDestroyed != null)
            onDestroyed();
    }
}
