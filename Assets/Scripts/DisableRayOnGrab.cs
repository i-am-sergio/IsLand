using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableRayOnGrab : MonoBehaviour
{
    public XRRayInteractor rayInteractor;  // Asigna el XRRayInteractor (la mano)
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Escucha eventos de cuando el objeto es agarrado o soltado
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Desactiva el rayo cuando el objeto es agarrado
        if (rayInteractor != null)
        {
            rayInteractor.gameObject.SetActive(false);
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Activa el rayo cuando el objeto es soltado
        if (rayInteractor != null)
        {
            rayInteractor.gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        // Asegura que los listeners sean removidos
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}