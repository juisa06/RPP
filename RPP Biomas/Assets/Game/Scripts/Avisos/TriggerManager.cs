using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;  // Necessário para trabalhar com o UI Text

public class TriggerManager : MonoBehaviour
{
    public Text triggerText;  // Referência ao componente Text no Canvas

    private void OnEnable()
    {
        TriggerObserver.OnTriggerEnterEvent += HandleTriggerEnter;
    }

    private void OnDisable()
    {
        TriggerObserver.OnTriggerEnterEvent -= HandleTriggerEnter;
    }

    private void HandleTriggerEnter(string triggerName)
    {
        // Atualize o texto com base no nome do trigger
        switch (triggerName)
        {
            case "Pule":
                triggerText.text = "Pule Para desviar de obstaculos";
                break;

            case "Enemy":
                triggerText.text = "há um inimigo a frente";
                break;

            // Adicione mais casos para outros triggers conforme necessário

            default:
                triggerText.text = "Jogador entrou em um trigger desconhecido: " + triggerName;
                break;
        }

        // Opcional: Limpar o texto após alguns segundos
        StartCoroutine(ClearTextAfterDelay(3f));
    }

    private IEnumerator ClearTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        triggerText.text = "";
    }
}