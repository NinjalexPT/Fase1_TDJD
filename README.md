Miguel Silva EDJD 27940
Hugo Oliveira EDJD 27920
Samuel Furtado EDJD 27916

RESUMO do jogo escolhido (OMG_ZOMBIES):

O jogo consiste de um platformer simples em que se tenta chegar ao final do nivel antes do final do tempo e coletar pontos (neste caso através de poções).

A personagem pode mover-se e saltar, porém os inimigos são estaticos.


Classes e Funcionalidades:

>Animation:

Classe que representa uma sprite sheet animada.
Define campos para gerenciar propriedades da animação, como textura (spriteSheet), intervalo entre frames (timeBetweenEachFrame), looping (isLooping), número de frames (NumberOfFrames), largura e altura de cada frame (FrameWidth e FrameHeight).
Método Animation(Texture2D spriteSheet, float timeBetweenEachFrame, bool isLooping) para inicializar os parâmetros da animação.

>Animator:

Classe que controla a reprodução de animações em uma sprite sheet.
Gerencia a animação atual, índice do frame atual, tempo de exibição do frame e origem da textura.
Método PlayAnimation(Animation animation) para iniciar ou continuar uma animação.
Método Draw(Vector2 position, SpriteEffects spriteEffects) para desenhar o frame atual da animação.

>Camera:

Representa a câmera do jogo.
Gerencia transformação, viewport e centro da câmera.
Método Update(Vector2 position, int xOffset, int yOffset) para atualizar a posição da câmera com base na posição do jogador.

>KeyboardManager:

Gerencia o estado do teclado do jogo.
Define métodos para atualizar e verificar o estado das teclas.

>Level:

Representa um nível do jogo.
Gerencia elementos como fundos, jogador, inimigos, mapa do nível, pontuação, etc.
Métodos para carregar conteúdo do nível, atualizar estado do nível e desenhar objetos do nível.

>Tilemap:

Representa o mapa de tiles do nível.
Gerencia a carga, desenho e interação com os tiles do mapa.
Métodos para carregar e desenhar o mapa de tiles, além de lidar com colisões.

>Resumo Geral:

Animation e Animator: Controlam a reprodução de animações em sprite sheets.

Camera: Gerencia a visualização do jogo, ajustando-se à posição do jogador.

KeyboardManager: Monitora e responde aos eventos do teclado.
Level: Gerencia todos os elementos de um nível do jogo, incluindo jogadores, inimigos, poções e a lógica de jogo.

Tilemap: Representa e controla o mapa de tiles do nível, incluindo carregamento, desenho e interação com os tiles.

Essas classes e funcionalidades formam a base para o desenvolvimento de um jogo em que você pode animar personagens, gerenciar a câmera, interagir com o teclado e criar e gerenciar níveis com diferentes elementos e mecânicas de jogo.


Classes de Cena:

>Credits (Créditos):

Representa a cena de créditos do jogo.

Gerencia o texto dos créditos, o teclado e a transição para outras cenas.

Métodos para carregar e desenhar os créditos, atualizar o teclado e mudar para a cena principal.

>Gameplay (Jogabilidade):

Representa a cena principal do jogo onde a jogabilidade ocorre.

Gerencia o teclado, o estado do nível atual, a câmera e a pontuação.

Métodos para carregar o conteúdo do jogo, atualizar o estado do jogo e desenhar a cena.

>MainMenu (Menu Principal):

Representa a cena do menu principal do jogo.

Gerencia o logotipo, os botões do menu e suas interações.

Métodos para carregar o conteúdo do menu, lidar com interações de botões e desenhar o menu.

>Scene (Cena):

Classe abstrata base para todas as cenas do jogo.

Define métodos abstratos LoadContent(), Draw(), e Update() que devem ser implementados por cenas específicas.

5)Storyboard (História/Introdução):

Representa uma sequência de introdução ou história no início/final do jogo.

Gerencia imagens da storyboard, o teclado e a transição para a próxima cena.

Métodos para carregar e desenhar a storyboard, atualizar o teclado e avançar para a próxima cena.

Resumo das Funcionalidades:
>Credits (Créditos):

Carrega e exibe os créditos do jogo.

Permite navegar de volta ao menu principal ao pressionar a tecla espaço.

>Gameplay (Jogabilidade):

Controla a lógica principal do jogo, incluindo atualização do estado do nível, controle da câmera e pontuação.

