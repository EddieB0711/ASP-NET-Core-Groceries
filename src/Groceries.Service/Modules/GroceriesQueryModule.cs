namespace Groceries.Service.Modules
{
    using Groceries.Service.Infrastructure.Entities;

    using Nancy;

    using Web.Contracts.Extensions;
    using Web.Contracts.Repositories;

    public class GroceriesQueryModule : NancyModule
    {
        private readonly IReadOnlyRepository<Grocery> _readOnlyRepository;

        private readonly IViewRepository<Grocery> _viewRepository;

        public GroceriesQueryModule(
            IReadOnlyRepository<Grocery> readOnlyRepository,
            IViewRepository<Grocery> viewRepository)
        {
            readOnlyRepository.Guard();
            viewRepository.Guard();

            _readOnlyRepository = readOnlyRepository;
            _viewRepository = viewRepository;

            GetAllGroceries();
            GetGrocery();
        }

        private void GetAllGroceries()
        {
            Get(
                "/groceries/",
                args =>
                    {
                        var records = _readOnlyRepository.Find();

                        return Response.AsJson(records);
                    });
        }

        private void GetGrocery()
        {
            Get(
                "/groceries/{id:int}",
                args =>
                    {
                        int id = args.id;
                        var record = _viewRepository.Get(x => x.Id == id);

                        return record != null ? Response.AsJson(record) : HttpStatusCode.BadRequest;
                    });
        }
    }
}