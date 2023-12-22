using Blazored.LocalStorage;

namespace NetSchool.Web.Services;

public class ConfigurationService(ILocalStorageService localStorage) : IConfigurationService
{
    private const string DarkModeKey = "darkMode";
    private const string NavigationMenuVisibleKey = "navigationMenuVisible";

    public async Task<bool> GetDarkModeAsync(CancellationToken cancellationToken = default)
    {
        return await localStorage.GetItemAsync<bool>(DarkModeKey, cancellationToken);
    }

    public async Task SetDarkModeAsync(bool value, CancellationToken cancellationToken = default)
    {
        await localStorage.SetItemAsync(DarkModeKey, value, cancellationToken);
    }

    public async Task<bool> GetNavigationMenuVisibleAsync(CancellationToken cancellationToken = default)
    { 
        return await localStorage.GetItemAsync<bool>(NavigationMenuVisibleKey, cancellationToken);
    }

    public async Task SetNavigationMenuVisibleAsync(bool value, CancellationToken cancellationToken = default)
    {
        await localStorage.SetItemAsync(NavigationMenuVisibleKey, value, cancellationToken);
    }
}