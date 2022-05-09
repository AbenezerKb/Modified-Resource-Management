﻿using ERP.Context;
using ERP.DTOs;
using ERP.DTOs.Item;
using ERP.Models;
using ERP.Services.ItemSiteQtyServices;
using ERP.Services.User;
using Microsoft.EntityFrameworkCore;

namespace ERP.Services.ItemServices
{
    public class ItemService : IItemService
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly IItemSiteQtyService _itemSiteQtyService;

        public ItemService(DataContext context, IUserService userService, IItemSiteQtyService itemSiteQtyService)
        {
            _context = context;
            _userService = userService;
            _itemSiteQtyService = itemSiteQtyService;
        }

        public async Task<List<Item>> GetAll()
        {
            var items = await _context.Items
                .Include(i => i.Material)
                .Include(i => i.Equipment)
                .ToListAsync();

            return items;
        }


        public async Task<int> ImportAssetNumbers(ImportAssetNoDTO importDTO)
        {
            if(importDTO.SiteId == -1 || importDTO.EquipmentModelId == -1)
                return 0;

            int count = 0;

            foreach (var importAsset in importDTO.Assets)
            {
                var asset = new EquipmentAsset();

                if(importAsset.AssetNo != null && importAsset.AssetNo.Trim() != String.Empty)
                {
                    asset.AssetNo = importAsset.AssetNo.Trim();
                    count++;

                    if(importAsset.SerialNo != null && importAsset.SerialNo.Trim() != String.Empty)
                        asset.SerialNo = importAsset.SerialNo.Trim();

                    asset.EquipmentModelId = importDTO.EquipmentModelId;
                    asset.CurrentSiteId = importDTO.SiteId;
                    _context.EquipmentAssets.Add(asset);
                }
            }

            await _context.SaveChangesAsync();
            await _itemSiteQtyService.AddEquipmentModel(importDTO.EquipmentModelId, importDTO.SiteId, count);

            return count;
        }

