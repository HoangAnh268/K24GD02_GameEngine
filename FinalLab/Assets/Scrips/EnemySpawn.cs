using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject enemyPrefab;
    public GameObject minibossPrefab;
    public GameObject bossPrefab;
    private GameObject currentBoss;

    [Header("Enemy Settings")]   
    public int numberOfEnemies = 5;  
    public int numberOfRow = 3;
    public float spacingX = 2f;
    public float spacingY = 2f;

    [Header("Spawn Settings")]
    public float spawnRate = 5f;
    public int waveBeforeBoss = 4;

    private GameObject[,] currentEnemies;
    private float timer = 0;
    private bool waitingSpawn = false;
    private int waveCount = 0;              // Đếm số đợt Enemy đang spawm   
    private bool minibossDefeated = false; //  Đánh dấu miniboss đã chết
    void Update()
    {
        if (!waitingSpawn)
        {
            if (!EnemyAlive() && currentBoss == null)
            {
                waitingSpawn = true;
                timer = spawnRate;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                if(waveCount < waveBeforeBoss)
                {
                    SpawnEnemy();
                    waveCount++;
                }
                else
                {
                    if (!minibossDefeated) 
                    {
                        SpawnMiniBoss();    
                    }
                    else
                    {
                        SpawnFinalBoss();
                    }
                    
                }
                waitingSpawn = false;
            }
        }
           
    }
    void SpawnEnemy()
    {
        currentEnemies = new GameObject[numberOfEnemies, numberOfRow];

        // Biên phải màn hình
        float rightEdge = Camera.main.orthographicSize * Camera.main.aspect;
        float startX = rightEdge - 1f; // chừa khoảng cách mép phải

        // Tính tổng chiều cao cụm enemy
        float totalHeight = (numberOfRow - 1) * spacingY;
        float startY = totalHeight / 2f; // hàng đầu tiên nằm ở trên, cân cụm theo Y

        for (int row = 0; row < numberOfRow; row++)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                // Spawn từ phải qua trái, và cụm cân giữa theo Y
                Vector3 spawnPos = new Vector3(
                    startX - i * spacingX,     // trải từ phải sang trái
                    startY - row * spacingY, // cân cụm theo Y
                    0f
                );
                currentEnemies[i, row] = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
    void SpawnMiniBoss()
    {
        if (currentBoss == null)
        {
            Vector3 offset = new Vector3(-3.7f, 0f, 0f);
            Vector3 pos = transform.position + offset;
            currentBoss = Instantiate(minibossPrefab, pos, Quaternion.identity);

            MiniBoss bm = currentBoss.GetComponent<MiniBoss>();
            bm.spawnManager = this;
            Debug.Log("MiniBoss Spawn");
        }
    }
    void SpawnFinalBoss()
    {
        if (currentBoss == null)
        {
            Vector3 offset = new Vector3(-3.7f, 0f, 0f);
            Vector3 pos = transform.position + offset;
            currentBoss = Instantiate(bossPrefab, pos, Quaternion.identity);
            Debug.Log("Final Boss Spawn");
        }
    }
    public void OnMiniBossDefeated()
    {
        minibossDefeated = true;
        currentBoss = null;
        waveCount = 0; // reset lai de spawn enemy lan 2
        
    }
    bool EnemyAlive()
    {
        if(currentEnemies == null)
            return false;
        foreach(var enemy in currentEnemies)
        {
            if(enemy != null)
                return true;
        }    
        return false;
    }    
}
