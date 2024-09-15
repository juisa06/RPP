using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aciona o evento com a posição do checkpoint
            CheckPointObserver.TriggerCheckpoint(transform.position);
        }
    }
}