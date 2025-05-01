using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] private float rotationLerpSpeed = 5f;  // Add this at the class level

    [SerializeField] private Collider2D ShipDetector;

    [SerializeField] private PlayerSelection playerSelection;
    UnityEngine.AI.NavMeshAgent agent;
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
        FaceTarget();
        
        
    }
    
    // public void CheckShipDetector()
    // {
    //     ContactFilter2D filter = new ContactFilter2D();
    //     Collider2D[] results = new Collider2D[10];
    //     int numColliders = ShipDetector.Overlap(filter, results);
        
    //     Debug.Log($"Number of objects detected: {numColliders}");
    // }

    void FaceTarget() {
        var vel = agent.velocity;
        vel.z = 0;

        if (vel != Vector3.zero) {
            

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(Vector3.forward, vel),
                Time.deltaTime * rotationLerpSpeed
            );
        }
    }



}
