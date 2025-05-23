using UnityEngine;
using Unity.Netcode;
public class PlayerMovement : NetworkBehaviour
{
    //Serialized Fields used to reference the input reader, the body transform, and the rigidbody
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Rigidbody2D rb;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 4f;

    [SerializeField] private float turningRate = 30f;

    private Vector2 previousMovementInput;

    public override void OnNetworkSpawn()
    {

        if (!IsOwner) { return; }
        inputReader.MoveEvent += HandleMove;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) { return; }
    }
    void Update()
    {
        if (!IsOwner) { return; }
        float zRotation = previousMovementInput.x * -turningRate * Time.deltaTime;
        bodyTransform.Rotate(0f, 0f, zRotation);
    }

    private void FixedUpdate()
    {
        if (!IsOwner) { return; }

        // rb.linearVelocity = (Vector2)bodyTransform.up * previousMovementInput.y * movementSpeed;
        rb.AddForce((Vector2)bodyTransform.up * previousMovementInput.y * movementSpeed, ForceMode2D.Force); // Apply force to the rigidbody
    }

    private void HandleMove(Vector2 movementInput)
    {
        previousMovementInput = movementInput;
    }
}
