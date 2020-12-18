using Xamarin.Forms;
using DevExpress.XamarinForms.Core.Themes;

namespace Scheduler_DarkTheme {
    public partial class MainPage : ContentPage {
        public MainPage() {
            DevExpress.XamarinForms.Scheduler.Initializer.Init();
            ThemeManager.ThemeName = Theme.Dark;
            InitializeComponent();
        }
    }
}
