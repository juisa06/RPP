using System;
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

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;

    public int medalsCollected = 0;

    private void Awake()
    {
        // Implementa o padrão Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Garante que o Player não seja destruído ao carregar uma nova cena
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
        // Verifica se o jogador está no chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Captura as entradas do jogador
        movement.x = Input.GetAxisRaw("Horizontal");

        // Aumenta a velocidade se Shift estiver pressionado
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.velocity = new Vector2(movement.x * currentSpeed, rb.velocity.y);

        // Pulo
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
