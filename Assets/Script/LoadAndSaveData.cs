using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadAndSaveData : MonoBehaviour
{


    public static LoadAndSaveData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de  LoadAndSaveData dans la scene ");
            return;
        }
        instance = this;

    }
        // Start is called before the first frame update
    void Start()
    {
        Inventory.instance.coinsCount = PlayerPrefs.GetInt("coinsCount", 0);
        Inventory.instance.UpdateTextUI();

        string[] itemsSaved = PlayerPrefs.GetString("items", "").Split(',');

        for (int i = 0; i < itemsSaved.Length; i++)
        {
            if (itemsSaved[i] != "")
            {
                int id = int.Parse(itemsSaved[i]);
                Item currentItem = ItemsDatabase.instance.allitems.Single(x => x.id == id);
                Inventory.instance.content.Add(currentItem);
            }
        }

        Inventory.instance.UpdateTextUI();
    }


    public void SaveData() 
    {
        PlayerPrefs.SetInt("coinsCount",Inventory.instance.coinsCount);
        
        if(CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1 ) ) 
        {
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
        }

        // sauvegarde des items

        string itemsInInventory = string.Join(",",Inventory.instance.content.Select(x => x.id));
        PlayerPrefs.SetString("items",itemsInInventory);
    }

}
