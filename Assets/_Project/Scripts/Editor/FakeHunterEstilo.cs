#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Menu: FakeHunter > Aplicar estilo na cena aberta
// Aplica o visual unificado (botões, ícones, HUD) na cena que estiver aberta.
// Pode ser rodado mais de uma vez; Ctrl+Z desfaz.
public static class FakeHunterEstilo
{
    // ---------------- Configuração dos botões ----------------
    class Estilo
    {
        public string cor;      // verde, vermelho, azul, amarelo
        public string icone;    // nome do ico_<icone>_branco/azul
        public float largura, altura, fonte;
        public bool iconeADireita;
        public Estilo(string cor, string icone, float l, float a, float f, bool dir = false)
        { this.cor = cor; this.icone = icone; largura = l; altura = a; fonte = f; iconeADireita = dir; }
    }

    static readonly Dictionary<string, Estilo> Botoes = new Dictionary<string, Estilo>
    {
        // Gameplay - investigação
        { "BTN_VerificarFonte",      new Estilo("azul",     "verificar_fonte",      320, 84, 28) },
        { "BTN_ConsultarReferencia", new Estilo("azul",     "consultar_referencia", 320, 84, 28) },
        { "BTN_Pesquisar",           new Estilo("azul",     "pesquisar",            320, 84, 28) },
        // Gameplay - decisão
        { "BTN_Compartilhar",        new Estilo("verde",    "compartilhar",         340, 104, 36) },
        { "BTN_NaoPropagar",         new Estilo("vermelho", "nao_propagar",         340, 104, 36) },
        // Gameplay - painéis
        { "BTN_Proxima",             new Estilo("amarelo",  "proxima",              340, 80, 28, true) },
        { "BTN_JogarDeNovo",         new Estilo("azul",     "repetir",              270, 80, 28) },
        { "BTN_MenuFim",             new Estilo("amarelo",  "menu",                 270, 80, 28) },
        { "BTN_ProximaFase",         new Estilo("verde",    "proxima",              270, 80, 28, true) },
        { "BTN_TentarDeNovo",        new Estilo("azul",     "repetir",              340, 80, 28) },
        { "BTN_MenuGameOver",        new Estilo("amarelo",  "menu",                 340, 80, 28) },
        // Menu
        { "BTN_Jogar",               new Estilo("verde",    "jogar",                400, 104, 40) },
        { "BTN_ComoJogar",           new Estilo("azul",     "ajuda",                400, 104, 40) },
        { "BTN_Sair",                new Estilo("vermelho", "sair",                 400, 104, 40) },
        { "BTN_FecharComoJogar",     new Estilo("amarelo",  "fechar",               260, 80, 28) },
        // Gameplay - tela de conclusão
        { "BTN_JogarDoInicio",       new Estilo("azul",     "repetir",              340, 84, 30) },
        { "BTN_MenuConclusao",       new Estilo("amarelo",  "menu",                 340, 84, 30) },
    };

    // Bordas 9-slice (esquerda, baixo, direita, cima) – batem com os PNGs gerados
    static readonly Vector4 BordaBotao = new Vector4(32, 38, 32, 30);
    static readonly Vector4 BordaBarra = new Vector4(14, 14, 14, 14);
    static readonly Vector4 BordaBalao = new Vector4(20, 40, 54, 20);   // cauda fica na borda inferior direita

    const float Profundidade = 8f;   // faixa inferior "3D" do botão
    const float Margem = 24f;        // margem interna esquerda/direita
    const float EspacoIcone = 10f;

    static readonly Color Branco = Color.white;
    static readonly Color Marinho = new Color32(27, 42, 74, 255);
    static readonly Color FundoHUD = new Color32(20, 31, 48, 255);
    static readonly Color Amarelo = new Color32(244, 182, 59, 255);

    // ---------------- Menu ----------------

