using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartJob
{
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingCartTask task = new ShoppingCartTask();
            task.Execute();
        }
    }
}
