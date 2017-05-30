using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views
{
    using Models;

    public class BoardView : ComponentView
    {
        private Board _components = null;
        public BoardView(Board nodes)
        {
            this._components = nodes;
        }

        public void Draw()
        {
            
        }
    }
}
