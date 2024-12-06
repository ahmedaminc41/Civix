using System.Security.Cryptography;
using System.Text;

namespace Civix.App.Api.Helpers
{
    public static class OtpSettings
    {
        public static string GenerateOtp()
        {
            var random = new Random();
            
            return random.Next(100_000, 999_999).ToString();
        }
        public static string HashOtp(string otp)
        {
            if (otp == null) return null;
            using var sha265 = SHA256.Create();
            var hashBytes = sha265.ComputeHash(Encoding.UTF8.GetBytes(otp));

            return Convert.ToBase64String(hashBytes);
        }
        public static bool VerifyOtp(string hashedOtp, string intputOtp)
        {
            var hashedInputOtp = HashOtp(intputOtp);

            return hashedInputOtp == hashedOtp;
        }

    }
}
