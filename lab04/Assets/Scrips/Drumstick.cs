using UnityEngine;

public class Drumstick : MonoBehaviour
{
    public float speed = 2f;
    public int scoreValue = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime); 
        if( transform.position.y < -Camera.main.orthographicSize - 1)
        {
            Destroy(gameObject);
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.instance.AddScore(scoreValue);
            Destroy(gameObject);
        }    
    }
}
