using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Azuli : MonoBehaviour
{
    public Transform player; // Referência ao jogador
    public float followDistanceX = 2f; // Distância mínima para seguir o jogador no eixo X
    public float heightAbovePlayer = 2f; // Altura fixa acima do jogador
    public float followSpeed = 2f; // Velocidade de movimento do pássaro

    void Update()
    {
        // Calcula a distância no eixo X entre o jogador e o pássaro
        float distanceToPlayerX = Mathf.Abs(transform.position.x - player.position.x);

        // Se a distância no eixo X for maior que a distância mínima, segue o jogador
        if (distanceToPlayerX > followDistanceX)
        {
            Vector2 targetPosition = new Vector2(player.position.x, player.position.y + heightAbovePlayer);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}