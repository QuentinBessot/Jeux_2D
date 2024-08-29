using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Nuage : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Vitesse de défilement
    private Vector2 startPosition;   // Position initiale du nuage

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculer le déplacement
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, 20); // Répéter la position pour un mouvement continu
        transform.position = startPosition + Vector2.right * newPosition;
    }
}
