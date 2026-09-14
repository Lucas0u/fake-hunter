# Fake Hunter – Prompts para geradores de imagem (um prompt por imagem)

Cada prompt já começa com o **bloco de estilo** repetido, para todas as imagens saírem iguais.
Copie um prompt por vez. O nome em negrito é o nome do arquivo para salvar.

- **Microsoft Designer / Bing Image Creator** → fundos, imagens das notícias e personagem
  (escolha proporção **16:9** quando indicado; depois redimensione no Photopea para o tamanho exato).
- **Recraft** → tudo que precisa de **fundo transparente**: logo, ícones, botões, painéis, barra,
  carimbos e avatares (use o estilo *Vector* ou *Icon*, marque *transparent background*).

Depois de gerar: redimensione/corte no Photopea (photopea.com) para o tamanho indicado e salve em PNG.

---

## PARTE A – MICROSOFT DESIGNER (fundos e imagens das notícias)

Bloco de estilo usado em todos os prompts desta parte:

> flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark

### A1. Fundos (gerar em 16:9, redimensionar para 1920x1080)

**bg_gameplay.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. Modern newspaper newsroom seen from desk height: light wooden desk with a turned-off monitor, coffee mug, notepad and colorful sticky notes; in the background a window with a city at sunset, a cork board with photos connected by red strings, a shelf with folders; soft warm lighting; the center of the image is darker, emptier and slightly blurred; no people; 16:9
```

**bg_menu.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. Modern newspaper newsroom seen from a wider, slightly elevated angle; a big magnifying glass resting on a folded newspaper on the right side of the desk; cork board with photos and strings, shelves with folders; the left third of the image is empty and darker; vivid colors, welcoming game-title mood; no people; 16:9
```

**bg_gameover.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. The same newspaper newsroom at night, lights off, only the cold glow of a monitor; on the desk a cardboard box with personal belongings (mug, photo frame, small plant) and an ID badge left beside it, suggesting someone was fired; cold navy and gray tones with a touch of red light from the monitor; the center of the image is dark and empty; no people; 16:9
```

**bg_fimdefase.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. The same newspaper newsroom in daylight, bright and cheerful, paper confetti falling, a big green rubber stamp with a checkmark symbol on a newspaper, a golden seal; warm and green tones; the center of the image is clean and empty; no people; 16:9
```

### A2. Personagem (opcional, gerar em 3:4 ou 9:16, depois recortar o fundo no remove.bg)

**personagem_jogador.png** (600x900, fundo transparente)
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. Full-body character of a young fact-checker journalist: button-up shirt with rolled sleeves, press badge on a lanyard, holding a magnifying glass, focused expression; standing pose; plain solid cream background; portrait orientation
```

### A3. Imagens das notícias (gerar em 16:9, redimensionar para 800x450)

**not_feriado.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A wall calendar with one day circled in red marker, a generic green chat message bubble floating beside it, festive confetti; 16:9
```

**not_vacina_gripe.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A simple public health clinic, a friendly nurse giving a vaccine shot in the arm of a smiling person, a short orderly line of people in the background; 16:9
```

**not_cha_boldo.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A steaming cup of green herbal tea on a table with fresh green leaves, an exaggerated magical sparkle glow around it, and a pill bottle crossed out with a red X; misleading advertisement mood; 16:9
```

**not_chuva_forte.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. Dark sky with heavy storm clouds and a lightning bolt over a small town, street with puddles, an umbrella flipped inside out by the wind; 16:9
```

**not_taxa_banco.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A smartphone with a red padlock on the screen and a blank suspicious link bar, coins spilling out of it, a fishing hook holding the phone (phishing scam concept); 16:9
```

**not_quadra_escola.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A brand-new covered sports court next to a colorful school building, children playing ball, a cut inauguration ribbon; 16:9
```

**not_tarifa_zero.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A colorful city bus stopped at a bus stop with doors open, people boarding freely without a turnstile, sunny Sunday mood; 16:9
```

**not_vagas_programacao.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A classroom with young students at laptops, a big screen showing colorful abstract code blocks (no readable letters), excited mood; 16:9
```

**not_virus_bom_dia.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A smartphone showing a cheesy "good morning" style picture of flowers and a rising sun, and behind the phone a funny green cartoon virus monster with big eyes peeking; exaggerated and humorous; 16:9
```

**not_cidade_violenta.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. An old yellowed printed bar chart on a table, dusty, with a torn corner revealing an old calendar page underneath; no readable numbers; 16:9
```

**not_kits_escolares.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A pile of new colorful school backpacks, notebooks and pencils on a school table, happy children in the background; 16:9
```

