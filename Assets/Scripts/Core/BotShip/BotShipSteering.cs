using UnityEngine;
using UnityEngine.AI;



public class BotShipSteering : MonoBehaviour
{

    [SerializeField] private float rotationLerpSpeed = 5f;  // Add this at the class level

    [SerializeField] private Enemy enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget();
    }


    void FaceTarget() {
        var vel = enemy.velocity;
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
