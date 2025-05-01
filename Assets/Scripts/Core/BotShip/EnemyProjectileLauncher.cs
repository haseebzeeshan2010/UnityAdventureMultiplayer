using Unity.Netcode;
using UnityEngine;

public class EnemyProjectileLauncher : NetworkBehaviour
{
    [Header("References")]

    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject serverProjectilePrefab;
    [SerializeField] private GameObject clientProjectilePrefab;


    [SerializeField] private Collider2D playerCollider;

    [Header("Settings")]
    [SerializeField] private float projectileSpeed;

    [SerializeField] private float fireRate;




    private bool shouldFire = true; // Indicates whether the player can fire a projectile

    private float timer;


    //OnNetworkSpawn and OnNetworkDespawn are called when the object is spawned or despawned on the network
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) { return; }

        this.shouldFire = true;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) { return; }

    }


    // The Update method is called every frame. This method updates the muzzle flash timer and checks if the player can fire a projectile.
    private void Update()
    {
        if (!IsServer) { return; }
        
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }


        if (timer > 0) { return; }

        PrimaryFireServerRpc(projectileSpawnPoint.position, projectileSpawnPoint.up);
        SpawnDummyProjectile(projectileSpawnPoint.position, projectileSpawnPoint.up);
        timer = 1 / fireRate;
    }




    // The PrimaryFireServerRpc method is called when the player fires a projectile. This method instantiates a server projectile and sets its velocity.


    [ServerRpc]
    private void PrimaryFireServerRpc(Vector3 spawnPos, Vector3 direction)
    {

        GameObject projectileInstance = Instantiate(
            serverProjectilePrefab,
            spawnPos,
            Quaternion.identity);
        projectileInstance.transform.up = direction;
        Physics2D.IgnoreCollision(playerCollider, projectileInstance.GetComponent<Collider2D>());
        if(projectileInstance.TryGetComponent<DealDamageOnContact>(out DealDamageOnContact DealDamage))
        {
            DealDamage.SetOwnerClientId(OwnerClientId);
        }
        if (projectileInstance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.linearVelocity = rb.transform.up * projectileSpeed;
        }
        Debug.Log("Remember to search up the instantiatespawn method to fix the projectile");
        SpawnDummyProjectileClientRpc(spawnPos, direction);
    }

    [ClientRpc]
    private void SpawnDummyProjectileClientRpc(Vector3 spawnPos, Vector3 direction)
    {

        SpawnDummyProjectile(spawnPos, direction);
    }

    private void SpawnDummyProjectile(Vector3 spawnPos, Vector3 direction)
    {
        

        GameObject projectileInstance = Instantiate(
            clientProjectilePrefab,
            spawnPos,
            Quaternion.identity);

        projectileInstance.transform.up = direction;

        Physics2D.IgnoreCollision(playerCollider, projectileInstance.GetComponent<Collider2D>());

        if (projectileInstance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.linearVelocity = rb.transform.up * projectileSpeed;
        }
    }
}
