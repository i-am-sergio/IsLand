using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LogPickup : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    public GameObject[] zonasFogata; // Zonas donde se puede hacer la fogata
    public GameObject markerPrefab; // Prefab para marcar la zona de fogata
    private List<GameObject> markers = new List<GameObject>(); // Para guardar las zonas visibles

    void Start()
    {
        // Obtener el componente XRGrabInteractable
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Suscribirse al evento de "OnSelectEntered" (cuando se agarra el objeto)
        grabInteractable.selectEntered.AddListener(OnGrab);

        // Suscribirse al evento de "OnSelectExited" (cuando se suelta el objeto)
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    // Método que se ejecuta cuando el objeto es agarrado
    private void OnGrab(SelectEnterEventArgs args)
    {
        ShowFogataZones(true); // Muestra las zonas de fogata
    }

    // Método que se ejecuta cuando el objeto es soltado
    private void OnRelease(SelectExitEventArgs args)
    {
        ShowFogataZones(false); // Oculta las zonas de fogata
    }

    // Método para mostrar/ocultar las zonas de fogata
    private void ShowFogataZones(bool show)
    {
        foreach (GameObject zona in zonasFogata)
        {
            if (show)
            {
                GameObject marker = Instantiate(markerPrefab, zona.transform.position, Quaternion.identity);
                markers.Add(marker); // Guardar el marcador
            }
            else
            {
                // Destruir los marcadores si no se deben mostrar
                foreach (GameObject marker in markers)
                {
                    Destroy(marker);
                }
                markers.Clear(); // Limpiar la lista de marcadores
            }
        }
    }

    private void OnDestroy()
    {
        // Asegurarse de quitar la suscripción a los eventos para evitar errores
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
