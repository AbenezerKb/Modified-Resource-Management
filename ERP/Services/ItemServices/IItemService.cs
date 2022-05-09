﻿using ERP.DTOs;
using ERP.DTOs.Item;
using ERP.Models;

namespace ERP.Services.ItemServices
{
    public interface IItemService
    {
        Task<List<Item>> GetAll();
        
        Task<Item> GetById(int id);
        
        Task<List<Item>> GetEquipments();
        Task<int> GetItemQty(GetQtyDTO qtyDTO);
        
        Task<List<Item>> GetMaterials();
        
        Task<List<EquipmentCategory>> GetEquipmentCategories();

        Task<EquipmentModel> GetModel(int modelId);
        Task<List<Item>> GetTransferableMaterials();
        Task<List<EquipmentAsset>> GetAssets(int modelId);
        Task<List<MinimumStockItemDTO>> GetMinimumStockItems(GetMinimumStockItemsDTO requestDTO);
        Task<bool> UpdateMinimumStockItems(List<MinimumStockItemDTO> requestDTO);
        Task<int> ImportAssetNumbers(ImportAssetNoDTO importDTO);
        Task<List<EquipmentAsset>> GetCleanAssets(int modelId);
    }
}