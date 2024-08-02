using MudBlazor;

namespace Dima.Web;

public static class Configuration
{
    public static MudTheme Theme = new()
    {
        Typography = new()
        {
            Default = new()
            {
                FontFamily = ["Raleway", "sans-serif"]
            }
        },
        PaletteLight = new()
        {
            Primary = "#1EFA2D",
            Secondary = Colors.LightGreen.Darken3,
            Background = Colors.Gray.Lighten4,
            AppbarBackground = "#1EFA2D",
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            PrimaryContrastText = Colors.Shades.Black,
            DrawerText = Colors.Shades.Black,
            DrawerBackground = Colors.LightGreen.Lighten4
        },
        PaletteDark = new()
        {
            Primary = Colors.LightGreen.Accent3,
            Secondary = Colors.LightGreen.Darken3,
            AppbarBackground = Colors.LightGreen.Accent3,
            AppbarText = Colors.Shades.Black
        }
    };
}
