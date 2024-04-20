using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace MultiTenantReactApp.Service
{
    public class TenantAssetsService
    {
        private readonly IWebHostEnvironment _environment;

        public TenantAssetsService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<IDictionary<string, string>> GetAllTenantAssets(int tenantId)
        {
            var tenantFolder = GetTenantAssetPath(tenantId);

            if (!Directory.Exists(tenantFolder))
            {
                throw new DirectoryNotFoundException($"Tenant folder not found for ID {tenantId}");
            }

            var assetFiles = Directory.GetFiles(tenantFolder);
            var assetDictionary = new Dictionary<string, string>();

            foreach (var filePath in assetFiles)
            {
                var fileName = Path.GetFileName(filePath);
                var key = GetFirstPartOfFileName(fileName);
                assetDictionary[key] = filePath;
            }

            return assetDictionary;
        }
        private string GetFirstPartOfFileName(string fileName)
        {
            // Get the part of the file name before the first dash "-"
            var parts = fileName.Split('-', 2);
            return parts[0];
        }

        private string GetTenantAssetPath(int tenantId)
        {
            // Assuming assets are stored in a folder named with the tenant ID
            var tenantAssetFolder = Path.Combine(_environment.WebRootPath, "uploads", tenantId.ToString());
            return tenantAssetFolder;
        }
    }

}
