using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;

public class Game
{
    private readonly GameRules rules;
    private readonly List<string> moves;
    private readonly byte[] hmacKey;

    public Game(string[] args)
    {
        if (args.Length % 2 == 0 || args.Length < 3 || args.Distinct().Count() != args.Length)
        {
            Console.WriteLine("Invalid input. Please provide an odd number of unique moves.");
            Environment.Exit(1);
        }

        moves = new List<string>(args);
        rules = new GameRules(args.Length);
        hmacKey = CryptoHelper.GenerateRandomKey(256);
    }

    public void Start()
    {
        do
        {
            PlayGame();
        } while (true);
    }

    private void PlayGame()
    {
        int computerMove = new Random().Next(1, moves.Count + 1);

        Console.WriteLine($"HMAC: {CryptoHelper.ComputeHMAC(moves[computerMove - 1], hmacKey)}");
        Console.WriteLine("Available moves:");
        for (int i = 0; i < moves.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {moves[i]}");
        }
        Console.WriteLine("0 - exit");
        Console.WriteLine("? - help");

        int playerMove;
        do
        {
            Console.Write("Enter your move: ");
            string userInput = Console.ReadLine().Trim();

            if (userInput == "?")
            {
                DisplayHelp();
            }
            else if (int.TryParse(userInput, out playerMove) && playerMove >= 0 && playerMove <= moves.Count)
            {
                if (playerMove == 0)
                {
                    Environment.Exit(0);
                }
                else
                {
                    PlayRound(playerMove, computerMove);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid move.");
            }
        } while (true);
    }

    private void PlayRound(int playerMove, int computerMove)
    {
        Console.WriteLine($"Your move: {moves[playerMove - 1]}");
        Console.WriteLine($"Computer move: {moves[computerMove - 1]}");

        if (playerMove == computerMove)
        {
            Console.WriteLine("It's a draw!");
        }
        else if (rules.IsPlayerWin(playerMove, computerMove))
        {
            Console.WriteLine("You win!");
        }
        else
        {
            Console.WriteLine("Computer wins!");
        }

        Console.WriteLine($"HMAC key: {BitConverter.ToString(hmacKey).Replace("-", string.Empty)}");
    }

    private void DisplayHelp()
    {
        Console.WriteLine("Game rules:");

        for (int i = 0; i < moves.Count; i++)
        {
            Console.Write($"\t{moves[i]}");
        }
        Console.WriteLine();

        for (int i = 0; i < moves.Count; i++)
        {
            Console.Write($"{moves[i]}\t");
            for (int j = 0; j < moves.Count; j++)
            {
                if (i == j)
                {
                    Console.Write("Draw\t");
                }
                else if (rules.IsPlayerWin(i + 1, j + 1))
                {
                    Console.Write("Win\t");
                }
                else
                {
                    Console.Write("Lose\t");
                }
            }
            Console.WriteLine();
        }
    }
}

