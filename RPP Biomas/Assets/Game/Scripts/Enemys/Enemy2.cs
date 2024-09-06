using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float shootRange = 7f; // Distância em que o inimigo começa a atirar
    public float fireRate = 1f; // Taxa de tiro (em segundos)
    public GameObject bulletPrefab; // Prefab do projétil
    public Transform firePoint; // Ponto de origem do projétil
    public LayerMask playerLayer; // Layer do jogador
    public float speed = 2f; // Velocidade de movimento lateral
    public int maxHealth = 3; // Vida máxima do inimigo

    private Transform player;
    private Rigidbody2D rb; // Referência ao Rigidbody2D do inimigo
    private float nextFireTime = 0f;
    private float movementDirection = 1f; // Direção do movimento lateral (1 para direita, -1 para esquerda)
    private int currentHealth; // Vida atual do inimigo

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>(); // Obter o componente Rigidbody2D
        currentHealth = maxHealth; // Definir a vida inicial do inimigo
    }

    void Update()
    {
        if (player != null && IsPlayerVisible()) // Verifica se o jogador ainda está na Layer correta
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= shootRange && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate; // Calcula o próximo tempo de tiro
            }

            // Movimentação lateral
            MoveSideways();
        }
    }

    void MoveSideways()
    {
        rb.velocity = new Vector2(speed * movementDirection, rb.velocity.y); // Define a velocidade do Rigidbody2D

        // Inverte a direção do movimento ao atingir a borda do caminho (exemplo simples)
        if (transform.position.x > 5f || transform.position.x < -5f)
        {
            movementDirection *= -1f; // Inverte a direção
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Cria o projétil na posição de disparo
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        if (rbBullet != null)
        {
            // Calcula a direção para o tiro com base na posição do jogador
            Vector2 direction = (player.position - transform.position).normalized;
            rbBullet.velocity = direction * 10f; // Define a velocidade do projétil
        }
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
