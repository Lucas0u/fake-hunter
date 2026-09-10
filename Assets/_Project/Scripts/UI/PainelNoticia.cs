using UnityEngine;
using TMPro;

public class PainelNoticia : MonoBehaviour
{
    [Header("Fase atual (lista de notícias em ordem)")]
    public FaseDeNoticias faseAtual;

    private int indiceAtual = 0;
    private Noticia noticiaAtual;

    [Header("Referências de UI - Notícia")]
    public TMP_Text txtTitulo;
    public TMP_Text txtCorpo;
    public TMP_Text txtAutor;
    public TMP_Text txtFonte;
    public TMP_Text txtData;

    [Header("Referência de UI - Resultado da investigação")]
    public TMP_Text txtResultado;

    void Start()
    {
        indiceAtual = 0;
        MostrarNoticiaAtual();
    }

    private void MostrarNoticiaAtual()
    {
        if (faseAtual == null || faseAtual.noticias.Length == 0)
        {
            Debug.LogWarning("Nenhuma fase/notícia configurada.");
            return;
        }

        noticiaAtual = faseAtual.noticias[indiceAtual];
        txtResultado.text = "";

        txtTitulo.text = noticiaAtual.titulo;
        txtCorpo.text = noticiaAtual.corpo;
        txtAutor.text = "Autor: " + (noticiaAtual.autor != null ? noticiaAtual.autor.nomeExibido : "Desconhecido");
        txtFonte.text = "Fonte: " + noticiaAtual.nomeCanal;
        txtData.text = "Data: " + noticiaAtual.data;
    }

    private void AvancarNoticia()
    {
        indiceAtual++;

        if (indiceAtual >= faseAtual.noticias.Length)
        {
            Debug.Log("FIM DA FASE!");
            return;
        }

        MostrarNoticiaAtual();
    }

    public void OnVerificarFonte()
    {
        txtResultado.text = noticiaAtual.textoVerificarFonte;
    }

    public void OnConsultarReferencia()
    {
        if (noticiaAtual.autor == null)
        {
            txtResultado.text = "Nenhuma informação sobre o autor.";
            return;
        }

        Fonte f = noticiaAtual.autor;
        txtResultado.text = $"{f.nomeExibido}\nStatus: {f.status}\n{f.historico}";
    }

    public void OnPesquisar()
    {
        txtResultado.text = noticiaAtual.textoPesquisar;
    }

    public void OnCompartilhar()
    {
        GameManager.Instance.AvaliarDecisao(true, noticiaAtual);
        AvancarNoticia();
    }

    public void OnNaoPropagar()
    {
        GameManager.Instance.AvaliarDecisao(false, noticiaAtual);
        AvancarNoticia();
    }
}