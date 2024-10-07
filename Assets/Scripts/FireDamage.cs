using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public int fireDMG = 10;
    public Collider playerCollider; // Asignar el collider del jugador
    private List<FireCampOnTrigger> fireCamps; // Lista de fogatas
    private Collider fireCollider; // El collider del fuego
    private Coroutine damageCoroutine;

    private void Start()
    {
        GameObject[] fireCampObjects = GameObject.FindGameObjectsWithTag("Campfires");
        fireCamps = new List<FireCampOnTrigger>();
        foreach (GameObject fireCampObject in fireCampObjects)
        {
            FireCampOnTrigger fireCampScript = fireCampObject.GetComponent<FireCampOnTrigger>();
            if (fireCampScript != null)
            {
                fireCamps.Add(fireCampScript);
            }
        }
        fireCollider = GetComponent<Collider>();
    }


    private void Update()
    {
        bool isPlayerInFireZone = false;
        foreach (FireCampOnTrigger fireCamp in fireCamps)
        {
            if (fireCamp.isFireOn)
            {
                if (fireCollider.bounds.Intersects(playerCollider.bounds))
                {
                    isPlayerInFireZone = true;
                    if (damageCoroutine == null)
                    {
                        damageCoroutine = StartCoroutine(InflictFireDamageOverTime());
                    }
                    break;
                }
            }
        }
        if (!isPlayerInFireZone)
        {
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
                Debug.Log("Daño infligido por fuego: " + fireDMG);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