        public async Task<bool> UpdateMinimumStockItems(List<MinimumStockItemDTO> requestDTO)
        {

            int siteId = (int)_userService.Employee.EmployeeSiteId;

            foreach (var item in requestDTO)
            {
                if(item.ItemType == ITEMTYPE.MATERIAL)
                {
                    var materialSiteQty = await _context.MaterialSiteQties
                        .Where(msq => msq.ItemId == item.ItemId && msq.SiteId == item.SiteId)
                        .FirstOrDefaultAsync();
                    
                    if (materialSiteQty == null)
                        throw new KeyNotFoundException($"Material with Item Id {item.ItemId} and Site Id {item.SiteId} Not Found.");

                    materialSiteQty.MinimumQty = item.Qty;
                }
                else if (item.ItemType == ITEMTYPE.EQUIPMENT)
                {
                    var equipmentSiteQty = await _context.EquipmentSiteQties
                        .Where(msq => msq.EquipmentModelId == item.EquipmentModelId && msq.SiteId == item.SiteId)
                        .FirstOrDefaultAsync();

                    if (equipmentSiteQty == null)
                        throw new KeyNotFoundException($"Equipment with Model Id {item.EquipmentModelId} and Site Id {item.SiteId} Not Found.");

                    equipmentSiteQty.MinimumQty = item.Qty;
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<MinimumStockItemDTO>> GetMinimumStockItems(GetMinimumStockItemsDTO requestDTO)
        {
            List<MinimumStockItemDTO> minimumStockItems = new();
            int siteId = (int)_userService.Employee.EmployeeSiteId;

            if(requestDTO.ItemType != ITEMTYPE.EQUIPMENT)
            {
                var items = await _context.MaterialSiteQties
                    .Where(msq => msq.SiteId == siteId &&
                        (requestDTO.ItemId == -1 || msq.ItemId == requestDTO.ItemId))
                    .Include(msq => msq.Item)
                    .ToListAsync();

                foreach (var item in items)
                {
                    minimumStockItems.Add(new MinimumStockItemDTO
                    {
                        Name = item.Item.Name,
                        ItemType = ITEMTYPE.MATERIAL,
                        ItemId = item.ItemId,
                        SiteId = item.SiteId,
                        Qty = item.MinimumQty

                    });
                }
            }

            if (requestDTO.ItemType != ITEMTYPE.MATERIAL)
            {
                var items = await _context.EquipmentSiteQties
                    .Where(msq => msq.SiteId == siteId &&
                        (requestDTO.ItemId == -1 || msq.EquipmentModel.ItemId == requestDTO.ItemId) &&
                        (requestDTO.EquipmentCategoryId == -1 || msq.EquipmentModel.Equipment.EquipmentCategoryId == requestDTO.EquipmentCategoryId))
                    .Include(msq => msq.EquipmentModel.Equipment.Item)
                    .ToListAsync();

                foreach (var item in items)
                {
                    minimumStockItems.Add(new MinimumStockItemDTO
                    {
                        Name = $"{item.EquipmentModel.Equipment.Item.Name}, {item.EquipmentModel.Name}",
                        ItemType = ITEMTYPE.EQUIPMENT,
                        EquipmentModelId = item.EquipmentModelId,
                        SiteId = item.SiteId,
                        Qty = item.MinimumQty

                    });
                }
            }

            return minimumStockItems;

        }

        public async Task<List<EquipmentCategory>> GetEquipmentCategories()
        {
            var categories = await _context.EquipmentCategories.ToListAsync();

            return categories;
        }
        
        public async Task<List<Item>> GetMaterials()
        {
            var items = await _context.Items.Where(item => item.Type == ITEMTYPE.MATERIAL)
                .Include(i => i.Material).ToListAsync();

            return items;
        }

        public async Task<List<Item>> GetTransferableMaterials()
        {
            var items = await _context.Items.Where(item => item.Type == ITEMTYPE.MATERIAL && item.Material.IsTransferable == true)
                .Include(i => i.Material).ToListAsync();

            return items;
        }

        public async Task<List<Item>> GetEquipments()
        {
            var items = await _context.Items.Where(item => item.Type == ITEMTYPE.EQUIPMENT)
                .Include(i => i.Equipment).ToListAsync();

            return items;
        }

        public async Task<Item> GetById(int id)
        {
            var item = await _context.Items
               .Where(item => item.ItemId == id)
               .Include(i => i.Material)
               .Include(i => i.Equipment)
               .ThenInclude(e => e.EquipmentModels)
               .FirstOrDefaultAsync();

            if (item == null) throw new KeyNotFoundException("Item Not Found");

            return item;
        }

        public async Task<EquipmentModel> GetModel(int modelId)
        {
            var model = await _context.EquipmentModels
                .Where(model => model.EquipmentModelId == modelId)
                .Include(model => model.EquipmentAssets)
                .FirstOrDefaultAsync();

            if (model == null) throw new KeyNotFoundException("Equipment Model Not Found");

            return model;
        }

        public async Task<List<EquipmentAsset>> GetAssets(int modelId)
        {
            var siteId = _userService.Employee.EmployeeSiteId;

            var assets = await _context.EquipmentAssets
                .Where(ea => ea.CurrentSiteId == siteId && ea.EquipmentModelId == modelId)
                .ToListAsync();

            return assets;

        }

        public async Task<List<EquipmentAsset>> GetCleanAssets(int modelId)
        {
            var siteId = _userService.Employee.EmployeeSiteId;

            var assets = await _context.EquipmentAssets
                .Where(ea => ea.CurrentSiteId == siteId && ea.EquipmentModelId == modelId && ea.AssetDamageId == null)
                .ToListAsync();

            return assets;

        }

        public async Task<int> GetItemQty(GetQtyDTO qtyDTO)
        {

            int qty = 0;

            var siteId = qtyDTO.SiteId != -1 ? qtyDTO.SiteId : _userService.Employee.EmployeeSiteId;

            if (siteId == null) return qty;

            if (qtyDTO.EquipmentModelId != -1)
            {
                var temp  = await _context.EquipmentSiteQties
                       .Where(esq => esq.EquipmentModelId == qtyDTO.EquipmentModelId && esq.SiteId == siteId)
                       .FirstOrDefaultAsync();
                
                if (temp != null) qty = temp.Qty;

            }


            if (qtyDTO.ItemId != -1)
            {
                var temp = await _context.MaterialSiteQties
                       .Where(esq => esq.ItemId == qtyDTO.ItemId && esq.SiteId == siteId)
                       .FirstOrDefaultAsync();

                if (temp != null) qty = temp.Qty;

            }

            return qty;
        }
    }
}
