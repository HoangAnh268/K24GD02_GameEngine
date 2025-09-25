using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    public float BackGroundSpeed = 5f;
    [SerializeField]
    private Material material;
    private float offset;
    private float parallaxFactor = 0.01f;
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    void Update()
    {
        ParallaxScroll();
    }
    private void ParallaxScroll()
    {
        float speed = BackGroundSpeed * parallaxFactor;
        offset += Time.deltaTime * speed;
        material.SetTextureOffset("_MainTex", Vector2.right * offset);
    }
}
