using Microsoft.AspNetCore.Mvc;
using Employe.Data; 
using Employe.Models; 
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Employe.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Employe.Controllers
{
    public class AssetsController : Controller
    {
        private readonly EmployeContext _context;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(EmployeContext context, ILogger<AssetsController> logger) // Tambahkan logger di sini
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult AssetData()
        {
            List<MST_Asset> assetList = _context.MST_Asset.ToList();
            _logger.LogInformation($"Jumlah data yang diambil: {assetList.Count}");

            if (assetList.Count == 0)
            {
                _logger.LogWarning("Tidak ada data ditemukan dalam tabel MST_Asset.");
            }

            foreach (var asset in assetList)
            {
                _logger.LogInformation($"Data: {asset.Id}, {asset.AssetNumber}");
            }
            return View(assetList);
        }
        [HttpPost]
        public IActionResult AddAsset(MST_Asset newAsset)
        {
            if (ModelState.IsValid)
            {
                newAsset.NewId = Guid.NewGuid().ToString().ToUpper();

                var name = HttpContext.Session.GetString("Name");
                newAsset.CreatedBy = name;
                newAsset.CreatedDate = DateTime.Now;

                _context.MST_Asset.Add(newAsset);
                _context.SaveChanges();
                _logger.LogInformation("Data asset baru berhasil ditambahkan.");

                return Json(new { success = true, message = "Asset has been added successfully." });
            }
            
            return Json(new { success = false, message = "Error while adding asset." });
        }

        [HttpPost]
        public IActionResult EditAsset(long id, MST_Asset updatedAsset)
        {
            if (ModelState.IsValid)
            {
                var asset = _context.MST_Asset.Find(id);
                if (asset != null)
                {
                    asset.AssetNumber = updatedAsset.AssetNumber;
                    asset.Area = updatedAsset.Area;
                    asset.OwningDepartment = updatedAsset.OwningDepartment;
                    asset.IsActive = updatedAsset.IsActive;
                    asset.UpdatedBy = HttpContext.Session.GetString("Name");
                    asset.UpdatedDate = DateTime.Now;

                    _context.SaveChanges();
                    _logger.LogInformation("Data asset berhasil diperbarui.");

                    return Json(new { success = true, message = "Asset has been updated successfully." });
                }
                return Json(new { success = false, message = "Asset not found." });
            }

            return Json(new { success = false, message = "Error while updating asset." });
        }

        [HttpPost]
        public IActionResult DeleteAsset(long id)
        {
            var asset = _context.MST_Asset.FirstOrDefault(a => a.Id == id);
            if (asset != null)
            {
                _context.MST_Asset.Remove(asset);
                _context.SaveChanges();
                _logger.LogInformation("Data asset berhasil dihapus.");
                return Json(new { success = true, message = "Data asset berhasil dihapus." });
            }
            else
            {
                return Json(new { success = false, message = "Data asset tidak ditemukan." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            var assets = await _context.MST_Asset.ToListAsync();
            return Json(assets);
        }
    }
}