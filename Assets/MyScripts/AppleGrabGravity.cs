using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AppleGrabGravity : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    public AudioClip dropSound; // Efecto de sonido cuando la manzana cae
    private AudioSource audioSource; // Componente AudioSource para reproducir sonidos

    void Start()
    {
        // Obtener el componente XRGrabInteractable
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Obtener el Rigidbody de la manzana
        rb = GetComponent<Rigidbody>();

        // Añadir un componente AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        // Suscribirse al evento de "OnSelectEntered" (cuando se agarra el objeto)
        grabInteractable.selectEntered.AddListener(OnGrab);

        // Suscribirse al evento de "OnSelectExited" (cuando se suelta el objeto)
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    // Método que se ejecuta cuando el objeto es agarrado
    private void OnGrab(SelectEnterEventArgs args)
    {
        // Desactivar la gravedad mientras el objeto es agarrado (opcional)
        rb.useGravity = false;
    }

    // Método que se ejecuta cuando el objeto es soltado
    private void OnRelease(SelectExitEventArgs args)
    {
        // Activar la gravedad cuando el usuario suelta la manzana
        rb.useGravity = true;
    }

    // Método que se ejecuta cuando la manzana colisiona con otro objeto
    private void OnCollisionEnter(Collision collision)
    {
        // Reproducir el sonido de caída si el objeto está cayendo
        if (rb.useGravity)
        {
            audioSource.PlayOneShot(dropSound);
        }
    }

    private void OnDestroy()
    {
        // Asegurarse de quitar la suscripción a los eventos para evitar errores
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
