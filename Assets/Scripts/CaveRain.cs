using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveRain : MonoBehaviour
{
    public ParticleSystem rainParticleSystem; // Asignar el sistema de part�culas de lluvia
    public Collider playerCollider; // Asignar el collider del jugador
    private Collider caveCollider; // El collider de la cueva
    public TimeManager timeManager; // Referencia al TimeManager para controlar la lluvia

    private void Start()
    {
        caveCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        // Verificar si es de noche y el estado de la lluvia desde el TimeManager
        if (timeManager.statusRain)
        {
            if (caveCollider.bounds.Intersects(playerCollider.bounds))
            {
                // Si el jugador est� en la cueva y la lluvia est� activa, detener la lluvia
                if (rainParticleSystem.isPlaying)
                {
                    rainParticleSystem.Stop();
                    Debug.Log("Lluvia desactivada dentro de la cueva.");
                }
            }
            else
            {
                // Si el jugador est� fuera de la cueva y es de noche, reactivar la lluvia
                if (!rainParticleSystem.isPlaying)
                {
                    rainParticleSystem.Play();
                    Debug.Log("Lluvia reactivada fuera de la cueva.");
                }
            }
        }
        else
        {
            // Si no es de noche, asegurarse de que la lluvia no est� activa
            if (rainParticleSystem.isPlaying)
            {
                rainParticleSystem.Stop();
                Debug.Log("Lluvia desactivada porque no es de noche.");
            }
        }
    }
}
