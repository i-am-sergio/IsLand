using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100; // Valor inicial de salud

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " ha recibido " + damage + " de daño. Salud restante: " + health);
        if (health <= 0)
        {
            Debug.Log(gameObject.name + " ha sido destruido.");
            Destroy(gameObject, 0.3f);
        }
    }
}
