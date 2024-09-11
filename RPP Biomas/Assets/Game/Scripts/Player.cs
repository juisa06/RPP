using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public GameObject bulletPrefab; // Prefab do projétil para o ataque à distância
    public Transform bulletSpawnPoint; // Ponto de origem do projétil

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;
    private bool canShoot = true;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        JumpAndRun();
        HandleAttack();
    }

    void JumpAndRun()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        movement.x = Input.GetAxisRaw("Horizontal");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.velocity = new Vector2(movement.x * currentSpeed, rb.velocity.y);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void HandleAttack()
    {
        // Verifica se o jogador pressionou a tecla de ataque à distância e se pode atirar
        if (Input.GetKeyDown(KeyCode.F) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        canShoot = false; // Desativa a capacidade de atirar
        // Instancia o projétil na posição do jogador, orientado para a direita ou esquerda
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = new Vector2((transform.localScale.x > 0 ? 1 : -1) * 10f, 0); // Direção baseada na orientação do jogador
        Destroy(bullet, 5f); // Destroi o projétil após 5 segundos

        yield return new WaitForSeconds(0.7f); // Aguarda 0.7 segundos antes de permitir o próximo tiro

        canShoot = true; // Reativa a capacidade de atirar
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);
            GameManager.Instance.LifePlayer--;
        }

        if (col.gameObject.CompareTag("InvisiblePotion"))
        {
            if (!GameManager.Instance.hasInvisiblePotion) // Verifica se o jogador já possui a poção
            {
                GameManager.Instance.hasInvisiblePotion = true;
                Destroy(col.gameObject);
                // Implementar a lógica de invisibilidade
            }
        }

        if (col.gameObject.CompareTag("DamageIncrease"))
        {
            GameManager.Instance.playerDamage++;
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("HealthPickup"))
        {
            GameManager.Instance.LifePlayer++;
            Destroy(col.gameObject);
            
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
