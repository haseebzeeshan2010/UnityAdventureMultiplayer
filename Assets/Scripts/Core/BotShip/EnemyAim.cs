using UnityEngine;

public class EnemyAim : MonoBehaviour
{



    [SerializeField] private PlayerSelection playerSelection;
    [SerializeField] private GameObject cannon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame with smooth lerp rotation
    [SerializeField] private float rotationLerpSpeed = 10f;
    void Update()
    {
        Vector3 enemyPosition = playerSelection.playerTransform.position;
        Debug.Log("Enemy Position: " + enemyPosition);
        Vector2 direction = enemyPosition - cannon.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        cannon.transform.rotation = Quaternion.Lerp(cannon.transform.rotation, targetRotation, rotationLerpSpeed * Time.deltaTime);
    }
}
