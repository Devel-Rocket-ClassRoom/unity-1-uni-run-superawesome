using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public Platform basicPlatformPrefab;
    public Platform obstaclePlatformPrefab;
    public Platform airObstaclePlatformPrefab;
  

    private int basicCount = 4;
    private int obstacleCount = 2;
    private int airObstacleCount = 2;

    public int count;

    public Vector2 spawnTimeRange = new Vector2(1f, 1.7f); //x: 최소, y: 최대
    private float timeSpawn;

    public Vector2 yRange = new Vector2(-3.5f, 1.5f);
    public float xPos = 20f;
    public float yPos = -4f;

    private Platform[] platforms;
    private int previousIndex = 0;
    private int randomIndex = 0;

    private float lastSpawnTime;

    private GameManager gameManager;

    private void Awake()
    {
        count = basicCount + obstacleCount + airObstacleCount;
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        platforms = new Platform[count];
        for(int i = 0; i < platforms.Length; i++)
        {
            if(i < basicCount)
            {
                platforms[i] = Instantiate(basicPlatformPrefab);
            }
            else if(i < basicCount + obstacleCount)
            {
                platforms[i] = Instantiate(obstaclePlatformPrefab);
            }
            else
            {
                platforms[i] = Instantiate(airObstaclePlatformPrefab);
            }
            platforms[i].gameObject.SetActive(false);
            platforms[i].gameManager = gameManager;
        }
    }

    private void Start()
    {

        lastSpawnTime = 0f;
        timeSpawn = 0f;
    }

    void Update()
    {
        if(gameManager.IsGameOver) return;

        if(Time.time > lastSpawnTime + timeSpawn)
        {
            lastSpawnTime = Time.time;
            timeSpawn = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

            Vector2 pos;
            pos.x = xPos;
            pos.y = yPos;
            //float y = Random.Range(yRange.x, yRange.y);
            randomIndex = Random.Range(0, platforms.Length);
            while (randomIndex == previousIndex)
            {
                randomIndex = Random.Range(0, platforms.Length);
            }
            platforms[randomIndex].transform.position = pos;

            platforms[randomIndex].gameObject.SetActive(false);
            platforms[randomIndex].gameObject.SetActive(true);

            previousIndex = randomIndex;
        }
    }
}
