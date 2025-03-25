using System.Text.Json.Serialization;
using System.Collections.Generic;
using System;

namespace App1.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("appLang")]
        public string AppLang { get; set; }

        [JsonPropertyName("systemSettings")]
        public Dictionary<string, object> SystemSettings { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }

    public class SignInResponse
    {
        [JsonPropertyName("data")]
        public SignInData Data { get; set; }
    }

    public class SignInData
    {
        [JsonPropertyName("user")]
        public User User { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
} 