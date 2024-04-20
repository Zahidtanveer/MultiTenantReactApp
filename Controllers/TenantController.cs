using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantReactApp.Service;

namespace MultiTenantReactApp.Controllers
{
    [Route("api/tenants")]
    [ApiController]
    public class TenantController : ControllerBase
    {

        private readonly TenantService _tenantService;

        public TenantController(TenantService tenantService)
        {
            _tenantService = tenantService;
        }

        // Endpoint to get data for a specific tenant by ID
        [HttpGet("{id}")]
        public IActionResult GetTenantById(int id)
        {
            var tenant = _tenantService.GetTenantById(id);
            if (tenant == null)
            {
                return NotFound();
            }
            return Ok(tenant);
        }

        // Endpoint to upload favicon for a specific tenant
        [HttpPost("{tenantId}/favicon")]
        public IActionResult UploadFavicon(int tenantId, IFormFile file)
        {
            var tenant = _tenantService.GetTenantById(tenantId);
            if (tenant == null)
            {
                return NotFound();
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var uploadsFolder = GetTenantFolderPath(tenantId);
            var uniqueFileName = $"favicon-{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Delete the existing file if it exists
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok($"Favicon uploaded for Tenant ID {tenantId}: {uniqueFileName}");
        }

        // Endpoint to upload home banner image for a specific tenant
        [HttpPost("{tenantId}/homebanner")]
        public IActionResult UploadHomeBanner(int tenantId, IFormFile file)
        {
            var tenant = _tenantService.GetTenantById(tenantId);
            if (tenant == null)
            {
                return NotFound();
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var uploadsFolder = GetTenantFolderPath(tenantId);
            var uniqueFileName = $"banner-{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Delete the existing file if it exists
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok($"Home banner uploaded for Tenant ID {tenantId}: {uniqueFileName}");
        }
        private string GetTenantFolderPath(int tenantId)
        {
            string tenantFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", tenantId.ToString());
            if (!Directory.Exists(tenantFolder))
            {
                Directory.CreateDirectory(tenantFolder); // Create folder if it doesn't exist
            }
            return tenantFolder;
        }
    }
}
