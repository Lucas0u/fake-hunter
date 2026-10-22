using UnityEngine;

// Selo que o jogador recebe na tela de conclusão, de acordo com o aproveitamento
// (acertos ÷ total de notícias) somando todas as fases.
public static class Classificacao
{
    public enum Selo
    {
        CacadoPelasFakeNews,   // desempenho ruim
        CacadorVegetariano,    // desempenho médio
        CacadorVoraz,          // desempenho bom
        Especialista           // 100% de acerto
    }

    public static Selo Calcular(int acertos, int erros, float limiteVoraz, float limiteVegetariano)
    {
        int total = acertos + erros;
        if (total > 0 && erros == 0) return Selo.Especialista;

        float aproveitamento = total == 0 ? 0f : 100f * acertos / total;
        if (aproveitamento >= limiteVoraz) return Selo.CacadorVoraz;
        if (aproveitamento >= limiteVegetariano) return Selo.CacadorVegetariano;
        return Selo.CacadoPelasFakeNews;
    }

    public static string Titulo(Selo s)
    {
        switch (s)
        {
            case Selo.Especialista: return "Especialista";
            case Selo.CacadorVoraz: return "Caçador voraz";
            case Selo.CacadorVegetariano: return "Caçador vegetariano";
            default: return "Caçado pelas fake news";
        }
    }

    public static string Descricao(Selo s)
    {
        switch (s)
        {
            case Selo.Especialista:
                return "Nenhuma fake news escapou! Com 100% de acerto, a comunidade confia de olhos fechados no seu trabalho.";
            case Selo.CacadorVoraz:
                return "Poucas fake news escapam de você. Mais um pouco de atenção aos detalhes e você vira especialista.";
            case Selo.CacadorVegetariano:
                return "Você sai para caçar, mas deixa muita fake news escapar. Investigue com mais calma antes de decidir.";
            default:
                return "Você chegou ao fim, mas as fake news levaram a melhor: muita desinformação passou por você. Revise as explicações e tente de novo!";
        }
    }

    public static Color Cor(Selo s)
    {
        switch (s)
        {
            case Selo.Especialista: return new Color32(214, 150, 30, 255);
            case Selo.CacadorVoraz: return new Color32(224, 106, 36, 255);
            case Selo.CacadorVegetariano: return new Color32(38, 140, 76, 255);
            default: return new Color32(200, 58, 52, 255);
        }
    }
}