    [MenuItem("FakeHunter/Aplicar estilo na cena aberta")]
    public static void Aplicar()
    {
        string pasta = AcharPastaEstilo();
        if (pasta == null)
        {
            EditorUtility.DisplayDialog("Fake Hunter",
                "Não encontrei a pasta Art/UI/Estilo (com btn_verde.png). Copie a pasta do zip para o projeto.", "OK");
            return;
        }

        ConfigurarImportacao(pasta);

        TMP_FontAsset bebas = AcharFonte("BebasNeue-Regular SDF", "Bebas");
        TMP_FontAsset nunitoBold = AcharFonte("Nunito-ExtraBold SDF", "Nunito-ExtraBold");

        var cena = EditorSceneManager.GetActiveScene();
        var raizes = cena.GetRootGameObjects();
        foreach (var r in raizes) Undo.RegisterFullObjectHierarchyUndo(r, "Aplicar estilo Fake Hunter");

        int nBotoes = 0;
        var log = new List<string>();

        foreach (var raiz in raizes)
        {
            foreach (var btn in raiz.GetComponentsInChildren<Button>(true))
            {
                if (!Botoes.TryGetValue(btn.name, out Estilo e)) continue;
                EstilizarBotao(btn, e, pasta, bebas, log);
                nBotoes++;
            }
        }

        bool hud = EstilizarHUD(raizes, pasta, nunitoBold, log);
        bool veredito = LigarIconesVeredito(raizes, pasta);

        EditorSceneManager.MarkSceneDirty(cena);

        string msg = $"Botões estilizados: {nBotoes}\nHUD: {(hud ? "sim" : "não encontrado nesta cena")}\n" +
                     $"Ícones de veredito: {(veredito ? "ligados" : "—")}";
        if (bebas == null) msg += "\n\nAviso: fonte BebasNeue-Regular SDF não encontrada; mantive a fonte atual dos botões.";
        if (log.Count > 0) msg += "\n\n" + string.Join("\n", log);
        msg += "\n\nConfira e salve a cena (Ctrl+S). Ctrl+Z desfaz.";
        Debug.Log("[FakeHunter] " + msg.Replace("\n", " | "));
        EditorUtility.DisplayDialog("Fake Hunter – estilo aplicado", msg, "OK");
    }

    // ---------------- Botões ----------------

    // Aplica o estilo a um botão pelo nome (usado por outros scripts de editor).
    internal static bool EstilizarSeConhecido(Button btn, string pasta, TMP_FontAsset fonte)
    {
        if (!Botoes.TryGetValue(btn.name, out Estilo e)) return false;
        EstilizarBotao(btn, e, pasta, fonte, new List<string>());
        return true;
    }


