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

            var gameMaster = new GameMaster(selectedOption, size, targetLength);

            var result = gameMaster.StartGame();
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
                if (int.TryParse(input, out int n) && n > 0)
                    return n;

                Console.WriteLine("Niepoprawna długość ciągu. Oczekiwana liczba większa od 0.");
            }
        }

        static GameType GetGameType()
        {
            Console.WriteLine("============Green-Tao=============\n" +
                              "\tWybierz rodzaj gry:\n" +
                              "\t1. Komputer vs Komputer\n" +
                              "\t2. Komputer vs Gracz\n" +
                              "\t0. Wyjście z gry");

            string input;
            while(true)
            {
                input = Console.ReadLine();
                switch(input)
                {
                    case "1": return GameType.ComputerVsComputer;
                    case "2": return GameType.ComputerVsUser;
                    case "0": Environment.Exit(0);
                        break;
                    default: Console.WriteLine("Nierozpoznana opcja!");
                        break;
                }
            }
        }
    }
}
