using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Script da cena de Menu: ligue os botões a estes métodos no Inspector.
public class MenuPrincipal : MonoBehaviour
{
    [Header("Nome da cena de jogo")]
    public string cenaGameplay = "Gameplay";

    [Header("Painel 'Como jogar' (opcional)")]
    public GameObject painelComoJogar;

    [Header("Páginas do 'Como jogar' (opcional)")]
    public GameObject paginaRegras;       // página 1
    public GameObject paginaSelos;        // página 2
    public TMP_Text txtComoJogarTitulo;
    public string tituloRegras = "Como jogar";
    public string tituloSelos = "Selos";

    void Start()
    {
        if (painelComoJogar != null) painelComoJogar.SetActive(false);
    }

    public void Jogar()
    {
        ProgressoJogo.Resetar();
        SceneManager.LoadScene(cenaGameplay);
    }

    public void AbrirComoJogar()
    {
        if (painelComoJogar != null) painelComoJogar.SetActive(true);
        MostrarPaginaRegras();
    }

    public void MostrarPaginaRegras() { MostrarPagina(false); }
    public void MostrarPaginaSelos()  { MostrarPagina(true); }

    private void MostrarPagina(bool selos)
    {
        if (paginaRegras != null) paginaRegras.SetActive(!selos);
        if (paginaSelos != null) paginaSelos.SetActive(selos);
        if (txtComoJogarTitulo != null && paginaSelos != null)
            txtComoJogarTitulo.text = selos ? tituloSelos : tituloRegras;
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
