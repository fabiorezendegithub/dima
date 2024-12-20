using MudBlazor;

namespace Dima.Web;

public static class Configuration
{
    public static string BackendUrl { get; set; } = "http://localhost:5226";
    public static string StripePublicKey { get; set; } = "";
    public const string HttpClientName = "dima";

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
            PrimaryContrastText = "#000000",
            Secondary = Colors.LightGreen.Darken3,
            Background = Colors.Gray.Lighten4,
            AppbarBackground = "#1EFA2D",
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            DrawerText = Colors.Shades.White,
            DrawerBackground = Colors.LightGreen.Darken4
        },
        PaletteDark = new()
        {
            Primary = Colors.LightGreen.Accent3,
            Secondary = Colors.LightGreen.Darken3,
            AppbarBackground = Colors.LightGreen.Accent3,
            AppbarText = Colors.Shades.Black,
            PrimaryContrastText = "#000000"
        }
    };
}
