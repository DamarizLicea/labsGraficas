using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilController : MonoBehaviour
{
    public float tiempoDeVida = 5f; // Tiempo de vida del proyectil
    public float tiempoDeCreacion; // Tiempo en que se creó el proyectil

    void Start()
{
    tiempoDeCreacion = Time.time;
    Destroy(gameObject, tiempoDeVida - (Time.time - tiempoDeCreacion));
}

    void Update()
    { 

    }
}