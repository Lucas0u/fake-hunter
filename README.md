# Fake Hunter

Jogo 2D educativo feito em Unity 6 (URP 2D). O jogador assume o papel de **checador de fatos**
de uma comunidade: a cada notícia recebida, pode investigar e depois decidir se **compartilha**
ou **não propaga**. Acertos aumentam a confiança da comunidade; erros a derrubam. Se a confiança
chegar a zero, o jogador é demitido.

O objetivo é ensinar, na prática, os sinais de desinformação: fontes não identificadas, urgência
artificial, curas milagrosas, sites clonados, fotos fora de contexto, dados desatualizados e
manchetes que distorcem estudos reais.

## Mecânicas

- **Investigação** (até 2 por notícia): Verificar Fonte, Consultar Referência, Pesquisar.
- **Decisão**: Compartilhar ou Não Propagar.
- **Veredito**: cada notícia é Verdadeira, Falsa ou Parcialmente Verdadeira. Só as
  totalmente verdadeiras devem ser compartilhadas.
- **Confiança da comunidade**: barra de 0 a 100%. Acerto +5%, erro -20%.
- **Pontuação**: pontos por acerto multiplicados pela dificuldade, com bônus por sequência.
- **Explicação**: após cada decisão, o jogo explica por que a notícia era o que era.

## Conteúdo

- 3 fases com 6 notícias cada (fácil, média, difícil).
- 11 fontes com histórico e status (confiável, não confiável, desconhecida).
- Todo o conteúdo é feito em ScriptableObjects (`Assets/_Project/Data`), sem precisar
  mexer em código para criar novas notícias: `Create > FakeHunter > Noticia / Fonte / FaseDeNoticias`.

## Estrutura

```
Assets/_Project/
├── Art/            sprites, UI e fundos
├── Data/           Fases, Fontes e Noticias (ScriptableObjects)
├── Scenes/         Gameplay (e Menu, opcional)
└── Scripts/
    ├── Data/       Noticia, Fonte, FaseDeNoticias
    ├── Managers/   GameManager
    └── UI/         PainelNoticia, MenuPrincipal
```

## Status e verificação

O projeto ainda **não foi validado dentro do Unity** (cena, scripts e assets foram montados por ferramentas externas).
Antes de continuar o desenvolvimento, siga o checklist em [PENDENCIAS.md](PENDENCIAS.md), seção 1:
abrir o projeto, conferir o Console sem erros e testar Menu → Gameplay → fim de fase → game over.
O mesmo arquivo lista as artes que faltam, melhorias de código e tarefas do repositório.

## Documentação

- [PENDENCIAS.md](PENDENCIAS.md) – checklist de verificação e tudo que ainda falta.
- [COMO_CONFIGURAR.md](COMO_CONFIGURAR.md) – como ligar os novos campos no Inspector.
- [PROMPTS_IMAGENS.md](PROMPTS_IMAGENS.md) – lista de imagens necessárias, tamanhos e prompts.

## Requisitos

- Unity 6000.5 ou superior
- Pacotes: Universal RP, TextMeshPro, Input System (já no `Packages/manifest.json`)