**not_celular_cancer.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A white laboratory mouse next to a giant smartphone emitting drawn radio waves, a scientist in a lab coat in the background with a doubtful expression; 16:9
```

**not_remedio_falsificado.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. Two almost identical medicine boxes side by side, one with an intact security seal and the other with a torn seal and a slightly different color, a magnifying glass over them; 16:9
```

**not_prefeito_investigado.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A computer screen showing a cloned newspaper website layout, the address bar marked with a red X, the shadow of a hand manipulating from behind the screen; no readable text; 16:9
```

**not_rio_seco.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. An old yellowed aged photograph of a cracked dry riverbed, vintage photo border, a blurred unreadable date stamp in the corner; 16:9
```

**not_taxa_pix.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A smartphone with a generic instant-transfer icon (lightning bolt inside a circle), coins, a red tax rubber stamp being pressed from behind, a blurred document; 16:9
```

**not_nova_especie_sapo.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A small emerald-green tree frog with yellow spots sitting on a leaf in a rainforest, rain drops, a researcher's magnifying glass observing it; 16:9
```

**not_desconto_celular.png**
```
flat cartoon digital illustration, soft outlines, subtle paper texture, soft shadows, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, no text, no letters, no numbers, no watermark. A cartoon online store window with smartphones and blank price tags being cut by scissors, a clock showing almost midnight, a credit card being pulled by a fishing hook; 16:9
```

---

## PARTE B – RECRAFT (fundo transparente: logo, UI, ícones, avatares)

Bloco de estilo usado em todos os prompts desta parte:

> flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers

No Recraft: estilo **Vector Art** (ou **Icon** para os ícones), ative **Transparent background**, e defina o tamanho/proporção indicado.

### B1. Logo e ícone do app

**logo_fakehunter.png** (1024x512 — o único com texto; se o texto sair errado, tente o mesmo prompt no Ideogram)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background. Game logo with the text "FAKE HUNTER" in bold newspaper-headline lettering, white letters with navy outline and a yellow drop shadow; a blue magnifying glass replaces the letter O of HUNTER and inside its lens there is a small piece of newspaper with a red X stamp; 2:1 wide
```

**icone_app.png** (512x512)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. App icon: rounded square with navy background, a big yellow magnifying glass in the center, inside the lens a small newspaper with a red stamp; 1:1
```

### B2. Painéis (gerar em 1:1 ou na proporção indicada, redimensionar depois)

**painel_noticia.png** (1200x760)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI panel: a cream newspaper-sheet card with slightly rounded corners, soft drop shadow, a yellow washi tape strip on the top-left corner and a paper clip on the top-right corner; the interior is plain and empty; uniform borders; 3:2
```

**painel_resultado.png** (1200x220)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI panel: wide rounded rectangle, semi-transparent navy fill, thin yellow border, a small magnifying glass icon embedded on the left of the top border; plain empty interior; wide banner shape
```

**painel_explicacao.png** (900x620)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI panel: fact-check card, cream rounded rectangle with a solid blue header band across the top, strong drop shadow, a faint translucent circular rubber stamp mark in the bottom-right corner; plain empty interior; 3:2
```

**painel_hud.png** (1920x110)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI element: a long horizontal top bar, navy with a subtle gradient and a thin yellow line along the bottom edge; very wide and short strip
```

**painel_menu.png** (700x800)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI panel: a vertical cream notepad with a spiral binding on top, rounded corners, drop shadow; plain empty interior; portrait 7:8
```

### B3. Botões (420x120 / 340x96 / 480x110 — gere e depois faça uma cópia mais escura no Photopea para o estado "pressionado")

**btn_compartilhar.png** (420x120)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI button: wide rounded rectangle in green #3FA34D with a darker green bottom edge (soft 3D effect), a white share-arrow icon on the left side, empty space on the right; 7:2 wide
```

**btn_nao_propagar.png** (420x120)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI button: wide rounded rectangle in red #D64545 with a darker red bottom edge (soft 3D effect), a white open-hand stop icon on the left side, empty space on the right; 7:2 wide
```

**btn_investigar.png** (340x96)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI button: wide rounded rectangle in blue #2F6690 with a darker blue bottom edge (soft 3D effect), no icon, plain empty face; 7:2 wide
```

**btn_proxima.png** (340x96)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI button: wide rounded rectangle in yellow #F4B942 with a darker yellow bottom edge (soft 3D effect), a navy right-pointing arrow icon on the right side, empty space on the left; 7:2 wide
```

**btn_menu.png** (480x110)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI button: wide newspaper-label style rounded rectangle, cream fill with a thick navy outline and a hard yellow offset shadow; plain empty face; 4:1 wide
```

### B4. Ícones (128x128, estilo Icon, 1:1)

**ico_verificar_fonte.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Icon: a magnifying glass over an ID badge card with a generic portrait silhouette; 1:1
```

**ico_consultar_referencia.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Icon: an open archive folder with a record card and a small seal; 1:1
```

