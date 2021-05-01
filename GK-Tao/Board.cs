using GK_Tao.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Tao
{
    public class Board : IPlayerBoard, IGameMasterBoard
    {
        #region Fields

        private IList<Field> fields;

        private int boardSize;

        #endregion

        public Board(int boardSize)
        {
            this.boardSize = boardSize;
            fields = BoardHelper.GetFieldsFromFile(boardSize).ToList();
        }

        #region Methods

        public IEnumerable<Field> GetFieldsByColor(FieldColor color)
        {
            return fields.Where(f => f.FieldColor == color);
        }

        public IEnumerable<Field> GetEmptyFields()
        {
            return fields.Where(f => f.FieldColor == FieldColor.White);
        }

        public Field GetFieldByValue(int value)
        {
            return fields.First(f => f.Value == value);
        }

        public void SetFieldColor(FieldColor color, int fieldValue)
        {
            if (color == FieldColor.White)
                throw new ArgumentException();

            fields.First(f => f.Value == fieldValue).FieldColor = color;
        }

        public bool IsFieldExist(int fieldValue)
        {
            return fields.Any(f => f.Value == fieldValue);
        }

        #endregion
    }
}
