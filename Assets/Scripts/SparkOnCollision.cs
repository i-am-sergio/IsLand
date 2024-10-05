using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class SparkOnCollision : MonoBehaviour
{
    public ParticleSystem sparks; // Asigna aquí el Particle System de chispas

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FrictionObject")) // Asegúrate de que el otro objeto tenga la etiqueta correcta
        {
            // Activa las chispas cuando la colisión comienza
            sparks.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("FrictionObject"))
        {
            // Detén las chispas cuando termina la colisión
            sparks.Stop();
        }
    }
}

