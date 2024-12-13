using System;

class Program
{
    static void Main()
    {
        // Example byte array representing the CSR
        byte[] csrBytes = new byte[] {
            0x30, 0x82, 0x01, 0x0A, 0x02, 0x82, 0x01, 0x01, 0x00, 0xA3, 0x5D, 0xF4, // Example data...
            // Add the rest of your CSR bytes here
        };

        // Convert the byte array to a Base64 string
        string base64Csr = Convert.ToBase64String(csrBytes);

        Console.WriteLine("Base64 Encoded CSR:");
        Console.WriteLine(base64Csr);
    }
}
