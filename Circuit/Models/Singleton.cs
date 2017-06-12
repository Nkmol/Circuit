using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Singleton<T>
        where T : class
    {
        protected Singleton()
        {
        }

        private static readonly Lazy<T> InstanceHolder =
            new Lazy<T>(() => (T) Activator.CreateInstance(typeof(T), true));

        public static T Instance => InstanceHolder.Value;
    }
}

