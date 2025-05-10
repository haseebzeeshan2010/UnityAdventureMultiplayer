using UnityEngine;
using Unity.Netcode;
public class EnemyOnDie : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health health;

    [SerializeField] private NetworkObject networkObject;



    [SerializeField] private BountyCoin coinPrefab;
    [SerializeField] private float coinSpread = 3f;
    [SerializeField] private LayerMask layerMask;

    private float coinRadius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinRadius = coinPrefab.GetComponent<CircleCollider2D>().radius;

    }

    // Update is called once per frame
    void Update()
    {
        if (health.CurrentHealth.Value <= 0)
        {
            HandleDie();
            networkObject.Despawn(true);
            
        }
    }

    private void HandleDie()
    {
        
        for (int i = 0; i < 25; i++)
        {
            BountyCoin coinInstance = Instantiate(coinPrefab, GetSpawnPoint(), Quaternion.identity);
            coinInstance.SetValue(60);
            coinInstance.NetworkObject.Spawn();
        }
    }
    private Vector2 GetSpawnPoint()
    {
        while (true)
        {
            Vector2 spawnPoint = (Vector2)transform.position + UnityEngine.Random.insideUnitCircle * coinSpread;
            int numColliders = Physics2D.OverlapCircleAll(spawnPoint, coinRadius, layerMask).Length;
            if (numColliders == 0)
            {
                return spawnPoint;
            }
        }
    }
}
