using UnityEngine;
using UnityEngine.AI;

public class IguanaAIController : MonoBehaviour {
    public Transform[] waypoints; // Puntos por los que patrulla
    private int currentWaypointIndex = 0;
    private NavMeshAgent navAgent; // Para el movimiento autónomo de la iguana
    private IguanaCharacter iguanaCharacter;
    public Transform player; // Jugador o Main Camera que la iguana detectará
    public float detectionRadius = 10f; // Radio de detección
    public float attackRange = 2f; // Distancia para iniciar ataque

    private bool isPlayerDetected = false;

    void Start() {
        navAgent = GetComponent<NavMeshAgent>();
        iguanaCharacter = GetComponent<IguanaCharacter>();
        navAgent.autoBraking = false; // Para que la iguana no se frene en cada waypoint
        GotoNextWaypoint();
    }

    void GotoNextWaypoint() {
        if (waypoints.Length == 0) return;
        navAgent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void Update() {
        // Patrullaje entre waypoints
        if (!isPlayerDetected && !navAgent.pathPending && navAgent.remainingDistance < 0.5f) {
            GotoNextWaypoint();
        }

        // Detectar al jugador
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer < detectionRadius) {
            isPlayerDetected = true;
            navAgent.destination = player.position;

            // Si está cerca, atacar
            if (distanceToPlayer < attackRange) {
                iguanaCharacter.Attack();
            }
        } else {
            isPlayerDetected = false; // Si está fuera del radio, vuelve a patrullar
        }

        // Movimiento
        float speed = navAgent.velocity.magnitude;
        iguanaCharacter.Move(speed, 0);
    }
}
