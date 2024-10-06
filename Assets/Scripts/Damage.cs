using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int DMG = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(DMG);
            Debug.Log("Daño infligido a " + collision.gameObject.name + ": " + DMG);
        }
    }
}