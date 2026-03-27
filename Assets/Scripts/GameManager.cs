using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public GameObject gameOverUi;


    private int score = 0;

    public bool IsGameOver { get; private set; }

    void Awake()
    {
        //gameOverUi.SetActive(false);
    }

    void Update()
    {
        if (IsGameOver && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int add)
    {
        score += add;
        scoreText.text = $"COIN : {score}";
    }

    public void OnPlayerDead()
    {
        IsGameOver = true;
        //gameOverUi.SetActive(true);
        //StopScrolling();
    }

    //public void StopScrolling()
    //{
    //    ScrollingObject[] scrollingObjects = FindObjectsOfType<ScrollingObject>();
    //    foreach (var obj in scrollingObjects)
    //    {
    //        obj.speed = 0f;
    //    }
    //}
}
