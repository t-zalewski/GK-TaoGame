using System.Collections.Generic;

namespace GK_Tao
{
    public interface IPlayerBoard
    {
        IEnumerable<Field> GetEmptyFields();
        IEnumerable<Field> GetFieldsByColor(Enums.FieldColor color);
        bool IsFieldExist(int fieldValue);
    }
}