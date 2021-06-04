using GK_Tao.Enums;
using GK_Tao.Players;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Tao
{
    public static class TestEnv
    {
        public static int testIterations = 1000;
        public static int[] RunTest(Strategy firstPlayerStrategy, Strategy secondPlayerStrategy, int size, int targetLength)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            int[] score = new int[3];
            Parallel.For(0, testIterations, (i) =>
            {
               GameMaster gm = new GameMaster(GameType.Tests, size, targetLength,
                   new Strategy[] { firstPlayerStrategy, secondPlayerStrategy }, false);
               GameStatus result = gm.StartGame(false);

               lock (score)
               {
                   if (result == GameStatus.BlueWon)
                       score[0]++;
                   
                   if (result == GameStatus.RedWon)
                       score[1]++;

                   if (result == GameStatus.Draw)
                       score[2]++;

                   if (result == GameStatus.InProgress)
                       throw new Exception("Game not ended");
               }
            });
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            //Console.WriteLine("RunTime " + elapsedTime);

            return score;
        }
    }
}
