using UnityEngine;
using UnityEngine.SceneManagement;

// Script da cena de Menu: ligue os botões a estes métodos no Inspector.
public class MenuPrincipal : MonoBehaviour
{
    [Header("Nome da cena de jogo")]
    public string cenaGameplay = "Gameplay";

    [Header("Painel 'Como jogar' (opcional)")]
    public GameObject painelComoJogar;

    void Start()
    {
        if (painelComoJogar != null) painelComoJogar.SetActive(false);
    }

    public void Jogar()
    {
        ProgressoJogo.faseSelecionada = null;
        SceneManager.LoadScene(cenaGameplay);
    }

    public void AbrirComoJogar()
    {
        if (painelComoJogar != null) painelComoJogar.SetActive(true);
    }

    public void FecharComoJogar()
    {
        if (painelComoJogar != null) painelComoJogar.SetActive(false);
    }

    public void Sair()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
