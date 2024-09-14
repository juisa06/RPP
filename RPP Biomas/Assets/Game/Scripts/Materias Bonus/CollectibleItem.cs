using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemType; // Pode ser "Animal", "RareStone", "PuzzlePiece"
    public string itemName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Encontra o GameManager para obter a fase atual
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager não encontrado!");
                return;
            }

            // Encontra o CollectiblesManager
            CollectiblesManager collectiblesManager = FindObjectOfType<CollectiblesManager>();
            if (collectiblesManager == null)
            {
                Debug.LogError("CollectiblesManager não encontrado!");
                return;
            }

            // Coleta o item, passando o tipo e nome
            collectiblesManager.CollectItem(itemType, itemName);
            Debug.Log($"{itemType} Coletado: {itemName}");

            // Destrói o item após a coleta
            Destroy(gameObject);
        }
    }
}