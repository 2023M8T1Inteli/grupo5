using System;
using System.Threading.Tasks;

//CHANGE THIS CLASS FOR THE GREG MAKER OUTPUT
public static class RandomNumber
{
    // Method to generate a random number between 1 and 12
    public static int Generate()
    {
        // Creating a new instance of the Random class
        Random random = new Random();

        // Generating and returning a random number between 1 and 12
        return random.Next(1, 13);
    }
}