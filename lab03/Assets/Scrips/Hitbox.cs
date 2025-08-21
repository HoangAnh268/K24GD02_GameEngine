using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public enum ColliderType { Foot, Side}
    public ColliderType colliderType; //Chon Foot hoac Side

    private KnightController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponentInParent<KnightController>();   
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            if (colliderType == ColliderType.Foot)
            {
                player.KillEnemy(collision.gameObject);
            }
            else if (colliderType == ColliderType.Side)
            {
                player.Die();
            }
        }
    }
}
