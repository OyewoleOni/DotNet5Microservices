
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;

namespace Play.Catalog.Service.Controller
{
    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase
    {
        //private static readonly List<ItemDto> items = new()
        //{
        //    new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
        //    new ItemDto(Guid.NewGuid(), "Antidote", "Cures Potion", 7, DateTimeOffset.UtcNow),
        //    new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow),
        //};

        private readonly ItemRepository itemsRepository = new();

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await itemsRepository.GetAllAsync())
                        .Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await itemsRepository.GetItemAsync(id);
            if (item is null) return NotFound();
            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {

            //var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.price,
            //DateTimeOffset.UtcNow);
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            //items.Add(item);
            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            //var existingItem = items.Where(x => x.Id == id).SingleOrDefault();

            //if (existingItem is null) return NotFound();

            //var updatedItem = existingItem with
            //{
            //    Name = updateItemDto.Name,
            //    Description = updateItemDto.Description,
            //    price = updateItemDto.price,
            //    CreatedDate = DateTimeOffset.UtcNow
            //};

            //var index = items.FindIndex(existingItem => existingItem.Id == id);
            //items[index] = updatedItem;
            var existingItem = await itemsRepository.GetItemAsync(id);
            if (existingItem is null) return NotFound();
            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.price;

            await itemsRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var Item = await itemsRepository.GetItemAsync(id);

            if (Item is null) return NotFound();

            await itemsRepository.RemoveAsync(Item.Id);
            return NoContent();
        }
    }
}