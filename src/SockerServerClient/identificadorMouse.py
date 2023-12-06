from pynput.mouse import Listener
import time

prev_mouse_pos = (0, 0)
quadrante_tocado = None

def on_move(x, y):
    global prev_mouse_pos, quadrante_tocado

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

# Iniciando o listener de movimento do mouse
with Listener(on_move=on_move) as listener:
    try:
        while True:
            # Imprimindo a direção a cada 2 segundos
            if quadrante_tocado is not None:
                print(quadrante_tocado)
                quadrante_tocado = None

            time.sleep(3)

    except KeyboardInterrupt:
        pass
