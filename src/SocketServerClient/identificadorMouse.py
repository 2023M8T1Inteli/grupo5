import pygame
from pynput.mouse import Listener as MouseListener
from pynput import keyboard
import threading
import time

# Inicializando o Pygame
pygame.init()

# Configurações do canvas
canvas_width = 800
canvas_height = 600
scrn = pygame.display.set_mode((canvas_width, canvas_height))
pygame.display.set_caption('Quadrante Tocado')

# Cor de fundo do canvas
cor_branca = (255, 255, 255)

# Tamanho desejado para as imagens
TAMANHO_IMAGEM = (400, 300)

# Variáveis para armazenar o quadrante atual e a imagem correspondente
quadrante_tocado = ""
img_path = ""

# Mapeamento de quadrantes para caminhos de imagem
quadrante_imagens = {
    "Quadrante 1 - Cima": "./img/cachorro.jpeg",
    "Quadrante 2 - Direita": "./img/gato.jpeg",
    "Quadrante 3 - Baixo": "./img/girafa.jpeg",
    "Quadrante 4 - Esquerda": "./img/tartaruga.jpg",
    "Quadrante 5 - Tecla": "./img/zebra.jpg"
}

# Inicializando as coordenadas anteriores do mouse
prev_mouse_pos = (0, 0)

# Variável de controle para garantir apenas uma imagem por vez
imagem_exibida = False

def exibir_imagem(quadrante_tocado):
    global imagem_exibida

    # Verificando se o quadrante está mapeado e nenhuma imagem está sendo exibida
    if quadrante_tocado in quadrante_imagens and not imagem_exibida:
        # Preenchendo o canvas com a cor branca
        scrn.fill(cor_branca)

        # Carregando a imagem
        img_path = quadrante_imagens[quadrante_tocado]
        img = pygame.image.load(img_path).convert()

        # Escalando a imagem para o tamanho desejado
        img = pygame.transform.scale(img, TAMANHO_IMAGEM)

        # Calculando as coordenadas para centralizar a imagem no meio do canvas
        x = (canvas_width - TAMANHO_IMAGEM[0]) // 2
        y = (canvas_height - TAMANHO_IMAGEM[1]) // 2

        # Exibindo a imagem no canvas
        scrn.blit(img, (x, y))
        pygame.display.flip()

        # Marcando que uma imagem está sendo exibida
        imagem_exibida = True

        # Agendando a remoção da imagem após 2 segundos
        threading.Timer(2, remover_imagem).start()

def remover_imagem():
    global imagem_exibida

    # Limpando a tela
    scrn.fill(cor_branca)
    pygame.display.flip()

    # Marcando que nenhuma imagem está sendo exibida
    imagem_exibida = False

def on_move(x, y):
    global quadrante_tocado, prev_mouse_pos

    # Identificando a direção com base nas coordenadas do mouse
    delta_x = x - prev_mouse_pos[0]
    delta_y = y - prev_mouse_pos[1]

    if abs(delta_x) > abs(delta_y):
        # Movimento horizontal
        if delta_x > 0:
            quadrante_tocado = "Quadrante 2 - Direita"
        else:
            quadrante_tocado = "Quadrante 4 - Esquerda"
    else:
        # Movimento vertical
        if delta_y > 0:
            quadrante_tocado = "Quadrante 3 - Baixo"
        else:
            quadrante_tocado = "Quadrante 1 - Cima"

    # Atualizando as coordenadas anteriores do mouse
    prev_mouse_pos = (x, y)

    # Exibindo a imagem correspondente ao quadrante no canvas
    exibir_imagem(quadrante_tocado)

def on_press(key):
    global quadrante_tocado

    # Quando qualquer tecla é pressionada, considera como Quadrante 5
    quadrante_tocado = "Quadrante 5 - Tecla"

    # Exibindo a imagem correspondente ao quadrante no canvas
    exibir_imagem(quadrante_tocado)

# Iniciando o listener de movimento do mouse
with MouseListener(on_move=on_move) as mouse_listener:
    # Iniciando o listener de teclado
    with keyboard.Listener(on_press=on_press) as keyboard_listener:
        try:
            while True:
                # Conferindo eventos do Pygame
                for event in pygame.event.get():
                    if event.type == pygame.QUIT:
                        # Se o usuário fechar a janela, encerre o loop
                        raise KeyboardInterrupt

                time.sleep(0.1)  # Aguardando um curto período para evitar uso excessivo da CPU

        except KeyboardInterrupt:
            pass
        finally:
            # Finalizando o Pygame
            pygame.quit()
