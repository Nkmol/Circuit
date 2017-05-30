using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    using Circuit;

    class Program
    {
        static void Main(string[] args)
        {
            BoardController controller = new BoardController();
            controller.LoadBoard();
            controller.DrawBoard();
            Console.ReadKey();
        }
    }
}
