using GK_Tao.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GK_Tao.Algorithms;

namespace GK_Tao.Players
{
    class StrategyPlayer : Player
    {
        #region Properties
        public double OffensiveFactor { get; }
        public double DefensiveFactor { get; }
        public int Size { get; private set; }
        public int TargetLength { get; private set; }
        private List<List<Field>> allSequences { get; set; }
        private List<List<Field>> myAvailableSequences { get; set; }
        private List<List<Field>> opponentAvailableSequences { get; set; }
        private Random random { get; }
        private bool withComputer;
        #endregion

        public StrategyPlayer(FieldColor playerColor, double offensiveFactor, double defensiveFactor, bool withComputer = true) : base(playerColor)
        {
            this.OffensiveFactor = offensiveFactor;
            this.DefensiveFactor = defensiveFactor;
            this.random = new Random();
            this.withComputer = withComputer;
        }

        public override void InitializeGame(IPlayerBoard board, int size, int targetLength)
        {
            this.Size = size;
            this.TargetLength = targetLength;
            this.allSequences = APFinder.FindAllSequences(board, targetLength, size).ToList();
            this.myAvailableSequences = this.allSequences.ToList();
            this.opponentAvailableSequences = this.allSequences.ToList();
        }

        public override int SelectFieldValue(IPlayerBoard board)
        {
            myAvailableSequences.RemoveAll(seq => seq.Exists(field => field.FieldColor == this.OpponentColor));

            int chosenValue = -1;

            var winningSequence = myAvailableSequences.Find(sequence => sequence.FindAll(field => field.FieldColor == FieldColor.White).Count == 1);
            if (winningSequence != null)
            {
                chosenValue = winningSequence.Find(field => field.FieldColor == FieldColor.White).Value;
            } 
            
            if (chosenValue < 0)
            {
                var opponentWinningSequence = opponentAvailableSequences.Find(sequence => sequence.FindAll(field => field.FieldColor == FieldColor.White).Count == 1);
                if (opponentWinningSequence != null)
                {
                    chosenValue = opponentWinningSequence.Find(field => field.FieldColor == FieldColor.White).Value;
                }
            }

            if (chosenValue < 0)
            {
                var promisingSequence = myAvailableSequences.Find(sequence => sequence.FindAll(field => field.FieldColor == FieldColor.White).Count == 2);
                if (promisingSequence != null)
                {
                    chosenValue = promisingSequence.FindAll(field => field.FieldColor == FieldColor.White).Select(field => field.Value).Min();
                }
            }

            if (chosenValue < 0)
            {
                chosenValue = ChooseBestStrategyValue(board);
            }
            
            opponentAvailableSequences.RemoveAll(seq => seq.Exists(item => item.Value == chosenValue));
            if (!withComputer)
                this.Sleep();
            return chosenValue;
        }
    
        private int ChooseBestStrategyValue(IPlayerBoard board)
        {
            var freeFields = board.GetFieldsByColor(FieldColor.White);

            double maxStrategyValue = 0;
            var chosenFieldValues = new List<int>();

            foreach (var field in freeFields)
            {
                double off = GetOffensiveValue(field);
                double def = GetDefensiveValue(field);
                double strategyValue = GetOffensiveValue(field) * this.OffensiveFactor + GetDefensiveValue(field) * this.DefensiveFactor;
                if (strategyValue > maxStrategyValue)
                {
                    maxStrategyValue = strategyValue;
                    chosenFieldValues.Clear();
                    chosenFieldValues.Add(field.Value);
                }
                else if (strategyValue == maxStrategyValue)
                {
                    chosenFieldValues.Add(field.Value);
                }
            }

            return chosenFieldValues[random.Next(chosenFieldValues.Count)];
        }
        private double GetOffensiveValue(Field field)
        {
            double value = 0;

            foreach (var sequence in myAvailableSequences)
            {
                if (sequence.Exists(f => f.Value == field.Value))
                {
                    var count = sequence.FindAll(f => f.FieldColor == this.Color).Count + 1;
                    value += Math.Pow(count, 3);
                }
            }
            return value;
        }

        private double GetDefensiveValue(Field field)
        {
            double value = 0;

            foreach (var sequence in opponentAvailableSequences)
            {
                if (sequence.Exists(f => f.Value == field.Value))
                {
                    var count = sequence.FindAll(f => f.FieldColor == this.Color).Count + 1;
                    value += Math.Pow(count, 3);
                }
            }
            return value;
        }

        private void Sleep()
        {
            const int sleepTime = 2000;
            Thread.Sleep(sleepTime);
        }
    }
}
