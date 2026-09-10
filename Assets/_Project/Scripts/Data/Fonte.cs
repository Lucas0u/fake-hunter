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

    [Header("Avaliação")]
    public StatusFonte status = StatusFonte.Desconhecida;

    [Header("Histórico (aparece ao clicar em 'Consultar Referência')")]
    [TextArea(2, 5)]
    public string historico = "Escreva aqui uma frase curta sobre o histórico dessa fonte.";
}