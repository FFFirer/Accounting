namespace Accounting.Web;

public interface IThemeService
{
    string CurrentTheme { get; set; }

    void ToggleTheme();
}