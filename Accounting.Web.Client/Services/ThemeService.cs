using Microsoft.JSInterop;

namespace Accounting.Web.Client;

internal sealed class ThemeService : IThemeService
{
    public event Action OnChange = default!;
    private string _currentTheme = "light";

    public string CurrentTheme
    {
        get => _currentTheme;
        set
        {
            _currentTheme = value;
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();

    public void ToggleTheme()
    {
        CurrentTheme = CurrentTheme == "dark" ? "light" : "dark";
    }
}