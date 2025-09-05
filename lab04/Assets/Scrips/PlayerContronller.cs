using UnityEngine;

public class PlayerContronller : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public GameObject bulletPrefab;
    public Transform firePoint;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    
    void Update()
    {
        Move();   
        Shoot();
    }
    void Move()
    {
        //Di chuyen bang Phim
        /*float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(x, y) * moveSpeed * Time.deltaTime);

        Vector3 direction = new Vector3(x, y, 0);     
        Vector3 TopLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -TopLeftPoint.x, TopLeftPoint.x),
            Mathf.Clamp(transform.position.y, -TopLeftPoint.y, TopLeftPoint.y)
        );
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }*/
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; //Vi game 2D nen de Z = 0
        Vector3 TopLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -TopLeftPoint.x, TopLeftPoint.x),
            Mathf.Clamp(transform.position.y, -TopLeftPoint.y, TopLeftPoint.y)
        );
        transform.position = Vector3.MoveTowards(transform.position, mousePos, moveSpeed * Time.deltaTime) ;

    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }
}