**ico_pesquisar.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Icon: a globe with a search bar overlapping it; 1:1
```

**ico_compartilhar.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Icon: a curved share arrow, white fill with navy outline; 1:1
```

**ico_bloquear.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Icon: an open hand in a stop gesture, white fill with navy outline; 1:1
```

**ico_verdadeiro.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Icon: a white checkmark inside a green circle; 1:1
```

**ico_falso.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Icon: a white X inside a red circle; 1:1
```

**ico_parcial.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Icon: a navy exclamation mark inside a yellow warning triangle; 1:1
```

**ico_confianca.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Icon: a shield with a small group of three people silhouettes inside; 1:1
```

**ico_pontos.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Icon: a yellow star with a navy outline; 1:1
```

**ico_sequencia.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Icon: a stylized flame, yellow and red with navy outline; 1:1
```

**ico_investigacoes.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Icon: a small blue magnifying glass; 1:1
```

### B5. Barra de confiança

**barra_confianca_fundo.png** (800x64)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI element: a long horizontal pill-shaped progress bar track, dark gray fill with a navy outline and a subtle inner shadow, empty; very wide and short
```

**barra_confianca_preenchimento.png** (800x64)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI element: a long horizontal pill-shaped progress bar fill, completely white with a subtle lighter highlight along the top, no outline; very wide and short
```

**barra_confianca_ponteiro.png** (64x64)
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. UI element: a small white circle knob with a navy outline; 1:1
```

### B6. Carimbos (400x400)

**carimbo_verdadeiro.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. A round green rubber stamp mark with a big checkmark in the center, grunge ink texture, double circle border; 1:1
```

**carimbo_falso.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. A round red rubber stamp mark with a big X in the center, grunge ink texture, double circle border; 1:1
```

**carimbo_parcial.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. A round yellow rubber stamp mark with a big exclamation mark in the center, grunge ink texture, double circle border; 1:1
```

### B7. Avatares das fontes (256x256, 1:1)

**avatar_carlos_noticias.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Circular avatar, cartoon bust portrait: middle-aged man with a baseball cap and dark sunglasses, sly smirk, holding a smartphone; orange circle behind him; 1:1
```

**avatar_prefeitura.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Circular avatar: generic city hall coat of arms, a building with columns and a tree, blue and white, inside a blue circle; 1:1
```

**avatar_jornal_cidade.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Circular avatar: traditional newspaper emblem, a folded newspaper with a laurel wreath, inside a navy circle; 1:1
```

**avatar_tia_marcia.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Circular avatar, cartoon bust portrait: friendly older woman with short gray hair, reading glasses hanging on a chain, holding a smartphone with chat bubbles around her; pink circle behind her; 1:1
```

**avatar_dr_saude_natural.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Circular avatar, cartoon bust portrait: man in a white coat without a badge, bead necklace, holding a steaming cup of tea, exaggerated wide smile; lime green circle behind him; 1:1
```

**avatar_ministerio_saude.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Circular avatar: a health cross symbol inside a shield, green and white, inside a green circle; 1:1
```

**avatar_verdade_oculta.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Circular avatar, cartoon bust: hooded figure with the face in shadow, only two glowing eyes visible, an open-eye symbol drawn on the hood (conspiracy vibe); dark purple circle behind; 1:1
```

**avatar_inmet.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Circular avatar: weather institute emblem, a stylized cloud with a sun and a lightning bolt, blue and yellow, inside a light blue circle; 1:1
```

**avatar_fofocas_cidade.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Circular avatar: gossip social media page emblem, a megaphone with hearts and an eye, inside a magenta circle; 1:1
```

**avatar_universidade.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Circular avatar: university crest with an open book and a flame, dark green and gold, inside a dark green circle; 1:1
```

**avatar_ze_do_bairro.png**
```
flat vector illustration, clean simple shapes, thick soft outlines, subtle soft shadow, color palette navy #1B2A41, blue #2F6690, yellow #F4B942, green #3FA34D, red #D64545, cream #F5F1E8, gray #8A94A6, isolated on transparent background, no text, no letters, no numbers. Circular avatar, cartoon bust portrait: friendly elderly man in a plaid shirt and straw hat, holding a smartphone with a surprised expression; yellow circle behind him; 1:1
```

---

## Onde salvar

```
Assets/_Project/Art/
├── UI/            bg_*, logo, icone_app, painel_*, btn_*, ico_*, barra_*, carimbo_*, personagem
└── Sprites/
    ├── Avatares/  avatar_*.png
    └── Noticias/  not_*.png
```

No Unity, importe tudo como **Texture Type: Sprite (2D and UI)**. Depois ligue as imagens
conforme o `COMO_CONFIGURAR.md`.
