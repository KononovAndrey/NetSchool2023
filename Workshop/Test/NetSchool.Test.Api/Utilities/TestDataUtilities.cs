namespace NetSchool.Test.Api;

using System;
using System.Linq;

public class TestDataUtilities
{
    private readonly Random random = new();

    public string GetRandomString(int length, bool enabledLowercase = true, bool enabledUppercase = true, bool enabledDigits = true)
    {
        var chars = string.Empty;
        chars += enabledLowercase ? "abcdefghijklmnopqrstuvwxyz" : "";
        chars += enabledUppercase ? "ABCDEFJHIJKLMNOPQRSTUVWXYZ" : "";
        chars += enabledDigits ? "0123456789" : "";

        if (chars == string.Empty)
            throw new Exception("Conditions for string generate are wrong. At least one of them must be true.");

        return new string(
            Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
    }

    public int GetRandomInt(int max)
    {
        return random.Next(max + 1);
    }
}