using System;
using System.Text;
using System.Text.Json;

public static class JwtHelper
{
    public static string? GetSubFromUserMetadata(string jwt)
    {
        try
        {
            var parts = jwt.Split('.');
            if (parts.Length != 3)
                throw new ArgumentException("JWT inválido");

            // JWT: header.payload.signature → pegamos apenas o payload
            var payload = parts[1];

            // Adiciona padding se necessário
            switch (payload.Length % 4)
            {
                case 2: payload += "=="; break;
                case 3: payload += "="; break;
            }

            var jsonBytes = Convert.FromBase64String(payload.Replace('-', '+').Replace('_', '/'));
            var jsonString = Encoding.UTF8.GetString(jsonBytes);

            // Parse JSON e lê user_metadata.sub
            using var doc = JsonDocument.Parse(jsonString);
            if (doc.RootElement.TryGetProperty("user_metadata", out var userMetadata))
            {
                if (userMetadata.TryGetProperty("sub", out var sub))
                {
                    return sub.GetString();
                }
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}
