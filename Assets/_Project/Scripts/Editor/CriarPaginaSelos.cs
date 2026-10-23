#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

// Menu: FakeHunter > Criar página de selos no Como jogar (cena Menu)
// Divide o cartão "Como jogar" em duas páginas: regras (texto atual) e selos.
// Pode ser rodado de novo para recriar a página de selos. Ctrl+Z desfaz.
public static class CriarPaginaSelos
{
    static readonly Color Marinho = new Color32(27, 42, 74, 255);
    static readonly Color TextoEscuro = new Color32(43, 43, 43, 255);

    [MenuItem("FakeHunter/Criar página de selos no Como jogar (cena Menu)")]
    public static void Criar()
    {
        var menu = Object.FindFirstObjectByType<MenuPrincipal>(FindObjectsInactive.Include);
        if (menu == null || menu.painelComoJogar == null)
        {
            EditorUtility.DisplayDialog("Fake Hunter", "Abra a cena Menu (não encontrei o MenuPrincipal com o painel Como jogar).", "OK");
            return;
        }
        var card = menu.painelComoJogar.transform.Find("CardComoJogar");
        if (card == null)
        {
            EditorUtility.DisplayDialog("Fake Hunter", "Não encontrei o CardComoJogar dentro do PainelComoJogar.", "OK");
            return;
        }

        string pasta = FakeHunterEstilo.AcharPastaEstilo();
        if (pasta == null)
        {
            EditorUtility.DisplayDialog("Fake Hunter", "Não encontrei a pasta Art/UI/Estilo.", "OK");
            return;
        }
        FakeHunterEstilo.ConfigurarImportacao(pasta);
        TMP_FontAsset bebas = FakeHunterEstilo.AcharFonte("BebasNeue-Regular SDF", "Bebas");
        TMP_FontAsset nunito = FakeHunterEstilo.AcharFonte("Nunito-Regular SDF", "Nunito-Regular");

        Undo.RegisterFullObjectHierarchyUndo(card.gameObject, "Criar página de selos");

        // ---------- Página 1: regras (reaproveita o texto atual) ----------
        var regras = card.Find("PaginaRegras");
        if (regras == null)
        {
            var go = NovoObjeto("PaginaRegras", card);
            Undo.RegisterCreatedObjectUndo(go, "Criar página de regras");
            Esticar((RectTransform)go.transform);
            regras = go.transform;
            var corpo = card.Find("TXT_ComoJogarCorpo");
            if (corpo != null) Undo.SetTransformParent(corpo, regras, "Mover texto");
        }

        // Recria a página 2 e o botão de ida
        foreach (var nome in new[] { "PaginaSelos" })
        {
            var velho = card.Find(nome);
            if (velho != null) Undo.DestroyObjectImmediate(velho.gameObject);
        }
        var irVelho = regras.Find("BTN_VerSelos");
        if (irVelho != null) Undo.DestroyObjectImmediate(irVelho.gameObject);

        var pagina = NovoObjeto("PaginaSelos", card);
        Undo.RegisterCreatedObjectUndo(pagina, "Criar página de selos");
        Esticar((RectTransform)pagina.transform);

        // ---------- Conteúdo da página 2 ----------
        var intro = Texto(pagina.transform, "TXT_SelosIntro",
            "No fim do jogo, você recebe um selo de acordo com o seu desempenho em todas as fases:",
            nunito, 22, TextoEscuro, TextAlignmentOptions.Top, new Vector2(0, 228), new Vector2(500, 70));
        intro.textWrappingMode = TextWrappingModes.Normal;

        var selos = new[]
        {
            (Classificacao.Selo.Especialista, "selo_especialista"),
            (Classificacao.Selo.CacadorVoraz, "selo_voraz"),
            (Classificacao.Selo.CacadorVegetariano, "selo_vegetariano"),
            (Classificacao.Selo.CacadoPelasFakeNews, "selo_cacado"),
        };
        float y = 120f;
        foreach (var (selo, arquivo) in selos)
        {
            var linha = NovoObjeto("Selo_" + selo, pagina.transform);
            Centralizar((RectTransform)linha.transform, new Vector2(0, y), new Vector2(500, 110));

            var icone = NovoObjeto("IMG_Selo", linha.transform);
            Centralizar((RectTransform)icone.transform, new Vector2(-195, 0), new Vector2(104, 104));
            var img = icone.AddComponent<Image>();
            img.sprite = FakeHunterEstilo.Carregar<Sprite>($"{pasta}/Selos/{arquivo}.png");
            img.preserveAspect = true; img.raycastTarget = false;

            Texto(linha.transform, "TXT_Nome", Classificacao.Titulo(selo), bebas, 32, Classificacao.Cor(selo),
                  TextAlignmentOptions.Left, new Vector2(55, 22), new Vector2(380, 40));
            var d = Texto(linha.transform, "TXT_Descricao", Classificacao.DescricaoCurta(selo), nunito, 20, TextoEscuro,
                          TextAlignmentOptions.TopLeft, new Vector2(55, -22), new Vector2(380, 56));
            d.textWrappingMode = TextWrappingModes.Normal;
            y -= 115f;
        }

        // ---------- Botões ----------
        var fechar = card.Find("BTN_FecharComoJogar");
        if (fechar != null)
        {
            var frt = (RectTransform)fechar;
            frt.anchoredPosition = new Vector2(-130, frt.anchoredPosition.y);
            var fb = fechar.GetComponent<Button>();
            if (fb != null) FakeHunterEstilo.EstilizarSeConhecido(fb, pasta, bebas);
            fechar.SetAsLastSibling();
        }
        float yBotoes = fechar != null ? ((RectTransform)fechar).anchoredPosition.y : -380f;

        var ir = Botao(regras, "BTN_VerSelos", "Selos", new Vector2(130, yBotoes));
        UnityEventTools.AddPersistentListener(ir.onClick, new UnityAction(menu.MostrarPaginaSelos));
        FakeHunterEstilo.EstilizarSeConhecido(ir, pasta, bebas);
        AdicionarSomDeClique(ir.gameObject);

        var voltar = Botao(pagina.transform, "BTN_VoltarRegras", "Voltar", new Vector2(130, yBotoes));
        UnityEventTools.AddPersistentListener(voltar.onClick, new UnityAction(menu.MostrarPaginaRegras));
        FakeHunterEstilo.EstilizarSeConhecido(voltar, pasta, bebas);
        AdicionarSomDeClique(voltar.gameObject);

        // ---------- Liga no MenuPrincipal ----------
        var so = new SerializedObject(menu);
        so.FindProperty("paginaRegras").objectReferenceValue = regras.gameObject;
        so.FindProperty("paginaSelos").objectReferenceValue = pagina;
        var titulo = card.Find("TXT_ComoJogarTitulo");
        if (titulo != null) so.FindProperty("txtComoJogarTitulo").objectReferenceValue = titulo.GetComponent<TMP_Text>();
        so.ApplyModifiedProperties();

        regras.gameObject.SetActive(true);
        pagina.SetActive(false);
        Selection.activeGameObject = pagina;
        EditorSceneManager.MarkSceneDirty(menu.gameObject.scene);

        EditorUtility.DisplayDialog("Fake Hunter – página de selos",
            "O Como jogar agora tem duas páginas: Regras e Selos.\n\n" +
            "A PaginaSelos fica desativada. Para ver no editor, ative a PaginaSelos e desative a PaginaRegras (e volte antes de salvar).\n\n" +
            "Salve a cena (Ctrl+S).", "OK");
    }

