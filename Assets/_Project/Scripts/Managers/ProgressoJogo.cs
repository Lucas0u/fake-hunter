using System.Collections.Generic;
using UnityEngine;

// Guarda dados que precisam sobreviver à troca de cena (ex.: qual fase carregar
// e o resultado de cada fase, usado na tela de conclusão).
public static class ProgressoJogo
{
    // Fase escolhida para a próxima carga da cena Gameplay. Null = usa a fase configurada no Inspector.
    public static FaseDeNoticias faseSelecionada;

    public class ResultadoFase
    {
        public FaseDeNoticias fase;
        public string nomeFase;
        public int acertos;
        public int erros;
        public int pontos;
        public int melhorSequencia;
        public float confiancaFinal;
        public int Total => acertos + erros;
    }

    private static readonly List<ResultadoFase> resultados = new List<ResultadoFase>();
    public static IReadOnlyList<ResultadoFase> Resultados => resultados;

    // Começa uma partida nova (botão Jogar do menu, ou jogar de novo desde a Fase 1).
    public static void Resetar()
    {
        faseSelecionada = null;
        resultados.Clear();
    }

    // Se o jogador repetir uma fase, o resultado novo substitui o anterior.
    public static void RegistrarFase(ResultadoFase r)
    {
        int i = r.fase == null ? -1 : resultados.FindIndex(x => x.fase == r.fase);
        if (i >= 0) resultados[i] = r;
        else resultados.Add(r);
    }

    public static int TotalAcertos { get { int t = 0; foreach (var r in resultados) t += r.acertos; return t; } }
    public static int TotalErros   { get { int t = 0; foreach (var r in resultados) t += r.erros; return t; } }
    public static int TotalPontos  { get { int t = 0; foreach (var r in resultados) t += r.pontos; return t; } }
    public static int MelhorSequencia { get { int m = 0; foreach (var r in resultados) m = Mathf.Max(m, r.melhorSequencia); return m; } }

    // 0 a 100
    public static float Aproveitamento
    {
        get
        {
            int total = TotalAcertos + TotalErros;
            return total == 0 ? 0f : 100f * TotalAcertos / total;
        }
    }
}
