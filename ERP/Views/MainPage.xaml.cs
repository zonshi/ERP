using App1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace App1.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            // 默认显示首页
            ContentFrame.Navigate(typeof(HomePage));
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = args.SelectedItem as NavigationViewItem;
            if (selectedItem == null) return;

            string tag = selectedItem.Tag?.ToString();
            if (string.IsNullOrEmpty(tag)) return;

            switch (tag)
            {
                case "Home":
                    ContentFrame.Navigate(typeof(HomePage));
                    break;
                case "UserManagement":
                    ContentFrame.Navigate(typeof(UserManagementPage));
                    break;
                case "Settings":
                    ContentFrame.Navigate(typeof(SettingsPage));
                    break;
                case "Logout":
                    LocalStorageService.RemoveToken();
                    LocalStorageService.SaveLoginInfo(false, false, string.Empty, string.Empty);
                    Frame.Navigate(typeof(LoginPage));
                    break;
            }
        }
    }
} 