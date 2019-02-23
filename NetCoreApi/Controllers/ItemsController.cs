using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using NetCoreApi.Models;
using System;
using System.Threading.Tasks;

namespace NetCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemContext _itemContext;

        public ItemsController(ItemContext itemContext)
        {
            _itemContext = itemContext;

            if (_itemContext.Items.CountAsync().Result == 0)
            {
                _itemContext.Items.Add(new ItemModel { Id = 0, Name = "Fatih" });
                itemContext.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemModel>>> GetItems()
        {
            return await _itemContext.Items.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemModel>> GetItem(long id)
        {
            var item = await _itemContext.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ItemModel>> CreateItem(ItemModel model)
        {
            _itemContext.Items.Add(model);
            await _itemContext.SaveChangesAsync();

            return CreatedAtAction(nameof(ItemModel), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(long id, ItemModel model)
        {
            if(id != model.Id)
            {
                return BadRequest();
            }

            _itemContext.Entry(model).State = EntityState.Modified;
            await _itemContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            var item =  await _itemContext.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _itemContext.Remove(item);
            await _itemContext.SaveChangesAsync();

            return NoContent();
        }

    }   
}