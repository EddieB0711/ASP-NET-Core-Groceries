namespace Groceries.Service.Infrastructure.Builders
{
    using System.Collections.Generic;

    using Groceries.Contracts.Commands;
    using Groceries.Contracts.Events;
    using Groceries.Service.Infrastructure.Entities;

    using Web.Contracts.Builders;
    using Web.Contracts.Extensions;

    public class FindAllGroceriesToAllGroceriesFoundBuilder : ICommandBuilder<FindAllGroceries, AllGroceriesFound>
    {
        public AllGroceriesFound Create(FindAllGroceries command, params object[] valueObjects)
        {
            var result = ((IList<Grocery>)valueObjects[0]).Serialize();
            return new AllGroceriesFound(result);
        }
    }
}