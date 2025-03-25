using Windows.Storage;
using System.Text.Json;

namespace App1.Services
{
    public class LocalStorageService
    {
        private static readonly string TokenKey = "AuthToken";
        private static readonly string RememberEmailKey = "RememberEmail";
        private static readonly string RememberPasswordKey = "RememberPassword";
        private static readonly string EmailKey = "Email";
        private static readonly string PasswordKey = "Password";
        private static readonly ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;

        public static void SaveToken(string token)
        {
            LocalSettings.Values[TokenKey] = token;
        }

        public static string GetToken()
        {
            return LocalSettings.Values[TokenKey]?.ToString();
        }

        public static void RemoveToken()
        {
            LocalSettings.Values.Remove(TokenKey);
        }

        public static void SaveLoginInfo(bool rememberEmail, bool rememberPassword, string email, string password)
        {
            LocalSettings.Values[RememberEmailKey] = rememberEmail;
            LocalSettings.Values[RememberPasswordKey] = rememberPassword;

            if (rememberEmail)
            {
                LocalSettings.Values[EmailKey] = email;
            }
            else
            {
                LocalSettings.Values.Remove(EmailKey);
            }

            if (rememberPassword)
            {
                LocalSettings.Values[PasswordKey] = password;
            }
            else
            {
                LocalSettings.Values.Remove(PasswordKey);
            }
        }

        public static (bool rememberEmail, bool rememberPassword, string email, string password) GetLoginInfo()
        {
            var rememberEmail = LocalSettings.Values[RememberEmailKey] as bool? ?? false;
            var rememberPassword = LocalSettings.Values[RememberPasswordKey] as bool? ?? false;
            var email = LocalSettings.Values[EmailKey]?.ToString() ?? string.Empty;
            var password = LocalSettings.Values[PasswordKey]?.ToString() ?? string.Empty;
            return (rememberEmail, rememberPassword, email, password);
        }
    }
} 