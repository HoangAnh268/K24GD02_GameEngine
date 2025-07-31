using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 2f;

    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left *  speed * Time.deltaTime);
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}
