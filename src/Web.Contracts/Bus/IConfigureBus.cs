namespace Web.Contracts.Bus
{
    using System.Threading.Tasks;

    using StructureMap;

    public interface IConfigureBus
    {
        Task ConfigureAsync(IContainer container);
    }
}