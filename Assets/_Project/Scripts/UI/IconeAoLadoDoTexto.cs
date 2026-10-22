using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Mantém um ícone encostado à esquerda do texto, qualquer que seja o alinhamento,
// e esconde o ícone quando o texto está vazio (ex.: "Sequência" antes de 2 acertos).
// Coloque este componente no objeto do ícone, que deve ser filho do texto.
[ExecuteAlways]
[RequireComponent(typeof(Image))]
public class IconeAoLadoDoTexto : MonoBehaviour
{
    public TMP_Text texto;
    public float espaco = 10f;

    private Image imagem;
    private RectTransform rt;
    private string ultimoTexto;

    void OnEnable()
    {
        imagem = GetComponent<Image>();
        rt = (RectTransform)transform;
        if (texto == null && transform.parent != null)
            texto = transform.parent.GetComponent<TMP_Text>();
        ultimoTexto = null;
    }

    void LateUpdate()
    {
        if (texto == null) return;

        bool vazio = string.IsNullOrWhiteSpace(texto.text) || !texto.gameObject.activeInHierarchy;
        if (imagem.enabled == vazio) imagem.enabled = !vazio;
        if (vazio) return;

        if (texto.text != ultimoTexto)
        {
            texto.ForceMeshUpdate();
            ultimoTexto = texto.text;
        }

        Bounds b = texto.textBounds;
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(1f, 0.5f);

        // textBounds está no espaço local do texto; converte para a âncora central.
        RectTransform rtTexto = texto.rectTransform;
        Vector2 deslocPivo = new Vector2(
            (rtTexto.pivot.x - 0.5f) * rtTexto.rect.width,
            (rtTexto.pivot.y - 0.5f) * rtTexto.rect.height);

        rt.anchoredPosition = new Vector2(b.min.x - espaco, b.center.y) + deslocPivo;
    }
}
