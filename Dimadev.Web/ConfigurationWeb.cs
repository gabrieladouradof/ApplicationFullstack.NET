using MudBlazor;
using MudBlazor.Utilities;

namespace Dimadev.Web
{
    public static class Configuration
    {
        public const string HttpClientName = "dima";

        public static string BackendUrl { get; set; } = "http://localhost:5252";

        public static MudTheme Theme { get; } = new MudTheme
        {
            Typography = new Typography
            {
                Default = new Default
                {
                    FontFamily = new[] { "Raleway", "sans-serif" }
                }
            },
            Palette = new PaletteLight
            {
                Primary = "#1EFA2D",
                PrimaryContrastText = new MudColor("#000000"),
                Secondary = Colors.Pink.Darken3,
                Background = Colors.Grey.Lighten4,
                AppbarBackground = new MudColor("#1EFA2D"),
                AppbarText = Colors.Shades.Black,
                TextPrimary = Colors.Shades.Black,
                DrawerText = Colors.Shades.White,
                DrawerBackground = Colors.Green.Darken4
            },
            PaletteDark = new PaletteDark
            {
                Primary = Colors.LightGreen.Accent3,
                Secondary = Colors.LightGreen.Darken3,
                AppbarBackground = Colors.LightGreen.Accent3,
                AppbarText = Colors.Shades.Black,
                PrimaryContrastText = new MudColor("#000000")
            }
        };
    }
}
