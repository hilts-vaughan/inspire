using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> _lstNeedToOrder = new List<int>();
            _lstNeedToOrder.AddRange(new int[] { 1, 5, 6, 8 });
            //need to sort this based on the below list

            List<int> _lstOrdered = new List<int>();//to order by this list
            _lstOrdered.AddRange(new int[] { 13, 5, 11, 1, 4, 9, 2, 7, 12, 10, 3, 8, 6 });

            List<int> results = new List<int>();

            foreach (var item in _lstOrdered)
            {
                if (_lstNeedToOrder.Contains(item))
                {
                    results.Add(item);
                }
            }


        }
    }
}
