using UnityEngine;

public class Platform : MonoBehaviour
{   
    public GameManager gameManager;
    protected bool isStepped = false;

    private void Start()
    {
        Debug.Log("실행됨");
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isStepped)
        {
            Debug.Log("Player stepped on the platform");
            isStepped=true;
        }
    }
}
