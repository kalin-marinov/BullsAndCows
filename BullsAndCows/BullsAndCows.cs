// A simple bulls and cows game

using System;
using System.Collections.Generic;

static class BullsAndCows
{
    static string myNumber;                                         // computer number
    static List<int> availableNumbers = GenerateNumbers();          // the list with all possible numbers
    static Random rng = new Random();                               // A random number generator
    static void Main()
    {
        Console.WriteLine("Bulls and Cows Game: \r\nRules : Both you and the computer must pick 4 digit number \r\nwhere all the digits are DIFFERENT and the first digit is NOT '0'");
        Console.WriteLine("\r\nWrite down your number, and dont enter it on the console, only answer the amounts of bulls and cows on your enemy's guess");
        Console.WriteLine("\r\nIt will be selected randomly which one of you will play first");
        Console.WriteLine("\r\nThe computer will always pick a valid number and answer the exact amount of bulls and cows you have on your guess");
        Console.WriteLine("\r\nPlay fair!");
        Console.WriteLine();
        Console.WriteLine("Press any key to start game");
        Console.ReadKey();
        Console.Clear();

        myNumber = availableNumbers[rng.Next(0, availableNumbers.Count)].ToString();        // The computer picks a number

        int turn = rng.Next(0, 2);
        int lineNumber = 0;

        if (turn == 0)   // if 0 player is first
        {
            try
            {
                Console.WriteLine("You are first!");
                var playerScore = PlayerTurn(0);
                if (playerScore.bulls == 4)
                {
                    Console.WriteLine("You win!");
                    return;
                }
                lineNumber += 4;
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Game Over! - There was an error during game!");
                Console.WriteLine("Either you lied when answering the amount of bulls and cows on the computers's guess");
                Console.WriteLine("Or you have entered an invalid number");
            }
        }

        try
        {
            while (true)
            {
                var computerScore = ComputerTurn(lineNumber);
                if (computerScore.bulls == 4)
                {
                    Console.WriteLine("I win! And my number was: {0}", myNumber);
                    return;
                }

                var playerScore = PlayerTurn(lineNumber);
                if (playerScore.bulls == 4)
                {
                    Console.WriteLine("You win!");
                    return;
                }

                lineNumber += 4;
            }
        }
        catch (Exception)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Game Over! - There was an error during game!");
            Console.WriteLine("Either you lied when answering the amount of bulls and cows on the computers's guess");
            Console.WriteLine("Or you have entered an invalid number as guess");
        }

    }

    static Score ComputerTurn(int lineNumber)
    {
        Console.SetCursorPosition(0, lineNumber);
        int guess = availableNumbers[rng.Next(0, availableNumbers.Count)];
        Console.WriteLine("My Guess: {0}", guess);
        Score computerScore = new Score();

        Console.Write("Bulls: ");
        computerScore.bulls = int.Parse(Console.ReadLine());
        if (computerScore.bulls == 4)
        {
            return new Score(4, 0);
        }
        Console.Write("Cows: ");
        computerScore.cows = int.Parse(Console.ReadLine());

        availableNumbers.ClearArray(computerScore, guess);  // remove all numbers that do not return the certain ammounts of cows and bulls

        return computerScore;
    }

    static Score PlayerTurn(int lineNumber)
    {
        Console.SetCursorPosition(Console.WindowWidth - 30, lineNumber);
        Console.Write("Your Guess: ");
        Score playerScore = GetScore(Console.ReadLine(), myNumber);
        Console.SetCursorPosition(Console.WindowWidth - 30, lineNumber + 1);
        Console.WriteLine("Bulls: {0}", playerScore.bulls);
        Console.SetCursorPosition(Console.WindowWidth - 30, lineNumber + 2);
        Console.WriteLine("Cows: {0}", playerScore.cows);
        return playerScore;
    }

    static void ClearArray(this List<int> array, Score score, int guess)
    {
        List<int> numbers = new List<int>(array);
        foreach (var item in numbers)
        {
            var newScore = GetScore(guess.ToString(), item.ToString());
            if (newScore.bulls != score.bulls || newScore.cows != score.cows)
            {
                array.Remove(item);
            }
        }
    }


    static Score GetScore(string guess, string number)
    {
        int cowsCount = 0;
        int bulsCount = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (guess[i] == number[j])
                {
                    if (i == j) bulsCount++;
                    else cowsCount++;
                }
            }
        }
        return new Score(bulsCount, cowsCount);
    }

    static List<int> GenerateNumbers()
    {
        List<int> result = new List<int>();
        for (int i = 1023; i <= 9999; i++)
        {
            bool[] usedNumbers = new bool[10];
            bool isValidNumber = true;
            int number = i;
            for (int j = 0; j < 4; j++)
            {
                if (usedNumbers[number % 10])
                {
                    isValidNumber = false;
                    break;
                }
                else
                {
                    usedNumbers[number % 10] = true;
                    number /= 10;
                }
            }
            if (isValidNumber)
            {
                result.Add(i);
            }
        }

        return result;
    }
}

struct Score
{
    public int bulls;
    public int cows;
    public Score(int bulls, int cows)
    {
        this.bulls = bulls;
        this.cows = cows;
    }

}


