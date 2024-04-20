using MultiTenantReactApp.Models;

namespace MultiTenantReactApp.Service
{
    public class TenantService
    {
        public List<Tenant> GetTenants()
        {
            return new List<Tenant>
        {
            new Tenant(1, "foo", true, "default"),
            new Tenant(2, "bar", true, "dark"),
            // Add more tenants as needed
        };
        }

        public Tenant? GetTenantById(int id)
        {
            return GetTenants()?.FirstOrDefault(tenant => tenant.Id == id);
        }
    }

}
