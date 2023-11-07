using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This player controller class will update the events from the vehicle player.
/// Standar coding documentarion can be found in 
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class PlayerController : MonoBehaviour
{   


    // Variables de movimiento del jugador
    // <value> speed: Velocidad del vehículo </value>
    // <value> turnSpeed: Velocidad de giro del vehículo </value>
    public float speed = 5.0f;
    public float turnSpeed = 0.0f;
    public float horizontalInput;
    public float forwardInput;

    //Variabkes de la cámara

    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;
    public string inputId;

    /// <summary> Metodo Start: Se ejecuta una vez al inicio del juego </summary>

    private void Start()
    {
    }

    /// <summary> Metodo Update: Se ejecuta una vez por frame </summary>

    private void Update()
    {
        // Movimiento del vehículo según el id del jugador, partir el teclado.
        horizontalInput = Input.GetAxis("Horizontal" + inputId);
        forwardInput = Input.GetAxis("Vertical" + inputId);

        // Movimiento del vehículo adelante y rotación
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        // Cambio entre cámaras
        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }

}