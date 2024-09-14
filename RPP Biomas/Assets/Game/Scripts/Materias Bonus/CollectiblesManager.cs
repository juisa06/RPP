using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager Instance { get; private set; }

    public GameObject[] animalImages;   // Array de GameObjects para Animais no Canvas
    public GameObject[] stoneImages;    // Array de GameObjects para Pedras no Canvas
    public GameObject[] puzzleImages;   // Array de GameObjects para Quebra-Cabeça no Canvas

    public List<string> collectedItems = new List<string>();    // Lista de itens coletados
    public List<string> currentPhaseItems = new List<string>(); // Itens específicos da fase atual

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

    private void Start()
    {
        SetAllImagesInactive(); // Assegura que todas as imagens estejam inativas no início
    }

    private void Update()
    {
        // Atualiza as imagens dos itens colecionados em tempo real
        foreach (var itemName in collectedItems)
        {
            if (currentPhaseItems.Contains(itemName)) 
            {
                UpdateImage(itemName); // Ativa as imagens coletadas
            }
        }
    }

    public void InitializePhaseItems(string[] phaseItems)
    {
        currentPhaseItems.Clear();
        currentPhaseItems.AddRange(phaseItems); // Adiciona os itens da fase atual
    }

    public void CollectItem(string itemType, string itemName)
    {
        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName);
            currentPhaseItems.Remove(itemName); // Remove o item da lista da fase atual
        }
    }

    public void ResetCurrentPhaseCollectibles()
    {
        foreach (var item in currentPhaseItems)
        {
            collectedItems.Remove(item); // Remove os itens da fase atual
        }

        SetAllImagesInactive(); // Desativa todas as imagens no Canvas
    }

    private void UpdateImage(string itemName)
    {
        // Verifica qual tipo de item foi coletado e ativa a imagem correspondente
        if (itemName.StartsWith("Animal"))
        {
            ShowSpecificImage(animalImages, itemName);
        }
        else if (itemName.StartsWith("RareStone"))
        {
            ShowSpecificImage(stoneImages, itemName);
        }
        else if (itemName.StartsWith("PuzzlePiece"))
        {
            ShowSpecificImage(puzzleImages, itemName);
        }
    }

    // Ativa apenas a imagem correspondente ao item coletado, com base no nome
    private void ShowSpecificImage(GameObject[] images, string itemName)
    {
        foreach (var image in images)
        {
            if (image.name == itemName)  // Comparar pelo nome do GameObject no Canvas
            {
                image.SetActive(true);  // Ativar a imagem quando o item é coletado
                break;
            }
        }
    }

    private void SetAllImagesInactive()
    {
        foreach (var image in animalImages)
        {
            image.SetActive(false);
        }

        foreach (var image in stoneImages)
        {
            image.SetActive(false);
        }

        foreach (var image in puzzleImages)
        {
            image.SetActive(false);
        }
    }
}
