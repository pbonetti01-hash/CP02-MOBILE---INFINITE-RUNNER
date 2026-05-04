using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Referências UI")]
    public Text textoMoedas; 
    public Text textoTimer;  

    private float tempoDecorrido = 0f;

    void Update()
    {
        AtualizarInterface();
        ContarTempo();
    }

    void AtualizarInterface()
    {
        if (GameManager.Instance != null && textoMoedas != null)
        {
            textoMoedas.text = "Moedas: " + GameManager.Instance.coins.ToString();
        }
    }

    void ContarTempo()
    {
        if (textoTimer != null)
        {
            tempoDecorrido += Time.deltaTime;
            
            string minutos = ((int)tempoDecorrido / 60).ToString("00");
            string segundos = (tempoDecorrido % 60).ToString("00");
            
            textoTimer.text = "Tempo: " + minutos + ":" + segundos;
        }
    }
}