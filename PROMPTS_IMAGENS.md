# Fake Hunter – Prompts das imagens (para o Claude Design)

Todas as imagens devem ser exportadas em **PNG** (com fundo transparente onde indicado).
No Unity, importe cada uma como **Texture Type: Sprite (2D and UI)**.

Cole primeiro o **prompt mestre** no Claude Design para fixar o estilo, depois peça cada imagem
com o prompt individual. Sempre peça **sem texto dentro da imagem** (exceto o logo): todo o texto
do jogo vem dos scripts.

---

## 0. Prompt mestre (colar antes de qualquer imagem)

> Estilo visual para um jogo 2D educativo chamado **Fake Hunter**, em que o jogador é um
> checador de fatos numa redação de jornal e decide se compartilha ou bloqueia notícias.
> Estilo: ilustração digital **flat/cartoon moderna**, contornos suaves, formas simples,
> leve textura de papel, sombras suaves. Sem realismo fotográfico.
> Paleta fixa: azul-marinho `#1B2A41` (fundos), azul `#2F6690` (UI neutra), amarelo-alerta
> `#F4B942` (destaque), verde `#3FA34D` (verdadeiro / compartilhar), vermelho `#D64545`
> (falso / bloquear), branco-papel `#F5F1E8` e cinza `#8A94A6`.
> Tema recorrente: lupa, jornal, carimbo, post-its, monitor, balões de mensagem.
> Nunca inclua texto, letras ou números nas imagens, a não ser quando eu pedir.
> Mantenha o mesmo estilo em todas as imagens desta sessão.

---

## 1. Fundos (1920 x 1080 px, PNG sem transparência)

| Arquivo | Prompt |
|---|---|
| `bg_gameplay.png` | Redação de jornal moderna vista de frente, à altura da mesa. Mesa de madeira clara com monitor apagado, xícara de café, bloco de notas e post-its coloridos. Ao fundo, janela com cidade ao entardecer, quadro de cortiça com fotos e fios ligando pistas (estilo investigação), estante com pastas. Iluminação suave e aconchegante. **O centro da imagem deve ser mais limpo e escuro** (leve desfoque e vinheta) porque a UI da notícia ficará sobre ele. Sem pessoas. |
| `bg_menu.png` | Mesma redação, ângulo mais aberto e levemente de cima. Uma lupa grande apoiada sobre um jornal em destaque no lado direito da mesa. **Lado esquerdo da imagem mais vazio e escuro** para receber o logo e os botões do menu. Clima de abertura de jogo, cores um pouco mais vivas. Sem pessoas. |
| `bg_gameover.png` | A mesma redação à noite, luzes apagadas, apenas a luz fria do monitor. Sobre a mesa, uma caixa de papelão com pertences (caneca, porta-retrato, planta pequena) e um crachá deixado ao lado, sugerindo demissão. Tons frios (azul-marinho e cinza) com um toque de vermelho vindo do monitor. Centro da imagem escuro e vazio para o painel de resultado. |
| `bg_fimdefase.png` | A mesma redação de dia, iluminada e alegre. Confetes de papel caindo, um carimbo verde grande de "aprovado" (sem texto, apenas um check) sobre o jornal, jornal com selo dourado. Tons quentes e verdes. Centro limpo para o painel de resumo. |

---

## 2. Logo (PNG transparente)

| Arquivo | Tamanho | Prompt |
|---|---|---|
| `logo_fakehunter.png` | 1024 x 512 | Logotipo do jogo **FAKE HUNTER** (aqui o texto é permitido). Letras grossas, estilo manchete de jornal, brancas com contorno azul-marinho e leve sombra amarela. Uma lupa azul atravessa a letra "A" ou substitui o "O"; dentro da lente, um fragmento de jornal com um carimbo vermelho de "X". Fundo transparente. |
| `icone_app.png` | 512 x 512 | Ícone quadrado com cantos arredondados: fundo azul-marinho, lupa amarela grande no centro, dentro da lente um jornal pequeno com carimbo vermelho. Sem texto. Fundo transparente fora do quadrado. |

---

## 3. Painéis de interface (PNG transparente, preparados para 9-slice)

Peça bordas uniformes e cantos simples, para poder configurar o **Border** no Sprite Editor do Unity.

