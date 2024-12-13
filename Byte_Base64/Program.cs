using System;

class Program
{
    static void Main()
    {
        // Example Base64-encoded string
        string base64Csr = "MIIBijCCASQCAQAwgYsxCzAJBgNVBAYTAlVTMQswCQYDVQQIDAJDQTESMBAGA1UEBwwJUGFsbyBBbHRvMRMwEQYDVQQKDApFeGFtcGxlIEluYzERMA8GA1UECwwIQ2VydCBEZXMxFTATBgNVBAMMDEV4YW1wbGUgQ1NSIHExHzAdBgkqhkiG9w0BCQEWEGV4YW1wbGVAZXhhbXBsZS5jb20=";

        // Convert the Base64 string back to a byte array
        byte[] csrBytes = Convert.FromBase64String(base64Csr);

        Console.WriteLine("Decoded byte array:");
        foreach (var b in csrBytes)
        {
            Console.Write($"{b:X2} "); // Prints each byte in hexadecimal
        }
    }
}