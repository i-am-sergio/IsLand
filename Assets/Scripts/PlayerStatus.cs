using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public Vector3 lastSleepPosition;

    
    public void UpdateSleepPosition(Vector3 sleepPosition)// Método para actualizar la posición del lugar donde el jugador durmió
    {
        lastSleepPosition = sleepPosition;
        Debug.Log("Último lugar de descanso actualizado: " + lastSleepPosition);
    }
}
