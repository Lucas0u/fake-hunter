# Fake Hunter – Pendências

Lista do que ainda falta ou precisa ser conferido. Marque os itens conforme forem concluídos.

## 1. Verificação no Unity (prioridade máxima)

Toda a cena, os scripts e os assets foram montados **sem abrir o editor**. Antes de qualquer outra coisa:

- [ ] Abrir o projeto no Unity 6 (`6000.5` ou superior) e confirmar que **não há erros de compilação** no Console.
- [ ] Abrir `Assets/_Project/Scenes/Menu.unity`, dar Play e testar: Jogar, Como jogar (abrir/fechar), Sair.
- [ ] Na cena `Gameplay`, testar uma fase completa: investigar (máx. 2 por notícia), decidir, ver o painel de explicação, avançar.
- [ ] Terminar a Fase 1 e conferir o painel de resumo com os botões **Jogar de novo / Próxima fase / Menu**; "Próxima fase" deve carregar a Fase 2 e sumir na Fase 3.
- [ ] Errar até a confiança chegar a 0% e conferir o painel de **Game Over**.
- [ ] Conferir visualmente: fita e clipe do cartão (`CardNoticia`) em tamanho razoável — se estiverem grandes, ajustar **Pixels Per Unit Multiplier** na Image; textos dos botões sem sobrepor os ícones; barra de confiança mudando de cor.
- [ ] Se algum texto aparecer com fonte errada: `Window > TextMeshPro > Import TMP Essential Resources`.

## 2. Arte que ainda falta

Prompts prontos em `PROMPTS_GERADORES.md`. Fluxo: gerar → remover fundo (`#FF00FF` ou `#00FF00`) → redimensionar → salvar com o nome indicado → importar como *Sprite (2D and UI)*.

- [ ] **5 imagens de canal** (`canal_whatsapp`, `canal_portal_oficial`, `canal_jornal`, `canal_blog`, `canal_rede_social`, 800x450). Hoje são retângulos de cor sólida em `Assets/_Project/Art/Sprites/Noticias`; basta salvar por cima mantendo o nome e o `.meta`.
- [ ] **11 avatares das fontes** (256x256, transparente) → arrastar no campo **Avatar** de cada `Fonte_*.asset`.
- [ ] `painel_explicacao` (900x620) – a versão gerada veio com formas extras e foi descartada. Ao gerar de novo, **não colocar a lista da paleta no prompt**, só as duas cores da peça. Ligar em `CardExplicacao`, `CardFimDeFase` e `CardGameOver` (Image Type: Sliced).
- [ ] `painel_hud` (1920x110) – mesmo problema; opcional (a barra de cor sólida funciona).
- [ ] Ícones (`ico_*`, 128x128) – opcionais; só 2 foram gerados e parecem trocados entre si. Não estão ligados na cena.
- [ ] Ícone do aplicativo: `Art/UI/icone_app.png` existe, mas ainda não foi definido em `Project Settings > Player > Icon`.

## 3. Código / cenas

- [ ] Estado "pressionado" dos botões usa só escurecimento de cor (Transition: Color Tint). Se quiser sprites próprios, gerar versões pressionadas e trocar para *Sprite Swap*.
- [ ] `PainelExplicacao`, `PainelFimDeFase` e `PainelGameOver` usam o cartão genérico até o `painel_explicacao` ser gerado.
- [ ] Áudio: pastas `Audio/Music` e `Audio/SFX` estão vazias. Sugestão: som de acerto/erro, clique de botão e trilha leve no menu (adicionar `AudioSource` no `GameManager` e tocar em `AvaliarDecisao`).
- [ ] Sistema de progresso persistente (salvar melhor pontuação com `PlayerPrefs`).
- [ ] Tela de seleção de fases no menu (hoje "Jogar" sempre começa na Fase 1).
- [ ] Ajustar valores de balanceamento após playtest: `perdaPorErro` (20), `ganhoPorAcerto` (5), `investigacoesPorNoticia` (2).

## 4. Conteúdo

- [ ] Revisar os textos das 18 notícias com o professor/orientador (tom, clareza, adequação).
- [ ] Ideias para mais notícias: deepfake em vídeo, print de tuíte falso, corrente de "criança desaparecida" antiga, promoção de influenciador sem aviso de publicidade.

## 5. Repositório

- [ ] Abrir o Pull Request da branch `melhorias-gameplay` para a `main`:
      https://github.com/Lucas0u/fake-hunter/pull/new/melhorias-gameplay
- [ ] Após o merge, apagar a pasta `fake-hunter-main` (cópia do ZIP, sem git) e trabalhar só no clone.
- [ ] Padronizar fim de linha: o repositório mistura CRLF/LF (o Git avisa "LF will be replaced by CRLF"). Adicionar um `.gitattributes` com `* text=auto` resolve.

## Referências

- `README.md` – visão geral do jogo.
- `COMO_CONFIGURAR.md` – hierarquia da cena e como ligar imagens.
- `PROMPTS_GERADORES.md` – prompts de todas as imagens (um por imagem, em inglês).
- `PROMPTS_IMAGENS.md` – versão original dos prompts em português, com a lista completa de peças.
