using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcoMovimiento : MonoBehaviour
{
    public Transform[] waypoints; // Array para almacenar los puntos
    public float velocidad = 5f;  // Velocidad del barco
    private int indiceActual = 0; // Índice del waypoint actual

    public AudioSource bocina;  // Para el sonido de la bocina
    private float tiempoEntreSonidos = 5f;  // Tiempo entre bocinazos
    private float temporizador = 0f;  // Temporizador interno

    void Start()
    {
        bocina = GetComponent<AudioSource>(); // Obtener el AudioSource
    }
    void Update()
    {
        if (waypoints.Length == 0) return; // Si no hay waypoints, salir

        // Mover el barco hacia el siguiente waypoint
        Transform objetivo = waypoints[indiceActual];
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime;

        // Si el barco está cerca del waypoint, pasa al siguiente
        if (Vector3.Distance(transform.position, objetivo.position) < 0.1f)
        {
            indiceActual = (indiceActual + 1) % waypoints.Length; // Cicla a través de los waypoints
        }



        // Controlar el sonido de la bocina
        temporizador += Time.deltaTime;
        if (temporizador >= tiempoEntreSonidos)
        {
            bocina.Play();  // Reproducir sonido de bocina
            temporizador = 0f;  // Reiniciar temporizador
        }
    }
}
