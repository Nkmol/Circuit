using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Board
    {
        private DirectGraph components;

        public Board(DirectGraph nodes)
        {
            this.components = nodes;
        }
    }
}
