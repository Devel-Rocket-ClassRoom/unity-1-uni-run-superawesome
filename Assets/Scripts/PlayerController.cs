using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    public EnergySystem energySystem;

    //애셋 필드
    public AudioClip deathClip;
    public float jumpForce = 17f;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;
    private bool isCrouching = false;

    private Vector2 originalSize;
    private Vector2 originalOffset;

    // 컴포넌트 필드
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private AudioSource playerAudio;
    private CapsuleCollider2D playerCollider;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = FindAnyObjectByType<GameManager>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        originalSize = playerCollider.size;
        originalOffset = playerCollider.offset;
        energySystem = FindAnyObjectByType<EnergySystem>();
    }


    void Update()
    {
        if(isDead) return;
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2)
        {
            playerRigidbody.linearVelocity = Vector2.zero;
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            isGrounded = false;
            //playerAudio.Play();
        }
        else if(Input.GetKeyUp(KeyCode.UpArrow) && playerRigidbody.linearVelocity.y > 0)
        {
            playerRigidbody.linearVelocity *= 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerCollider.size = new Vector2(originalSize.x, originalSize.y / 2f);
            playerCollider.offset = new Vector2(
                originalOffset.x,
                originalOffset.y - originalSize.y / 4f
            );

            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            playerCollider.size = originalSize;
            playerCollider.offset = originalOffset;

            isCrouching = false;
        }
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("isCouching", isCrouching);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
            animator.SetBool("Grounded", isGrounded);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead") && !isDead)
        {
            energySystem.RemoveEnergy(energySystem.coinPenalty);
        }
        else if(collision.CompareTag("Instant Death"))
        {
            energySystem.RemoveEnergy(energySystem.maxEnergy);
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        //playerAudio.PlayOneShot(deathClip);

        playerRigidbody.linearVelocity = Vector2.zero;
        playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
        isDead = true;


        gameManager.OnPlayerDead();
    }
}