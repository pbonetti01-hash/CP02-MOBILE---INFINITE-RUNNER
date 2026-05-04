using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuManager : MonoBehaviour
{
    [Header("Configurações de Cenas")]
    public string[] cenasDoJogo;

    public void EntrarNoJogo()
    {
        if (cenasDoJogo.Length > 0)
        {
            int indiceSorteado = Random.Range(0, cenasDoJogo.Length);
            string cenaEscolhida = cenasDoJogo[indiceSorteado];

            Debug.Log("Sorteado: " + cenaEscolhida);
            SceneManager.LoadScene(cenaEscolhida);
        }
    }

    public void SairDoJogo()
    {
        Debug.Log("O jogo fechou!"); 
        
        Application.Quit();
    }
}