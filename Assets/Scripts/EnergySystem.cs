// EnergySystem.cs
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    public PlayerController playerController;

    public float maxEnergy = 100f;
    public float drainRate = 5f;      // 초당 감소량
    public float coinRecharge = 10f;  // 코인 1개당 충전량
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

        // 시간이 지날수록 에너지 감소
        CurrentEnergy = Mathf.Clamp(CurrentEnergy - drainRate * Time.deltaTime, 0f, maxEnergy);

        // 에너지 0 되면 게임오버
        if (CurrentEnergy <= 0f)
        {
            gameManager.OnPlayerDead();
            return;
        }

        // 스크롤바에 매핑 (0~100 → 0.0~1.0)
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