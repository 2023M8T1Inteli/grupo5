from flask import Flask, jsonify
from flask_cors import CORS
import subprocess  # Importe o módulo subprocess

app = Flask(__name__)
CORS(app)

@app.route('/start_websocket', methods=['POST'])
def start_websocket():
    try:
        # Adicione aqui a lógica para iniciar a conexão WebSocket
        print("Conexão WebSocket iniciada!")

        # Inicie o script clienteWebSocket.py em um novo processo
        subprocess.Popen(['python3', 'SocketServerClient/clienteWebSocket.py'])
        
        return jsonify({"message": "Conexão WebSocket iniciada com sucesso!"})
    except Exception as e:
        print(f"Erro ao iniciar clienteWebSocket.py: {e}")
        return jsonify({"error": "Erro ao iniciar clienteWebSocket.py"}), 500

if __name__ == "__main__":
    app.run(port=3001)
