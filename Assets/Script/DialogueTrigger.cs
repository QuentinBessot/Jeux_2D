using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool isInRange = false;

    private GameObject[] inventoryElements;
    private Text interactUI;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
        inventoryElements =  GameObject.FindGameObjectsWithTag("Inventaire");
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            interactUI.enabled = false;
            DialogueManager.instance.EndDialogue();
            DisableInventoryElements(true);

        }
    }

    void TriggerDialogue()
    {
        // Démarrer le dialogue
        DialogueManager.instance.StartDialogue(dialogue);

        // Désactiver tous les éléments avec le tag "Inventaire"
        DisableInventoryElements(false);
    }

    // Méthode pour désactiver les éléments avec le tag "Inventaire"
    void DisableInventoryElements(bool present)
    {

        // Parcourir chaque élément et le désactiver
        foreach (GameObject element in inventoryElements)
        {
            element.SetActive(present);
        }
    }
}
