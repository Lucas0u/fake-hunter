using UnityEngine;

[CreateAssetMenu(fileName = "NovaFonte", menuName = "FakeHunter/Fonte")]
public class Fonte : ScriptableObject
{
    public enum StatusFonte
    {
        Confiavel,
        NaoConfiavel,
        Desconhecida
    }

    [Header("Identificação")]
    public string nomeExibido = "Nome da Fonte";

    [Tooltip("Foto de perfil / logo da fonte (opcional). Recomendado: 256x256 PNG.")]
    public Sprite avatar;

    [Header("Avaliação")]
    public StatusFonte status = StatusFonte.Desconhecida;

    [Header("Histórico (aparece ao clicar em 'Consultar Referência')")]
    [TextArea(2, 5)]
    public string historico = "Escreva aqui uma frase curta sobre o histórico dessa fonte.";

    // Texto amigável em português para mostrar ao jogador
    public string StatusTexto()
    {
        switch (status)
        {
            case StatusFonte.Confiavel: return "Confiável";
            case StatusFonte.NaoConfiavel: return "Não confiável";
            default: return "Desconhecida";
        }
    }

    // Cor usada na UI para o status
    public Color StatusCor()
    {
        switch (status)
        {
            case StatusFonte.Confiavel: return new Color(0.25f, 0.75f, 0.35f);
            case StatusFonte.NaoConfiavel: return new Color(0.85f, 0.25f, 0.25f);
            default: return new Color(0.9f, 0.7f, 0.2f);
        }
    }
}
