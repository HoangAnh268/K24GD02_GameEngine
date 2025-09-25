using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float speedRange = 3f;   // khoảng cách di chuyển sang trái/phải
    public float minfireRate = 2f;  // bắn nhanh nhất
    public float maxfireRate = 5f;  // bắn chậm nhất
    public GameObject enemybulletPrefab;
    public Transform firePoint;

    private int direction = 1;
    private float fireTimer;
    private Vector3 startPos;
    private float nextfireRate;     // Thời gian ngẫu nhiên để bắn

    [Header("Sound")]
    public AudioClip deathSound;
    
    void Start()
    {
        startPos = transform.position;    
        SetNextFireRate();

        
    }  
    void Update()
    {
        transform.Translate(Vector2.up * direction * speed * Time.deltaTime);
        if(Mathf.Abs(transform.position.y - startPos.y) >= speedRange)
        {
            direction *= -1;
        }
        fireTimer += Time.deltaTime;
        if(fireTimer >= nextfireRate)
        {
            Shoot();
            fireTimer = 0;
        }
    }
    void Shoot()
    {
        if (enemybulletPrefab != null && firePoint != null)
        {
            Instantiate(enemybulletPrefab, firePoint.position, Quaternion.identity);
        }
    }
    void SetNextFireRate()
    {
        fireTimer = 0f;
        nextfireRate = Random.Range(minfireRate, maxfireRate);// Random tg bắn
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>()?.Die();
        }
        else if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Die();
        }

    }
    void Die()
    {
        GameManager.Instance.AddScore(100);

        if (deathSound != null)
        {
            AudioManager.Instance.PlaySFX(deathSound);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
