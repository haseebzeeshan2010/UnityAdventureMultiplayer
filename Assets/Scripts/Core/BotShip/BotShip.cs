using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class BotShip : NetworkBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Transform target;

    [SerializeField] private float speed = 5f; // Speed of the bot ship

    [SerializeField] private float turnSpeed = 30f; // Turning rate of the bot ship
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector2 directionToTarget = ((Vector2)target.position - (Vector2)transform.position).normalized;
            float angleDiff = Vector2.SignedAngle(transform.up, directionToTarget);
            rb.AddTorque(angleDiff * turnSpeed * Time.deltaTime, ForceMode2D.Force);
        }
        //add code underneath to make the bot ship move towards the target
        if (target != null)
        {
            rb.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Force);
        }
    }
}
