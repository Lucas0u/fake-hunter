using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Configuração de confiança")]
    public float confiancaInicial = 100f;
    [Range(0f, 30f)] public float ganhoPorAcerto = 5f;
    [Range(0f, 30f)] public float perdaPorErro = 20f;

    [Header("Configuração de pontuação")]
    public int pontosBase = 100;          // multiplicado pela dificuldade (1, 2 ou 3)
    public int bonusPorSequencia = 25;    // bônus extra a cada acerto seguido

    [Header("Referências de UI - HUD")]
    public Slider sliderConfianca;
    public TMP_Text txtConfiancaValor;
    public TMP_Text txtFeedback;
    public TMP_Text txtPontos;            // opcional
    public TMP_Text txtSequencia;         // opcional
    public Image imgFillConfianca;        // opcional: muda de cor conforme a confiança

    [Header("Referências de UI - Fim de fase (opcional)")]
    public GameObject painelFimDeFase;
    public TMP_Text txtResumoFase;
    public GameObject btnProximaFase;    // opcional: some quando não há próxima fase

    [Header("Referências de UI - Game Over (opcional)")]
    public GameObject painelGameOver;
    public TMP_Text txtResumoGameOver;

    [Header("Cenas")]
    public string cenaMenu = "Menu";

    private float confiancaAtual;
    private int pontos;
    private int sequenciaAtual;
    private int melhorSequencia;
    private int acertos;
    private int erros;
    private bool jogoAcabou;
    private FaseDeNoticias faseConcluida;

    public bool JogoAcabou => jogoAcabou;
    public float Confianca => confiancaAtual;
    public int Pontos => pontos;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        confiancaAtual = confiancaInicial;
        pontos = 0;
        sequenciaAtual = 0;
        melhorSequencia = 0;
        acertos = 0;
        erros = 0;
        jogoAcabou = false;

        if (painelFimDeFase != null) painelFimDeFase.SetActive(false);
        if (painelGameOver != null) painelGameOver.SetActive(false);
        if (txtFeedback != null) txtFeedback.text = "";

        AtualizarUI();
    }

    // acaoCompartilhar: true = jogador decidiu "Compartilhar"
    // Retorna true se o jogador acertou.
    public bool AvaliarDecisao(bool acaoCompartilhar, Noticia noticia)
    {
        if (jogoAcabou) return false;

        bool acertou = acaoCompartilhar == noticia.DeveCompartilhar();

        if (acertou)
        {
            acertos++;
            sequenciaAtual++;
            if (sequenciaAtual > melhorSequencia) melhorSequencia = sequenciaAtual;

            int multiplicador = (int)noticia.dificuldade + 1;
            pontos += pontosBase * multiplicador + bonusPorSequencia * (sequenciaAtual - 1);

            confiancaAtual = Mathf.Min(100f, confiancaAtual + ganhoPorAcerto);
            MostrarFeedback("Decisão correta! A confiança da comunidade aumentou.", true);
        }
        else
        {
            erros++;
            sequenciaAtual = 0;

            confiancaAtual = Mathf.Max(0f, confiancaAtual - perdaPorErro);

            string motivo = acaoCompartilhar
                ? "Você espalhou uma informação que não era totalmente verdadeira."
                : "Você bloqueou uma informação verdadeira e importante.";
            MostrarFeedback("Decisão incorreta. " + motivo, false);
        }

        AtualizarUI();

        if (confiancaAtual <= 0f)
        {
            GameOver();
        }

        return acertou;
    }

    // Chamado pelo PainelNoticia quando todas as notícias da fase acabaram
    public void FinalizarFase(FaseDeNoticias fase)
    {
        if (jogoAcabou) return;
        faseConcluida = fase;

        string nome = fase != null ? fase.nomeFase : "Fase";
        string resumo =
            $"{nome} concluída!\n\n" +
            $"Acertos: {acertos}\n" +
            $"Erros: {erros}\n" +
            $"Melhor sequência: {melhorSequencia}\n" +
            $"Confiança final: {confiancaAtual:0}%\n" +
            $"Pontuação: {pontos}";

        if (painelFimDeFase != null)
        {
            painelFimDeFase.SetActive(true);
            if (txtResumoFase != null) txtResumoFase.text = resumo;
            if (btnProximaFase != null)
                btnProximaFase.SetActive(fase != null && fase.proximaFase != null);
        }
        else
        {
            Debug.Log("FIM DA FASE!\n" + resumo);
        }
    }

    private void GameOver()
    {
        jogoAcabou = true;

        string resumo =
            "Você foi demitido!\n\n" +
            "A comunidade perdeu a confiança no seu trabalho.\n\n" +
            $"Acertos: {acertos}   Erros: {erros}\n" +
            $"Pontuação: {pontos}";

        if (painelGameOver != null)
        {
            painelGameOver.SetActive(true);
            if (txtResumoGameOver != null) txtResumoGameOver.text = resumo;
        }
        else
        {
            Debug.Log("GAME OVER: " + resumo);
        }
    }

    // ---------- Botões ----------

    public void ProximaFase()
    {
        if (faseConcluida == null || faseConcluida.proximaFase == null) { ReiniciarFase(); return; }
        ProgressoJogo.faseSelecionada = faseConcluida.proximaFase;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReiniciarFase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VoltarAoMenu()
    {
        ProgressoJogo.faseSelecionada = null;
        if (Application.CanStreamedLevelBeLoaded(cenaMenu))
            SceneManager.LoadScene(cenaMenu);
        else
            ReiniciarFase();
    }

    // ---------- UI ----------

    private void AtualizarUI()
    {
        if (sliderConfianca != null) sliderConfianca.value = confiancaAtual;
        if (txtConfiancaValor != null) txtConfiancaValor.text = $"Confiança: {confiancaAtual:0}%";
        if (txtPontos != null) txtPontos.text = $"Pontos: {pontos}";
        if (txtSequencia != null) txtSequencia.text = sequenciaAtual > 1 ? $"Sequência: {sequenciaAtual}x" : "";

        if (imgFillConfianca != null)
        {
            if (confiancaAtual > 60f) imgFillConfianca.color = new Color(0.25f, 0.75f, 0.35f);
            else if (confiancaAtual > 30f) imgFillConfianca.color = new Color(0.95f, 0.75f, 0.2f);
            else imgFillConfianca.color = new Color(0.85f, 0.25f, 0.25f);
        }
    }

    private void MostrarFeedback(string mensagem, bool acertou)
    {
        if (txtFeedback == null) return;
        txtFeedback.text = mensagem;
        txtFeedback.color = acertou ? new Color(0.25f, 0.75f, 0.35f) : new Color(0.85f, 0.25f, 0.25f);
    }
}
