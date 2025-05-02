using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Transform target;

    [SerializeField] private Collider2D ShipDetector;

    [SerializeField] private PlayerSelection playerSelection;

    
    UnityEngine.AI.NavMeshAgent agent;

    internal Vector3 velocity = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {


        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateUpAxis = false;
        agent.updateRotation = false;

    }

    // Update is called once per frame
    public void Update()
    {
        Debug.Log($"Enemy Position: {playerSelection.playerTransform}"); // Log enemy position
        target = playerSelection.playerTransform; // Assign the player's transform to the variable
        // CheckShipDetector();
        agent.SetDestination(target.position);
        // FaceTarget();
        
        velocity = agent.velocity;
        velocity.z = 0;
    }

    
    
}
