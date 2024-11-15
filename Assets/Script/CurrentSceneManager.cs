using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public int coinsPickedUpCount;

    public Vector3 respawnPoint;

    public int levelToUnlock;

    public static CurrentSceneManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de  CurrentSceneManager dans la scene ");
            return;
        }
        instance = this;

        respawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
