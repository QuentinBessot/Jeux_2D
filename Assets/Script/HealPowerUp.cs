using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp : MonoBehaviour
{

    public int healfPoints;

    public AudioClip healSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            if (PlayerHealth.instance.currentHealth != PlayerHealth.instance.maxHealth)
            {
                AudioManager.instance.PlayClipAt(healSound, transform.position);
                PlayerHealth.instance.HealPlayer(healfPoints);
                Destroy(gameObject);
            }
        }
    }

}
 