Carrega e exibe o conteúdo do jogo, como níveis e elementos visuais.

Gerencia a transição entre níveis e a conclusão do jogo.

>MainMenu (Menu Principal):

Exibe o menu principal do jogo com opções como iniciar, ver créditos e sair.

Responde a interações do jogador, como clicar em botões para iniciar o jogo ou visualizar os créditos.

Carrega e exibe o conteúdo do menu, como logotipo e botões interativos.

>Scene (Cena):

Serve como uma estrutura base para todas as cenas do jogo.

Define métodos que cada cena específica deve implementar para carregar, desenhar e atualizar seu conteúdo.

>Storyboard (História/Introdução):

Mostra uma sequência de imagens no início ou final do jogo.

Permite avançar para a próxima cena ao pressionar a tecla espaço.

Gerencia a transição entre cenas após a conclusão da storyboard.

Essas classes trabalham juntas para criar uma estrutura de cena flexível e modular para o jogo, permitindo a transição suave entre diferentes partes da experiência do jogador, como menus, jogabilidade e sequências narrativas.


>Campos e Propriedades:

level: Referência ao nível atual em que o jogador está.

idleAnimation, runAnimation, jumpAnimation, deadAnimation: Animações para diferentes estados do personagem.

animator: Gerenciador de animação para controlar as animações do jogador.

flip: Indicador de orientação do sprite (esquerda/direita).

runSound, runSoundInstance, jumpSound, dieSound: Sons associados a diferentes ações do jogador.

isAlive: Indica se o personagem está vivo.

position: Posição atual do jogador.

velocity: Vetor de velocidade do jogador.

speed: Velocidade de movimento horizontal do jogador.

isJumping, jumpTime: Estado e tempo de duração do salto.

GRAVITY, MAX_JUMP_TIME, MAX_JUMP_SPEED: Constantes relacionadas à física do salto.

isOnGround: Indica se o jogador está no chão.

textureBounds: Limites da textura do sprite do jogador.

collider: Colisor para detecção de colisões.

previousBottom: Posição inferior anterior do jogador.

layer: Camada na qual o jogador está sendo desenhado.

>Métodos:

Construtor (Player(Level level, Vector2 position)):

Inicializa o jogador com o nível atual e a posição especificada.

Carrega o conteúdo do jogador, como animações e sons.

LoadContent():

Carrega animações e sons do jogador.

ResetPlayer(Vector2 position):

Reinicializa o jogador para uma posição específica.

Define o jogador como vivo e reinicia suas propriedades.

Update():

Atualiza o estado e comportamento do jogador.

Verifica entrada do teclado para movimento e salto.

Aplica física ao jogador, lidando com colisões e atualizando animações.

Jump(float elapsedTime, float velocityY):

Permite ao jogador realizar um salto, ajustando sua velocidade vertical.

HandleTilemapCollisions():

Detecta e resolve colisões com o mapa de tiles.

Mantém o jogador no chão e evita atravessar obstáculos.

ResetVelocityIfCollide(Vector2 previousPosition):


Reseta a velocidade do jogador se não houver movimento.

ResetPhysicsApplied():

Reseta as propriedades físicas aplicadas ao jogador.

OnPlayerWithoutTime():

Chamado quando o jogador fica sem tempo.

Define o jogador como morto e executa a animação de morte.

OnPlayerDied(Enemy killedBy):

Chamado quando o jogador morre, especificando a causa (por exemplo, inimigo).

Define o jogador como morto, executa a animação de morte e toca o som correspondente.

OnPlayerCompletedLevel():

Chamado quando o jogador completa o nível.

Realiza ações necessárias após a conclusão do nível.

Draw():

Desenha o jogador na tela, aplicando orientação horizontal conforme necessário.

FlipPlayer():

Inverte a orientação do sprite do jogador com base na direção do movimento.


Essa classe encapsula o comportamento e a lógica do jogador no jogo, permitindo interações dinâmicas com o ambiente e outros elementos, como animações, física de movimento e detecção de colisões.

CLasses:

1)SceneType (Tipo de Cena):

Uma enumeração que representa diferentes tipos de cenas em um jogo, como MainMenu, Storyboard, Gameplay e Credits. Cada cena é associada a um valor inteiro para identificação.

2)RectangleHelper (Auxiliar de Retângulo):

Uma classe estática com métodos auxiliares para trabalhar com retângulos, como calcular o centro de um retângulo (GetOrigin) e o centro da borda inferior (GetBottomCenter). Esses métodos são úteis para posicionamento e detecção de colisão.

