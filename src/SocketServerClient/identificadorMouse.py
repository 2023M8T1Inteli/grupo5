import pygame
from pynput.mouse import Listener as MouseListener
from pynput import keyboard
import threading
import time

pygame.init()

canvas_width = 800
canvas_height = 600
scrn = pygame.display.set_mode((canvas_width, canvas_height))
pygame.display.set_caption('Quadrante Tocado')


cor_branca = (255, 255, 255)

TAMANHO_IMAGEM = (400, 300)

quadrante_tocado = ""
img_path = ""

quadrante_imagens = {
    "Quadrante 1 - Cima": "./img/cachorro.jpeg",
    "Quadrante 2 - Direita": "./img/gato.jpeg",
    "Quadrante 3 - Baixo": "./img/elefante.jpeg",
    "Quadrante 4 - Esquerda": "./img/tartaruga.jpg",
    "Quadrante 5 - Tecla": "./img/zebra.jpg"
}


prev_mouse_pos = (0, 0)


imagem_exibida = False

def exibir_imagem(quadrante_tocado):
    global imagem_exibida

    if quadrante_tocado in quadrante_imagens and not imagem_exibida:
        scrn.fill(cor_branca)

        img_path = quadrante_imagens[quadrante_tocado]
        img = pygame.image.load(img_path).convert()

        img = pygame.transform.scale(img, TAMANHO_IMAGEM)

        x = (canvas_width - img.get_width()) // 2
        y = (canvas_height - img.get_height()) // 2

        scrn.blit(img, (x, y))
        pygame.display.flip()

        imagem_exibida = True
        print(quadrante_tocado)

        threading.Timer(2, remover_imagem).start()

def remover_imagem():
    global imagem_exibida

    scrn.fill(cor_branca)
    pygame.display.flip()

    imagem_exibida = False

def on_move(x, y):
    global quadrante_tocado, prev_mouse_pos

    delta_x = x - prev_mouse_pos[0]
    delta_y = y - prev_mouse_pos[1]

    if abs(delta_x) > abs(delta_y):
        if delta_x > 0:
            quadrante_tocado = "Quadrante 2 - Direita"
        else:
            quadrante_tocado = "Quadrante 4 - Esquerda"
    else:
        if delta_y > 0:
            quadrante_tocado = "Quadrante 3 - Baixo"
        else:
            quadrante_tocado = "Quadrante 1 - Cima"

    prev_mouse_pos = (x, y)

    exibir_imagem(quadrante_tocado)

def obter_quadrante_atual():
    return quadrante_tocado

def on_press(key):
    global quadrante_tocado

    quadrante_tocado = "Quadrante 5 - Tecla"

    exibir_imagem(quadrante_tocado)


with MouseListener(on_move=on_move) as mouse_listener:

    with keyboard.Listener(on_press=on_press) as keyboard_listener:
        try:
            while True:

                for event in pygame.event.get():
                    if event.type == pygame.QUIT:
                        raise KeyboardInterrupt

                time.sleep(0.1)  

        except KeyboardInterrupt:
            pass
        finally:
            pygame.quit()
