
# import asyncio
# import websockets
# from identificadorMouse import obter_quadrante_atual  # Certifique-se de ajustar o caminho conforme necessário

# async def conectar_servidor():
#     uri = "ws://localhost:7266"
#     async with websockets.connect(uri) as websocket:
#         print("Conectado ao servidor WebSocket.")
#         await enviar_dados(websocket)

# async def enviar_dados(websocket):
#     while True:
#         quadrante_tocado = obter_quadrante_atual()

#         # Lógica para obter o caminho da imagem
#         imagem_path = input("Digite o caminho da imagem: ")

#         # Montando a mensagem
#         mensagem = f"{quadrante_tocado},{imagem_path}"
#         print(mensagem)

#         # Enviando a mensagem para o servidor WebSocket
#         await websocket.send(mensagem)
#         print(f"Dados enviados: {mensagem}")

# async def main():
#     await conectar_servidor()

# if __name__ == "__main__":
#     asyncio.run(main())
