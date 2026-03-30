using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameManager gameManager;
    public EnergySystem energySystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        energySystem = FindAnyObjectByType<EnergySystem>();
    }



        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dead"))
        {
            gameObject.SetActive(false);
        }
        if(other.CompareTag("Player"))
        {
            gameManager.AddScore(1);
            gameObject.SetActive(false);
            energySystem?.AddEnergy(energySystem.coinRecharge);
        }
    }

}
