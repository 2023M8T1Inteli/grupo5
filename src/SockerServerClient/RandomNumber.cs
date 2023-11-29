using System;
using System.Threading.Tasks;


//SUBSTITUIR ESSA FUNÇÃO PELA QUE LÊ OS VALORES DO GREG
public static class RandomNumber
{
    public static int Generate()
    {
        Random random = new Random();
        return random.Next(1, 13);
    }
}