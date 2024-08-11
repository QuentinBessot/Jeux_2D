using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private Text interactUI;
    private bool isInRange;

    public Animator animator;
    public int coinsToAdd;
    public AudioClip SoundToPlay;

    // Références pour les ScriptableObjects des potions
    public Item healthPotion;
    public Item speedPotion;

    // Probabilités pour obtenir les différents items (tu peux les ajuster)
    [Range(0, 100)] public int healthPotionChance = 33;
    [Range(0, 100)] public int speedPotionChance = 33;
    [Range(0, 100)] public int coinsChance = 34;

    void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        animator.SetTrigger("OpenChest");
        GiveRandomReward();
        AudioManager.instance.PlayClipAt(SoundToPlay, transform.position);
        GetComponent<BoxCollider2D>().enabled = false;
        interactUI.enabled = false;
    }

    void GiveRandomReward()
    {
        int randomValue = Random.Range(0, 100);

        if (randomValue < healthPotionChance)
        {
            Inventory.instance.content.Add(healthPotion);
            Debug.Log("You got a Health Potion!");
            Inventory.instance.UpdateInventoryUI();
        }
        else if (randomValue < healthPotionChance + speedPotionChance)
        {
            Inventory.instance.content.Add(speedPotion);
            Debug.Log("You got a Speed Potion!");
            Inventory.instance.UpdateInventoryUI();
        }
        else
        {
            Inventory.instance.AddCoins(coinsToAdd);
            Debug.Log("You got some coins!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = false;
            isInRange = false;
        }
    }
}
