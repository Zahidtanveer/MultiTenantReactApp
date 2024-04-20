using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantReactApp.Service;
using System.Threading.Tasks;

[ApiController]
[Route("api/tenants/{tenantId}/assets")]
public class TenantAssetsController : ControllerBase
{
    private readonly TenantAssetsService _assetService;

    public TenantAssetsController(TenantAssetsService assetService)
    {
        _assetService = assetService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTenantAssets(int tenantId)
    {
        try
        {


            var assets =await _assetService.GetAllTenantAssets(tenantId);

            return Ok(assets);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error getting tenant assets: {ex.Message}");
        }
    }
}
