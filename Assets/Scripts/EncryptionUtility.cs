using System;
using System.Text;
using UnityEngine;

public class EncryptionUtility
{
    private static string key => SystemInfo.deviceUniqueIdentifier + Application.companyName;

    public static string Encrypt(string input)
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            result.Append((char)(input[i] ^ key[i % key.Length]));
        }

        return Convert.ToBase64String(Encoding.UTF8.GetBytes(result.ToString()));
    }

    public static string Decrypt(string input)
    {
        string data = Encoding.UTF8.GetString(Convert.FromBase64String(input));
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            result.Append((char)(data[i] ^ key[i % key.Length]));
        }

        return result.ToString();
    }
}