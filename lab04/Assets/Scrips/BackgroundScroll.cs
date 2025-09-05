using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float BackGroundSpeed = 0.1f;
    private Material material;  
    void Start()
    {
        material = GetComponent<Renderer>().material;    
    }
   
    void Update()
    {
        float offset = Time.time * BackGroundSpeed;
        material.mainTextureOffset = new Vector2(0, offset); // cuon ngang neu cuon doc thi doi lai Vector2(offset, 0);
    }
}