    // ---------------- Utilitários ----------------

    static GameObject NovoObjeto(string nome, Transform pai)
    {
        var go = new GameObject(nome, typeof(RectTransform));
        go.layer = LayerMask.NameToLayer("UI");
        go.transform.SetParent(pai, false);
        return go;
    }

    static void Esticar(RectTransform rt)
    {
        rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero; rt.offsetMax = Vector2.zero;
    }

    static void Centralizar(RectTransform rt, Vector2 pos, Vector2 tamanho)
    {
        rt.anchorMin = rt.anchorMax = rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos; rt.sizeDelta = tamanho;
    }

    static TextMeshProUGUI Texto(Transform pai, string nome, string conteudo, TMP_FontAsset fonte, float tamanho,
                                 Color cor, TextAlignmentOptions alinhamento, Vector2 pos, Vector2 caixa)
    {
        var go = NovoObjeto(nome, pai);
        Centralizar((RectTransform)go.transform, pos, caixa);
        var t = go.AddComponent<TextMeshProUGUI>();
        if (fonte != null) t.font = fonte;
        t.text = conteudo; t.fontSize = tamanho; t.color = cor; t.alignment = alinhamento;
        t.raycastTarget = false; t.textWrappingMode = TextWrappingModes.NoWrap;
        return t;
    }

    static Button Botao(Transform pai, string nome, string rotulo, Vector2 pos)
    {
        var go = NovoObjeto(nome, pai);
        Centralizar((RectTransform)go.transform, pos, new Vector2(230, 80));
        var img = go.AddComponent<Image>();
        var btn = go.AddComponent<Button>();
        btn.targetGraphic = img;
        var txt = NovoObjeto("Texto", go.transform);
        Esticar((RectTransform)txt.transform);
        var t = txt.AddComponent<TextMeshProUGUI>();
        t.text = rotulo; t.alignment = TextAlignmentOptions.Center;
        return btn;
    }

    static void AdicionarSomDeClique(GameObject go)
    {
        foreach (var guid in AssetDatabase.FindAssets("SomAoClicar t:MonoScript"))
        {
            var ms = AssetDatabase.LoadAssetAtPath<MonoScript>(AssetDatabase.GUIDToAssetPath(guid));
            var tipo = ms != null ? ms.GetClass() : null;
            if (tipo != null && typeof(MonoBehaviour).IsAssignableFrom(tipo))
            {
                if (go.GetComponent(tipo) == null) go.AddComponent(tipo);
                return;
            }
        }
    }
}
#endif
