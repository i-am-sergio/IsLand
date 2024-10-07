using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100; // Valor inicial de salud
    public GameObject deathPanel; // Panel de muerte
    private PlayerRestart playerRestart;

    private void Start()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
        // Buscar el script de PlayerRestart en el jugador
        playerRestart = GetComponent<PlayerRestart>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " ha recibido " + damage + " de daño. Salud restante: " + health);
        if (health <= 0)
        {
            Debug.Log(gameObject.name + " ha sido destruido.");
            if (deathPanel != null)
            {
                deathPanel.SetActive(true); // Activar el panel de muerte
            }
        }
    }

    public void RestartGame()
    {
        if (playerRestart != null)
        {
            Debug.Log("Reiniciando el juego...");
            playerRestart.RespawnPlayer(); // Llamar al método para reaparecer
        }
        else
        {
            Debug.LogWarning("PlayerRestart script no encontrado en el jugador.");
        }

        // Ocultar el panel de muerte y reiniciar la salud
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }

        // Restaurar la salud del jugador
        health = 100;
        Debug.Log(gameObject.name + " ha sido resucitado.");
    }
}
