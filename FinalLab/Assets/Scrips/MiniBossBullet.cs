using UnityEngine;

public class MiniBossBullet : MonoBehaviour
{
    public float speed = 9f;
    void Start()
    {
        
    }
   
    void Update()
    {
       transform.Translate(Vector2.left *  speed * Time.deltaTime);
       if(transform.position.x < -Camera.main.orthographicSize - 3f)
       {
            Destroy(gameObject);
       }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>()?.Die();
            Destroy(gameObject);
        }
    }
}
