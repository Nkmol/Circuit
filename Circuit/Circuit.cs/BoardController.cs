using Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Circuit
{
    public class BoardController
    {
        public BoardController()
        {
            
        }

        public void Load()
        {
            var path = @"C:\Users\srm\Desktop\test.txt";
            var reader = new FileReader(path);

            try
            {
                foreach (var c in reader.Read())
                {
                    Console.WriteLine(c);
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
