using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Collider2D checkpointCollider;

    private void Awake()
    {
        checkpointCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aciona o evento com a posição do checkpoint
            CheckPointObserver.TriggerCheckpoint(transform.position);

            // Desativa o colisor para evitar múltiplos triggers
            checkpointCollider.enabled = false;
        }
    }
}