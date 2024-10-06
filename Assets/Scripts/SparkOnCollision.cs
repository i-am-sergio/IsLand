using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkOnCollision : MonoBehaviour
{
    public ParticleSystem sparks;
    public bool isSparking = false; // Bool para saber si el objeto está generando chispas

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FrictionObject"))
        {
            sparks.Play();
            isSparking = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("FrictionObject"))
        {
            sparks.Stop();
            isSparking = false;
        }
    }
}