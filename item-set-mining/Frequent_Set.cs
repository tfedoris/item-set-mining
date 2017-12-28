using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace item_set_mining
{
    class Frequent_Set
    {
        public List<string> Freq_Set
        {
            get;
            set;
        }

        public int Support
        {
            get;
            set;
        }
        
        public Frequent_Set(List<string> fs, int s)
        {
            Freq_Set = fs;
            Support = s;
        }

        public void Print_Freq_Set()
        {
            Console.Write("{ ");
            for (int i = 0; i < Freq_Set.Count; i++)
            {
                Console.Write(Freq_Set[i]);
                if (i < Freq_Set.Count - 1)
                    Console.Write(",");
                Console.Write(" ");
            }
            Console.Write("}");
            Console.Write($": {Support}");
        }
    }
}
