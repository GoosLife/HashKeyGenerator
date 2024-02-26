using System.Security.Cryptography;

namespace HashKeyGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] algorithms = new string[] { "HMACSHA1", "HMACMD5", "HMACSHA256", "HMACSHA384", "HMACSHA512" };

            Console.WriteLine("Choose a hashing algorithm: ");

            for (int i = 0; i < algorithms.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {algorithms[i]}");
            }

            int choice = int.Parse(Console.ReadLine());

            HMAC? algorithm = choice switch
            {
                1 => new HMACSHA1(),
                2 => new HMACMD5(),
                3 => new HMACSHA256(),
                4 => new HMACSHA384(),
                5 => new HMACSHA512(),
                _ => null
            };

            if (algorithm == null)
            {
                Console.WriteLine("You failed to choose an algorithm (loop not implemented).\nPress any key to exit...");
                return;
            }

            Console.WriteLine("Enter a string to hash: ");
            string input = Console.ReadLine();

            byte[] hash = Hasher.GenerateHash(input, algorithm);
            Console.WriteLine("HEX:\t" + Hasher.WriteHashHex(hash));
            Console.WriteLine("ASCII:\t" + Hasher.WriteHashASCII(hash));

            // Test the hash
            Console.WriteLine("IS VALID: " + Hasher.VerifyHash(input, hash, algorithm));

            // Test for a random collision, just to demonstrate that the hash is unique
            byte[] randomHash = Hasher.GenerateHash(Guid.NewGuid().ToString(), algorithm);
            Console.WriteLine("IS UNIQUE: " + !Hasher.VerifyHash(input, randomHash, algorithm));

            Console.WriteLine("Press any key to exit...");
        }
    }
}
