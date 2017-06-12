using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Factory<T>
    {
        protected readonly Dictionary<string, Type> Types = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        public void AddType(string typenaming, Type type)
        {
            Types[typenaming] = type;
        }

        public abstract T Create(string type);
    }
}
