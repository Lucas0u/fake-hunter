using UnityEngine;

// Guarda dados que precisam sobreviver à troca de cena (ex.: qual fase carregar).
public static class ProgressoJogo
{
    // Fase escolhida para a próxima carga da cena Gameplay. Null = usa a fase configurada no Inspector.
    public static FaseDeNoticias faseSelecionada;
}
