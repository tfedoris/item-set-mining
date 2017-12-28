using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace item_set_mining
{
    class Transaction_DB
    {
        Excel.Application xlApp;
        Excel.Workbook xlWorkbook;
        Excel._Worksheet xlWorksheet;
        Excel.Range xlRange;
        string excel_path;

        public int Min_Support
        {
            get;
            set;
        }

        public int Max_Transaction_Size
        {
            get;
            set;
        }

        public List<Transaction> Transactions
        {
            get;
            set;
        }

        public List<String> Item_Base
        {
            get;
            set;
        }

        public List<List<String>> All_Item_Sets
        {
            get;
            set;
        }

        public Transaction_DB(int ms, string path)
        {
            excel_path = path;
            Min_Support = ms;
            Max_Transaction_Size = 0;
            Transactions = new List<Transaction>();
            Item_Base = new List<String>();
            Init_Transaction_DB();
            All_Item_Sets = Power_Set(Item_Base);
        }

        public void Init_Transaction_DB()
        {
            xlApp = new Excel.Application();
            xlWorkbook = xlApp.Workbooks.Open(@excel_path);
            xlWorksheet = xlWorkbook.Sheets[1];
            xlRange = xlWorksheet.UsedRange;

            int row_count = xlRange.Rows.Count;
            int col_count = xlRange.Columns.Count;

            for (int row = 1; row <= row_count; row++)
            {
                Transaction new_transaction = new Transaction(row);

                for (int col = 1; col <= col_count; col++)
                {
                    if (xlRange.Cells[row, col] != null && xlRange.Cells[row, col].Value2 != null)
                    {
                        new_transaction.Add_Item(xlRange.Cells[row, col].Value2.ToString());
                        if (!In_List(xlRange.Cells[row, col].Value2.ToString()))
                            Item_Base.Add(xlRange.Cells[row, col].Value2.ToString());
                    }
                }

                Transactions.Add(new_transaction);
                if (new_transaction.Size > Max_Transaction_Size)
                    Max_Transaction_Size = new_transaction.Size;
            }



            //Excel cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //Release COM objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //Close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //Quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        public void Print_DB()
        {
            foreach (Transaction transaction in Transactions)
            {
                transaction.Print_Transaction();
                Console.Out.WriteLine();
            }
        }

        bool In_List(string el)
        {
            bool found = false;

            if (Item_Base.Count > 0)
            {

                foreach (string item in Item_Base)
                {
                    if (item == el)
                    {
                        found = true;
                        break;
                    }
                }
            }
            return found;
        }

        List<List<string>> Power_Set(List<string> list)
        {
            List<List<string>> power_set = new List<List<string>>() { new List<string>() };
            
            foreach (string item in list)
            {
                foreach (List<string> set in power_set.ToArray())
                    power_set.Add(new List<string>(set) { item });
            }

            power_set.RemoveAt(0);
            power_set.Sort((a, b) => a.Count.CompareTo(b.Count));

            return power_set;
        }

        private void Print_PowerSet()
        {
            List<List<string>> power_set = Power_Set(Item_Base);

            foreach (List<string> set in power_set)
            {
                foreach (string element in set)
                    Console.Out.Write($"{element} ");
                Console.Out.WriteLine();
            }
        }
    }
}