3)CollisionType (Tipo de Colisão):

Uma enumeração que define diferentes tipos de colisões para tiles em um jogo, como transparente (permitindo sobreposição) e block (funcionando como uma barreira física).

4)Popup (Janela Pop-up):

Uma classe que representa uma janela pop-up no jogo, carregando uma textura e desenhando-a na tela centrada.

5)Label (Etiqueta de Texto):

Uma classe para renderizar etiquetas de texto na tela, com funcionalidades para definir a posição, fonte e cor do texto.

6)Image (Imagem):

Uma classe para carregar e renderizar imagens na tela, permitindo ajuste de camada para controle de sobreposição visual.

7)Button (Botão):

Uma classe que representa botões interativos na interface do usuário, com capacidade de resposta a cliques e manipulação de eventos.

8)Tile (Tile/Bloco):

Uma classe que representa um tile ou bloco no jogo, com propriedades para textura e tipo de colisão.

9)Potion (Poção):

Uma classe para representar poções no jogo, incluindo lógica para carregar, coletar e renderizar.

10)Enemy (Inimigo):

Uma classe que representa um inimigo animado no jogo, com funcionalidades para carregar, animar e renderizar.


Cada uma dessas classes desempenha um papel importante no desenvolvimento do jogo, abordando desde elementos visuais como imagens, textos e botões até componentes mais complexos como interações de colisão e comportamentos de personagens. Essas descrições fornecem uma visão geral útil das diferentes partes do código e como elas se encaixam no contexto de um jogo desenvolvido com MonoGame.


Game1:

>Campos e Propriedades:

GraphicsDeviceManager (graphics): Gerenciador de dispositivo gráfico para configurações de renderização.

SpriteBatch (spriteBatch): Utilizado para desenhar texturas na tela.

int (screenWidth, screenHeight): Dimensões da tela do jogo.

ContentManager (content): Gerencia o conteúdo carregado para o jogo, como texturas, sons e fontes.

GameTime (gameTime): Fornece informações sobre o tempo do jogo.

Scene (currentScene): Referência à cena atual do jogo.

Métodos Principais:
>Construtor (Game1()):

Inicializa o gerenciador de dispositivos gráficos (graphics).
Define configurações básicas, como tamanho da janela e se deve ou não ser em tela cheia.

>Initialize():

Sobrescrito para configurar as preferências do buffer de backbuffer, como largura e altura.

>LoadContent():

Sobrescrito para carregar o conteúdo inicial do jogo.
Inicializa o SpriteBatch para desenho na tela.
Define a cena inicial do jogo, carregando o conteúdo necessário para essa cena.

>Update(GameTime gameTime):

Sobrescrito para atualizar a lógica da cena atual com base no tempo de jogo (gameTime).
Pode incluir atualizações de entrada do jogador, física do jogo, lógica de IA e outros elementos de jogo.

>Draw(GameTime gameTime):

Sobrescrito para desenhar a cena atual na tela.
Utiliza o SpriteBatch para desenhar elementos visuais, como texturas, sprites e outros objetos na tela.


Funcionamento Geral:
A classe Game1 é a classe principal que gerencia o ciclo de vida do jogo. Ela controla a inicialização, carregamento de conteúdo, atualização e renderização do jogo.

>Durante a inicialização, ela configura o ambiente gráfico e define as preferências de renderização.

>No carregamento de conteúdo, ela carrega recursos essenciais para o jogo, como texturas, sons e outras mídias.

>No método de atualização, ela processa a lógica do jogo, atualizando o estado dos objetos e respondendo a eventos de entrada.
>No método de desenho, ela renderiza a cena atual na tela, utilizando o SpriteBatch para desenhar os elementos visuais.


Essa classe serve como o ponto de entrada para o jogo MonoGame e é fundamental para o controle do fluxo de execução e interação com o usuário. Todas as operações principais do jogo, como lógica de jogo e renderização, são gerenciadas aqui.


erros encontrados:
O personagem fica mais pequeno quando salta (provavelmente devido á diferença de tamanho do sprite em si)
Devido ao jogo mover o ecrã com o jogador no centro, quando se está a chegar ao final, devido á estrutura do nivel, ficar com o ecrã parcialmente obscurecido.  


Bibliografia

https://github.com/luispereira1999/omg-zombies
