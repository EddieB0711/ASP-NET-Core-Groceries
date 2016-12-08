namespace Groceries.Module.Controllers
{
    using System.Threading.Tasks;

    using Groceries.Infrastructure.Commands;
    using Groceries.Module.Models;

    using Microsoft.AspNetCore.Mvc;

    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;

    public class CreateController : Controller
    {
        private readonly IAsyncCommandBus _bus;

        private readonly ICommandBuilder<GroceryViewModel, CreateGroceryCommand> _createGroceryCommandBuilder;

        public CreateController(
            IAsyncCommandBus bus,
            ICommandBuilder<GroceryViewModel, CreateGroceryCommand> createGroceryCommandBuilder)
        {
            bus.Guard();
            createGroceryCommandBuilder.Guard();

            _bus = bus;
            _createGroceryCommandBuilder = createGroceryCommandBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroceryViewModel model)
        {
            await _bus.SendAsync(_createGroceryCommandBuilder.Create(model));

            ModelState.Clear();

            return Redirect("Index");
        }
    }
}