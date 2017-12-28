using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace item_set_mining
{
    class Freq_Item_Mine
    {
        Transaction_DB db;
        List<Frequent_Set> frequent_sets;

        public Freq_Item_Mine(int min_support, string path)
        {
            db = new Transaction_DB(min_support, path);
            frequent_sets = null;
            Begin_Mining();
        }

        private void Begin_Mining()
        {
            Apriori();
            Print_Frequent_Sets();
        }

        private void Apriori()
        {
            frequent_sets = new List<Frequent_Set>();
            int k = 1; // current target item set size
            List<List<string>> k_candidates = Candidates(k);

            while (k <= db.Max_Transaction_Size)
            {
                Prune(k_candidates);
                k++;
                k_candidates = Candidates(k);
            }

        }

        private List<List<string>> Candidates(int k)
        {
            List<List<string>> candidates = new List<List<string>>();

            foreach (List<string> item_set in db.All_Item_Sets)
            {
                if (item_set.Count > k)
                    break;
                else if (item_set.Count == k)
                    candidates.Add(item_set);
            }

            return candidates;
        }

        private void Prune(List<List<string>> item_sets)
        {
            foreach (List<string> set in item_sets)
            {
                int support = 0;

                foreach (Transaction t in db.Transactions)
                {
                    if (Is_Supported(t, set))
                        support++;
                }

                if (support >= db.Min_Support)
                    frequent_sets.Add(new Frequent_Set(set, support));
            }
        }

        private bool Is_Supported(Transaction t, List<string> set)
        {
            bool status = false;

            foreach (string s in set)
            {
                foreach (string i in t.Item_Set)
                {
                    if (s == i)
                    {
                        status = true;
                        break;
                    }

                    else
                        status = false;
                }

                if (!status)
                    break;
            }

            return status;
        }

        private void Print_Frequent_Sets()
        {
            Console.Out.WriteLine("Frequent Item Sets:");
            int k = 0;

            Console.Out.WriteLine("\n0 ITEMS:");
            Console.Out.WriteLine("{ EMPTY SET }: " + db.Transactions.Count);
            
            foreach (Frequent_Set fs in frequent_sets)
            {
                if (fs.Freq_Set.Count > k)
                {
                    k = fs.Freq_Set.Count;
                    if (k != 1)
                        Console.Out.WriteLine($"\n{k} ITEMS:");
                    else
                        Console.Out.WriteLine($"\n{k} ITEM:");
                }

                fs.Print_Freq_Set();
                Console.Out.WriteLine();
            }

            Console.Out.WriteLine();
        }
    }
}
