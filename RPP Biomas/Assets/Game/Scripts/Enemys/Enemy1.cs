using System;
using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    public float speed = 2f; // Velocidade de movimento do inimigo
    public float followRange = 5f; // Distância em que o inimigo começa a seguir o jogador
    public LayerMask playerLayer; // Layer do jogador
    public int maxHealth = 3; // Vida máxima do inimigo

    private Transform player; // Referência ao jogador
    private Rigidbody2D rb; // Referência ao Rigidbody2D do inimigo
    private int currentHealth; // Vida atual do inimigo

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encontrar o jogador pela tag
        rb = GetComponent<Rigidbody2D>(); // Obter o componente Rigidbody2D
        currentHealth = maxHealth; // Definir a vida inicial do inimigo
    }

    void Update()
    {
        if (player != null && IsPlayerVisible()) // Verifica se o jogador ainda está na Layer correta
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= followRange)
            {
                FollowPlayer();
            }
        }
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized; // Calcula a direção para o jogador
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime); // Move o inimigo usando o Rigidbody2D
    }

    bool IsPlayerVisible()
    {
        return (player.gameObject.layer == Mathf.Log(playerLayer.value, 2)); // Verifica se o jogador está na Layer correta
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduz a vida do inimigo

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BuletPlayer")
        {
            TakeDamage(1);
            Destroy(col.gameObject);
        }
    }

    void Die()
    {
        Destroy(gameObject); // Destroi o inimigo
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRange);
    }
}   