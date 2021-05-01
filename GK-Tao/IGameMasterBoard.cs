namespace GK_Tao
{
    internal interface IGameMasterBoard : IPlayerBoard
    {
        Field GetFieldByValue(int value);
        void SetFieldColor(Enums.FieldColor color, int fieldValue);
    }
}