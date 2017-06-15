namespace Datatypes
{
    using System;

    public interface IFactory<T>
    {
        void AddType(string typenaming, Type type);
        T Create(string type);
    }
}