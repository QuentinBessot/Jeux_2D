using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Nuage : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Vitesse de d�filement
    private Vector2 startPosition;   // Position initiale du nuage

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculer le d�placement
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, 20); // R�p�ter la position pour un mouvement continu
        transform.position = startPosition + Vector2.right * newPosition;
    }
}
