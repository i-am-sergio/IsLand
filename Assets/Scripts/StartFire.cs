using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireIgnition : MonoBehaviour
{
    public ParticleSystem[] fireParticleSystems;  // Array de sistemas de part�culas para las animaciones del fuego
    public string frictionObjectTag = "FrictionObject";  // Tag del objeto que debe activar el fuego
    public float fireDuration = 10.0f;  // Duraci�n en segundos del fuego

    private List<SparkOnCollision> activeFrictionObjects = new List<SparkOnCollision>();  // Lista de objetos que est�n haciendo chispas
    private bool fireStarted = false;  // Para evitar encender el fuego m�s de una vez

    private void Start()
    {
        // Aseg�rate de que los sistemas de part�culas est�n apagados al iniciar
        foreach (ParticleSystem ps in fireParticleSystems)
        {
            ps.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entr� tiene el tag "FrictionObject" y que el fuego a�n no ha sido encendido
        if (other.CompareTag(frictionObjectTag) && !fireStarted)
        {
            SparkOnCollision sparkScript = other.GetComponent<SparkOnCollision>();
            if (sparkScript != null && !activeFrictionObjects.Contains(sparkScript))
            {
                // A�adir a la lista de objetos con chispas
                activeFrictionObjects.Add(sparkScript);
                Debug.Log("Objeto con chispas a�adido.");

                // Verifica si hay al menos dos objetos haciendo chispas
                CheckForFireStart();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sali� tiene el tag "FrictionObject"
        if (other.CompareTag(frictionObjectTag))
        {
            SparkOnCollision sparkScript = other.GetComponent<SparkOnCollision>();
            if (sparkScript != null && activeFrictionObjects.Contains(sparkScript))
            {
                // Remover de la lista de objetos con chispas
                activeFrictionObjects.Remove(sparkScript);
                Debug.Log("Objeto con chispas removido.");
            }
        }
    }

    private void CheckForFireStart()
    {
        // Verifica si al menos dos objetos est�n haciendo chispas activamente
        int activeSparks = 0;
        foreach (SparkOnCollision spark in activeFrictionObjects)
        {
            if (spark.sparks.isPlaying)  // Verifica si las chispas est�n activas
            {
                activeSparks++;
            }
        }

        // Si hay al menos dos objetos haciendo chispas, comienza la corrutina para encender el fuego
        if (activeSparks >= 1)
        {
            Debug.Log("M�nimo de 2 objetos haciendo chispas alcanzado. Encendiendo fuego...");
            StartCoroutine(StartFireWithDelay());
        }
        else
        {
            Debug.Log("No hay suficientes objetos haciendo chispas.");
        }
    }

    private IEnumerator StartFireWithDelay()
    {
        fireStarted = true;

        // Enciende las animaciones de los sistemas de part�culas
        foreach (ParticleSystem ps in fireParticleSystems)
        {
            ps.Play();
        }

        Debug.Log("Fogata encendida.");

        // Espera la duraci�n del fuego antes de apagarlo
        yield return new WaitForSeconds(fireDuration);

        // Apaga las animaciones de los sistemas de part�culas
        foreach (ParticleSystem ps in fireParticleSystems)
        {
            ps.Stop();
        }

        Debug.Log("Fogata apagada.");
    }
}
