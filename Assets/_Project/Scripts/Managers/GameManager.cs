using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Configuração")]
    public float confiancaInicial = 100f;
    [Range(0f, 30f)] public float ganhoPorAcerto = 5f;
    [Range(0f, 30f)] public float perdaPorErro = 20f;

    [Header("Referências de UI")]
    public Slider sliderConfianca;
    public TMP_Text txtConfiancaValor;
    public TMP_Text txtFeedback;

    private float confiancaAtual;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        confiancaAtual = confiancaInicial;
        AtualizarUI();
    }

    // acaoEscolhida: true = jogador decidiu "Compartilhar"
    // eraVerdadeira: true = a notícia realmente era verdadeira
    public void AvaliarDecisao(bool acaoCompartilhar, Noticia noticia)
    {
        bool deveriaCompartilhar = noticia.veredito == Noticia.Veredito.Verdadeira;
        bool acertou = acaoCompartilhar == deveriaCompartilhar;

        if (acertou)
        {
            confiancaAtual = Mathf.Min(100f, confiancaAtual + ganhoPorAcerto);
            MostrarFeedback("Decisão correta! A confiança da comunidade se mantém.", acertou);
        }
        else
        {
            confiancaAtual = Mathf.Max(0f, confiancaAtual - perdaPorErro);
            MostrarFeedback("Decisão incorreta. A confiança da comunidade caiu.", acertou);
        }

        AtualizarUI();

        if (confiancaAtual <= 0f)
        {
            Debug.Log("GAME OVER: Você foi demitido.");

        }
    }

    private void AtualizarUI()
    {
        sliderConfianca.value = confiancaAtual;
        txtConfiancaValor.text = $"Confiança: {confiancaAtual:0}%";
    }

    private void MostrarFeedback(string mensagem, bool acertou)
    {
        if (txtFeedback == null) return;
        txtFeedback.text = mensagem;
        txtFeedback.color = acertou ? Color.green : Color.red;
    }
}