using System;
using System.Security.Cryptography;
using System.Text;

namespace GroceryOrderingManagementSystem
{
    internal static class PasswordSecurity
    {
        private const string HashPrefix = "SHA256$";

        //=====================================================
        // PASSWORD STORAGE
        //=====================================================
        public static string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = SHA256.HashData(passwordBytes);

            return HashPrefix + Convert.ToHexString(hashBytes);
        }

        //=====================================================
        // PASSWORD VERIFICATION
        //=====================================================
        public static bool VerifyPassword(
            string enteredPassword,
            string storedPassword)
        {
            if (storedPassword.StartsWith(
                    HashPrefix,
                    StringComparison.OrdinalIgnoreCase))
            {
                string expectedHash = storedPassword.Substring(HashPrefix.Length);

                try
                {
                    byte[] expectedHashBytes = Convert.FromHexString(expectedHash);
                    byte[] actualHashBytes = SHA256.HashData(
                        Encoding.UTF8.GetBytes(enteredPassword));

                    return expectedHashBytes.Length == actualHashBytes.Length
                        && CryptographicOperations.FixedTimeEquals(
                            actualHashBytes,
                            expectedHashBytes);
                }
                catch (FormatException)
                {
                    // A damaged hash is never accepted as a valid password.
                    return false;
                }
            }

            // Existing coursework databases may still contain plain-text
            // passwords. They remain usable until an administrator or member
            // saves a new password, which is stored as a hash.
            return string.Equals(
                enteredPassword,
                storedPassword,
                StringComparison.Ordinal);
        }
    }
}
