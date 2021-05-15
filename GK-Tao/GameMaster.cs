using GK_Tao.Enums;
using GK_Tao.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK_Tao.Algorithms;

namespace GK_Tao
{
    public class GameMaster
    {
        #region Fields
        private GuiController guiController;
        private List<List<Field>>[] playerAPPossibilities;
        private List<Field> winningAP = null;
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
            List<List<Field>> allPossibilities = null;
            do
            {
                this.Board = new Board(size);
                allPossibilities = APFinder.FindAllSequences(Board, TargetLength, Size);
            } while (allPossibilities.Count == 0);

            this.Players = PlayersFactory.CreatePlayers(gameType, Strategy.OffensiveStrategy, Strategy.RandomStrategy);
            this.playerAPPossibilities = new List<List<Field>>[Players.Length];
            
            for (int i = 0; i < playerAPPossibilities.Length; i++)
                playerAPPossibilities[i] = allPossibilities.ToList();
        }

        public GameStatus StartGame()
        {
            this.Players[0].InitializeGame(this.Board, this.Size, this.TargetLength);
            this.Players[1].InitializeGame(this.Board, this.Size, this.TargetLength);

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
                this.UpdatePlayerPossibilities(playerIdTurn, move);
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

        private void UpdatePlayerPossibilities(int currentPlayerId, int moveMade)
        {
            int anotherPlayerId = (currentPlayerId + 1) % 2;
            this.playerAPPossibilities[anotherPlayerId].RemoveAll(ap => ap.Any(f => f.Value == moveMade));
        }

        private void CheckIfGameEndedAndSetStatus()
        {
            if (playerAPPossibilities.All(apPoss => apPoss.Count == 0))
            {
                this.GameStatus = GameStatus.Draw;
                return;
            }

            for (int id = 0; id < Players.Length; id++)
                if (CheckIfPlayerWon(id))
                {
                    this.GameStatus = Players[id].Color == FieldColor.Blue ? GameStatus.BlueWon : GameStatus.RedWon;
                    return;
                }
        }

        private bool CheckIfPlayerWon(int playerId)
        {
            FieldColor playerColor = Players[playerId].Color;
            IEnumerable<Field> playerFields = Board.GetFieldsByColor(playerColor);
            foreach (List<Field> playerWinningAP in playerAPPossibilities[playerId])
            {
                bool apMatched = true;
                foreach (Field field in playerWinningAP)
                    if (!playerFields.Any(f => f.Value == field.Value))
                    {
                        apMatched = false;
                        break;
                    }

                if (apMatched)
                {
                    winningAP = playerWinningAP;
                    return true;
                }
            }

            return false;
        }

        private void EndGame()
        {
            if (this.GameStatus == GameStatus.Draw)
            {
                this.guiController.DrawBoardWithInfo(this.Board, "Gra skończona remisem");
                return;
            }

            this.guiController.DrawBoardWithInfo(this.Board, 
                $"Gra skończona. Wygrał gracz {(this.GameStatus == GameStatus.BlueWon ? '1' : '2')}\n" +
                $"Zwycięski ciąg arytmetyczny {CreateWinningAPString()}");
        }

        private string CreateWinningAPString()
        {
            string apString = "";
            if (winningAP != null)
            {
                if (winningAP[0].Value > winningAP[winningAP.Count - 1].Value)
                    winningAP.Reverse();
                
                foreach (Field field in winningAP)
                    apString += $"{field.Value}, ";

                int apDiff = winningAP[1].Value - winningAP[0].Value;
                apString += $"\nróżnica ciągu wynosi {apDiff}.";
            }

            return apString;
        }

    }
}
