using UnityEngine;

public class EggBullet : MonoBehaviour
{
    public float speed = 9f;
    public GameObject friedEggPrefab;
    public float friedEggTime = 3f;
    private bool isFried = false;
   
    void Start()
    {
             
    }

    
    void Update()
    {
        if (!isFried)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            if (transform.position.y < -Camera.main.orthographicSize + 0.5f)
            {
                TransformToFriedEgg();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isFried && collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
    void TransformToFriedEgg()
    {
        // Spawn prefab Fried Egg có animation chiên
        GameObject friedEgg = Instantiate(friedEggPrefab, transform.position, Quaternion.identity);

        //Huy trung chien sau vai giay
        Destroy(friedEgg, friedEggTime);

        //Qua trung bi huy 
        Destroy(gameObject);
    }
}
