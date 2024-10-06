using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSleep : MonoBehaviour
{
    public Dormir dormirScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador detectado en la zona de dormir.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador salió de la zona de dormir.");
        }
    }
}