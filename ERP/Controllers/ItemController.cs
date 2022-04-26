using ERP.Context;
using ERP.Models;
using ERP.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.DTOs.Item;
using ERP.Services.ItemServices;
using Microsoft.AspNetCore.Authorization;

namespace ERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class ItemController : Controller
    {
        private readonly DataContext context;
        private readonly IItemService _itemService;

        public ItemController(DataContext context, IItemService itemService)
        {
            this.context = context;
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAll()
        {
            var items = await  _itemService.GetAll();

            return Ok(items);
        }

        [HttpGet("asset")]
        public async Task<ActionResult<List<EquipmentAsset>>> GetAssets(int modelId)
        {
            var items = await _itemService.GetAssets(modelId);

            return Ok(items);
        }

        [HttpPost("minimum-stock")]
        public async Task<ActionResult<List<MinimumStockItemDTO>>> GetMinimumStock(GetMinimumStockItemsDTO minimumStockDTO)
        {
            var items = await _itemService.GetMinimumStockItems(minimumStockDTO);

            return Ok(items);
        }

        [HttpPost("minimum-stock/update")]
        public async Task<ActionResult<bool>> UpdateMinimumStock(List<MinimumStockItemDTO> minimumStockItems)
        {
            var result = await _itemService.UpdateMinimumStockItems(minimumStockItems);

            return Ok(result);
        }

        [HttpGet("{ItemId}")]
        public async Task<ActionResult<Item>> GetOne(int ItemId)
        {
           
            try
            {
                var item = await _itemService.GetById(ItemId);
                return Ok(item);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("equipmentmodel/{ModelId}")]
        public async Task<ActionResult<EquipmentModel>> GetModel(int ModelId)
        {

            try
            {
                var model = await _itemService.GetModel(ModelId);
                return Ok(model);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("material")]
        public async Task<ActionResult<List<Item>>> GetMaterial()
        {
            var items = await _itemService.GetMaterials();

            return Ok(items);
        }

        [HttpGet("material/transferable")]
        public async Task<ActionResult<List<Item>>> GetTransferableMaterial()
        {
            var items = await _itemService.GetTransferableMaterials();

            return Ok(items);
        }

        [HttpGet("equipment")]
        public async Task<ActionResult<List<Item>>> GetEquipment()
        {
            var items = await _itemService.GetEquipments();

            return Ok(items);
        }

        [HttpGet("equipmentcategory")]
        public async Task<ActionResult<List<EquipmentCategory>>> GetCategory()
        {
            var categories = await _itemService.GetEquipmentCategories();

            return Ok(categories);
        }

        [HttpPost("material/add")]
        public async Task<ActionResult<List<Item>>> CreateMaterialItem(CreateMaterialItemDTO materialItemDTO)
        {
            Item item = new();
            item.Type = ITEMTYPE.MATERIAL;
            item.Name = materialItemDTO.Name;

            context.Items.Add(item);

            await context.SaveChangesAsync();

            Material material = new();
            material.ItemId = item.ItemId;
            material.Spec = materialItemDTO.Spec;
            material.Unit = materialItemDTO.Unit;
            material.Cost = materialItemDTO.Cost;
            material.IsTransferable = materialItemDTO.IsTransferable;

            context.Materials.Add(material);

            await context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPost("equipment/add")]
        public async Task<ActionResult<List<Item>>> CreateEquipmentItem(CreateEquipmentItemDTO equipmentItemDTO)
        {
            Item item = new();
            item.Type = ITEMTYPE.EQUIPMENT;
            item.Name = equipmentItemDTO.Name;

            context.Items.Add(item);

            await context.SaveChangesAsync();

            Equipment equipment = new();
            equipment.ItemId = item.ItemId;
            equipment.Unit = equipmentItemDTO.Unit;
            equipment.Description = equipmentItemDTO.Description;
            equipment.EquipmentCategoryId = equipmentItemDTO.EquipmentCategoryId;

            context.Equipments.Add(equipment);

            await context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPost("equipmentcategory/add")]
        public async Task<ActionResult<List<EquipmentCategory>>> CreateEquipmentCategory(CreateEquipmentCategoryDTO equipmentCategoryDTO)
        {
            EquipmentCategory equipmentCategory = new();
            equipmentCategory.Name = equipmentCategoryDTO.Name;

            context.EquipmentCategories.Add(equipmentCategory);

            await context.SaveChangesAsync();

            return Ok(equipmentCategory);
        }

        [HttpPost("equipmentmodel/add")]
        public async Task<ActionResult<List<EquipmentModel>>> CreateEquipmentModel(CreateEquipmentModelDTO equipmentModelDTO)
        {
            EquipmentModel equipmentModel = new();
            equipmentModel.ItemId = equipmentModelDTO.ItemId;
            equipmentModel.Name = equipmentModelDTO.Name;
            equipmentModel.Cost = equipmentModelDTO.Cost;

            context.EquipmentModels.Add(equipmentModel);

            await context.SaveChangesAsync();

            return Ok(equipmentModel);
        }
        
        [HttpPost("qty")]
        public async Task<ActionResult<int>> GetItemSiteQty(GetQtyDTO getQtyDTO)
        {
            var qty = await _itemService.GetItemQty(getQtyDTO);

            return Ok(qty);
        }
    }
}
