using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject basicCoinPrefab;
    public GameObject heartCoinPrefab;
    public GameObject archCoinPrefab;

    public int count;

    private int basicCount = 300;
    private int heartCount = 6;
    private int archCount = 9;

    public Vector2 spawnTimeRange = new Vector2(0.05f, 0.08f); //x: 최소, y: 최대
    private float timeSpawn;

    public Vector2 yRange = new Vector2(-3f, -1f);
    public float xPos = 20f;
    //public float yPos = 0f;

    private GameObject[] coins;
    private int previousIndex = 0;
    private int randomIndex = 0;

    private float lastSpawnTime;

    private GameManager gameManager;

    private void Awake()
    {
        count = basicCount + heartCount + archCount;
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        coins = new GameObject[count];
        for (int i = 0; i < coins.Length; i++)
        {
            if (i < basicCount)
            {
                coins[i] = Instantiate(basicCoinPrefab);
            }
            else if (i < basicCount + heartCount)
            {
                coins[i] = Instantiate(heartCoinPrefab);
            }
            else
            {
                coins[i] = Instantiate(archCoinPrefab);
            }
            coins[i].gameObject.SetActive(false);
            //coins[i].gameManager = gameManager;
        }
    }

    private void Start()
    {

        lastSpawnTime = 0f;
        timeSpawn = 0f;
    }

    void Update()
    {
        if (gameManager.IsGameOver) return;

        if (Time.time > lastSpawnTime + timeSpawn)
        {
            lastSpawnTime = Time.time;

            timeSpawn = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            Vector2 pos;
            pos.x = xPos;
            pos.y = Random.Range(yRange.x, yRange.y);
            //float y = Random.Range(yRange.x, yRange.y);
            randomIndex = Random.Range(0, coins.Length);
            //while (randomIndex == previousIndex)
            //{
            //    if(previousIndex >= basicCount)
            //    {
            //        timeSpawn = Random.Range(spawnTimeRange.x+1f, spawnTimeRange.y+1f);
            //        randomIndex = Random.Range(0, basicCount);
            //    }
            //    else
            //    {
            //        randomIndex = Random.Range(0, coins.Length);
            //    }
            //}
            randomIndex = Random.Range(0, coins.Length);


            int safetyNet = 0;
            while (coins[randomIndex].activeSelf && safetyNet < 100)
            {
                randomIndex = Random.Range(0, coins.Length);
                safetyNet++;
            }


            coins[randomIndex].transform.position = pos;

            coins[randomIndex].gameObject.SetActive(false);
            coins[randomIndex].gameObject.SetActive(true);

            
            previousIndex = randomIndex;
        }
    }
}
