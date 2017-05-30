using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Board
    {
        private DirectGraph<Component> components;

        public Board(DirectGraph<Component> nodes)
        {
            this.components = nodes;
        }
    }
}
