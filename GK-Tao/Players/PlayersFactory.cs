﻿using GK_Tao.Enums;
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

        private static Player GetPlayerByStrategy(Strategy strategy, FieldColor color)
        {
            switch(strategy)
            {
                case Strategy.RandomStrategy:
                    return new RandomBot(color);
                default:
                    return null;
            }
        }
    }
}
