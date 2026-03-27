using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f;
    private GameManager gm;
    void Awake()
    {
        gm = FindAnyObjectByType<GameManager>();
        speed = 10f;
    }

    void Update()
    {
        if (gm.IsGameOver)
        {
            return;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        //transform.position += Vector3.left * speed * Time.deltaTime;
        
    }
}
