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

    [Header("Tela de conclusão (depois da última fase)")]
    public GameObject painelConclusao;
    public Image imgSeloConclusao;
    public TMP_Text txtSeloTitulo;
    public TMP_Text txtSeloDescricao;
    public TMP_Text txtEstatisticas;
    public Sprite seloCacado;             // ruim
    public Sprite seloVegetariano;        // médio
    public Sprite seloVoraz;              // bom
    public Sprite seloEspecialista;       // 100%
    [Tooltip("Aproveitamento mínimo (%) para 'Caçador voraz'.")]
    [Range(0f, 100f)] public float limiteVoraz = 80f;
    [Tooltip("Aproveitamento mínimo (%) para 'Caçador vegetariano'. Abaixo disso: 'Caçado pelas fake news'.")]
    [Range(0f, 100f)] public float limiteVegetariano = 60f;

    [Header("Áudio")]
    public AudioSource fonteSom;          // AudioSource com Play On Awake desmarcado
    public AudioClip somAcerto;
    public AudioClip somErro;
    public AudioClip somFimDeFase;
    public AudioClip somGameOver;
    public AudioClip somConclusao;        // opcional: se vazio, usa o som de fim de fase
    [Range(0f, 1f)] public float volumeSom = 1f;

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
        if (painelConclusao != null) painelConclusao.SetActive(false);
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
            TocarSom(somAcerto);
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
            TocarSom(somErro);
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

        ProgressoJogo.RegistrarFase(new ProgressoJogo.ResultadoFase
        {
            fase = fase,
            nomeFase = fase != null ? fase.nomeFase : "Fase",
            acertos = acertos,
            erros = erros,
            pontos = pontos,
            melhorSequencia = melhorSequencia,
            confiancaFinal = confiancaAtual
        });

        // Última fase: mostra a tela de conclusão com o resultado do jogo inteiro.
        bool ultimaFase = fase == null || fase.proximaFase == null;
        if (ultimaFase && painelConclusao != null)
        {
            MostrarConclusao();
            return;
        }

        string nome = fase != null ? fase.nomeFase : "Fase";
        string resumo =
            $"{nome} concluída!\n\n" +
            $"Acertos: {acertos}\n" +
            $"Erros: {erros}\n" +
            $"Melhor sequência: {melhorSequencia}\n" +
            $"Confiança final: {confiancaAtual:0}%\n" +
            $"Pontuação: {pontos}";

        TocarSom(somFimDeFase);

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

        TocarSom(somGameOver);

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

    // ---------- Conclusão ----------

    private void MostrarConclusao()
    {
        jogoAcabou = true;

        int totalAcertos = ProgressoJogo.TotalAcertos;
        int totalErros = ProgressoJogo.TotalErros;
        var selo = Classificacao.Calcular(totalAcertos, totalErros, limiteVoraz, limiteVegetariano);

        TocarSom(somConclusao != null ? somConclusao : somFimDeFase);
        painelConclusao.SetActive(true);

        if (imgSeloConclusao != null)
        {
            Sprite sp = selo switch
            {
                Classificacao.Selo.Especialista => seloEspecialista,
                Classificacao.Selo.CacadorVoraz => seloVoraz,
                Classificacao.Selo.CacadorVegetariano => seloVegetariano,
                _ => seloCacado,
            };
            imgSeloConclusao.sprite = sp;
            imgSeloConclusao.enabled = sp != null;
        }

        if (txtSeloTitulo != null)
        {
            txtSeloTitulo.text = Classificacao.Titulo(selo);
            txtSeloTitulo.color = Classificacao.Cor(selo);
        }
        if (txtSeloDescricao != null) txtSeloDescricao.text = Classificacao.Descricao(selo);
        if (txtEstatisticas != null) txtEstatisticas.text = MontarEstatisticas();
    }

    private static string MontarEstatisticas()
    {
        const string c1 = "<pos=24%>", c2 = "<pos=55%>", c3 = "<pos=76%>";
        var sb = new System.Text.StringBuilder();
        sb.Append($"<b>Fase{c1}Acertos{c2}Erros{c3}Pontos</b>\n");

        foreach (var r in ProgressoJogo.Resultados)
        {
            // "Fase 2 – Meia verdade" -> "Fase 2"
            string nome = r.nomeFase;
            int corte = nome.IndexOfAny(new[] { '–', '-', ':' });
            if (corte > 0) nome = nome.Substring(0, corte).Trim();
            sb.Append($"{nome}{c1}<color=#2FA35A>{r.acertos}</color>{c2}<color=#D9473F>{r.erros}</color>{c3}{r.pontos}\n");
        }

        int acertos = ProgressoJogo.TotalAcertos, erros = ProgressoJogo.TotalErros;
        sb.Append($"<b>Total{c1}<color=#2FA35A>{acertos}</color>{c2}<color=#D9473F>{erros}</color>{c3}{ProgressoJogo.TotalPontos}</b>\n\n");
        sb.Append($"Aproveitamento: <b>{ProgressoJogo.Aproveitamento:0}%</b> ({acertos} de {acertos + erros} notícias)\n");
        sb.Append($"Melhor sequência: <b>{ProgressoJogo.MelhorSequencia} acertos seguidos</b>");
        return sb.ToString();
    }

    // No Play Mode: clique com o botão direito no componente GameManager > "Testar tela de conclusão"
    [ContextMenu("Testar tela de conclusão")]
    private void TestarConclusao()
    {
        if (!Application.isPlaying || painelConclusao == null) { Debug.Log("Entre no Play Mode e crie a tela de conclusão antes."); return; }
        ProgressoJogo.Resetar();
        int[] err = { 0, 1, 1, 2 };
        for (int i = 0; i < 4; i++)
            ProgressoJogo.RegistrarFase(new ProgressoJogo.ResultadoFase
            {
                nomeFase = $"Fase {i + 1} – teste", acertos = 6 - err[i], erros = err[i],
                pontos = (6 - err[i]) * 100 * (i + 1), melhorSequencia = 6 - err[i], confiancaFinal = 80
            });
        MostrarConclusao();
    }

    // ---------- Botões ----------

    // Botão "Jogar de novo" da tela de conclusão: recomeça da Fase 1.
    public void JogarDoInicio()
    {
        ProgressoJogo.Resetar();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

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
        ProgressoJogo.Resetar();
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

    // ---------- Áudio ----------

    private void TocarSom(AudioClip clip)
    {
        if (fonteSom == null || clip == null) return;
        fonteSom.PlayOneShot(clip, volumeSom);
    }
}