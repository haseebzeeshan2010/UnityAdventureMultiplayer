using Unity.Netcode;
using UnityEngine;

public class CoinSpawner : NetworkBehaviour
{
    [SerializeField] private RespawningCoin coinPrefab;

    [SerializeField] private int maxCoins = 50;
    [SerializeField] private int coinValue = 10;
    [SerializeField] private Vector2 xSpawnRange;
    [SerializeField] private Vector2 ySpawnRange;
    [SerializeField] private LayerMask layerMask;

    private Collider2D[] coinBuffer = new Collider2D[1];
    private float coinRadius;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        coinRadius = 0.6f;

        for (int i = 0; i < maxCoins; i++)
        {
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        RespawningCoin coinInstance = Instantiate(
            coinPrefab,
            GetSpawnPoint(),
            Quaternion.identity);

        coinInstance.SetValue(coinValue);
        coinInstance.GetComponent<NetworkObject>().Spawn();
        coinInstance.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        coinInstance.OnCollected += HandleCoinCollected;
    }

    private void HandleCoinCollected(RespawningCoin coin)
    {
        coin.transform.position = GetSpawnPoint();
        coin.Reset();
    }

    private Vector2 GetSpawnPoint()
    {
        float x = 0;
        float y = 0;
        while (true)
        {
            x = Random.Range(xSpawnRange.x, xSpawnRange.y);
            y = Random.Range(ySpawnRange.x, ySpawnRange.y);
            Vector2 spawnPoint = new Vector2(x, y);
            int numColliders = Physics2D.OverlapCircleAll(spawnPoint, coinRadius, layerMask).Length;

            if (numColliders == 0)
            {
                return spawnPoint;
            }
        }
    }
}
