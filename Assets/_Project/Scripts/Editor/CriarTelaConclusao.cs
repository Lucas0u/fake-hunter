#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

// Menu: FakeHunter > Criar tela de conclusão (cena Gameplay)
// Monta o PainelConclusao no Canvas da cena Gameplay e liga tudo no GameManager.
// Pode ser rodado de novo para recriar a tela. Ctrl+Z desfaz.
public static class CriarTelaConclusao
{
    static readonly Color Marinho = new Color32(27, 42, 74, 255);
    static readonly Color TextoEscuro = new Color32(43, 43, 43, 255);
    static readonly Color TextoCinza = new Color32(90, 96, 110, 255);
    static readonly Color Creme = new Color32(246, 240, 232, 255);

    [MenuItem("FakeHunter/Criar tela de conclusão (cena Gameplay)")]
    public static void Criar()
    {
        var gm = Object.FindFirstObjectByType<GameManager>(FindObjectsInactive.Include);
        if (gm == null)
        {
            EditorUtility.DisplayDialog("Fake Hunter", "Abra a cena Gameplay (não encontrei o GameManager).", "OK");
            return;
        }

        string pasta = FakeHunterEstilo.AcharPastaEstilo();
        if (pasta == null)
        {
            EditorUtility.DisplayDialog("Fake Hunter", "Não encontrei a pasta Art/UI/Estilo. Ela vem do pacote do passo 2.", "OK");
            return;
        }
        FakeHunterEstilo.ConfigurarImportacao(pasta);

        Transform canvas = gm.painelGameOver != null ? gm.painelGameOver.transform.parent
                         : gm.painelFimDeFase != null ? gm.painelFimDeFase.transform.parent
                         : Object.FindFirstObjectByType<Canvas>(FindObjectsInactive.Include)?.transform;
        if (canvas == null)
        {
            EditorUtility.DisplayDialog("Fake Hunter", "Não encontrei o Canvas da cena.", "OK");
            return;
        }

        var existente = canvas.Find("PainelConclusao");
        if (existente != null)
        {
            if (!EditorUtility.DisplayDialog("Fake Hunter", "Já existe um PainelConclusao. Quer recriar?", "Recriar", "Cancelar"))
                return;
            Undo.DestroyObjectImmediate(existente.gameObject);
        }

        TMP_FontAsset bebas = FakeHunterEstilo.AcharFonte("BebasNeue-Regular SDF", "Bebas");
        TMP_FontAsset nunito = FakeHunterEstilo.AcharFonte("Nunito-Regular SDF", "Nunito-Regular");

        // ---------- Painel de fundo (tela cheia, bloqueia cliques) ----------
        var painel = NovoObjeto("PainelConclusao", canvas);
        Undo.RegisterCreatedObjectUndo(painel, "Criar tela de conclusão");
        Esticar((RectTransform)painel.transform);
        var fundo = painel.AddComponent<Image>();
        var fundoRef = gm.painelFimDeFase != null ? gm.painelFimDeFase.GetComponent<Image>() : null;
        if (fundoRef != null && fundoRef.sprite != null) { fundo.sprite = fundoRef.sprite; fundo.color = Color.white; }
        else fundo.color = new Color(0.08f, 0.12f, 0.19f, 0.92f);
        painel.transform.SetAsLastSibling();

        // ---------- Cartão ----------
        var card = NovoObjeto("CardConclusao", painel.transform);
        var crt = (RectTransform)card.transform;
        Centralizar(crt, new Vector2(0, -10), new Vector2(1240, 880));
        var cardImg = card.AddComponent<Image>();
        Image cardRef = null;
        if (gm.painelFimDeFase != null)
        {
            var t = gm.painelFimDeFase.transform.Find("CardFimDeFase");
            if (t != null) cardRef = t.GetComponent<Image>();
        }
        if (cardRef != null)
        {
            cardImg.sprite = cardRef.sprite; cardImg.type = cardRef.type;
            cardImg.color = cardRef.color; cardImg.pixelsPerUnitMultiplier = cardRef.pixelsPerUnitMultiplier;
        }
        else cardImg.color = Creme;

        // ---------- Cabeçalho ----------
        Texto(card.transform, "TXT_ConclusaoTitulo", "Investigação concluída!", bebas, 68, Marinho,
              TextAlignmentOptions.Center, new Vector2(0, 375), new Vector2(1100, 80));
        Texto(card.transform, "TXT_ConclusaoSubtitulo", "Seu desempenho em todas as fases", nunito, 24, TextoCinza,
              TextAlignmentOptions.Center, new Vector2(0, 318), new Vector2(1100, 40));

        // ---------- Coluna esquerda: selo ----------
        var selo = NovoObjeto("IMG_Selo", card.transform);
        Centralizar((RectTransform)selo.transform, new Vector2(-320, 95), new Vector2(320, 320));
        var seloImg = selo.AddComponent<Image>();
        seloImg.preserveAspect = true; seloImg.raycastTarget = false;
        seloImg.sprite = FakeHunterEstilo.Carregar<Sprite>($"{pasta}/Selos/selo_voraz.png");

        var tSelo = Texto(card.transform, "TXT_SeloTitulo", "Caçador voraz", bebas, 54, Classificacao.Cor(Classificacao.Selo.CacadorVoraz),
                          TextAlignmentOptions.Center, new Vector2(-320, -105), new Vector2(520, 70));
        var tDesc = Texto(card.transform, "TXT_SeloDescricao", Classificacao.Descricao(Classificacao.Selo.CacadorVoraz), nunito, 24, TextoEscuro,
                          TextAlignmentOptions.Top, new Vector2(-320, -215), new Vector2(500, 140));
        tDesc.textWrappingMode = TextWrappingModes.Normal;

        // ---------- Divisória ----------
        var div = NovoObjeto("Divisoria", card.transform);
        Centralizar((RectTransform)div.transform, new Vector2(30, 20), new Vector2(4, 520));
        var divImg = div.AddComponent<Image>();
        divImg.color = new Color(Marinho.r, Marinho.g, Marinho.b, 0.15f); divImg.raycastTarget = false;

        // ---------- Coluna direita: estatísticas ----------
        var tEst = Texto(card.transform, "TXT_Estatisticas",
            "<b>Fase<pos=24%>Acertos<pos=55%>Erros<pos=76%>Pontos</b>\nFase 1<pos=24%>6<pos=55%>0<pos=76%>700\n" +
            "<b>Total<pos=24%>6<pos=55%>0<pos=76%>700</b>\n\nAproveitamento: <b>100%</b>",
            nunito, 27, TextoEscuro, TextAlignmentOptions.TopLeft, new Vector2(320, 30), new Vector2(540, 500));
        tEst.textWrappingMode = TextWrappingModes.Normal;
        tEst.lineSpacing = 18;

        // ---------- Botões ----------
        var bNovo = Botao(card.transform, "BTN_JogarDoInicio", "Jogar de novo", new Vector2(-190, -362));
        var bMenu = Botao(card.transform, "BTN_MenuConclusao", "Menu", new Vector2(190, -362));
        UnityEventTools.AddPersistentListener(bNovo.onClick, new UnityAction(gm.JogarDoInicio));
        UnityEventTools.AddPersistentListener(bMenu.onClick, new UnityAction(gm.VoltarAoMenu));
        FakeHunterEstilo.EstilizarSeConhecido(bNovo, pasta, bebas);
        FakeHunterEstilo.EstilizarSeConhecido(bMenu, pasta, bebas);
        AdicionarSomDeClique(bNovo.gameObject);
        AdicionarSomDeClique(bMenu.gameObject);

        // ---------- Liga no GameManager ----------
        var so = new SerializedObject(gm);
        so.FindProperty("painelConclusao").objectReferenceValue = painel;
        so.FindProperty("imgSeloConclusao").objectReferenceValue = seloImg;
        so.FindProperty("txtSeloTitulo").objectReferenceValue = tSelo;
        so.FindProperty("txtSeloDescricao").objectReferenceValue = tDesc;
        so.FindProperty("txtEstatisticas").objectReferenceValue = tEst;
        so.FindProperty("seloCacado").objectReferenceValue = FakeHunterEstilo.Carregar<Sprite>($"{pasta}/Selos/selo_cacado.png");
        so.FindProperty("seloVegetariano").objectReferenceValue = FakeHunterEstilo.Carregar<Sprite>($"{pasta}/Selos/selo_vegetariano.png");
        so.FindProperty("seloVoraz").objectReferenceValue = FakeHunterEstilo.Carregar<Sprite>($"{pasta}/Selos/selo_voraz.png");
        so.FindProperty("seloEspecialista").objectReferenceValue = FakeHunterEstilo.Carregar<Sprite>($"{pasta}/Selos/selo_especialista.png");
        so.ApplyModifiedProperties();

        painel.SetActive(false);
        Selection.activeGameObject = painel;
        EditorSceneManager.MarkSceneDirty(gm.gameObject.scene);

        EditorUtility.DisplayDialog("Fake Hunter – tela de conclusão",
            "PainelConclusao criado e ligado no GameManager.\n\n" +
            "Ele fica desativado. Para ver no editor, marque a caixinha do PainelConclusao (e desmarque antes de salvar).\n\n" +
            "Para testar sem jogar as 4 fases: dê Play, clique com o botão direito no componente GameManager e escolha \"Testar tela de conclusão\".\n\n" +
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
        t.text = conteudo;
        t.fontSize = tamanho;
        t.color = cor;
        t.alignment = alinhamento;
        t.richText = true;
        t.raycastTarget = false;
        t.textWrappingMode = TextWrappingModes.NoWrap;
        return t;
    }

    static Button Botao(Transform pai, string nome, string rotulo, Vector2 pos)
    {
        var go = NovoObjeto(nome, pai);
        Centralizar((RectTransform)go.transform, pos, new Vector2(340, 84));
        var img = go.AddComponent<Image>();
        var btn = go.AddComponent<Button>();
        btn.targetGraphic = img;
        var txt = NovoObjeto("Texto", go.transform);
        Esticar((RectTransform)txt.transform);
        var t = txt.AddComponent<TextMeshProUGUI>();
        t.text = rotulo;
        t.alignment = TextAlignmentOptions.Center;
        return btn;
    }

    // Usa o componente de som de clique do projeto, se o Unity o reconhecer.
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
