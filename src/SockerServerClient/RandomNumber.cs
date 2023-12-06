// RandomNumber.cs

public static class RandomNumber
{
    private static int posicaoAnteriorX;
    private static int posicaoAnteriorY;

    // Método para identificar o quadrante com base no movimento do mouse
    public static int IdentificarQuadranteDirecao(int x, int y)
    {
        int deltaX = x - posicaoAnteriorX;
        int deltaY = y - posicaoAnteriorY;

        // Determinar a direção com base nas diferenças nas coordenadas (deltaX e deltaY)
        if (Math.Abs(deltaX) > Math.Abs(deltaY))
        {
            // Movimento horizontal
            return (deltaX > 0) ? 2 : 4; // Direita: 2, Esquerda: 4
        }
        else
        {
            // Movimento vertical
            return (deltaY > 0) ? 1 : 3; // Cima: 1, Baixo: 3
        }
    }
}
