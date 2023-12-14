# identificadorMouse.py

import asyncio
from pynput import keyboard

quadrante_tocado = None
tecla_pressionada = False
ultima_tecla = None
g_websocket = None


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
