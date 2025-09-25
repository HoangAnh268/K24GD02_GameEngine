using UnityEngine;
using UnityEngine.UI;

public class MiniBoss : MonoBehaviour
{
    [HideInInspector] public EnemySpawn spawnManager;
    [Header("Boss HealthBar")]
    public int maxHealth = 600;
    public Slider healthBar;
    private int currentHealth;

    [Header("Movement Settings")]
    public float radius = 3f;               //bán kính vòng tròn
    public float rotateSpeed = 2f;          //Tốc độ quay
    private Vector3 centerPoint;            //Tâm vòng tròn
    private float angle = 0f;               //Góc hiện tại (radian)

    [Header("Shooting Settings")]
    public GameObject bossbulletPrefab;     //Đạn thường
    public GameObject specialbulletPrefab;  //Đạn đặc biệt
    public Transform firePoint;
    public float fireRate = 2f;
    private float fireTimer;

    [Header("Sound")]
    public AudioClip deathSound;
    

    // Pattern bắn
    private int singleShotCount = 0;        //Đếm số lần bắn 1 viên
    private int tripleShotCount = 0;        //Đếm số lần bắn 3 tia
    private int specialShotCount = 0;       // Đếm số lần bắn đặc biệt

    private bool isDead = false; //Tránh gọi OnDestroy nhiều lần
    void Start()
    {
        currentHealth = maxHealth;
        if(healthBar != null )
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }   
        centerPoint = transform.position;

        //Âm thanh deathSound
        
    }   
    void Update()
    {
        // Tính toán di chuyển vòng tròn
        angle += rotateSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        transform.position = centerPoint + new Vector3(x, y, 0f);

        //Viên đạn bắn
        fireTimer += Time.deltaTime;
        if(fireTimer > fireRate)
        {
            ShootPattern();
            fireTimer = 0f;
        }
    }
    //Logic Pattern
    void ShootPattern()
    {
        // 5 lần bắn 1 viên thường
        if (singleShotCount < 5)
        {
            ShootSingle(bossbulletPrefab);
            singleShotCount++;
            return;
        }

        // 5 lần bắn 3 tia thường
        if (tripleShotCount < 5)
        {
            ShootTriple();
            tripleShotCount++;
            return;
        }
        // 3 lần bắn 1 viên đặc biệt
        if (specialShotCount < 3)
        {
            ShootSingle(specialbulletPrefab);
            specialShotCount++;
            return;
        }
        singleShotCount = 0;
        tripleShotCount = 0;
        specialShotCount = 0;
    }
    void ShootSingle(GameObject bulletPrefab)
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
    void ShootTriple()
    {
        if (bossbulletPrefab == null || firePoint == null)
            return;
        Instantiate(bossbulletPrefab, firePoint.position, firePoint.rotation);          //Thẳng
        Instantiate(bossbulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 15));  //Lệch phải
        Instantiate(bossbulletPrefab, firePoint.position, Quaternion.Euler(0, 0, -15)); //Lệch trái
    }    
    public void TakeDamege(int damege)
    {
        currentHealth -= damege;
        if(currentHealth < 0) 
            currentHealth = 0;
        if(healthBar != null)
            healthBar.value = currentHealth;
        if (currentHealth <= 0)
            Die();
    }
    void Die()
    {
        if(isDead) return;
        isDead = true;
        GameManager.Instance.AddScore(1000);
        if(spawnManager != null)
            spawnManager.OnMiniBossDefeated();

        if (deathSound != null)
        {
            AudioManager.Instance.PlaySFX(deathSound);
        }
        
        Destroy(gameObject);
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            TakeDamege(30);
        }
        else if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>()?.Die();
        }      
    }   
}
