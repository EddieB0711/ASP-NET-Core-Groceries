namespace Groceries.Module.Controllers
{
    using System.Threading.Tasks;

    using Groceries.Infrastructure.Commands;
    using Groceries.Infrastructure.Entities;
    using Groceries.Infrastructure.Services;
    using Groceries.Module.Models;

    using Microsoft.AspNetCore.Mvc;

    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;

    public class ManageController : Controller
    {
        private readonly IAsyncCommandBus _bus;

        private readonly ICommandBuilder<GroceryViewModel, UpdateGroceryCommand> _updateGroceryCommandBuilder;

        private readonly ICommandBuilder<GroceryViewModel, DeleteGroceryCommand> _deleteGroceryCommandBuilder;

        private readonly IGroceryService _service;

        private readonly ICommandBuilder<Grocery, GroceryViewModel> _builder;

        public ManageController(
            IAsyncCommandBus bus,
            ICommandBuilder<GroceryViewModel, UpdateGroceryCommand> updateGroceryCommandBuilder,
            ICommandBuilder<GroceryViewModel, DeleteGroceryCommand> deleteGroceryCommandBuilder,
            IGroceryService service,
            ICommandBuilder<Grocery, GroceryViewModel> builder)
        {
            bus.Guard();
            updateGroceryCommandBuilder.Guard();
            deleteGroceryCommandBuilder.Guard();
            service.Guard();
            builder.Guard();

            _bus = bus;
            _updateGroceryCommandBuilder = updateGroceryCommandBuilder;
            _deleteGroceryCommandBuilder = deleteGroceryCommandBuilder;
            _service = service;
            _builder = builder;
        }

        public async Task<IActionResult> Index(int id)
        {
            var record = await _service.GetGroceryAsync(id);
            var model = _builder.Create(record);

            return View(model);
        }

        public async Task<IActionResult> Update(GroceryViewModel model)
        {
            await _bus.SendAsync(_updateGroceryCommandBuilder.Create(model));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(GroceryViewModel model)
        {
            await _bus.SendAsync(_deleteGroceryCommandBuilder.Create(model));

            return RedirectToAction("Index", "Home");
        }
    }
}