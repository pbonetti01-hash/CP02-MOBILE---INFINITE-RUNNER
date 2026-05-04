using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameOverManager : MonoBehaviour
{
    [Header("Configurações de Navegação")]
    [Tooltip("Digite o nome exato da cena do Menu Principal")]
    public string nomeDaCenaMenu = "MainMenu";

    [Header("Sorteio de Fases")]
    [Tooltip("Arraste ou digite o nome de todas as cenas que podem ser sorteadas")]
    public List<string> cenasDeJogo = new List<string>();

    public void TentarNovamente()
    {
        Time.timeScale = 1f;

        if (cenasDeJogo.Count > 0)
        {
            int indiceSorteado = Random.Range(0, cenasDeJogo.Count);
            string cenaEscolhida = cenasDeJogo[indiceSorteado];

            SceneManager.LoadScene(cenaEscolhida);
        }
        else
        {
            Debug.LogWarning("Lista de cenas vazia! Recarregando a cena atual.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void VoltarAoMenu()
    {
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(nomeDaCenaMenu))
        {
            SceneManager.LoadScene(nomeDaCenaMenu);
        }
        else
        {
            Debug.LogError("O nome da cena do menu não foi preenchido no Inspector!");
        }
    }
}