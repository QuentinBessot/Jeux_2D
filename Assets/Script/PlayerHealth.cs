using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public float invicibilityFlashDelay = 0.2f;
    public float invicibilityTimeAfter = 3f;

    public bool isInvincible = false;

    public SpriteRenderer graphics;

    public HealthBar healthBar;

    public AudioClip hitSound;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scene ");
            return;
        }
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyUp(KeyCode.H)) 
        {
            TakeDommage(60);
        }
    }

    public void HealPlayer(int amount)
    {
        if ((currentHealth + amount) > maxHealth) 
        {
            currentHealth = maxHealth;
        }else
        {
            currentHealth += amount;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDommage(int damage)
    {
        if (!isInvincible)
        {
            AudioManager.instance.PlayClipAt(hitSound, transform.position);
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0 ) 
            {
                Die();
                return;
            }

            isInvincible=true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void Die()
    {
        Debug.LogWarning("Le joeur est mort");
        Move_Players.instance.enabled = false;
        Move_Players.instance.animator.SetTrigger("Die") ;
        Move_Players.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        Move_Players.instance.rb.velocity = Vector3.zero;
        Move_Players.instance.capsuleCollider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        Move_Players.instance.enabled = true;
        Move_Players.instance.animator.SetTrigger("Respawn");
        Move_Players.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        Move_Players.instance.capsuleCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f,1f, 1f,0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
            yield return new WaitForSeconds(invicibilityTimeAfter);
            isInvincible = false;
    }
}
