namespace Web.Contracts.Entities
{
    public interface ILiteral<out T>
    {
        T Id { get; }
    }
}