using GK_Tao.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Tao
{
    public class GuiController
    {
        public void DrawBoard(Board board, bool clearScreen = true)
        {
            var blueFields = String.Join("  ", board.GetFieldsByColor(FieldColor.Blue).Select(f => f.Value).OrderBy(f => f));
            var redFields = String.Join("  ", board.GetFieldsByColor(FieldColor.Red).Select(f => f.Value).OrderBy(f => f));
            var emptyFields = String.Join("  ", board.GetFieldsByColor(FieldColor.White).Select(f => f.Value).OrderBy(f => f));

            if(clearScreen)
                Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("===Gracz 1===");
            Console.WriteLine(blueFields);

            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(emptyFields);
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(redFields);
            Console.WriteLine("===Gracz 2===");

            Console.ResetColor();
        }

        public void DrawBoardWithError(Board board, string error, bool clearScreen = true)
        {
            this.DrawBoard(board, clearScreen);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(error);

            Console.ResetColor();
        }

        public void DrawBoardWithInfo(Board board, string info, bool clearScreen = true)
        {
            this.DrawBoard(board, clearScreen);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(info);

            Console.ResetColor();
        }
    }
}
