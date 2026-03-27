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

            // 2. 만약 뽑은 놈이 이미 활성화(사용 중) 상태라면, 비활성 상태인 놈을 찾을 때까지 계속 다시 뽑습니다.
            // (단, 무한 루프 방지를 위해 최대 시도 횟수를 두는 것이 안전하지만, 코인 개수가 넉넉하다면 아래처럼 간단히 짤 수 있습니다.)
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
