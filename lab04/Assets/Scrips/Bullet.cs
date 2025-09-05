using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;   
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed *  Time.deltaTime );   

        if( transform.position.y > Camera.main.orthographicSize + 1) //khi di ra khoi vung camera se xoa dan
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); //Huy Enemy
            Destroy(gameObject);          //Huy Bullet           
        }
    }
}
