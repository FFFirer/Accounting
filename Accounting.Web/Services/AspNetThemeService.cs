using Accounting.Web;

namespace Accounting.Web;

public sealed class AspNetThemeService : IThemeService
{
    private readonly IHttpContextAccessor _accessor;

    public event Action OnChange = default!;
    public AspNetThemeService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
        if( accessor.HttpContext?.Request.Cookies.TryGetValue("theme", out var value) ?? false)
        {
            _currentTheme = value;
        }
    }

    private string _currentTheme = "light";
    public string CurrentTheme { get => _currentTheme; set { _currentTheme = value; } }

    public void ToggleTheme()
    {
        CurrentTheme = CurrentTheme == "dark" ? "light" : "dark";
    }
}