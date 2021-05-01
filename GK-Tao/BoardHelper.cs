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
        #endregion

        public static IEnumerable<Field> GetFieldsFromFile(int size)
        {
            var primesText = System.IO.File.ReadAllText(filePath);
            var fields = primesText.Split("\r\n\t ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Take(size * constModifier)
                .OrderBy(p => rng.Next())
                .Take(size)
                .Select(p => new Field(){ FieldColor= Enums.FieldColor.White, Value = int.Parse(p)});

            return fields;
        }
    }
}
