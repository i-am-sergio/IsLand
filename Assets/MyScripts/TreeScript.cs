using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public GameObject applePrefab; // El modelo de la manzana que instanciarás
    public Transform spawnPoint;   // El punto desde el cual caerán las manzanas

    // Detecta cuando el jugador entra en el Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Asegúrate de que solo el jugador pueda activar el evento
        if (other.CompareTag("Player"))
        {
            // Instanciar la manzana en el punto de caída y aplicar física
            GameObject apple = Instantiate(applePrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = apple.GetComponent<Rigidbody>();

            // Si la manzana tiene un Rigidbody, aplicar gravedad para que caiga
            if (rb != null)
            {
                rb.useGravity = true;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
