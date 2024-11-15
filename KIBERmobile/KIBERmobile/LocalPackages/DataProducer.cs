using System.Globalization;
using System.Text.RegularExpressions;

namespace KIBERmobile.LocalPackages;

public static class DataProducer
{
    public static int SetInt(string? value)
    {
        int result;

        try
        {
            result = int.Parse(value!);
        }
        catch
        {
            result = 0;
        }

        return result;
    }
    
    public static string SetString(string? value)
    {
        string result;

        try
        {
            result = value ?? "Не указано";
        }
        catch
        {
            result = "Упс... Что-то пошло нет так";
        }

        return result;

    }
    
    public static ImageSource SetImageSource(string? imgUrl, string size)
    {
        ImageSource result;

        try
        {
            result = new UriImageSource()
            {
                Uri = new Uri($"{imgUrl}/{size}"),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(0, 0, 3, 0)
            };
        }
        catch
        {
            result = new UriImageSource()
            {
                Uri = new Uri($"https://kiber-one.fun/gate/v1/static/image/user/error/error"),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(0, 0, 3, 0)
            };
        }

        return result;
    }

    public static bool IsNullOrEmpty(params string?[] values)
    {
        foreach (var value in values)
            if (string.IsNullOrEmpty(value))
                return true;

        return false;
    }

    public static bool IsValidNumber(string? number)
    {
        if (number == null) return false;
        
        try
        {
            return Regex.IsMatch(number,
                @"^(\+?(\d{1,3})[\- ]?)(\(?\d{3}\)?[\- ]?)[\d\- ]{7,10}$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static bool IsValidEmail(string? email)
    {
        if (email == null) return false;
        
        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                var domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}