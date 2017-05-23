using Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Circuit
{
    using Models;

    public class BoardController
    {
        public BoardController()
        {
            
        }

        public void Load()
        {
            var path = @"C:\Users\srm\Desktop\Circuit1_FullAdder.txt";
            var reader = new FileReader(path);
            var boardParser = new BoardParser();

            try
            {
                foreach (var line in reader.ReadLine())
                {
                    boardParser.Parse(line);
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
