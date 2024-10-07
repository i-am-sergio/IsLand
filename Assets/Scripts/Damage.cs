using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int DMG = 10;  // Daño infligido
    public Collider playerCollider;  // Asignar el collider del jugador (debe ser el Collider del jugador)
    private CapsuleCollider capsuleCollider;  // El collider del objeto que infligirá daño
    private Coroutine damageCoroutine;

    private void Start()
    {
        // Asegúrate de que el CapsuleCollider esté marcado como trigger
        capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider != null)
        {
            capsuleCollider.isTrigger = true;
        }
        else
        {
            Debug.LogError("Este objeto no tiene un CapsuleCollider.");
        }
    }

    // Detecta cuando el jugador entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            Debug.Log("Jugador ha entrado en el área de daño.");
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(InflictDamageOverTime());
            }
        }
    }

    // Detecta cuando el jugador sale del trigger
    private void OnTriggerExit(Collider other)
    {
        if (other == playerCollider)
        {
            Debug.Log("Jugador ha salido del área de daño.");
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    // Corutina para infligir daño de manera continua mientras el jugador esté en el área de daño
    private IEnumerator InflictDamageOverTime()
    {
        while (true)
        {
            Health health = playerCollider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(DMG);
                Debug.Log("Daño infligido al jugador: " + DMG);
            }
            yield return new WaitForSeconds(3f);  // Intervalo entre cada daño
        }
    }
}
