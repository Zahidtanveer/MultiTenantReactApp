namespace MultiTenantReactApp.Models
{

    public record Tenant(int Id, string Host, bool IsActive, string ThemeName);
   
}
