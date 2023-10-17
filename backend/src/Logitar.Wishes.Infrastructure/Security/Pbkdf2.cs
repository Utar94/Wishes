using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Logitar.Wishes.Infrastructure.Security;

internal record Pbkdf2
{
  private const string Prefix = "PBKDF2";
  private const char Separator = ':';

  private readonly KeyDerivationPrf _algorithm;
  private readonly int _iterations;
  private readonly byte[] _salt;
  private readonly byte[] _hash;

  public Pbkdf2(string password, KeyDerivationPrf algorithm = KeyDerivationPrf.HMACSHA256, int iterations = 600000, int saltLength = 32, int? hashLength = null)
  {
    _algorithm = algorithm;
    _iterations = iterations;
    _salt = RandomNumberGenerator.GetBytes(saltLength);
    _hash = ComputeHash(password, hashLength ?? saltLength);
  }
  private Pbkdf2(KeyDerivationPrf algorithm, int iterations, byte[] salt, byte[] hash)
  {
    _algorithm = algorithm;
    _iterations = iterations;
    _salt = salt;
    _hash = hash;
  }

  public static Pbkdf2 Decode(string s)
  {
    var values = s.Split(Separator);
    if (values.First() != Prefix || values.Length != 5)
    {
      throw new ArgumentException($"The value '{s}' is not a valid PBKDF2 string.", nameof(s));
    }

    return new Pbkdf2(Enum.Parse<KeyDerivationPrf>(values[1]), int.Parse(values[2]),
      Convert.FromBase64String(values[3]), Convert.FromBase64String(values[4]));
  }
  public static bool TryDecode(string s, out Pbkdf2? pbkdf2)
  {
    try
    {
      pbkdf2 = Decode(s);

      return true;
    }
    catch (Exception)
    {
      pbkdf2 = null;

      return false;
    }
  }

  public string Encode() => string.Join(Separator, Prefix, _algorithm, _iterations,
    Convert.ToBase64String(_salt), Convert.ToBase64String(_hash));

  public bool IsMatch(string password) => _hash.SequenceEqual(ComputeHash(password, _hash.Length));

  private byte[] ComputeHash(string password, int length) => KeyDerivation.Pbkdf2(password, _salt, _algorithm, _iterations, length);
}
