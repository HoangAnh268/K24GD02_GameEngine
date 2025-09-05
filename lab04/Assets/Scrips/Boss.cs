using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealt;

    public Slider healthBar; //Thanh mau boss

    public float speed = 2f; // toc do di chuyen
    public float speedRange = 5f; // khoang cach qua lai
    private int direction = 1; // 1 = sang phải, -1 = sang trái
    private Vector3 startPos;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    private float fireTimer;
    void Start()
    {
        currentHealt = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealt;
        }

        startPos = transform.position; // nhớ vị trí spawn để tính phạm vi di chuyển
    }

  
    void Update()
    {
        //Di chuyen qua di chuyen lai
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        //Doi huong khi di chuyen pham vi
        if(Mathf.Abs(transform.position.x - startPos.x) >= speedRange)
        {
            direction *= -1;
        }
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }
    public void TakeDamege(int damege)
    {
        currentHealt -= damege;
        if (currentHealt < 0)
        {
            currentHealt = 0;
        }
        if(healthBar != null)
        {
            healthBar.value = currentHealt;
        }
        if (currentHealt <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Boss died");
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            TakeDamege(10);
        }
        if(collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject );
        }
    }
}
