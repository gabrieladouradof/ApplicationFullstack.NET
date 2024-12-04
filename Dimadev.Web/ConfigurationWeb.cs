using MudBlazor;
using MudBlazor.Utilities;

namespace Dimadev.Web
{
    public static class Configuration
    {
        public const string HttpClientName = "dima";

        public static string BackendUrl { get; set; } = "http://localhost:5252";


        public static MudTheme theme = new()
        {
            Typography = new Typography
            {
                Default = new Default()
                {
                    FontFamily = ["Raleway", "sans-serif"]
                }
            },
            PaletteLight = new PaletteLight
            {
                Primary = "#1EFA2D",
                Secondary = Colors.Pink.Darken3,
                Background = Colors.Shades.White,
                AppbarBackground = new MudBlazor.Utilities.MudColor("#1EFA2D"),
                AppbarText = Colors.Shades.Black,
                TextPrimary = Colors.Shades.Black,
                PrimaryContrastText = Colors.Shades.Black,
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
