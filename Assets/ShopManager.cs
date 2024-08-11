using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text PNJNameText;
    public Animator animator;

    public GameObject sellButtonPrefab;
    public Transform sellButtons;

    public static ShopManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d une instance de Shop Manager dans la scene ");
            return;
        }

        instance = this;
    }

    public void OpenShop(Item[] items, string pnjName) 
    {
        PNJNameText.text = pnjName;
        UpdateItemsToSell(items);
        animator.SetBool("IsOpen", true);
    }

    void UpdateItemsToSell(Item[] items) 
    {
        // Supprime les boutons deja present 
        for (int i = 0; i < sellButtons.childCount; i++)
        {
            Destroy(sellButtons.GetChild(i).gameObject);
        }
        // ajoute pour chaque item dans le shop 
        for (int i = 0; i < items.Length; i++)
        {
            GameObject button = Instantiate(sellButtonPrefab, sellButtons);
            SellButtonItem buttonScript = button.GetComponent<SellButtonItem>();
            buttonScript.ItemName.text = items[i].name;
            buttonScript.itemImage.sprite = items[i].image;
            buttonScript.itemPrice.text = items[i].Price.ToString();

            buttonScript.item = items[i];

            button.GetComponent<Button>().onClick.AddListener(delegate { buttonScript.BuyItem(); });
        }
    } 

    public void CloseShop()
    {
        animator.SetBool("IsOpen", false);
    }
}
