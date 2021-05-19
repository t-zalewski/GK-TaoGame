using GK_Tao.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GK_Tao.Players
{
    public class RandomBot : Player
    {
        #region Fields
        private Random rng;
        #endregion

        public RandomBot(FieldColor playerColor) : base(playerColor)
        {
            rng = new Random();
        }

        public override void InitializeGame(IPlayerBoard board, int size, int targetLength)
        {
            
        }

        public override int SelectFieldValue(IPlayerBoard board)
        {
            var emptyFields = board.GetEmptyFields().ToList();
            var ind = rng.Next(emptyFields.Count);
            //this.Sleep();
            return emptyFields[ind].Value;
        }

        private void Sleep()
        {
            const int sleepTime = 2000;
            Thread.Sleep(sleepTime);
        }
    }
}
