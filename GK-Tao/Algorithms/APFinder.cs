using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Tao.Algorithms
{
    public static class APFinder
    {
        public static List<List<Field>> FindAllSequences(IPlayerBoard board, int targetLength, int size)
        {
            var allSequences = new List<List<Field>>();
            var fields = board.GetFieldsSorted();

            var sequence = new Stack<Field>();
            int maxDiff = (fields.Last().Value - fields.First().Value) / (targetLength - 1);
            bool sequenceWithDiffFound;

            for (int i = 0; i < size; i++)
            {
                sequence.Push(fields[i]);

                for (int j = i + 1; j < size; j++)
                {
                    int diff = fields[j].Value - fields[i].Value;
                    sequenceWithDiffFound = false;

                    if (diff > maxDiff)
                        break; //No need to search further - difference is to big, we won't find AP of required length

                    sequence.Push(fields[j]);
                    int lastIndex = j;

                    for (int k = j + 1; k < size; k++)
                    {

                        if (fields[k].Value > fields[lastIndex].Value + diff)
                        {
                            break;
                        }
                        else if (fields[k].Value < fields[lastIndex].Value + diff)
                        {
                            continue;
                        }
                        else
                        {
                            //we found the next element of a sequence
                            sequence.Push(fields[k]);
                            lastIndex = k;

                            if (sequence.Count == targetLength)
                            {
                                allSequences.Add(sequence.ToList());
                                sequenceWithDiffFound = true;
                                break;
                            }
                        }
                    }

                    sequence.Clear();
                    sequence.Push(fields[i]);

                    if (sequenceWithDiffFound)
                        continue;

                }

                sequence.Clear();
            }

            return allSequences;
        }
    }
}
