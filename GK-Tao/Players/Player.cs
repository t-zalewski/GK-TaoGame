using GK_Tao.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Tao.Players
{
    public abstract class Player
    {
        #region Fields
        #endregion

        #region Properties
        public FieldColor Color { get; }

        protected FieldColor OpponentColor => Color == FieldColor.Blue ? FieldColor.Red : FieldColor.Blue;
        #endregion

        public Player (FieldColor playerColor)
        {
            this.Color = playerColor;
        }

        public abstract void InitializeGame(IPlayerBoard board, int size, int targetLength);

        public abstract int SelectFieldValue(IPlayerBoard board);
    }
}
