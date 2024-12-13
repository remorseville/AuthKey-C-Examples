using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

class Program
{
    // DigiCert AuthKey
    private const string AuthKey = "4D0404C6_BRING_YOUR_OWN_KEY_3391ED9C8740C855391FBA";

    // CSR is formatted as a single string. No ---Begin Certificate Request-- or ---End Certificate Request--
    private const string Csr = "MIIElzCCAn8CAQAwJjELMAkGA1UEBhMCVVMxFzAVBgNVBAMMDmludmVydq3+JzeqUpO4krTlJNQDc4+EanNTgAP/RvAlIpxsHMFZHxExR64twCxlSLtCl3n6TIjpCERgCMYjv5LykPByplgfLQYT9txeIYMw7PilyM9wn1TDaxxfE----omitted----UjkFpdMHryNRXvyTvjYFp5oB+y5zUawxeqpaU1Kr1H+lP4fsFRbTE8iPf7AGwGaOsDK4+ru1HC8dgwid3k3qrkkrTxHJhHh3YTmK93me56yNgnLX7H+8V7eXGHEfP/cYVOL2Ju5TmopyF2szLiVxXeKZksZL4fchFnGFSRZb5xFQyVNTwbU2V";

    // Format YYYY/MM/DD/hh/mm/ss
    private const string Timestamp = "20210812000000";

    static void Main()
    {
        RequestToken(AuthKey, Csr, Timestamp);
    }

    private static string IntToBase36(BigInteger num)
    {
        const string digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var result = string.Empty;

        do
        {
            var remainder = (int)(num % 36);
            result = digits[remainder] + result;
            num /= 36;
        } while (num > 0);

        return result;
    }

    private static string EnsureLength(string base36Hash)
    {
        while (base36Hash.Length < 50)
        {
            base36Hash = "0" + base36Hash;
        }
        return base36Hash;
    }

    private static string Hashing(string authKey, string csr, string timestamp)
    {
        var secret = timestamp + csr;
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(authKey));
        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(secret));
        var hexHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        Console.WriteLine("SHA256 Hex: " + hexHash );

        // Convert hex hash to BigInteger
        var decimalHash = BigInteger.Parse("0" + hexHash, System.Globalization.NumberStyles.HexNumber);
        Console.WriteLine("Base10 Decimal Hash: " + decimalHash );

        var base36Hash = IntToBase36(decimalHash);
        Console.WriteLine("Base36 Hash: " + base36Hash );

        return EnsureLength(base36Hash);
    }

    private static void RequestToken(string authKey, string csr, string timestamp)
    {
        var validHash = Hashing(authKey, csr, timestamp);
        var myToken = timestamp + validHash;
        Console.WriteLine("Request Token: " + myToken.ToLower());
    }
}
