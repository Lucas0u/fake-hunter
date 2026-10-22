using UnityEngine;

[CreateAssetMenu(fileName = "NovaFase", menuName = "FakeHunter/FaseDeNoticias")]
public class FaseDeNoticias : ScriptableObject
{
    [Header("Identificação")]
    public string nomeFase = "Fase 1";

    [TextArea(2, 4)]
    public string descricao = "Texto curto mostrado no início da fase.";

    [Header("Notícias desta fase, na ordem que devem aparecer")]
    public Noticia[] noticias;

    [Tooltip("Se marcado, a ordem das notícias é sorteada a cada partida.")]
    public bool embaralhar = false;

    [Header("Próxima fase (opcional)")]
    public FaseDeNoticias proximaFase;

    [Header("Regras especiais")]
    [Tooltip("Se marcado, 'Consultar Referência' não mostra se a fonte é confiável. O jogador precisa avaliar sozinho (ex.: erros de português).")]
    public bool ocultarStatusDaFonte = false;

    [Tooltip("Dica escondida no ícone de lâmpada. Se ficar vazia, o ícone não aparece nesta fase.")]
    [TextArea(2, 4)]
    public string dica = "";
}
