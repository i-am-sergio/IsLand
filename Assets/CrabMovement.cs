using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabMovement : MonoBehaviour
{
    public Animator animator;
    public Transform planeTransform;  // Referencia al terreno donde el cangrejo puede moverse
    public float moveSpeed = 2.0f;  // Velocidad de movimiento
    public float changeDirectionTime = 2.0f;  // Tiempo entre cambios de dirección
    public AudioSource walkAudioSource;  // Sonido de caminata

    private Vector3 movementDirection;
    private float timeSinceChange;
    private bool isImpaled = false;  // Indica si el cangrejo ha sido clavado
    private bool isWalking = false;  // Estado para verificar si el cangrejo está caminando
    private Bounds planeBounds;  // Límites del terreno

    void Start()
    {
        // Obtener los límites del terreno (MeshRenderer)
        MeshRenderer planeMeshRenderer = planeTransform.GetComponent<MeshRenderer>();
        if (planeMeshRenderer != null)
        {
            planeBounds = planeMeshRenderer.bounds;
        }

        // Establecer una dirección aleatoria
        SetRandomDirection();
    }

    void Update()
    {
        if (!isImpaled)
        {
            MoveCrab();
        }
        else if (isWalking)
        {
            // Si está clavado y aún se está reproduciendo el sonido de caminata, lo detenemos
            StopWalkingSound();
        }
    }

    void MoveCrab()
    {
        // Mueve el cangrejo en la dirección actual
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);

        // Mantener dentro de los límites
        KeepCrabInBounds();

        // Si no estaba caminando y ahora sí lo está, reproducimos el sonido de caminata
        if (!isWalking && movementDirection != Vector3.zero)
        {
            StartWalkingSound();
        }

        // Cambiar de dirección después de un tiempo
        timeSinceChange += Time.deltaTime;
        if (timeSinceChange >= changeDirectionTime)
        {
            SetRandomDirection();
            timeSinceChange = 0;
        }
    }

    void SetRandomDirection()
    {
        // Establecer una dirección aleatoria
        movementDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

        // Si la dirección es cero (sin movimiento), detener el sonido
        if (movementDirection == Vector3.zero && isWalking)
        {
            StopWalkingSound();
        }
        else
        {
            StartWalkingSound();
        }
    }

    void StartWalkingSound()
    {
        if (walkAudioSource != null && !walkAudioSource.isPlaying)
        {
            walkAudioSource.Play();
            isWalking = true;
        }
    }

    void StopWalkingSound()
    {
        if (walkAudioSource != null && walkAudioSource.isPlaying)
        {
            walkAudioSource.Stop();
            isWalking = false;
        }
    }

    void KeepCrabInBounds()
    {
        // Mantén al cangrejo dentro de los límites del terreno sin cambiar su posición inicial
        Vector3 crabPosition = transform.position;

        // Ajustar los límites según el tamaño real del terreno
        Vector3 planeMin = planeBounds.min;
        Vector3 planeMax = planeBounds.max;

        crabPosition.x = Mathf.Clamp(crabPosition.x, planeMin.x, planeMax.x);
        crabPosition.z = Mathf.Clamp(crabPosition.z, planeMin.z, planeMax.z);

        transform.position = crabPosition;
    }

    public void ImpaleCrab()
    {
        // Esta función será llamada cuando el cangrejo sea clavado
        isImpaled = true;
    }
}
