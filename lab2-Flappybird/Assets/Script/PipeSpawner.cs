using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate;
    public float heightOffset = 2f;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isGameStarted)
            return;

        if(timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPipe();
            timer = 0;
        }
    }
    void SpawnPipe()
    {
        float random = Random.Range(-heightOffset, heightOffset);
        Vector3 spawnPos = new Vector3(transform.position.x, random, 0);
        Instantiate(pipePrefab, spawnPos, Quaternion.identity);
    }
}
