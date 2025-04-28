using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] private float rotationLerpSpeed = 5f;  // Add this at the class level
    UnityEngine.AI.NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateUpAxis = false;
        agent.updateRotation = false;

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        FaceTarget();
    }

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