    static void EstilizarBotao(Button btn, Estilo e, string pasta, TMP_FontAsset fonte, List<string> log)
    {
        var rt = (RectTransform)btn.transform;
        bool fundoClaro = e.cor == "amarelo";
        Color corConteudo = fundoClaro ? Marinho : Branco;

        // Fundo
        var img = btn.GetComponent<Image>();
        if (img != null)
        {
            img.sprite = Carregar<Sprite>($"{pasta}/Botoes/btn_{e.cor}.png");
            img.type = Image.Type.Sliced;
            img.pixelsPerUnitMultiplier = 1f;
            img.preserveAspect = false;
            img.color = Color.white;
            btn.targetGraphic = img;
        }

        // Tamanho (também para layout groups)
        rt.sizeDelta = new Vector2(e.largura, e.altura);
        var le = btn.GetComponent<LayoutElement>();
        if (le == null) le = Undo.AddComponent<LayoutElement>(btn.gameObject);
        le.preferredWidth = e.largura; le.preferredHeight = e.altura;
        le.minWidth = e.largura; le.minHeight = e.altura;
        le.flexibleWidth = 0; le.flexibleHeight = 0;

        // Estados (tint)
        btn.transition = Selectable.Transition.ColorTint;
        var cb = btn.colors;
        cb.normalColor = Color.white;
        cb.highlightedColor = new Color(0.93f, 0.93f, 0.93f, 1f);
        cb.pressedColor = new Color(0.78f, 0.78f, 0.78f, 1f);
        cb.selectedColor = Color.white;
        cb.disabledColor = new Color(0.55f, 0.55f, 0.6f, 0.65f);
        cb.colorMultiplier = 1f;
        cb.fadeDuration = 0.08f;
        btn.colors = cb;

        float altFace = e.altura - Profundidade;
        float tamIcone = Mathf.Round(altFace * 0.46f);

        // Ícone
        var icoT = btn.transform.Find("Icone");
        GameObject ico;
        if (icoT == null)
        {
            ico = new GameObject("Icone", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            Undo.RegisterCreatedObjectUndo(ico, "Criar ícone");
            ico.transform.SetParent(btn.transform, false);
        }
        else ico = icoT.gameObject;

        var icoImg = ico.GetComponent<Image>();
        icoImg.sprite = Carregar<Sprite>($"{pasta}/Icones/ico_{e.icone}_{(fundoClaro ? "azul" : "branco")}.png");
        icoImg.raycastTarget = false;
        icoImg.preserveAspect = true;
        icoImg.color = Color.white;
        var icoRt = (RectTransform)ico.transform;
        float x = e.iconeADireita ? -Margem : Margem;
        icoRt.anchorMin = icoRt.anchorMax = new Vector2(e.iconeADireita ? 1 : 0, 0.5f);
        icoRt.pivot = new Vector2(e.iconeADireita ? 1 : 0, 0.5f);
        icoRt.sizeDelta = new Vector2(tamIcone, tamIcone);
        icoRt.anchoredPosition = new Vector2(x, Profundidade / 2f);

        // Texto
        var txt = btn.GetComponentInChildren<TMP_Text>(true);
        if (txt != null)
        {
            var tr = txt.rectTransform;
            tr.anchorMin = Vector2.zero; tr.anchorMax = Vector2.one; tr.pivot = new Vector2(0.5f, 0.5f);
            float esq = e.iconeADireita ? Margem : Margem + tamIcone + EspacoIcone;
            float dir = e.iconeADireita ? Margem + tamIcone + EspacoIcone : Margem;
            tr.offsetMin = new Vector2(esq, Profundidade);
            tr.offsetMax = new Vector2(-dir, 0);
            if (fonte != null) txt.font = fonte;
            txt.color = corConteudo;
            txt.alignment = TextAlignmentOptions.Center;
            txt.textWrappingMode = TextWrappingModes.NoWrap;
            txt.enableAutoSizing = true;
            txt.fontSizeMax = e.fonte;
            txt.fontSizeMin = e.fonte - 8;
            txt.fontSize = e.fonte;
            txt.characterSpacing = 1f;
            txt.raycastTarget = false;
        }

        // Imagens-filhas vazias (sem sprite) aparecem como retângulo branco: desliga.
        foreach (var filho in btn.GetComponentsInChildren<Image>(true))
        {
            if (filho == img || filho.gameObject == ico) continue;
            if (filho.sprite == null && filho.gameObject.activeSelf)
            {
                filho.gameObject.SetActive(false);
                log.Add($"Desativei '{filho.name}' (imagem vazia) dentro de {btn.name}.");
            }
        }
    }

    // ---------------- HUD ----------------

    static bool EstilizarHUD(GameObject[] raizes, string pasta, TMP_FontAsset fonte, List<string> log)
    {
        var hud = Achar(raizes, "PainelHUD");
        if (hud == null) return false;

        var hudImg = hud.GetComponent<Image>();
        if (hudImg != null) hudImg.color = FundoHUD;

        // Linha amarela na base do HUD
        var linha = hud.transform.Find("LinhaHUD");
        if (linha == null)
        {
            var go = new GameObject("LinhaHUD", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            Undo.RegisterCreatedObjectUndo(go, "Criar linha HUD");
            go.transform.SetParent(hud.transform, false);
            linha = go.transform;
        }
        var lrt = (RectTransform)linha;
        lrt.anchorMin = new Vector2(0, 0); lrt.anchorMax = new Vector2(1, 0); lrt.pivot = new Vector2(0.5f, 0);
        lrt.sizeDelta = new Vector2(0, 4); lrt.anchoredPosition = Vector2.zero;
        var limg = linha.GetComponent<Image>(); limg.color = Amarelo; limg.raycastTarget = false;
        linha.SetAsLastSibling();

        // Barra de confiança
        var sld = hud.GetComponentInChildren<Slider>(true);
        if (sld != null)
        {
            var srt = (RectTransform)sld.transform;
            srt.anchoredPosition = new Vector2(100, srt.anchoredPosition.y);
            srt.sizeDelta = new Vector2(380, 28);
            sld.interactable = false;
            sld.transition = Selectable.Transition.None;
            if (sld.handleRect != null) sld.handleRect.gameObject.SetActive(false);

            var fundo = sld.transform.Find("Background");
            if (fundo != null)
            {
                var fr = (RectTransform)fundo;
                fr.anchorMin = Vector2.zero; fr.anchorMax = Vector2.one; fr.offsetMin = fr.offsetMax = Vector2.zero;
                var fi = fundo.GetComponent<Image>();
                fi.sprite = Carregar<Sprite>($"{pasta}/HUD/barra_trilho.png");
                fi.type = Image.Type.Sliced; fi.color = Color.white; fi.pixelsPerUnitMultiplier = 1f;
            }
            if (sld.fillRect != null)
            {
                var area = (RectTransform)sld.fillRect.parent;
                area.anchorMin = Vector2.zero; area.anchorMax = Vector2.one;
                area.offsetMin = new Vector2(3, 3); area.offsetMax = new Vector2(-3, -3);
                sld.fillRect.sizeDelta = new Vector2(0, 0);
                var fill = sld.fillRect.GetComponent<Image>();
                fill.sprite = Carregar<Sprite>($"{pasta}/HUD/barra_preenchimento.png");
                fill.type = Image.Type.Sliced; fill.pixelsPerUnitMultiplier = 1.3f;
            }

            CriarSelo(sld.transform, "IconeConfianca", $"{pasta}/HUD/selo_confianca.png", fixoAEsquerda: true);
        }

        var txtConf = Achar(raizes, "TXT_ConfiancaValor");
        if (txtConf != null)
        {
            var r = (RectTransform)txtConf.transform;
            r.anchoredPosition = new Vector2(500, r.anchoredPosition.y);
        }

        foreach (var nome in new[] { "TXT_ConfiancaValor", "TXT_Progresso", "TXT_Sequencia", "TXT_Pontos" })
        {
            var go = Achar(raizes, nome);
            if (go == null) continue;
            var t = go.GetComponent<TMP_Text>();
            if (fonte != null) t.font = fonte;
            t.fontSize = 26;
            t.enableAutoSizing = false;
            t.textWrappingMode = TextWrappingModes.NoWrap;
        }

        CriarSelo(Achar(raizes, "TXT_Progresso")?.transform, "IconeProgresso", $"{pasta}/HUD/selo_progresso.png");
        CriarSelo(Achar(raizes, "TXT_Sequencia")?.transform, "IconeSequencia", $"{pasta}/HUD/selo_sequencia.png");
        CriarSelo(Achar(raizes, "TXT_Pontos")?.transform, "IconePontos", $"{pasta}/HUD/selo_pontos.png");
        return true;
    }

    static void CriarSelo(Transform pai, string nome, string caminho, bool fixoAEsquerda = false)
    {
        if (pai == null) return;
        var t = pai.Find(nome);
        GameObject go;
        if (t == null)
        {
            go = new GameObject(nome, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            Undo.RegisterCreatedObjectUndo(go, "Criar selo");
            go.transform.SetParent(pai, false);
        }
        else go = t.gameObject;

        var img = go.GetComponent<Image>();
        img.sprite = Carregar<Sprite>(caminho);
        img.raycastTarget = false; img.preserveAspect = true; img.color = Color.white;
        var rt = (RectTransform)go.transform;
        rt.sizeDelta = new Vector2(52, 52);

        if (fixoAEsquerda)
        {
            rt.anchorMin = rt.anchorMax = new Vector2(0, 0.5f);
            rt.pivot = new Vector2(1, 0.5f);
            rt.anchoredPosition = new Vector2(-12, 0);
        }
        else
        {
            var seguir = go.GetComponent<IconeAoLadoDoTexto>();
            if (seguir == null) seguir = Undo.AddComponent<IconeAoLadoDoTexto>(go);
            seguir.texto = pai.GetComponent<TMP_Text>();
            seguir.espaco = 10f;
        }
    }

    // ---------------- Veredito ----------------

    static bool LigarIconesVeredito(GameObject[] raizes, string pasta)
    {
        PainelNoticia painel = null;
        foreach (var r in raizes) { painel = r.GetComponentInChildren<PainelNoticia>(true); if (painel) break; }
        if (painel == null) return false;

        var so = new SerializedObject(painel);
        so.FindProperty("iconeVerdadeiro").objectReferenceValue = Carregar<Sprite>($"{pasta}/HUD/selo_verdadeiro.png");
        so.FindProperty("iconeFalso").objectReferenceValue = Carregar<Sprite>($"{pasta}/HUD/selo_falso.png");
        so.FindProperty("iconeParcial").objectReferenceValue = Carregar<Sprite>($"{pasta}/HUD/selo_parcial.png");
        so.ApplyModifiedProperties();
        return true;
    }

    // ---------------- Utilitários ----------------

    internal static string AcharPastaEstilo()
    {
        foreach (var guid in AssetDatabase.FindAssets("btn_verde t:Texture2D"))
        {
            string p = AssetDatabase.GUIDToAssetPath(guid);
            if (Path.GetFileNameWithoutExtension(p) != "btn_verde") continue;
            string botoes = Path.GetDirectoryName(p).Replace('\\', '/');
            return Path.GetDirectoryName(botoes).Replace('\\', '/');
        }
        return null;
    }

    internal static void ConfigurarImportacao(string pasta)
    {
        foreach (var guid in AssetDatabase.FindAssets("t:Texture2D", new[] { pasta }))
        {
            string p = AssetDatabase.GUIDToAssetPath(guid);
            var ti = AssetImporter.GetAtPath(p) as TextureImporter;
            if (ti == null) continue;

            Vector4 borda = Vector4.zero;
            string nome = Path.GetFileNameWithoutExtension(p);
            if (nome.StartsWith("btn_")) borda = BordaBotao;
            else if (nome.StartsWith("barra_")) borda = BordaBarra;
            else if (nome.StartsWith("balao_")) borda = BordaBalao;

            bool mudou = ti.textureType != TextureImporterType.Sprite
                      || ti.spriteImportMode != SpriteImportMode.Single
                      || ti.mipmapEnabled
                      || ti.spriteBorder != borda
                      || ti.textureCompression != TextureImporterCompression.Uncompressed;
            if (!mudou) continue;

            ti.textureType = TextureImporterType.Sprite;
            ti.spriteImportMode = SpriteImportMode.Single;
            ti.mipmapEnabled = false;
            ti.alphaIsTransparency = true;
            ti.spriteBorder = borda;
            ti.textureCompression = TextureImporterCompression.Uncompressed;
            ti.SaveAndReimport();
        }
    }

    internal static TMP_FontAsset AcharFonte(string nomeExato, string trecho)
    {
        var fontes = AssetDatabase.FindAssets("t:TMP_FontAsset")
            .Select(g => AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(AssetDatabase.GUIDToAssetPath(g)))
            .Where(f => f != null).ToList();
        return fontes.FirstOrDefault(f => f.name == nomeExato)
            ?? fontes.FirstOrDefault(f => f.name.Contains(trecho));
    }

    internal static T Carregar<T>(string caminho) where T : Object
    {
        var a = AssetDatabase.LoadAssetAtPath<T>(caminho);
        if (a == null) Debug.LogWarning("[FakeHunter] Não encontrei " + caminho);
        return a;
    }

    static GameObject Achar(GameObject[] raizes, string nome)
    {
        foreach (var r in raizes)
            foreach (var t in r.GetComponentsInChildren<Transform>(true))
                if (t.name == nome) return t.gameObject;
        return null;
    }
}
#endif
