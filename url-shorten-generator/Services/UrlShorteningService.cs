using Microsoft.EntityFrameworkCore;

namespace url_shorten_generator.Services;

public class UrlShorteningService
{
    public const int NumberOfCharacters = 7;
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

    private readonly Random _random = new();
    private ApplicationDbContext _context;

    public UrlShorteningService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateUniqueUrl()
    {
        var codeChars = new char[NumberOfCharacters];

        while (true)
        {
            for (int i = 0; i < NumberOfCharacters; i++)
            {
                var randomIndex = _random.Next(Alphabet.Length - 1);
                codeChars[i] = Alphabet[randomIndex];
            }

            var code = new string(codeChars);

            if (!await _context.ShortenedUrls.AnyAsync(u => u.Code == code))
            {
                return code;
            }
        }
        
    }
}