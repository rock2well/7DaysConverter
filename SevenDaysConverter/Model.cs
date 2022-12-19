using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SevenDaysConverter
{
    public class Model
    {
        public char[] Word { get; set; } = new char[12];

        public List<string> Candidate = new List<string>();

        public Model()
        {
            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < Word.Length; i++)
            {
                Word[i] = (char)('a' - 1);
            }
        }

        public void Reset(int index)
        {
            if (index < 0 || index >= Word.Length)
                return;
            Word[index] = (char)('a' - 1);
        }

        public void Add(int index)
        {
            if (index < 0 || index >= Word.Length)
                return;

            Word[index] = (char)(Word[index] + 1);
            if (Word[index] > 'z')
                Word[index] = (char)('a' - 1);
        }

        public void Sub(int index)
        {
            if (index < 0 || index >= Word.Length)
                return;

            Word[index] = (char)(Word[index] - 1);
            if (Word[index] < (char)('a' - 1))
                Word[index] = 'z';
        }

        public void Convert()
        {
            Candidate.Clear();

            //! 現在設定しているデータを変換する
            for(int i=0;i<25;++i)
            {
                string candidate = "";
                foreach(char c in Word)
                {
                    if ('a' <= c && c <= 'z')
                    {
                        char trans = (char)(c + i);
                        if (trans >= 'z')
                        {
                            trans = (char)((int)'a' + (int)trans - (int)'z');
                        }

                        candidate += trans;
                    }
                }

                Candidate.Add(candidate);
            }
        }
    }
}
