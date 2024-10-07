using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100; // Valor inicial de salud
    public GameObject deathPanel; // Panel de muerte
    public GameObject dangerPanel; // Panel de peligro
    private PlayerRestart playerRestart;

    private void Start()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
        if (dangerPanel != null)
        {
            dangerPanel.SetActive(false);
        }
        // Buscar el script de PlayerRestart en el jugador
        playerRestart = GetComponent<PlayerRestart>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " ha recibido " + damage + " de daño. Salud restante: " + health);
        if (dangerPanel != null)
        {
            Debug.Log("Despliege de Danger");
            dangerPanel.SetActive(true);
            StartCoroutine(ShakeScreen());
        }
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
    private IEnumerator ShakeScreen()
    {
        // Aquí puedes ajustar la duración y la intensidad del temblor
        float duration = 0.5f;
        float magnitude = 0.1f;
        Vector3 originalPosition = Camera.main.transform.localPosition;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = originalPosition; // Restablecer la posición original
        if (dangerPanel != null)
        {
            dangerPanel.SetActive(false); // Desactivar el panel de peligro después del temblor
        }
    }
}
