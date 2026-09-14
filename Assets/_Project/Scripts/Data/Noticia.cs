using UnityEngine;

[CreateAssetMenu(fileName = "NovaNoticia", menuName = "FakeHunter/Noticia")]
public class Noticia : ScriptableObject
{
    public enum Veredito
    {
        Verdadeira,
        Falsa,
        ParcialmenteVerdadeira
    }

    public enum Dificuldade
    {
        Facil,
        Media,
        Dificil
    }

    [Header("Conteúdo exibido na tela")]
    public string titulo = "Título da notícia";

    [TextArea(2, 4)]
    public string corpo = "Texto curto da notícia.";

    [Tooltip("Imagem que ilustra a notícia (opcional). Recomendado: 800x450 PNG.")]
    public Sprite imagem;

    public string nomeCanal = "Ex: WhatsApp, Portal Oficial...";
    public string data = "08/09/2026";

    [Header("Autor (referência à Fonte)")]
    public Fonte autor;

    [Header("Resultados da investigação")]
    [TextArea(2, 4)]
    public string textoVerificarFonte = "Texto que aparece ao clicar em 'Verificar Fonte'.";

    [TextArea(2, 4)]
    public string textoPesquisar = "Texto que aparece ao clicar em 'Pesquisar'.";

    [Header("Dados de avaliação (não mostrados ao jogador)")]
    public Veredito veredito = Veredito.Verdadeira;
    public Dificuldade dificuldade = Dificuldade.Facil;

    [Header("Explicação mostrada após a decisão do jogador")]
    [TextArea(3, 6)]
    public string explicacao = "Explique por que a notícia é verdadeira, falsa ou parcialmente verdadeira.";

    // Só notícias totalmente verdadeiras devem ser compartilhadas.
    // Uma notícia parcialmente verdadeira ainda espalha desinformação.
    public bool DeveCompartilhar()
    {
        return veredito == Veredito.Verdadeira;
    }

    public string VereditoTexto()
    {
        switch (veredito)
        {
            case Veredito.Verdadeira: return "VERDADEIRA";
            case Veredito.Falsa: return "FALSA";
            default: return "PARCIALMENTE VERDADEIRA";
        }
    }
}
