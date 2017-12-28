using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace item_set_mining
{
    class Transaction
    {
        public List<String> Item_Set
        {
            get;
            set;
        }

        public int Size
        {
            get;
            set;
        }

        public int Support
        {
            get;
            set;
        }

        public int TID
        {
            get;
            set;
        }

        public Transaction()
        {
            TID = 0;
            Item_Set = new List<String>();
        }

        public Transaction(int tid)
        {
            TID = tid;
            Item_Set = new List<String>();
        }

        public void Add_Item(String item)
        {
            Item_Set.Add(item);
            Size = Item_Set.Count;
        }

        void Print_Item_Set()
        {
            Console.Write("{ ");
            for (int i = 0; i < Item_Set.Count; i++)
            {
                Console.Write(Item_Set[i]);
                if (i < Item_Set.Count - 1)
                    Console.Write(",");
                Console.Write(" ");
            }
            Console.Write("}");
        }

        public void Print_Transaction()
        {
            Console.Out.Write($"{TID}: ");
            Print_Item_Set();
        }
    }
}