| Arquivo | Tamanho | Prompt |
|---|---|---|
| `painel_noticia.png` | 1200 x 760 | Cartão de notícia no estilo de folha de jornal branco-papel, cantos levemente arredondados, sombra suave, uma fita adesiva (washi tape) amarela no canto superior esquerdo e um clipe de papel no canto superior direito. Interior liso e vazio (o texto entra por cima). Bordas de 40 px iguais nos quatro lados. |
| `painel_resultado.png` | 1200 x 220 | Caixa de resultado de investigação: fundo azul-marinho translúcido (aprox. 85% de opacidade), borda fina amarela, cantos arredondados, um pequeno ícone de lupa embutido no canto esquerdo da borda superior. Interior liso. |
| `painel_explicacao.png` | 900 x 620 | Ficha de checagem de fatos: cartão branco-papel com uma faixa superior de 100 px (cor neutra azul `#2F6690`) onde o título entrará, cantos arredondados, sombra forte, um carimbo circular translúcido e discreto no canto inferior direito (sem texto). Interior liso. |
| `painel_hud.png` | 1920 x 110 | Barra superior do HUD: faixa azul-marinho com leve degradê, borda inferior amarela de 4 px, transparente fora da faixa. Sem texto. |
| `painel_menu.png` | 700 x 800 | Painel vertical para o menu principal e para a tela "Como jogar": bloco de anotações branco-papel com espiral no topo, cantos arredondados, sombra. Interior liso. |

---

## 4. Botões (PNG transparente)

Para cada botão, peça **duas versões**: normal e pressionada (levemente mais escura e 4 px mais baixa). O estado desabilitado o Unity faz sozinho (cinza).

| Arquivo | Tamanho | Prompt |
|---|---|---|
| `btn_compartilhar.png` | 420 x 120 | Botão retangular arredondado verde `#3FA34D`, borda inferior mais escura (efeito 3D suave), com um ícone branco de seta de compartilhar no lado esquerdo. Espaço vazio à direita do ícone para o texto. |
| `btn_nao_propagar.png` | 420 x 120 | Mesmo formato, vermelho `#D64545`, com ícone branco de mão em sinal de "pare" ou círculo cortado. |
| `btn_investigar.png` | 340 x 96 | Botão retangular arredondado azul `#2F6690`, borda inferior mais escura, sem ícone (os ícones entram separados). Um só arquivo serve para os três botões de investigação. |
| `btn_proxima.png` | 340 x 96 | Mesmo formato, amarelo `#F4B942`, com ícone de seta para a direita em azul-marinho no lado direito. |
| `btn_menu.png` | 480 x 110 | Botão largo estilo etiqueta de jornal: branco-papel com borda azul-marinho de 4 px, cantos arredondados, sombra dura amarela deslocada 6 px. Sem ícone. |

---

## 5. Ícones (128 x 128 px, PNG transparente, estilo flat, traço grosso)

| Arquivo | Prompt |
|---|---|
| `ico_verificar_fonte.png` | Lupa sobre um cartão de identificação (crachá) com foto genérica. |
| `ico_consultar_referencia.png` | Pasta de arquivo aberta com uma ficha e um selo pequeno. |
| `ico_pesquisar.png` | Globo terrestre com uma barra de busca sobreposta. |
| `ico_compartilhar.png` | Seta curva de compartilhar, branca com contorno. |
| `ico_bloquear.png` | Mão aberta em sinal de pare, branca com contorno. |
| `ico_verdadeiro.png` | Check dentro de um círculo verde. |
| `ico_falso.png` | X dentro de um círculo vermelho. |
| `ico_parcial.png` | Ponto de exclamação dentro de um triângulo amarelo. |
| `ico_confianca.png` | Coração ou escudo com um pequeno grupo de pessoas dentro (representa a confiança da comunidade). |
| `ico_pontos.png` | Estrela amarela com contorno azul-marinho. |
| `ico_sequencia.png` | Chama estilizada (sequência de acertos). |
| `ico_investigacoes.png` | Lupa pequena com um número "2" inexistente (só a lupa; o número vem do jogo). |

---

## 6. Barra de confiança (PNG transparente)

