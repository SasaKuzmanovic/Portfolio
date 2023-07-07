using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHUD : MonoBehaviour
{
    [Header("Health and stamina variables")]
    public int maxHealth = 5;
    public int currentHealth;
    public float stamina = 0;
    public float maxStamina = 100;
    public float staminaIncrease = 3;
    public float staminaDrain = 3;

    [Header("Healthbar and stamina bar")]
    public Healthbar healthbar;
    public StaminaBar staminaBarScript;
    public Slider staminaBar;

    [Header("Player stuff")]
    public Rigidbody2D rb;
    public float jumpForce = 0;
    public int jumpCount = 0;
    public int allowedJumps = 0;
    public float gravityScale = 0;
    public float fallingGravityScale = 0;
    public bool isGrounded = false;
    public float playerSpeed = 5.0f;
    public bool m_FacingRight = true;
    public bool m_FacingLeft = false;
    Vector2 savedlocalScale;
    public Animator animator;
    public bool resetJump = false;

    private float cooldownTimer = 5;

    /// <summary>
    /// set the CurrentHealth to max at the start
    /// Change the healthbar to represent this
    /// Same thing with Stamina
    /// set the rigidbody of the Player and then set the hud around that
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);

        stamina = maxStamina;
        staminaBarScript.setMaxStamina(stamina);

        rb = GetComponent<Rigidbody2D>();
        savedlocalScale = transform.localScale;
    }

    // Update is called once per frame
    /// <summary>
    /// Getting players playerInput and then do a range of things based on that.
    /// Decrease the players Energy, set their aniamtions to the correct ones 
    /// Increase energy, handle jumping and jump timers etc
    /// </summary>
    void Update()
    {
        ////////////////////////////////////////////////////////////////////////////            <<--------- MOVEMENT
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * playerSpeed, rb.velocity.y);

        if (rb.velocity.x > 0.001f)
        {
            animator.SetFloat("speed", Mathf.Abs(playerSpeed));
            transform.localScale = new Vector2(savedlocalScale.x, savedlocalScale.y);
            m_FacingLeft = false;
            m_FacingRight = true;
            DecreaseEnergy();
        }
        else if (rb.velocity.x < -0.001f)
        {
            animator.SetFloat("speed", Mathf.Abs(playerSpeed));
            transform.localScale = new Vector2(-savedlocalScale.x, savedlocalScale.y);
            m_FacingLeft = true;
            m_FacingRight = false;
            DecreaseEnergy();
        }

        if (rb.velocity.x == 0.0f)
        {
            animator.SetFloat("speed", Mathf.Abs(0));
            IncreaseEnergy();
        }

        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////            <<--------- JUMPING


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            jumpCount += 1;
            if (jumpCount == allowedJumps)
            {
                isGrounded = false;
            }
        }

        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = gravityScale;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallingGravityScale;
        }

        staminaBar.value = stamina;
        if(stamina >= 20.0f)
        {
            playerSpeed = 5.0f;
        }
        ////////////////////////////////////////////////////////////////////////////

        if(currentHealth <= 0)
        {
            Debug.Log("The Player Died - Do our restart scene ");
            SceneManager.LoadScene("Adams Scene 1"); //currently reloading my level for now, we can restart the entire game here.
        }

        if(resetJump == true)
        {
            resetJumpingValues();
        }

    }

    /// <summary>
    /// Take damage and then set the healthbar to represent that
    /// </summary>
    /// <param name="t_damage"></param>
    public void TakeDamage(int t_damage)
    {
        currentHealth -= t_damage;
        healthbar.setHealth(currentHealth);
    }

    /// <summary>
    /// Decrease the players energy and change the plauyer speed and stamina bar to represent this 
    /// </summary>
    private void DecreaseEnergy()
    {
        if(stamina != 0.0f)
        {
            stamina -= staminaDrain * Time.deltaTime;
        }

        if(stamina <= 0)
        {
            stamina = 0.0f;
            playerSpeed = 2.0f;
        }

        staminaBarScript.setStamina(stamina);
    }

    /// Increase the players energy and change the stamina bar to represent this 

    private void IncreaseEnergy()
    {
        stamina += staminaIncrease * Time.deltaTime;

        if(stamina >= maxStamina)
        {
            stamina = maxStamina;
        }

        staminaBarScript.setStamina(stamina);
    }

    /// <summary>
    /// Check for collision with Projectile
    /// If its true, take damage
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(1);
        }
       
    }

    /// <summary>
    /// reset the jumping values of the player after the coodown timer
    /// Both jumpforce and gravity scale are set 
    /// </summary>
   public void resetJumpingValues()
   {
        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0)
        {
            jumpForce = 12;
            gravityScale = 8;

        }
        
        Debug.Log(cooldownTimer);

   }
}
