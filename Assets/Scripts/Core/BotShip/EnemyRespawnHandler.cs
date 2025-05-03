using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class EnemyRespawnHandler : NetworkBehaviour
{
    [SerializeField] private NetworkObject shipPrefab;
    public int ship;

    private bool spawn = true;


    float time = 0f;
    float timeDelay = 3f; // Delay before respawning the ship
    private void Start()
    {
        // ship = GameObject.FindGameObjectsWithTag("BotShip").Length;
        // RespawnShip();
    }
    private void Update()
    {
        if (!IsServer) { return; } // Ensure this code runs only on the server
        ship = GameObject.FindGameObjectsWithTag("BotShip").Length;
        time = time + 1f*Time.deltaTime;

        if (time >= timeDelay)
        {
            RespawnShip();
            time = 0f; // Reset the timer after respawning
            timeDelay = Random.Range(60f, 120f); // Randomize the delay for the next respawn
        }
        
    }

    

    private void RespawnShip()
    {
        ship = GameObject.FindGameObjectsWithTag("BotShip").Length;
        if (ship == 0){
            Debug.Log("Ship not found, waiting to respawn...");
            var newShip = Instantiate(shipPrefab, SpawnPoint.GetRandomSpawnPos(), Quaternion.identity);
            var newShipNetworkObject = newShip.GetComponent<NetworkObject>();
            newShipNetworkObject.Spawn();
        }
        else
        {
            Debug.Log("Ship found, no need to respawn.");
            spawn = true; // Reset the spawn flag if the ship is found
            return;
        }
        Debug.Log("Ship count: " + ship); // Debug log to check the ship count

    }
}
