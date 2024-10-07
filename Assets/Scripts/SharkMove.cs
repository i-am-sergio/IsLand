using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    public Transform[] waypoints; // Array para los puntos de ruta
    public float speed = 5f;  // Velocidad del tiburón
    private int currentWaypointIndex = 0; // Índice del waypoint actual

    public AudioSource sharkSound;  // Para el sonido del tiburón
    private float timeBetweenSounds = 3f;  // Tiempo entre sonidos
    private float soundTimer = 0f;  // Temporizador interno

    public Collider areaCollider; // El collider que define el área donde se debe activar el sonido
    public Collider playerCollider; // El collider del jugador

    private bool isPlayerInArea = false; // Variable para controlar si el jugador está dentro del área

    void Start()
    {
        sharkSound = GetComponent<AudioSource>(); // Obtener el AudioSource
    }

    void Update()
    {
        if (waypoints.Length == 0) return; // Si no hay waypoints, salir

        // Mover el tiburón hacia el siguiente waypoint
        Transform target = waypoints[currentWaypointIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Si el tiburón está cerca del waypoint, pasar al siguiente
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Cicla entre los waypoints
        }

        // Verificar si el jugador está dentro del área grande
        if (areaCollider.bounds.Intersects(playerCollider.bounds))
        {
            if (!isPlayerInArea)
            {
                isPlayerInArea = true; // El jugador ha entrado al área
                soundTimer = 0f; // Reiniciar temporizador
            }

            // Controlar el sonido del tiburón
            soundTimer += Time.deltaTime;
            if (soundTimer >= timeBetweenSounds && !sharkSound.isPlaying)
            {
                sharkSound.Play();  // Reproducir sonido
            }
        }
        else
        {
            if (isPlayerInArea)
            {
                isPlayerInArea = false; // El jugador ha salido del área
                if (sharkSound.isPlaying)
                {
                    sharkSound.Stop(); // Detener el sonido cuando el jugador salga
                }
            }
        }
    }
}
