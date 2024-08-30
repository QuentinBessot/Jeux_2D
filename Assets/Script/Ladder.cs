using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ladder : MonoBehaviour
{
    private bool isInRange;
    private Move_Players playerMovement;
    public BoxCollider2D collider;
    public Text interactUI;

    private void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Move_Players>();
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
        if (isInRange && playerMovement.isClimbing && Input.GetKeyDown(Move_Players.instance.interact))
        {
            playerMovement.isClimbing = false;
            collider.isTrigger = false;
            return;
        }

        if(isInRange && Input.GetKeyDown(Move_Players.instance.interact))
        { 
            playerMovement.isClimbing = true;
            collider.isTrigger = true;
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
            isInRange = false;
            playerMovement.isClimbing = false;
            collider.isTrigger = false;
            interactUI.enabled = false;
        }
    }
}
