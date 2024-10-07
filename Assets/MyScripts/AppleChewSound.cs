using UnityEngine;

public class AppleChewSound : MonoBehaviour
{
    public AudioClip chewSound; // Efecto de sonido de masticar
    private AudioSource audioSource;

    public float chewDistance = 1.5f; // Distancia a la que se activará el sonido de masticar
    public float chewDuration = 5.0f; // Duración del tiempo que la manzana estará "comiendo"

    private bool isChewing = false; // Bandera para indicar si se está comiendo

    private void Start()
    {
        // Añadir un componente AudioSource al objeto
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        CheckIfCanChew(); // Verifica si la manzana está cerca de la cámara
    }

    // Método para verificar la proximidad a la cámara
    private void CheckIfCanChew()
    {
        // Obtener la posición de la cámara
        Vector3 cameraPosition = Camera.main.transform.position;

        // Comprobar la distancia entre la manzana y la cámara
        float distanceToCamera = Vector3.Distance(transform.position, cameraPosition);

        // Si está lo suficientemente cerca y no se ha reproducido el sonido aún
        if (distanceToCamera <= chewDistance)
        {
            // Reproducir el sonido de masticar si no se ha reproducido ya
            if (!audioSource.isPlaying && chewSound != null && !isChewing)
            {
                audioSource.PlayOneShot(chewSound);
                isChewing = true; // Iniciar el estado de masticar
                Invoke("DestroyApple", chewDuration); // Programar la destrucción después de 5 segundos
            }
        }
    }

    // Método para destruir la manzana
    private void DestroyApple()
    {
        Destroy(gameObject); // Destruir el objeto de la manzana
    }
}
