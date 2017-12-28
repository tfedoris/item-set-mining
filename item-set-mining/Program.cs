using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace item_set_mining
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            int min_support;

            Console.Write("Please enter the path for the Excel file you wish to use: ");
            path = Console.ReadLine();
            path = path.Trim(new Char[] { '"' });

            Console.Write("\nPlease enter the minimum support threshold you wish to use: ");
            min_support = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");

            Freq_Item_Mine fim = new Freq_Item_Mine(min_support, path);
        }
    }
}
