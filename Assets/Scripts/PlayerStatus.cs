using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public Vector3 lastSleepPosition;

    
    public void UpdateSleepPosition(Vector3 sleepPosition)// M�todo para actualizar la posici�n del lugar donde el jugador durmi�
    {
        lastSleepPosition = sleepPosition;
        Debug.Log("�ltimo lugar de descanso actualizado: " + lastSleepPosition);
    }
}
