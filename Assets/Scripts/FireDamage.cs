using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public int fireDMG = 10;
    public Collider playerCollider; // Asignar el collider del jugador
    private FireCampOnTrigger fireCamp;
    private Collider fireCampCollider;
    private Collider fireCollider; // El collider del fuego
    private Coroutine damageCoroutine;

    private void Start()
    {
        GameObject fireCampObject = GameObject.FindGameObjectWithTag("Campfires");
        if (fireCampObject != null)
        {
            fireCamp = fireCampObject.GetComponent<FireCampOnTrigger>();
            fireCampCollider = fireCampObject.GetComponent<Collider>();
        }

        fireCollider = GetComponent<Collider>(); // El collider del fuego
    }

    private void Update()
    {
        if (fireCamp != null && fireCamp.isFireOn)
        {
            // Verificamos si el fuego est� en contacto tanto con el jugador como con el campfire
            if (fireCampCollider.bounds.Intersects(fireCollider.bounds) &&
                playerCollider.bounds.Intersects(fireCollider.bounds))
            {
                // Si ambas colisiones ocurren, comenzamos a infligir da�o
                if (damageCoroutine == null)
                {
                    damageCoroutine = StartCoroutine(InflictFireDamageOverTime());
                }
            }
            else
            {
                // Si alguna colisi�n se interrumpe, detenemos el da�o
                if (damageCoroutine != null)
                {
                    StopCoroutine(damageCoroutine);
                    damageCoroutine = null;
                }
            }
        }
        else
        {
            // Si el fuego est� apagado, nos aseguramos de detener cualquier coroutine activa
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator InflictFireDamageOverTime()
    {
        while (true)
        {
            Health health = playerCollider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(fireDMG);
                Debug.Log("Da�o infligido por fuego: " + fireDMG);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
