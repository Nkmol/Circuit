namespace Models
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}