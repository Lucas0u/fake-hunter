#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

// Menu: FakeHunter > Criar botão de dica (cena Gameplay)
// Cria o ícone de lâmpada no canto do PainelResultado e o balão com a dica,
// e liga tudo no PainelNoticia. O ícone só aparece em fases que têm dica (Fase 4).
public static class CriarBotaoDica
{
    static readonly Color Marinho = new Color32(27, 42, 74, 255);

    [MenuItem("FakeHunter/Criar botão de dica (cena Gameplay)")]
    public static void Criar()
    {
        var painel = Object.FindFirstObjectByType<PainelNoticia>(FindObjectsInactive.Include);
        if (painel == null)
        {
            EditorUtility.DisplayDialog("Fake Hunter", "Abra a cena Gameplay (não encontrei o PainelNoticia).", "OK");
            return;
        }

        string pasta = FakeHunterEstilo.AcharPastaEstilo();
        if (pasta == null)
        {
            EditorUtility.DisplayDialog("Fake Hunter", "Não encontrei a pasta Art/UI/Estilo.", "OK");
            return;
        }
        FakeHunterEstilo.ConfigurarImportacao(pasta);

        // Pai: o PainelResultado (fundo escuro onde aparecem os resultados)
        Transform pai = null;
        var achado = GameObject.Find("PainelResultado");
        if (achado != null) pai = achado.transform;
        else if (painel.txtResultado != null) pai = painel.txtResultado.transform.parent;
        if (pai == null)
        {
            EditorUtility.DisplayDialog("Fake Hunter", "Não encontrei o PainelResultado.", "OK");
            return;
        }

        foreach (var nome in new[] { "BTN_Dica", "BalaoDica" })
        {
            var velho = pai.Find(nome);
            if (velho != null) Undo.DestroyObjectImmediate(velho.gameObject);
        }

        TMP_FontAsset bebas = FakeHunterEstilo.AcharFonte("BebasNeue-Regular SDF", "Bebas");
        TMP_FontAsset nunito = FakeHunterEstilo.AcharFonte("Nunito-Regular SDF", "Nunito-Regular");

        // ---------- Ícone ----------
        var icone = NovoObjeto("BTN_Dica", pai);
        Undo.RegisterCreatedObjectUndo(icone, "Criar botão de dica");
        var irt = (RectTransform)icone.transform;
        irt.anchorMin = irt.anchorMax = new Vector2(1, 1);
        irt.pivot = new Vector2(0.5f, 0.5f);
        irt.anchoredPosition = new Vector2(-70, -8);
        irt.sizeDelta = new Vector2(76, 76);
        var iimg = icone.AddComponent<Image>();
        iimg.sprite = FakeHunterEstilo.Carregar<Sprite>($"{pasta}/HUD/selo_dica.png");
        iimg.preserveAspect = true;
        var ibtn = icone.AddComponent<Button>();
        ibtn.targetGraphic = iimg;
        var cores = ibtn.colors;
        cores.highlightedColor = new Color(1f, 0.95f, 0.8f, 1f);
        cores.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
        ibtn.colors = cores;
        UnityEventTools.AddPersistentListener(ibtn.onClick, new UnityAction(painel.OnDica));

        // ---------- Balão ----------
        var balao = NovoObjeto("BalaoDica", pai);
        Undo.RegisterCreatedObjectUndo(balao, "Criar balão de dica");
        var brt = (RectTransform)balao.transform;
        brt.anchorMin = brt.anchorMax = new Vector2(1, 1);
        brt.pivot = new Vector2(1, 0);
        brt.anchoredPosition = new Vector2(-30, 34);
        brt.sizeDelta = new Vector2(580, 200);
        var bimg = balao.AddComponent<Image>();
        bimg.sprite = FakeHunterEstilo.Carregar<Sprite>($"{pasta}/HUD/balao_dica.png");
        bimg.type = Image.Type.Sliced;
        var bbtn = balao.AddComponent<Button>();           // clicar no balão também fecha
        bbtn.targetGraphic = bimg;
        bbtn.transition = Selectable.Transition.None;
        UnityEventTools.AddPersistentListener(bbtn.onClick, new UnityAction(painel.OnDica));

        var titulo = Texto(balao.transform, "TXT_DicaTitulo", "Dica", bebas, 32, Marinho, TextAlignmentOptions.TopLeft);
        var trt = titulo.rectTransform;
        trt.anchorMin = new Vector2(0, 1); trt.anchorMax = new Vector2(1, 1); trt.pivot = new Vector2(0.5f, 1);
        trt.offsetMin = new Vector2(24, -56); trt.offsetMax = new Vector2(-24, -14);

        var corpo = Texto(balao.transform, "TXT_Dica",
            "Nesta fase a ficha da fonte não mostra se ela é confiável.", nunito, 22, new Color32(43, 43, 43, 255),
            TextAlignmentOptions.TopLeft);
        corpo.textWrappingMode = TextWrappingModes.Normal;
        corpo.enableAutoSizing = true; corpo.fontSizeMin = 16; corpo.fontSizeMax = 22;
        var crt = corpo.rectTransform;
        crt.anchorMin = Vector2.zero; crt.anchorMax = Vector2.one;
        crt.offsetMin = new Vector2(24, 42); crt.offsetMax = new Vector2(-24, -52);

        icone.transform.SetAsLastSibling();
        balao.transform.SetAsLastSibling();
        balao.SetActive(false);

        // ---------- Liga no PainelNoticia ----------
        var so = new SerializedObject(painel);
        so.FindProperty("btnDica").objectReferenceValue = ibtn;
        so.FindProperty("balaoDica").objectReferenceValue = balao;
        so.FindProperty("txtDica").objectReferenceValue = corpo;
        so.ApplyModifiedProperties();

        Selection.activeGameObject = icone;
        EditorSceneManager.MarkSceneDirty(painel.gameObject.scene);
        EditorUtility.DisplayDialog("Fake Hunter – botão de dica",
            "Ícone de dica criado no canto do PainelResultado e ligado no PainelNoticia.\n\n" +
            "Ele só aparece nas fases que têm dica (a Fase 4). Clique nele para abrir o balão e clique de novo para fechar.\n\n" +
            "Salve a cena (Ctrl+S).", "OK");
    }

    static GameObject NovoObjeto(string nome, Transform pai)
    {
        var go = new GameObject(nome, typeof(RectTransform));
        go.layer = LayerMask.NameToLayer("UI");
        go.transform.SetParent(pai, false);
        return go;
    }

    static TextMeshProUGUI Texto(Transform pai, string nome, string conteudo, TMP_FontAsset fonte, float tamanho,
                                 Color cor, TextAlignmentOptions alinhamento)
    {
        var go = NovoObjeto(nome, pai);
        var t = go.AddComponent<TextMeshProUGUI>();
        if (fonte != null) t.font = fonte;
        t.text = conteudo; t.fontSize = tamanho; t.color = cor; t.alignment = alinhamento;
        t.raycastTarget = false; t.textWrappingMode = TextWrappingModes.NoWrap;
        return t;
    }
}
#endif