| Arquivo | Tamanho | Prompt |
|---|---|---|
| `barra_confianca_fundo.png` | 800 x 64 | Trilho da barra: cápsula arredondada cinza-escuro com borda azul-marinho e leve sombra interna. |
| `barra_confianca_preenchimento.png` | 800 x 64 | Preenchimento da barra: cápsula arredondada **totalmente branca** com brilho sutil na parte de cima (o jogo tinge a cor por código: verde, amarelo ou vermelho). |
| `barra_confianca_ponteiro.png` | 64 x 64 | Pequeno círculo branco com contorno azul-marinho (handle opcional). |

---

## 7. Avatares das fontes (256 x 256 px, PNG transparente, formato circular)

Retrato em busto, estilo cartoon simples, fundo circular colorido atrás da figura. Sem texto.

| Arquivo | Prompt |
|---|---|
| `avatar_carlos_noticias.png` | Homem de meia-idade com boné e óculos escuros, sorriso malandro, segurando celular. Fundo laranja. |
| `avatar_prefeitura.png` | Não é pessoa: brasão genérico de prefeitura (prédio com colunas e uma árvore) em azul e branco. Fundo azul. |
| `avatar_jornal_cidade.png` | Não é pessoa: logotipo de jornal tradicional, jornal dobrado com uma coroa de louros simples. Fundo azul-marinho. |
| `avatar_tia_marcia.png` | Senhora simpática de cabelo curto grisalho, óculos pendurados no pescoço, segurando celular com balões de mensagem ao redor. Fundo rosa. |
| `avatar_dr_saude_natural.png` | Homem de jaleco branco falso (sem crachá), colar de contas, segurando uma xícara de chá fumegante, sorriso exagerado. Fundo verde-limão. |
| `avatar_ministerio_saude.png` | Não é pessoa: símbolo de cruz de saúde dentro de um escudo, verde e branco. Fundo verde. |
| `avatar_verdade_oculta.png` | Figura encapuzada com o rosto na sombra, apenas os olhos aparecendo, olho aberto no capuz (símbolo de conspiração). Fundo roxo escuro. |
| `avatar_inmet.png` | Não é pessoa: nuvem com sol e raio estilizados dentro de um círculo, azul e amarelo. Fundo azul-claro. |
| `avatar_fofocas_cidade.png` | Não é pessoa: ícone de megafone com corações e um olho, estilo perfil de rede social. Fundo magenta. |
| `avatar_universidade.png` | Não é pessoa: brasão universitário com livro aberto e chama, verde-escuro e dourado. Fundo verde-escuro. |
| `avatar_ze_do_bairro.png` | Homem idoso simpático de camisa xadrez e chapéu de palha, segurando celular com cara de espanto. Fundo amarelo. |

---

## 8. Imagens das notícias (800 x 450 px, PNG, sem transparência, sem texto)

Cada imagem ilustra a notícia como uma "foto de matéria". Estilo cartoon flat, sem texto.

### Fase 1 – Fácil

| Arquivo | Notícia | Prompt |
|---|---|---|
| `not_feriado.png` | Feriadão amanhã? | Calendário de parede com um dia circulado em vermelho e um balão de mensagem de WhatsApp genérico flutuando ao lado, festivo, confetes. |
| `not_vacina_gripe.png` | Campanha de vacinação | Posto de saúde simples com uma enfermeira aplicando vacina no braço de uma pessoa sorridente; fila curta e organizada ao fundo. |
| `not_cha_boldo.png` | Chá de boldo cura diabetes | Xícara de chá verde fumegante sobre mesa, folhas de boldo, ao lado um brilho "mágico" exagerado e um frasco de remédio riscado, clima de propaganda enganosa. |
| `not_chuva_forte.png` | Alerta de chuva forte | Céu escuro com nuvens pesadas e raio sobre uma cidade pequena, rua com poças, guarda-chuva virado pelo vento. |
| `not_taxa_banco.png` | Banco vai bloquear conta | Celular com cadeado vermelho na tela e um link suspeito (barra sem texto), moedas escorrendo para fora, gancho de pesca (phishing) segurando o celular. |
| `not_quadra_escola.png` | Nova quadra na escola | Quadra poliesportiva coberta recém-construída ao lado de uma escola colorida, crianças jogando bola, fita de inauguração cortada. |

### Fase 2 – Média

