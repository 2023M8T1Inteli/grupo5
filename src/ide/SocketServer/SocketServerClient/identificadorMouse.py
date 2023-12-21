import asyncio
from pynput import keyboard
import pygame
import time
import threading


# Configurações do canvas
canvas_width = 800
canvas_height = 600
background_color = (255, 255, 255)  # Branco
scrn = pygame.display.set_mode((canvas_width, canvas_height))
pygame.display.set_caption('Quadrante Tocado')

pygame.init()

# Mapeamento de quadrantes para caminhos de imagem
quadrante_imagens = {
    "1": "./SocketServerClient/img/cachorro.jpeg",
    "2": "./SocketServerClient/img/gato.jpeg",
    "3": "./SocketServerClient/img/leao.jpeg",
    "4": "./SocketServerClient/img/sapo.jpeg",
}

quadrante_tocado = None
tecla_pressionada = False
ultima_tecla = None
g_websocket = None


def exibir_imagem(quadrante_tocado):
    # Verificando se o quadrante está mapeado
    if quadrante_tocado in quadrante_imagens:
        # Carregando a imagem
        img_path = quadrante_imagens[quadrante_tocado]
        img = pygame.image.load(img_path).convert()
        # Obtendo as dimensões da imagem
        img_width, img_height = img.get_size()

        # Calculando as coordenadas para centralizar a imagem
        x = (canvas_width - img_width) // 2
        y = (canvas_height - img_height) // 2
        scrn.fill(background_color)

        # Exibindo a imagem no canvas
        scrn.blit(img, (x, y))  # Desenha a imagem centralizada
        pygame.display.flip()
        time.sleep(2)
        remover_imagem()


def remover_imagem():
    # Limpando a tela
    scrn.fill((255, 255, 255))
    pygame.display.flip()

async def enviar(quadrante):
    global g_websocket
    await g_websocket.send(quadrante_tocado)

def posicao_quadrantes(key):
    global tecla_pressionada, quadrante_tocado, ultima_tecla, g_websocket


    if key and isinstance(key, keyboard.Key):
        if key == keyboard.Key.up and not tecla_pressionada:
            tecla_pressionada = True
            quadrante_tocado = "1"
            asyncio.run(enviar(quadrante_tocado))
            print("Quadrante 1")
        elif key == keyboard.Key.left and not tecla_pressionada:
            tecla_pressionada = True
            quadrante_tocado = "2"
            asyncio.run(enviar(quadrante_tocado))
            print("Quadrante 2")
        elif key == keyboard.Key.down and not tecla_pressionada:
            tecla_pressionada = True
            quadrante_tocado = "3"
            asyncio.run(enviar(quadrante_tocado))
            print("Quadrante 3")
        elif key == keyboard.Key.right and not tecla_pressionada:
            tecla_pressionada = True
            quadrante_tocado = "4"
            asyncio.run(enviar(quadrante_tocado))
            print("Quadrante 4")
        exibir_imagem(quadrante_tocado)
    return quadrante_tocado



def on_release(key):
    global tecla_pressionada
    tecla_pressionada = False
    if key == keyboard.Key.esc:
        return False

async def quadrante_listener(websocket):
    global g_websocket
    g_websocket = websocket
    with keyboard.Listener(on_press=posicao_quadrantes,on_release=on_release) as listener:
        print("Teclado listener iniciado.")
        await listener.join()
