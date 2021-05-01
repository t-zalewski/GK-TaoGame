using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK_Tao.Enums;

namespace GK_Tao.Players
{
    class UserPlayer : Player
    {
        public UserPlayer(FieldColor playerColor) : base(playerColor)
        {
        }

        public override int SelectFieldValue(IPlayerBoard board)
        {
            Console.WriteLine("Wybierz pole: ");
            string selectedValue = Console.ReadLine();
            if (int.TryParse(selectedValue, out int res))
                return res;

            return -1;
        }
    }
}
