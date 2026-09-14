# Fake Hunter – Guia de configuração

## 1. O que já está pronto na cena `Gameplay`

A cena foi reconstruída com todos os painéis criados e ligados aos scripts. Hierarquia do `Canvas`:

```
Canvas
├── Background                 (fundo geral – coloque aqui o sprite bg_gameplay.png)
├── PainelHUD                  (barra superior)
│   ├── SLD_Confianca          (barra de confiança; o Fill muda de cor)
│   ├── TXT_ConfiancaValor
│   ├── TXT_Progresso          ("Notícia 1 de 6")
│   ├── TXT_Sequencia          ("Sequência: 3x")
│   └── TXT_Pontos
├── CardNoticia                (cartão da notícia – sprite painel_noticia.png)
│   ├── IMG_Noticia            (imagem da notícia, 400x225)
│   ├── TXT_Titulo / TXT_Corpo
│   ├── IMG_Avatar             (avatar da fonte, 64x64)
│   └── TXT_Autor / TXT_Fonte / TXT_Data
├── PainelInvestigacao
│   ├── BTN_VerificarFonte → PainelNoticia.OnVerificarFonte
│   ├── BTN_ConsultarReferencia → PainelNoticia.OnConsultarReferencia
│   └── BTN_Pesquisar → PainelNoticia.OnPesquisar
├── TXT_InvestigacoesRestantes
├── PainelResultado
│   └── TXT_Resultado
├── PainelDecisao
│   ├── BTN_Compartilhar → PainelNoticia.OnCompartilhar
│   └── BTN_NaoPropagar → PainelNoticia.OnNaoPropagar
├── TXT_Feedback
├── PainelExplicacao           (inativo; aparece após cada decisão)
│   └── CardExplicacao
│       ├── TXT_ExplicacaoTitulo / TXT_ExplicacaoVeredito / TXT_ExplicacaoCorpo
│       └── BTN_Proxima → PainelNoticia.OnProximaNoticia
├── PainelFimDeFase            (inativo)
│   └── CardFimDeFase
│       ├── TXT_FimTitulo / TXT_ResumoFase
│       ├── BTN_JogarDeNovo → GameManager.ReiniciarFase
│       └── BTN_MenuFim → GameManager.VoltarAoMenu
├── PainelGameOver             (inativo)
│   └── CardGameOver
│       ├── TXT_GameOverTitulo / TXT_ResumoGameOver
│       ├── BTN_TentarDeNovo → GameManager.ReiniciarFase
│       └── BTN_MenuGameOver → GameManager.VoltarAoMenu
└── PainelNoticia              (objeto com o script PainelNoticia; Fase Atual = Fase1)
```

Todos os campos de `PainelNoticia` e `GameManager` já estão preenchidos. Os painéis usam cores
sólidas e o sprite padrão do Unity; basta trocar pelos PNGs gerados (ver `PROMPTS_IMAGENS.md`).

### Se algo aparecer errado ao abrir
- Se o Unity reclamar de "missing script", clique no objeto e reatribua o script indicado acima.
- Se os textos aparecerem com fonte estranha, selecione-os e escolha a fonte `LiberationSans SDF`
  (Window > TextMeshPro > Import TMP Essential Resources, caso ainda não tenha importado).

## 2. Conteúdo

- **11 fontes** em `Assets/_Project/Data/Fontes`
- **18 notícias** em `Assets/_Project/Data/Noticias` (6 fáceis, 6 médias, 6 difíceis)
- **3 fases** em `Assets/_Project/Data/Fases` (`Fase1` → `Fase2` → `Fase3`)

Para testar outra fase, troque o campo **Fase Atual** do objeto `PainelNoticia`.
Para criar conteúdo novo: botão direito na pasta > `Create > FakeHunter > Noticia / Fonte / FaseDeNoticias`.

## 3. Colocando as imagens

1. Importe os PNGs em `Assets/_Project/Art/...` com **Texture Type: Sprite (2D and UI)**.
2. Fundo: selecione `Canvas > Background` e arraste `bg_gameplay.png` em **Source Image**.
3. Painéis (`CardNoticia`, `PainelResultado`, `CardExplicacao`, etc.): arraste o sprite em
   **Source Image**, defina **Image Type: Sliced** e configure o **Border** no Sprite Editor.
4. Botões: arraste o sprite na **Image** do botão e coloque a cor em branco.
5. Notícias: selecione cada `Noticia_*.asset` e arraste `not_*.png` no campo **Imagem**.
6. Fontes: selecione cada `Fonte_*.asset` e arraste `avatar_*.png` no campo **Avatar**.

## 4. Cena de Menu

Já existe em `Assets/_Project/Scenes/Menu.unity` (logo, botões Jogar / Como jogar / Sair e painel "Como jogar"),
e as Build Settings já têm `Menu` (0) e `Gameplay` (1). Para testar o fluxo completo, abra a cena `Menu` e dê Play.

## 4.1 Passagem de fase

Ao terminar uma fase, o painel de resumo mostra **Jogar de novo**, **Próxima fase** (só quando a fase tem
`proximaFase` configurada) e **Menu**. A fase escolhida é guardada em `ProgressoJogo.faseSelecionada` e
lida pelo `PainelNoticia` ao recarregar a cena.

## 5. Regras de pontuação (ajustáveis no GameManager)

- Acerto: `+5%` de confiança, `+100 pontos × dificuldade` (1, 2 ou 3) `+25 × sequência`.
- Erro: `-20%` de confiança e a sequência zera.
- Notícia **parcialmente verdadeira** conta como "não compartilhar".
- Confiança em `0%` = game over.
- `Investigacoes Por Noticia` (no objeto `PainelNoticia`): padrão 2; use 0 para ilimitado.
