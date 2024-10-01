using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;
    public float groundPoint = - 2.3f;

    private Rigidbody2D rb;
    private bool isGrounded;

    private BoxCollider2D boxCollider;
    private Vector2 originalColliderSize = new Vector2(0.6f, 1.1f);
    private Vector2 originalColliderOffset = new Vector2(0f, -0.05f);
    private Vector2 duckColliderSize = new Vector2(0.8f, 0.6f);
    private Vector2 duckColliderOffSet = new Vector2(0f, -0.3f);
    private Animator animator;

    // DoubleJump
    private int jumpCount ;
    public int maxJumpCount = 1;


    // Shield
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private float shieldDuration = 5f;
    [SerializeField] private float shieldCooldown = 10f;
    private float shieldCooldownTimer = 0f;
    private GameObject currentShield;
    private bool isShielded = false;
    public bool isInvincible = false;
    public bool isShieldUnlocked = false;

    //Shoot
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float shootCooldown = 5f;
    private float shootCooldownTimer = 0f;
    public bool isShootUnlocked = false;

    //UI
    [SerializeField] private Image shieldImage;
    [SerializeField] private Image shootImage;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = transform.GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (transform.position.y < groundPoint)
        {
            isGrounded = true;
            jumpCount = maxJumpCount;
        }
        else isGrounded = false;

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpCount > 0 )
        {
            Jump();

        }

        if (Input.GetKey(KeyCode.DownArrow) && isGrounded) 
        {
            Duck();
        }
        else 
        {
            StandUp();
        }

        if(Input.GetKeyDown(KeyCode.A) && !isShielded && shieldCooldownTimer <= 0f && isShieldUnlocked) 
        {
            ActivateShield();
  
        }

        if (Input.GetKeyDown(KeyCode.S) && shootCooldownTimer <= 0f && isShootUnlocked)
        {
            Shoot();
        }

        if (shieldCooldownTimer > 0f)
        {
            shieldCooldownTimer -= Time.deltaTime;
            UpdateShieldCooldownUI();
        }
        if (shootCooldownTimer > 0f)
        {
            shootCooldownTimer -= Time.deltaTime;
            UpdateShootCooldownUI();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Death") || collision.gameObject.CompareTag("Beam"))
        {
            if (isShielded)
            {
                isShielded = false;
                if (shieldPrefab != null)
                {
                    SoundManager.instance.PlaySound(SoundManager.Sound.Pop);
                    Destroy(currentShield); 
                    currentShield = null;
                    
                }

                if (!isInvincible)
                {
                    StartCoroutine(InvincibilityEffect());
                }
            }
            else if (!isInvincible)
            {
                GameManager.instance.GameOver();
            }
        }
    }

    private void Duck()
    {
        animator.SetBool("isDucking", true);
        boxCollider.size = duckColliderSize;
    }

    private void StandUp()
    {

        animator.SetBool("isDucking", false);
        boxCollider.size = originalColliderSize;
    }
    
    
    //--------------------------JUMP------------------------------

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        jumpCount--;
        SoundManager.instance.PlaySound(SoundManager.Sound.PlayerJump);
    }

    public void UnlockJump()
    {
        maxJumpCount = 2;
    }

    

    // -------------------------SHIELD----------------------------
    private IEnumerator InvincibilityEffect()
    {
        isInvincible = true; 
        float duration = 1.5f; 
        float blinkInterval = 0.1f; 


        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

       
        for (float t = 0; t < duration; t += blinkInterval)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; 
            yield return new WaitForSeconds(blinkInterval);
        }

        
        spriteRenderer.enabled = true;
        isInvincible = false; 
    }

    private void ActivateShield()
    {
        SoundManager.instance.PlaySound(SoundManager.Sound.Shield);
        currentShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
        isShielded = true;
        shieldCooldownTimer = shieldCooldown;
        StartCoroutine(ShieldDurationRoutine());
    }

    private IEnumerator ShieldDurationRoutine()
    {
        float blinkDuration = 1f;
        yield return new WaitForSeconds(shieldDuration - blinkDuration);

        if (currentShield != null)
        {
            SpriteRenderer shieldRenderer = currentShield.GetComponentInChildren<SpriteRenderer>();
            
            float blinkInterval = 0.1f;

            for (float t = 0; t < blinkDuration; t += blinkInterval)
            {
                if (currentShield != null && shieldRenderer != null)
                {
                    shieldRenderer.enabled = !shieldRenderer.enabled; // Toggle shield visibility
                }
                yield return new WaitForSeconds(blinkInterval);
            }
        }

        if (currentShield != null)
        {
            SoundManager.instance.PlaySound(SoundManager.Sound.Pop);
            Destroy(currentShield);
        }

        isShielded = false;
    }

    public void UnlockShield()
    {
        isShieldUnlocked = true;
    }


    //------------------------SHOOT-----------------------------------
    private void Shoot()
    {
        SoundManager.instance.PlaySound(SoundManager.Sound.Shoot);
        Instantiate(fireballPrefab, shootPoint.position , Quaternion.identity);
        shootCooldownTimer = shootCooldown;
    }

    public void UnlockShoot()
    {
        isShootUnlocked = true;
    }

    private void UpdateShieldCooldownUI()
    {
        if (shieldImage != null)
        {
            float fillAmount = Mathf.Clamp01(shieldCooldownTimer / shieldCooldown); 
            shieldImage.fillAmount = fillAmount; 
        }
    }

    private void UpdateShootCooldownUI()
    {
        if (shootImage != null)
        {
            float fillAmount = Mathf.Clamp01(shootCooldownTimer / shootCooldown);
            shootImage.fillAmount = fillAmount;
        }
    }
}
