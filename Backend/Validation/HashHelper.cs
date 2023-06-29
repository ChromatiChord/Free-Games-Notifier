using System.Text;
using System.Security.Cryptography;

static class HashHelper{
    public static string HashString(string originalString) {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(originalString));

            // Convert byte array to a string
            var hashed = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return hashed;
        }
    }
}