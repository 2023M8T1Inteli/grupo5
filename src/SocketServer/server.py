# server.py

import asyncio
import websockets

async def handle_client(websocket, path):
    try:
        while True:
            # Recebe o quadrante do cliente
            quadrante = await websocket.recv()
            if quadrante:
                print(f"Quadrante recebido do cliente: {quadrante}")

                # Lógica adicional aqui, se necessário

                # Envia uma confirmação de volta para o cliente
                await websocket.send(f"Quadrante {quadrante} recebido pelo servidor")
    except websockets.exceptions.ConnectionClosed:
        print("Conexão fechada pelo cliente")

async def main():
    server = await websockets.serve(handle_client, "localhost", 8765)
    print("Servidor WebSocket iniciado. Aguardando conexões...")
    await server.wait_closed()

if __name__ == "__main__":
    asyncio.run(main())
