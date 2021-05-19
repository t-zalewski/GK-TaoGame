using GK_Tao.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Tao
{
    class Program
    {
        static void Main(string[] args)
        {
            var selectedOption = Program.GetGameType();
            var size = Program.GetSize();
            var targetLength = Program.GetTargetLength();

            if (selectedOption == GameType.Tests)
                RunTests(size, targetLength);
            else
            {
                var gameMaster = new GameMaster(selectedOption, size, targetLength);

                var result = gameMaster.StartGame();
                Console.Read();
            }
        }

        static int GetSize()
        {
            Console.WriteLine("Wprowadź liczbę liczb do wylosowania: ");
            string input;
            while(true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out int n) && n > 0)
                    return n;

                Console.WriteLine("Niepoprawny rozmiar zadania. Oczekiwana liczba większa od 0.");
            }
        }

        static int GetTargetLength()
        {
            Console.WriteLine("Wprowadź długość ciągu będącego celem gry: ");
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out int n) && n > 2)
                    return n;

                Console.WriteLine("Niepoprawna długość ciągu. Oczekiwana liczba większa od 2.");
            }
        }

        static GameType GetGameType()
        {
            Console.WriteLine("============Green-Tao=============\n" +
                              "\tWybierz rodzaj gry:\n" +
                              "\t1. Komputer vs Komputer\n" +
                              "\t2. Komputer vs Gracz\n" +
                              "\t0. Wyjście z gry\n" +
                              "\t3. Testy\n");

            string input;
            while (true)
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "1": return GameType.ComputerVsComputer;
                    case "2": return GameType.ComputerVsUser;
                    case "3": return GameType.Tests;
                    case "0":
                        Environment.Exit(0);
                        break;
                    
                    default:
                        Console.WriteLine("Nierozpoznana opcja!");
                        break;
                }
            }
        }

        private static void RunTests(int size, int targetLength)
        {
            Console.WriteLine("All statistics are for the first player.");
            //first is always blue

            Console.WriteLine("Random vs. Offensive:");
            int[] score = TestEnv.RunTest(Strategy.RandomStrategy, Strategy.OffensiveStrategy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Offensive vs. Random:");
            score = TestEnv.RunTest(Strategy.OffensiveStrategy, Strategy.RandomStrategy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Random vs. Deffensive:");
            score = TestEnv.RunTest(Strategy.RandomStrategy, Strategy.DefensiveStratgy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Deffensive vs. Random:");
            score = TestEnv.RunTest(Strategy.DefensiveStratgy, Strategy.RandomStrategy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Random vs. Balanced:");
            score = TestEnv.RunTest(Strategy.RandomStrategy, Strategy.BalancedStrategy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Balanced vs. Random:");
            score = TestEnv.RunTest(Strategy.BalancedStrategy, Strategy.RandomStrategy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();
            
            Console.WriteLine("Offensive vs. Balanced:");
            score = TestEnv.RunTest(Strategy.OffensiveStrategy, Strategy.BalancedStrategy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Balanced vs. Offensive:");
            score = TestEnv.RunTest(Strategy.BalancedStrategy, Strategy.OffensiveStrategy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Offensive vs. Deffensive:");
            score = TestEnv.RunTest(Strategy.OffensiveStrategy, Strategy.DefensiveStratgy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Deffensive vs. Offensive:");
            score = TestEnv.RunTest(Strategy.DefensiveStratgy, Strategy.OffensiveStrategy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Deffensive vs. Balanced:");
            score = TestEnv.RunTest(Strategy.DefensiveStratgy, Strategy.BalancedStrategy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Balanced vs. Deffensive:");
            score = TestEnv.RunTest(Strategy.BalancedStrategy, Strategy.DefensiveStratgy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Balanced vs. Balanced:");
            score = TestEnv.RunTest(Strategy.BalancedStrategy, Strategy.BalancedStrategy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Deffensive vs. Deffensive:");
            score = TestEnv.RunTest(Strategy.DefensiveStratgy, Strategy.DefensiveStratgy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Offensive vs. Offensive:");
            score = TestEnv.RunTest(Strategy.OffensiveStrategy, Strategy.OffensiveStrategy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();

            Console.WriteLine("Random vs. Random:");
            score = TestEnv.RunTest(Strategy.RandomStrategy, Strategy.RandomStrategy, size, targetLength);
            Console.WriteLine($"Won: {score[0]}, lost: {score[1]}, drawn: {score[2]}");
            Console.WriteLine();
        }
    }
}
