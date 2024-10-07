using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;  // Importar el XR Interaction Toolkit

public class SwordInteraction : MonoBehaviour
{
    private AudioSource audioSource;  // Fuente de audio para los efectos de sonido
    private XRGrabInteractable swordGrabInteractable;  // Referencia al XRGrabInteractable de la espada para verificar si está siendo agarrada

    // Start is called before the first frame update
    void Start()
    {
        // Deshabilitar la interacción para todos los cangrejos al inicio
        GameObject[] crabs = GameObject.FindGameObjectsWithTag("Crab");
        foreach (GameObject crab in crabs)
        {
            XRGrabInteractable crabGrabInteractable = crab.GetComponent<XRGrabInteractable>();
            if (crabGrabInteractable != null)
            {
                crabGrabInteractable.enabled = false;  // Desactivar la interacción al inicio
            }
        }

        // Obtener el componente AudioSource de la espada
        audioSource = GetComponent<AudioSource>();

        // Obtener el XRGrabInteractable de la espada
        swordGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificamos si la espada está siendo agarrada y si estamos colisionando con un cangrejo
        if (collision.gameObject.CompareTag("Crab") && swordGrabInteractable.isSelected)
        {
            // Obtener los componentes del cangrejo
            XRGrabInteractable crabGrabInteractable = collision.gameObject.GetComponent<XRGrabInteractable>();
            Rigidbody crabRb = collision.gameObject.GetComponent<Rigidbody>();
            Collider crabCollider = collision.gameObject.GetComponent<Collider>();

            // Si el cangrejo no está clavado, lo clavamos
            if (crabGrabInteractable != null && crabRb != null && crabCollider != null)
            {
                StickCrab(collision.gameObject, crabGrabInteractable, crabRb, crabCollider);
            }
        }
    }

    void StickCrab(GameObject crab, XRGrabInteractable crabGrabInteractable, Rigidbody crabRb, Collider crabCollider)
    {
        // Dejar de moverse el cangrejo
        CrabMovement crabMovement = crab.GetComponent<CrabMovement>();
        if (crabMovement != null)
        {
            crabMovement.ImpaleCrab();  // Detener el movimiento del cangrejo
        }

        // Pegamos el cangrejo a la espada
        crab.transform.SetParent(this.transform);
        crab.transform.localPosition = new Vector3(0, -0.4f, -0.1f);  // Ajustamos la posición del cangrejo en la espada

        // Ignorar colisiones entre el cangrejo y la espada, pero dejar el Collider activo
        Physics.IgnoreCollision(crabCollider, GetComponent<Collider>(), true);  // Ignorar colisión con la espada

        // Configuramos las físicas del cangrejo
        crabRb.isKinematic = true;  // Lo hacemos kinematic para que no siga interactuando con las físicas

        // Activamos la interacción con el cangrejo para que pueda ser agarrado
        crabGrabInteractable.enabled = true;  // Activamos la interacción
        crabGrabInteractable.selectExited.AddListener(delegate { OnCrabReleased(crab, crabRb, crabCollider); });  // Listener para cuando el cangrejo es soltado

        // Reproducir el sonido de clavar la espada
        if (audioSource != null)
        {
            audioSource.Play();  // Reproducir el sonido
        }
    }

    // Esta función se llama cuando el cangrejo es soltado
    void OnCrabReleased(GameObject crab, Rigidbody crabRb, Collider crabCollider)
    {
        // Reactivar las colisiones al ser soltado
        Physics.IgnoreCollision(crabCollider, GetComponent<Collider>(), false);  // Reactivar colisiones con la espada

        // Restaurar las físicas del cangrejo
        crabRb.isKinematic = false;  // El cangrejo vuelve a interactuar con las físicas

        // Desvincular el cangrejo de la espada
        crab.transform.SetParent(null);  // El cangrejo deja de ser hijo de la espada
    }
}
