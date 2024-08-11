using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDatabase : MonoBehaviour
{
    public static ItemsDatabase instance;
    public Item[] allitems;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de  ItemsDatabase dans la scene ");
            return;
        }
        instance = this;
    }
}
