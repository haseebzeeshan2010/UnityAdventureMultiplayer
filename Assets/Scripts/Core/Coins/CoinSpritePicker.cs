using UnityEngine;
using Unity.Netcode;
public class CoinSpritePicker : NetworkBehaviour
{
    [SerializeField] private Sprite[] coinSprites; // Array of coin sprites
    [SerializeField] private SpriteRenderer coinSpriteRenderer; // Reference to the SpriteRenderer component
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnNetworkSpawn()
    {
        coinSpriteRenderer.sprite = coinSprites[Random.Range(0, coinSprites.Length)]; // Pick a random sprite from the array
    }
}
