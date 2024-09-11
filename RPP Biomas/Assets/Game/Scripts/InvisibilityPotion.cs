using UnityEngine;

public class InvisibilityPotion : MonoBehaviour
{
    // Tempo de duração da invisibilidade
    public float invisibilityDuration = 5f; 
    // Tempo de cooldown entre usos
    public float cooldownTime = 15f;
    
    // Controle de estado da invisibilidade e cooldown
    private bool isInvisible = false;
    private bool onCooldown = false;

    private Renderer playerRenderer;
    private Color originalColor; // Para armazenar a cor original do jogador

    void Start()
    {
        // Inicializa o componente Renderer do jogador
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color; // Armazena a cor original do jogador
    }

    void Update()
    {
        // Verifica se a tecla 'Q' foi pressionada e se não está em cooldown
        if (Input.GetKeyDown(KeyCode.Q) && !onCooldown && GameManager.Instance.hasInvisiblePotion == true)
        {
            ActivateInvisibility();
        }
    }

    void ActivateInvisibility()
    {
        // Ativa a invisibilidade
        isInvisible = true;
        onCooldown = true;
        
        // Muda a cor para torná-lo mais transparente
        Color transparentColor = originalColor;
        transparentColor.a = 0.3f; // Ajuste o valor de transparência (0 = completamente invisível, 1 = completamente visível)
        playerRenderer.material.color = transparentColor;

        // Desativa a detecção pelos inimigos
        gameObject.layer = LayerMask.NameToLayer("Invisible");

        // Inicia a contagem para reverter a invisibilidade e para o cooldown
        Invoke("DeactivateInvisibility", invisibilityDuration);
        Invoke("ResetCooldown", cooldownTime);
    }

    void DeactivateInvisibility()
    {
        // Reverte a invisibilidade
        isInvisible = false;
        playerRenderer.material.color = originalColor; // Restaura a cor original

        // Reativa a detecção pelos inimigos
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void ResetCooldown()
    {
        // Reseta o cooldown
        onCooldown = false;
    }
}
