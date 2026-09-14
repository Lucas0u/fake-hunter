using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PainelNoticia : MonoBehaviour
{
    [Header("Fase atual (lista de notícias em ordem)")]
    public FaseDeNoticias faseAtual;

    [Header("Regras")]
    [Tooltip("Quantas investigações o jogador pode fazer por notícia. 0 = ilimitado.")]
    public int investigacoesPorNoticia = 2;

    [Header("Referências de UI - Notícia")]
    public TMP_Text txtTitulo;
    public TMP_Text txtCorpo;
    public TMP_Text txtAutor;
    public TMP_Text txtFonte;
    public TMP_Text txtData;
    public Image imgNoticia;          // opcional
    public Sprite imagemPadrao;       // opcional: usada quando a notícia não tem imagem própria
    public Image imgAvatarAutor;      // opcional
    public TMP_Text txtProgresso;     // opcional: "Notícia 2 de 6"

    [Header("Referências de UI - Investigação")]
    public TMP_Text txtResultado;
    public TMP_Text txtInvestigacoesRestantes; // opcional
    public Button btnVerificarFonte;           // opcional
    public Button btnConsultarReferencia;      // opcional
    public Button btnPesquisar;                // opcional

    [Header("Referências de UI - Decisão")]
    public Button btnCompartilhar;    // opcional
    public Button btnNaoPropagar;     // opcional

    [Header("Referências de UI - Painel de explicação (opcional)")]
    public GameObject painelExplicacao;
    public TMP_Text txtExplicacaoTitulo;
    public TMP_Text txtExplicacaoVeredito;
    public TMP_Text txtExplicacaoCorpo;

    private List<Noticia> noticias = new List<Noticia>();
    private int indiceAtual = 0;
    private Noticia noticiaAtual;
    private int investigacoesUsadas = 0;
    private bool decisaoTomada = false;

    void Start()
    {
        MontarListaDeNoticias();
        indiceAtual = 0;

        if (painelExplicacao != null) painelExplicacao.SetActive(false);

        MostrarNoticiaAtual();
    }

    private void MontarListaDeNoticias()
    {
        noticias.Clear();
        if (faseAtual == null || faseAtual.noticias == null) return;

        foreach (Noticia n in faseAtual.noticias)
            if (n != null) noticias.Add(n);

        if (faseAtual.embaralhar)
        {
            for (int i = noticias.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                Noticia temp = noticias[i];
                noticias[i] = noticias[j];
                noticias[j] = temp;
            }
        }
    }

    private void MostrarNoticiaAtual()
    {
        if (noticias.Count == 0)
        {
            Debug.LogWarning("Nenhuma fase/notícia configurada.");
            return;
        }

        noticiaAtual = noticias[indiceAtual];
        investigacoesUsadas = 0;
        decisaoTomada = false;

        txtResultado.text = "";
        txtTitulo.text = noticiaAtual.titulo;
        txtCorpo.text = noticiaAtual.corpo;
        txtAutor.text = "Autor: " + (noticiaAtual.autor != null ? noticiaAtual.autor.nomeExibido : "Desconhecido");
        txtFonte.text = "Publicado em: " + noticiaAtual.nomeCanal;
        txtData.text = "Data: " + noticiaAtual.data;

        if (imgNoticia != null)
        {
            Sprite img = noticiaAtual.imagem != null ? noticiaAtual.imagem : imagemPadrao;
            imgNoticia.sprite = img;
            imgNoticia.enabled = img != null;
        }

        if (imgAvatarAutor != null)
        {
            Sprite avatar = noticiaAtual.autor != null ? noticiaAtual.autor.avatar : null;
            imgAvatarAutor.sprite = avatar;
            imgAvatarAutor.enabled = avatar != null;
        }

        if (txtProgresso != null)
            txtProgresso.text = $"Notícia {indiceAtual + 1} de {noticias.Count}";

        AtualizarBotoes();
    }

    private void AvancarNoticia()
    {
        indiceAtual++;

        if (indiceAtual >= noticias.Count)
        {
            GameManager.Instance.FinalizarFase(faseAtual);
            return;
        }

        MostrarNoticiaAtual();
    }

    // ---------- Investigação ----------

    private bool PodeInvestigar()
    {
        if (decisaoTomada) return false;
        if (investigacoesPorNoticia <= 0) return true;
        return investigacoesUsadas < investigacoesPorNoticia;
    }

    private void RegistrarInvestigacao(Button botaoUsado)
    {
        investigacoesUsadas++;
        if (botaoUsado != null) botaoUsado.interactable = false;
        AtualizarBotoes();
    }

    public void OnVerificarFonte()
    {
        if (!PodeInvestigar()) return;
        txtResultado.text = "<b>Verificar Fonte</b>\n" + noticiaAtual.textoVerificarFonte;
        RegistrarInvestigacao(btnVerificarFonte);
    }

    public void OnConsultarReferencia()
    {
        if (!PodeInvestigar()) return;

        if (noticiaAtual.autor == null)
        {
            txtResultado.text = "<b>Consultar Referência</b>\nNenhuma informação sobre o autor.";
        }
        else
        {
            Fonte f = noticiaAtual.autor;
            string cor = ColorUtility.ToHtmlStringRGB(f.StatusCor());
            txtResultado.text =
                $"<b>Consultar Referência</b>\n{f.nomeExibido}\n" +
                $"Status: <color=#{cor}>{f.StatusTexto()}</color>\n{f.historico}";
        }

        RegistrarInvestigacao(btnConsultarReferencia);
    }

    public void OnPesquisar()
    {
        if (!PodeInvestigar()) return;
        txtResultado.text = "<b>Pesquisar</b>\n" + noticiaAtual.textoPesquisar;
        RegistrarInvestigacao(btnPesquisar);
    }

    // ---------- Decisão ----------

    public void OnCompartilhar()
    {
        Decidir(true);
    }

    public void OnNaoPropagar()
    {
        Decidir(false);
    }

    private void Decidir(bool compartilhar)
    {
        if (decisaoTomada || noticiaAtual == null) return;
        decisaoTomada = true;

        bool acertou = GameManager.Instance.AvaliarDecisao(compartilhar, noticiaAtual);
        AtualizarBotoes();

        if (GameManager.Instance.JogoAcabou) return;

        if (painelExplicacao != null)
        {
            MostrarExplicacao(acertou);
        }
        else
        {
            // Sem painel de explicação configurado: avança direto (comportamento antigo)
            AvancarNoticia();
        }
    }

    private void MostrarExplicacao(bool acertou)
    {
        painelExplicacao.SetActive(true);

        if (txtExplicacaoTitulo != null)
        {
            txtExplicacaoTitulo.text = acertou ? "Você acertou!" : "Você errou!";
            txtExplicacaoTitulo.color = acertou ? new Color(0.25f, 0.75f, 0.35f) : new Color(0.85f, 0.25f, 0.25f);
        }

        if (txtExplicacaoVeredito != null)
            txtExplicacaoVeredito.text = "Esta notícia é " + noticiaAtual.VereditoTexto();

        if (txtExplicacaoCorpo != null)
            txtExplicacaoCorpo.text = noticiaAtual.explicacao;
    }

    // Ligar ao botão "Próxima" do painel de explicação
    public void OnProximaNoticia()
    {
        if (painelExplicacao != null) painelExplicacao.SetActive(false);
        AvancarNoticia();
    }

    // ---------- UI ----------

    private void AtualizarBotoes()
    {
        bool podeInvestigar = PodeInvestigar();

        // Só reativa os botões de investigação quando uma nova notícia começa
        if (investigacoesUsadas == 0 && !decisaoTomada)
        {
            if (btnVerificarFonte != null) btnVerificarFonte.interactable = true;
            if (btnConsultarReferencia != null) btnConsultarReferencia.interactable = true;
            if (btnPesquisar != null) btnPesquisar.interactable = true;
        }
        else if (!podeInvestigar)
        {
            if (btnVerificarFonte != null) btnVerificarFonte.interactable = false;
            if (btnConsultarReferencia != null) btnConsultarReferencia.interactable = false;
            if (btnPesquisar != null) btnPesquisar.interactable = false;
        }

        if (btnCompartilhar != null) btnCompartilhar.interactable = !decisaoTomada;
        if (btnNaoPropagar != null) btnNaoPropagar.interactable = !decisaoTomada;

        if (txtInvestigacoesRestantes != null)
        {
            if (investigacoesPorNoticia <= 0)
                txtInvestigacoesRestantes.text = "";
            else
                txtInvestigacoesRestantes.text =
                    $"Investigações restantes: {Mathf.Max(0, investigacoesPorNoticia - investigacoesUsadas)}";
        }
    }
}
