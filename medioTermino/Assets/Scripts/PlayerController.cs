using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    // Variables para el disparo
    public GameObject proyectilPrefab;
    public Transform puntoDeDisparo;
    public TextMeshProUGUI contadorProyectilesText;
    private int contadorProyectiles = 0;
    private int maxProyectiles = 70;
    private bool disparoRealizado = false;

    // Variables para el movimiento
    public float speed = 5.0f;
    public float turnSpeed = 0.0f;
    public float horizontalInput;
    public float forwardInput;


    // Inicio de la secuencia de coreografías, actualizacion de contador de proyectiles
    void Start()
    {
        StartCoroutine(EjecutarSecuenciaDeCoreografias());
        ActualizarContadorProyectiles();
    }

    // Metodo para manejo de ejecucion de todas las coreografias

    IEnumerator EjecutarSecuenciaDeCoreografias()
    {
       Debug.Log("Comenzando ejecución de coreografías");
        yield return StartCoroutine(EjecutarCoreografia(Coreografia1(), 10));
        Debug.Log("Terminada Coreografia1, tiempo actual: " + Time.time);
        yield return StartCoroutine(EjecutarCoreografia(Coreografia2(), 10));
        Debug.Log("Terminada Coreografia2, tiempo actual: " + Time.time);
        yield return StartCoroutine(EjecutarCoreografia(Coreografia3(), 10));
        Debug.Log("Terminada Coreografia3, tiempo actual: " + Time.time);
    }

    // Metodo para manejo de ejecucion de coreografias individuales

    IEnumerator EjecutarCoreografia(IEnumerator coreografia, float duracion)
    {
        float startTime = Time.time;

        while (Time.time - startTime < duracion)
        {
            yield return StartCoroutine(coreografia);
        }
    }

    void Update()
    {
        if (!disparoRealizado)
        {
            DispararProyectil();
            disparoRealizado = true;
        }

        // Visto en el lab de carros

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        Vector3 movimiento = new Vector3(horizontalInput, 0, forwardInput);
        transform.Translate(movimiento * Time.deltaTime * speed);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        LimpiarProyectiles();
    }

    // Instancia proyectiles, actualiza contador de proyectiles, elimina proyectiles mas antiguos, 
    void DispararProyectil()
    {
        GameObject nuevoProyectil = Instantiate(proyectilPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation);
        contadorProyectiles++;

        Rigidbody proyectilRigidbody = nuevoProyectil.GetComponent<Rigidbody>();
        proyectilRigidbody.velocity = puntoDeDisparo.forward * 10f; // Ajusta la velocidad según tus necesidades
        proyectilRigidbody.GetComponent<ProyectilController>().tiempoDeCreacion = Time.time;

        if (contadorProyectiles > maxProyectiles)
        {
            EliminarProyectilMasAntiguo();
        }

        ActualizarContadorProyectiles();
    }

    // Elimina proyectiles que hayan sobrepasado su tiempo de vida
   void LimpiarProyectiles()
{
    GameObject[] proyectiles = GameObject.FindGameObjectsWithTag("Proyectil");

    foreach (GameObject proyectil in proyectiles)
    {
        ProyectilController proyectilController = proyectil.GetComponent<ProyectilController>();

        if (proyectilController != null && Time.time >= proyectilController.tiempoDeVida + 8f)
        {
            Destroy(proyectil);
            contadorProyectiles--;
        }
    }

    ActualizarContadorProyectiles();
}

    // Elimina el proyectil mas antiguo, no se ve porque sino borra todo
    void EliminarProyectilMasAntiguo()
{
    GameObject[] proyectiles = GameObject.FindGameObjectsWithTag("Proyectil");

    if (proyectiles.Length > 0)
    {
        GameObject proyectilMasAntiguo = proyectiles[0];
        float tiempoMasAntiguo = proyectilMasAntiguo.GetComponent<ProyectilController>().tiempoDeCreacion;

        foreach (GameObject proyectil in proyectiles)
        {
            ProyectilController proyectilController = proyectil.GetComponent<ProyectilController>();

            if (proyectilController != null && proyectilController.tiempoDeCreacion < tiempoMasAntiguo)
            {
                tiempoMasAntiguo = proyectilController.tiempoDeCreacion;
                proyectilMasAntiguo = proyectil;
            }
        }

        Destroy(proyectilMasAntiguo);
        contadorProyectiles--;

        ActualizarContadorProyectiles();
    }
}

    // Actualiza el contador de proyectiles
    void ActualizarContadorProyectiles()
    {
        if (contadorProyectiles <= 0)
        {
            contadorProyectiles = 0;
        }

        contadorProyectilesText.text = "Proyectiles: " + contadorProyectiles;
        //Debug.Log("Proyectiles: " + contadorProyectiles);
    }

    // Coreografias
    IEnumerator Coreografia1()
    {
        float startTime = Time.time;

        while (Time.time - startTime < 10f)
        {
            Debug.Log("Ejecutando Coreografia1, tiempo actual: " + Time.time);
            LimpiarProyectiles();
            for (int j = 0; j < 7; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    float angle = i * 45f;
                    Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
                    SpawnBullet(transform.position, rotation);
                }

                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    IEnumerator Coreografia2()
    {
        float startTime = Time.time;

        while (Time.time - startTime < 10f)
        {
            Debug.Log("Ejecutando Coreografia2, tiempo actual: " + Time.time);
            LimpiarProyectiles();

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 360; i += 15)
                {
                    float angle = i + j * 10f;
                    float radius = j * 2.0f;
                    Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
                    Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, 0f, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
                    SpawnBullet(transform.position + offset, rotation);
                }

                yield return new WaitForSeconds(0.5f);
            }
        }
    }

IEnumerator Coreografia3()
    {
        float startTime = Time.time;

        while (Time.time - startTime < 10f)
        {
            Debug.Log("Ejecutando Coreografia3, tiempo actual: " + Time.time);
            LimpiarProyectiles();

            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 360; i += 30)
                {
                    Quaternion rotation1 = Quaternion.Euler(0f, i + j * 10f, 0f);
                    Quaternion rotation2 = Quaternion.Euler(0f, i + j * 10f + 90f, 0f);
                    SpawnBullet(transform.position, rotation1);
                    SpawnBullet(transform.position, rotation2);
                }

                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    // Auxiliar rotacion, velocidades...
    void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        GameObject nuevoProyectil = Instantiate(proyectilPrefab, position, rotation);
        contadorProyectiles++;

        Rigidbody proyectilRigidbody = nuevoProyectil.GetComponent<Rigidbody>();
        proyectilRigidbody.velocity = rotation * Vector3.forward * 10f; 
        proyectilRigidbody.GetComponent<ProyectilController>().tiempoDeCreacion = Time.time;

        if (contadorProyectiles > maxProyectiles)
        {
            EliminarProyectilMasAntiguo();
        }

        ActualizarContadorProyectiles();
    }
}


