using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    //Giới hạn số viên đạn
    public int maxBullet = 3;
    private int currentBullet = 0;

    public AudioClip shootSound;
    private AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // lấy kích thước sprite

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }
    void Update()
    {
        Move();
        Shoot();
    }
    void Move()
    {
        Vector3 mouPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouPos.z = 0f;
      
        transform.position = Vector3.MoveTowards(transform.position, mouPos, speed * Time.deltaTime);

        // Lấy biên màn hình (trái - phải - dưới - trên)
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float halfWidth = spriteRenderer.bounds.extents.x;
        float halfHeight = spriteRenderer.bounds.extents.y;

        float minX = bottomLeft.x + halfWidth;
        float maxX = topRight.x - halfWidth;
        float minY = bottomLeft.y + halfHeight;
        float maxY = topRight.y - halfHeight;
       
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );
    }
    void Shoot()
    {
        if( Input.GetMouseButtonDown(0) && currentBullet < maxBullet)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            // Gán callback khi viên đạn bị hủy
            bullet.GetComponent<Bullet>().onDestroyed = () =>
            {
                currentBullet--;
            };
            currentBullet++;
            if (shootSound != null)
            {
                AudioManager.Instance.PlaySFX(shootSound);
            }
        }
        
    }
    public void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseLife();
        }
        Destroy(gameObject);
    }    
}
