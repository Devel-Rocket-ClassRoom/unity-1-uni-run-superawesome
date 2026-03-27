using UnityEngine;

public class ObstaclePlatform : Platform
{
    public GameObject[] obstacles;
    public float obstacleRatio = 0.3f;

    private void OnEnable()
    {
        foreach (var obstacle in obstacles)
        {
            obstacle.SetActive(Random.value < obstacleRatio);
        }
        isStepped = false;
    }
}
