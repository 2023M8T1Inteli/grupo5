# clienteWebSocket.py

import asyncio
import websockets

from identificadorMouse import posicao_quadrantes, on_release, quadrante_listener

async def main():
    uri = "ws://localhost:8765"

    

    async with websockets.connect(uri) as websocket:
        try:
            while True:
                # Wait for a quadrante to be touched
                tecla_press = await asyncio.create_task(quadrante_listener(websocket))
                # quadrante = posicao_quadrantes(tecla_press)
                # if quadrante:
                #     # Envia o quadrante para o servidor
                #     await websocket.send(quadrante)
                #     print(f"Quadrante {quadrante} enviado para o servidor")

                #     # Aguarda a resposta do servidor
                #     resposta = await websocket.recv()
                #     print(f"Resposta do servidor: {resposta}")

        except KeyboardInterrupt:
            pass

if __name__ == "__main__":
    asyncio.run(main())
