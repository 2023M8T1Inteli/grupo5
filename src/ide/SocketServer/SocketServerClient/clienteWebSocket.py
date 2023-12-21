# clienteWebSocket.py

import asyncio
import websockets

from identificadorMouse import posicao_quadrantes, on_release, quadrante_listener

async def main():
    uri = "ws://localhost:8765"

    

    async with websockets.connect(uri) as websocket:
        try:
            while True:
                tecla_press = await asyncio.create_task(quadrante_listener(websocket))

        except KeyboardInterrupt:
            pass

if __name__ == "__main__":
    asyncio.run(main())
