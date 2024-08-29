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

    // R�f�rences pour les ScriptableObjects des potions
    public Item healthPotion;
    public Item speedPotion;

    // Images pour les r�compenses
    public Sprite healthPotionImage;
    public Sprite speedPotionImage;
    public Sprite coinsImage;

    // Probabilit�s pour obtenir les diff�rents items (tu peux les ajuster)
    [Range(0, 100)] public int healthPotionChance = 33;
    [Range(0, 100)] public int speedPotionChance = 33;
    [Range(0, 100)] public int coinsChance = 34;

    // R�f�rence � l'�l�ment Text UI pour afficher la r�compense
    public Text rewardTextUI;  // Assurez-vous d'attacher un Text UI dans l'inspecteur

    // R�f�rence � l'image UI pour afficher l'image de la r�compense
    public Image rewardImageUI;  // Assurez-vous d'attacher un Image UI dans l'inspecteur

    void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();

        // Masquer le texte de r�compense et l'image au d�but
        if (rewardTextUI != null)
        {
            rewardTextUI.enabled = false;
        }
        if (rewardImageUI != null)
        {
            rewardImageUI.enabled = false;
        }
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
        string rewardMessage = "";
        Sprite rewardImage = null;

        if (randomValue < healthPotionChance)
        {
            Inventory.instance.content.Add(healthPotion);
            rewardMessage = "You got a Health Potion!";
            rewardImage = healthPotionImage;
            Inventory.instance.UpdateInventoryUI();
        }
        else if (randomValue < healthPotionChance + speedPotionChance)
        {
            Inventory.instance.content.Add(speedPotion);
            rewardMessage = "You got a Speed Potion!";
            rewardImage = speedPotionImage;
            Inventory.instance.UpdateInventoryUI();
        }
        else
        {
            Inventory.instance.AddCoins(coinsToAdd);
            rewardMessage = "You got some coins!";
            rewardImage = coinsImage;
        }

        // Afficher le message de r�compense et l'image dans l'UI
        ShowRewardMessage(rewardMessage, rewardImage);
    }

    void ShowRewardMessage(string message, Sprite image)
    {
        if (rewardTextUI != null)
        {
            rewardTextUI.text = message;
            rewardTextUI.enabled = true;
        }

        if (rewardImageUI != null)
        {
            rewardImageUI.sprite = image;
            rewardImageUI.enabled = true;
        }

        // Optionnel : Masquer le message et l'image apr�s un certain temps
        StartCoroutine(HideRewardMessage());
    }

    IEnumerator HideRewardMessage()
    {
        yield return new WaitForSeconds(2.0f);  // Attendre 2 secondes avant de masquer le texte et l'image
        if (rewardTextUI != null)
        {
            rewardTextUI.enabled = false;
        }
        if (rewardImageUI != null)
        {
            rewardImageUI.enabled = false;
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
