using GK_Tao.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Tao.Players
{
    public static class PlayersFactory
    {
        private static Strategy defaultBot1Strategy = Strategy.RandomStrategy;
        private static Strategy defaultBot2Strategy = Strategy.RandomStrategy;

        public static Player[] CreatePlayers(GameType gameType)
        {
            return PlayersFactory.CreatePlayers(gameType, PlayersFactory.defaultBot1Strategy, PlayersFactory.defaultBot2Strategy);
        }

        public static Player[] CreatePlayers(GameType gameType, Strategy bot1Strategy, Strategy bot2Strategy)
        {
            var player1 = GetPlayerByStrategy(bot1Strategy, FieldColor.Blue);
            var player2 = gameType == GameType.ComputerVsUser ? new UserPlayer(FieldColor.Red) : GetPlayerByStrategy(bot2Strategy, FieldColor.Red);

            return new Player[] { player1, player2 };
        }

        public static Player[] CreatePlayers(GameType gameType, Strategy[] computerStrategies, bool isComputerFirst)
        {
            Player firstPlayer = null, secondPlayer = null;

            if (gameType == GameType.ComputerVsUser)
            {
                firstPlayer = isComputerFirst ? GetPlayerByStrategy(computerStrategies[0], FieldColor.Blue, false) : new UserPlayer(FieldColor.Blue);
                secondPlayer = isComputerFirst ? new UserPlayer(FieldColor.Red) : GetPlayerByStrategy(computerStrategies[0], FieldColor.Red, false);
            }

            if (gameType == GameType.ComputerVsComputer)
            {
                firstPlayer = GetPlayerByStrategy(computerStrategies[0], FieldColor.Blue, false);
                secondPlayer = GetPlayerByStrategy(computerStrategies[1], FieldColor.Red, false);
            }

            return new Player[] { firstPlayer, secondPlayer };
        }

        public static Player GetPlayerByStrategy(Strategy strategy, FieldColor color, bool withComputer = true)
        {
            switch(strategy)
            {
                case Strategy.RandomStrategy:
                    return new RandomBot(color, withComputer);
                case Strategy.OffensiveStrategy:
                    return new StrategyPlayer(color, 0.75, 0.25, withComputer);
                case Strategy.DefensiveStratgy:
                    return new StrategyPlayer(color, 0.25, 0.75, withComputer);
                case Strategy.BalancedStrategy:
                    return new StrategyPlayer(color, 0.5, 0.5, withComputer);
                default:
                    return null;
            }
        }
    }
}
