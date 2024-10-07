using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int DMG = 10;  // Da�o infligido
    public Collider playerCollider;  // Asignar el collider del jugador (debe ser el Collider del jugador)
    private CapsuleCollider capsuleCollider;  // El collider del objeto que infligir� da�o
    private Coroutine damageCoroutine;

    private void Start()
    {
        // Aseg�rate de que el CapsuleCollider est� marcado como trigger
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
            Debug.Log("Jugador ha entrado en el �rea de da�o.");
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
            Debug.Log("Jugador ha salido del �rea de da�o.");
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    // Corutina para infligir da�o de manera continua mientras el jugador est� en el �rea de da�o
    private IEnumerator InflictDamageOverTime()
    {
        while (true)
        {
            Health health = playerCollider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(DMG);
                Debug.Log("Da�o infligido al jugador: " + DMG);
            }
            yield return new WaitForSeconds(3f);  // Intervalo entre cada da�o
        }
    }
}