| Arquivo | Notícia | Prompt |
|---|---|---|
| `not_tarifa_zero.png` | Ônibus de graça aos domingos | Ônibus urbano colorido parado no ponto, porta aberta, pessoas entrando sem passar pela catraca, sol de domingo. |
| `not_vagas_programacao.png` | Curso gratuito de programação | Sala de aula com jovens em frente a notebooks, tela grande mostrando blocos de código coloridos (sem letras legíveis), clima animado. |
| `not_virus_bom_dia.png` | Vírus por mensagem de bom dia | Celular mostrando uma imagem de flores com sol nascendo (típico "bom dia"), e por trás um vírus cartoon verde com olhos espiando, exagerado e engraçado. |
| `not_cidade_violenta.png` | Cidade mais violenta do estado | Gráfico de barras impresso e amarelado, com poeira e um canto rasgado mostrando um calendário antigo, sobre uma mesa. Sem números legíveis. |
| `not_kits_escolares.png` | Kits escolares | Pilha de mochilas coloridas novas, cadernos e lápis sobre mesa de escola, crianças ao fundo felizes. |
| `not_celular_cancer.png` | Celular causa câncer | Rato de laboratório branco ao lado de um celular gigante emitindo ondas desenhadas, cientista de jaleco ao fundo com cara de dúvida. |

### Fase 3 – Difícil

| Arquivo | Notícia | Prompt |
|---|---|---|
| `not_remedio_falsificado.png` | Lote de remédio falsificado | Duas caixas de remédio quase idênticas lado a lado, uma com selo de segurança e outra com selo rasgado e cor ligeiramente diferente, lupa em cima. |
| `not_prefeito_investigado.png` | Prefeito investigado (site clonado) | Tela de computador mostrando um site de jornal com layout duplicado; um pequeno detalhe da barra de endereço com um X vermelho (sem texto legível), sombra de uma mão manipulando por trás. |
| `not_rio_seco.png` | Foto do rio seco | Foto amarelada e envelhecida de um leito de rio rachado e seco, com borda de foto antiga e um selo de data borrado (sem texto legível). |
| `not_taxa_pix.png` | Taxa no Pix | Celular com ícone genérico de transferência instantânea (raio dentro de um círculo), moedas e um carimbo de imposto vermelho sendo batido por trás, documento com "vazado" desfocado (sem texto). |
| `not_nova_especie_sapo.png` | Nova espécie de sapo | Sapinho verde-esmeralda com manchas amarelas sobre uma folha na mata, gotas de chuva, uma lupa de pesquisador observando. |
| `not_desconto_celular.png` | 90% de desconto em celulares | Vitrine de loja online cartoon com celulares e etiquetas de preço com tesoura cortando (sem números), relógio marcando quase meia-noite, cartão de crédito sendo puxado por um anzol. |

---

## 9. Extras opcionais

| Arquivo | Tamanho | Prompt |
|---|---|---|
| `personagem_jogador.png` | 600 x 900, transparente | Personagem do jogador (checador de fatos): jovem de camisa social com as mangas dobradas, crachá de imprensa, lupa na mão, expressão concentrada. Estilo cartoon, corpo inteiro, para aparecer no canto do menu. |
| `carimbo_verdadeiro.png` | 400 x 400, transparente | Carimbo circular verde com um check grande no centro, textura de tinta de carimbo. |
| `carimbo_falso.png` | 400 x 400, transparente | Carimbo circular vermelho com um X grande, textura de tinta. |
| `carimbo_parcial.png` | 400 x 400, transparente | Carimbo circular amarelo com ponto de exclamação, textura de tinta. |
| `selo_dificuldade.png` | 3 arquivos de 96 x 96 | Três selos pequenos: uma estrela (fácil), duas estrelas (médio), três estrelas (difícil), amarelas com contorno azul-marinho. |

---

## Onde colocar no projeto

```
Assets/_Project/Art/
├── UI/            (fundos, painéis, botões, ícones, barra, logo, carimbos)
├── Sprites/
│   ├── Avatares/  (avatar_*.png)
│   └── Noticias/  (not_*.png)
```

Depois de importar, arraste cada `not_*.png` no campo **Imagem** da notícia correspondente
(`Assets/_Project/Data/Noticias`) e cada `avatar_*.png` no campo **Avatar** da fonte
(`Assets/_Project/Data/Fontes`).
