using Logitar.Wishes.Infrastructure.Security;

namespace Logitar.Wishes.Infrastructure.Caching;

internal record ProtectedCache<T>
{
  private readonly T _value;
  private readonly Pbkdf2 _pbkdf2;

  public ProtectedCache(T value, string password)
  {
    _value = value;
    _pbkdf2 = new(password);
  }

  public T? GetValue(string password) => _pbkdf2.IsMatch(password) ? _value : default;
}
