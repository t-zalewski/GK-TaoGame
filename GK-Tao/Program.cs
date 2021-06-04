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
            bool isComputerFirst = false;
            Strategy[] strategies = new Strategy[2];

            switch (selectedOption)
            {
                case GameType.ComputerVsComputer:
                    strategies[0] = Program.GetComputerStrategy();
                    strategies[1] = Program.GetComputerStrategy();
                    RunGame(selectedOption, size, targetLength, strategies, isComputerFirst);
                    break;
                case GameType.ComputerVsUser:
                    strategies[0] = Program.GetComputerStrategy();
                    isComputerFirst = Program.IsComputerFirstPlayer();
                    RunGame(selectedOption, size, targetLength, strategies, isComputerFirst);
                    break;
                case GameType.Tests:
                    int testsCount = Program.GetTestsCount();
                    RunTests(size, targetLength, testsCount);
                    break;
                default:
                    break;
            }
        }

        private static void RunGame(GameType selectedOption, int size, int targetLength, Strategy[] strategies, bool isComputerFirst)
        {
            var gameMaster = new GameMaster(selectedOption, size, targetLength, strategies, isComputerFirst);

            var result = gameMaster.StartGame();
            Console.Read();
        }

        private static bool IsComputerFirstPlayer()
        {
            Console.WriteLine("Wskaż gracza rozpoczynającego: K - komputer, U - użytkownik");
            string input;
            while (true)
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "K": return true;
                    case "U": return false;
                    
                    default:
                        Console.WriteLine("Nierozpoznana opcja!");
                        break;
                }
            }

        }

        private static int GetTestsCount()
        {
            Console.WriteLine("Wprowadź liczbę testów do przeprowadzenia:");
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out int n) && n > 0)
                    return n;

                Console.WriteLine("Niepoprawny rozmiar zadania. Oczekiwana liczba większa od 0.");
            }

        }
        private static Strategy GetComputerStrategy()
        {
            Console.WriteLine("Wprowadź nazwę strategii gracza komputerowego: \nL - losowa, O - ofensywna, D - defensywna, Z - zbalansowana");
            string input;
            while (true)
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "L": return Strategy.RandomStrategy;
                    case "O": return Strategy.OffensiveStrategy;
                    case "D": return Strategy.DefensiveStratgy;
                    case "Z": return Strategy.BalancedStrategy;

                    default:
                        Console.WriteLine("Nierozpoznana opcja!");
                        break;
                }
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

        private static void RunTests(int size, int targetLength, int testsCount)
        {
            Console.WriteLine("All statistics are for the first player.");
            //first is always blue
            TestEnv.testIterations = testsCount;
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
