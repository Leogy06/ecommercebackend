using EcommerceBackend.Models;
using EcommerceBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly MenuItemService _menuItemService;

        public MenuItemController(MenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuItem(MenuItemModel[] menuItems)
        {
            var createdMenuItems = await _menuItemService.CreateAsync(menuItems);
            return Ok(createdMenuItems);
        }
    }
}
