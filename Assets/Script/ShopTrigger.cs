using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTrigger : MonoBehaviour
{

    private bool isInRange = false;
    public KeyBindingManager keyBindingManager;

    private Text interactUI;
    public string pnjName;
    public Item[] itemsToSell;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(keyBindingManager.movePlayers.interact))
        {
            ShopManager.instance.OpenShop(itemsToSell, pnjName);
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
            ShopManager.instance.CloseShop();
            // DialogueManager.instance.EndDialogue();
        }
    }
}
