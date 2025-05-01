using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerSelection : MonoBehaviour
{
    private List<TankPlayer> playersInZone = new List<TankPlayer>();

    public Transform playerTransform; // Reference to the player's transform

    private void Update()
    {
        foreach (TankPlayer player in playersInZone)
        {
            if (player != null) // Check if the player is not null and is the owner
            {
                Debug.Log($"Player Transform: {playerTransform}"); // Log player position
                playerTransform = player.transform; // Assign the player's transform to the variable
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        if(!col.attachedRigidbody.TryGetComponent<TankPlayer>(out TankPlayer player)) {return;} // Check if the collider is a player

        playersInZone.Add(player); // Add the player to the list of players in the zone

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        
        if(!col.attachedRigidbody.TryGetComponent<TankPlayer>(out TankPlayer player)) {return;} // Check if the collider is a player

        playersInZone.Remove(player); // Remove the player to the list of players in the zone
    

    }
}
