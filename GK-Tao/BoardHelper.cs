using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Tao
{
    public static class BoardHelper
    {
        #region Fields
        private const string filePath = @"./Data/primes1.txt";

        private const int constModifier = 5;

        private static Random rng = new Random();

        private static IEnumerable<string> loadedFile = null;
        #endregion

        public static IEnumerable<Field> GetFieldsFromFile(int size)
        {
            if (loadedFile == null)
            {
                var primesText = System.IO.File.ReadAllText(filePath);
                loadedFile = primesText.Split("\r\n\t ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }

            lock (loadedFile)
            {
                var fields = loadedFile.Take(size * constModifier)
                    .OrderBy(p => rng.Next())
                    .Take(size)
                    .Select(p => new Field() { FieldColor = Enums.FieldColor.White, Value = int.Parse(p) });

                return fields;
            }
        }
    }
}
