using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyDealDamageOnContact : MonoBehaviour
{
    [SerializeField] private int damage = 5;





    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.attachedRigidbody == null) { return; }

        

        if (col.attachedRigidbody.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
        }
    }
}
