using App1.Models;
using App1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System;

namespace App1.Views
{
    public sealed partial class LoginPage : Page
    {
        private readonly HttpClient _httpClient;

        public LoginPage()
        {
            this.InitializeComponent();
            _httpClient = new HttpClient();
            LoadSavedLoginInfo();
        }

        private void LoadSavedLoginInfo()
        {
            var (rememberEmail, rememberPassword, email, password) = LocalStorageService.GetLoginInfo();
            if (rememberEmail)
            {
                EmailTextBox.Text = email;
                RememberEmailCheckBox.IsChecked = true;
            }
            if (rememberPassword)
            {
                PasswordBox.Password = password;
                RememberPasswordCheckBox.IsChecked = true;
                RememberEmailCheckBox.IsChecked = true;
            }
        }

        private void RememberPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            RememberEmailCheckBox.IsChecked = true;
        }

        private void RememberPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // 当取消勾选记住密码时，保持记住用户名的状态不变
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginButton.IsEnabled = false;
                ErrorMessage.Visibility = Visibility.Collapsed;

                var loginData = new
                {
                    email = EmailTextBox.Text,
                    password = PasswordBox.Password
                };

                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-Authenticator", "basic");

                var response = await _httpClient.PostAsync("http://localhost:13000/api/auth:signIn", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var signInResponse = JsonSerializer.Deserialize<SignInResponse>(responseContent);
                    LocalStorageService.SaveToken(signInResponse.Data.Token);
                    LocalStorageService.SaveLoginInfo(
                        RememberEmailCheckBox.IsChecked ?? false,
                        RememberPasswordCheckBox.IsChecked ?? false,
                        EmailTextBox.Text,
                        PasswordBox.Password
                    );
                    Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    ErrorMessage.Text = "登录失败，请检查邮箱和密码是否正确。";
                    ErrorMessage.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = $"发生错误：{ex.Message}";
                ErrorMessage.Visibility = Visibility.Visible;
            }
            finally
            {
                LoginButton.IsEnabled = true;
            }
        }
    }
} 