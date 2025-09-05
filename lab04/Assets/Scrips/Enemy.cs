using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float speedRange = 3f;  // khoảng cách di chuyển sang trái/phải
    public float minfireRate = 2f;// bắn nhanh nhất
    public float maxfireRate = 5f;//bắn chậm nhất
    public GameObject eggPrefab;
    public Transform firePoint;
    public GameObject drumstickPrefab;

    private int direction = 1; // 1 là sang phải , -1 là sang trái
    private float fireTimer;
    private Vector3 startPos;
    private float nextfireRate; // tg ngẫu nhiên đẻ bắn
    void Start()
    {
        startPos = transform.position; // Vị trí spawn đầu tiên    
        SetNextFireRate(); //chọn tg bắn lần đầu
    }

    
    void Update()
    {

        //if (transform.position.y < -Camera.main.orthographicSize - 1)
        //{
        //    Destroy(gameObject);
        //}


        // Di chuyển qua lại
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        // Nếu vượt quá phạm vi thì đổi hướng
        if (Mathf.Abs(transform.position.x - startPos.x) >= speedRange)
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
        if (eggPrefab != null && firePoint != null)
        {
            Instantiate(eggPrefab, firePoint.position, Quaternion.identity);
        }
    }
    void SetNextFireRate()
    {
        fireTimer = 0f;
        nextfireRate = Random.Range(minfireRate, maxfireRate);//Random tg bắn 
    }    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            //Destroy(gameObject);
            Die();
        }
        else if(collision.CompareTag("Bullet"))//bi player ban
        {
            Destroy(collision.gameObject);//Huy dan
            Die();
        }
    }
    void Die()
    {
        //Spawn drumstick khi enemy chet
        if(drumstickPrefab != null)
        {
            Instantiate(drumstickPrefab, transform.position, Quaternion.identity);
        }
        //Khong cong diem
        Destroy(gameObject);
    }
}
