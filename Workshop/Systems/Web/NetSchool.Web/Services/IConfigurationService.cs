namespace NetSchool.Web.Services;

public interface IConfigurationService
{
    Task<bool> GetDarkModeAsync(CancellationToken cancellationToken = default);
    Task SetDarkModeAsync(bool value, CancellationToken cancellationToken = default);

    Task<bool> GetNavigationMenuVisibleAsync(CancellationToken cancellationToken = default);
    Task SetNavigationMenuVisibleAsync(bool value, CancellationToken cancellationToken = default);
}