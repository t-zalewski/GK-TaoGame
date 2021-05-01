using GK_Tao.Enums;
using GK_Tao.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Tao
{
    public class GameMaster
    {
        #region Fields
        private GuiController guiController;
        #endregion

        #region properties
        public GameType GameType { get; }
        public int Size { get; }
        public int TargetLength { get; }
        public Board Board { get; }
        public Player[] Players { get; }
        public GameStatus GameStatus { get; private set; }
        #endregion

        public GameMaster(GameType gameType, int size, int targetLength)
        {
            this.GameType = gameType;
            this.Size = size;
            this.TargetLength = targetLength;
            this.guiController = new GuiController();

            this.Board = new Board(size);
            this.Players = PlayersFactory.CreatePlayers(gameType);
        }

        public GameStatus StartGame()
        {
            this.GameStatus = GameStatus.InProgress;
            this.guiController.DrawBoard(this.Board);
            
            var playerIdTurn = 0;

            while(this.GameStatus == GameStatus.InProgress)
            {
                var currentPlayer = Players[playerIdTurn];

                var move = currentPlayer.SelectFieldValue(this.Board);
                if (!this.CheckIfCorrectMove(move))
                {
                    this.guiController.DrawBoardWithError(this.Board, $"Niepoprawny ruch! Podano pole: {move}");
                    continue;
                }

                this.MarkSelectedField(move, currentPlayer);
                this.CheckIfGameEndedAndSetStatus();

                this.guiController.DrawBoard(this.Board);
                playerIdTurn = (playerIdTurn + 1) % 2;
            }

            this.EndGame();
            return this.GameStatus;
        }

        private bool CheckIfCorrectMove(int fieldValue)
        {
            if (fieldValue < 1)
                return false;

            if (!this.Board.IsFieldExist(fieldValue))
                return false;

            if (this.Board.GetFieldByValue(fieldValue).FieldColor != FieldColor.White)
                return false;

            return true;
        }

        private void MarkSelectedField(int fieldValue, Player player)
        {
            this.Board.SetFieldColor(player.Color, fieldValue);
        }

        private void CheckIfGameEndedAndSetStatus()
        {
            // TODO: Check if players won
            var emptyFields = this.Board.GetEmptyFields();
            if (emptyFields.Count() == 0)
            {
                this.GameStatus = GameStatus.Draw;
            }
        }

        private void EndGame()
        {
            if (this.GameStatus == GameStatus.Draw)
            {
                this.guiController.DrawBoardWithInfo(this.Board, "Gra skończona remisem");
                return;
            }

            this.guiController.DrawBoardWithInfo(this.Board, $"Gra skończona. Wygrał gracz {(this.GameStatus == GameStatus.BlueWon ? '1' : '2')}");
        }

    }
}
