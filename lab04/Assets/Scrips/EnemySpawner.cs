using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject chickenPrefab;
    public GameObject bossPrefab;
    public float spawnRate = 5f; // Khoang thoi gian sinh ra Enemy
    public int numberOfEnemies = 5; // so ga trong 1 hang
    public int numberOfRow = 3;
    public float spacingX = 2f; // khoang cach giua nhung con ga
    public float spacingY = 2f; // khoang cach doc giua cac hang
    public int waveBeforeBoss = 3; // Sau 3 dot thi sinh ra Boss

    private GameObject[,] currentEnemies;
    private float timer = 0;
    private bool waitingSpawn = false;
    private int waveCount = 0; //dem so dot ga dang spawn
    
    
    void Update()
    {
        //timer -= Time.deltaTime;    
        if(!EnemyAlive() && !waitingSpawn)
        {
            waitingSpawn = true;          
            timer = spawnRate;
        }

        if (waitingSpawn)
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
                    SpawnBoss();
                }
                waitingSpawn = false;
            }
        }

    }
    void SpawnEnemy()
    {
        currentEnemies = new GameObject[numberOfRow, numberOfEnemies];
        //Tinh vi tri spawn
        float startX = -((numberOfEnemies - 1) * spacingX) / 2f;
        float startY = Camera.main.orthographicSize - 1; //Nam gan phia tren man hinh
        for(int row = 0; row < numberOfRow; row++)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector3 Spawn = new Vector3(startX + i * spacingX, startY - row * spacingY, 0);
                currentEnemies[row, i] = Instantiate(chickenPrefab, Spawn, Quaternion.identity);
            }
        }
    }  
    void SpawnBoss()
    {
        Vector3 pos = new Vector3(0, Camera.main.orthographicSize - 2, 0); // giua man hinh
        Instantiate(bossPrefab, pos, Quaternion.identity);
        Debug.Log("Boss Spawn!!");
    }
    bool EnemyAlive() //Kiểm tra trên màn hình còn Enemy hay không còn thì ko spawn hết thì spawn tiếp tục
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
