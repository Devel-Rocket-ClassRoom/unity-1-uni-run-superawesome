// EnergySystem.cs
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    public PlayerController playerController;

    public float maxEnergy = 100f;
    public float drainRate = 10f;      // 초당 감소량
    public float coinRecharge = 5f;  // 코인 1개당 충전량
    public float coinPenalty = 30f;     // 장애물 충돌 시 감소량
   

    public Image fillImage;

    public float CurrentEnergy { get; private set; }

    private GameManager gameManager;

    private void Awake()
    {
        CurrentEnergy = maxEnergy;
        gameManager = FindAnyObjectByType<GameManager>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        if (gameManager.IsGameOver) return;

        CurrentEnergy = Mathf.Clamp(CurrentEnergy - drainRate * Time.deltaTime, 0f, maxEnergy);

        if (CurrentEnergy <= 0f)
        {
            gameManager.OnPlayerDead();
            return;
        }

        fillImage.fillAmount = CurrentEnergy / maxEnergy;
    }

    public void AddEnergy(float amount)
    {
        CurrentEnergy = Mathf.Clamp(CurrentEnergy + amount, 0f, maxEnergy);
    }

    public void RemoveEnergy(float amount)
    {
        CurrentEnergy = Mathf.Clamp(CurrentEnergy - amount, 0f, maxEnergy);
        if(CurrentEnergy <= 0f)
        {
            fillImage.fillAmount = CurrentEnergy / maxEnergy;

            playerController.Die();
        }
    }
}