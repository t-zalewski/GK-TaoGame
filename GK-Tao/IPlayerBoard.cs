using System.Collections.Generic;

namespace GK_Tao
{
    public interface IPlayerBoard
    {
        IEnumerable<Field> GetEmptyFields();
        IEnumerable<Field> GetFieldsByColor(Enums.FieldColor color);
        Field[] GetFieldsSorted();
        bool IsFieldExist(int fieldValue);
    }
}