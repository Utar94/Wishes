using Logitar.Wishes.Infrastructure.Security;

namespace Logitar.Wishes.Infrastructure.Caching;

internal record ProtectedCache<T>
{
  private readonly T _value;
  private readonly Pbkdf2 _pbkdf2;

  public ProtectedCache(T value, string password)
  {
    _value = value;

    /* NOTE(fpion): OWASP recommends 600000 iterations when using SHA-256. This is a security risk
     * since the Portal uses 600000 iterations. The result is that Wishes is less secure than the
     * Portal, which simplifies the job of an attack who would like to try keys. */
    _pbkdf2 = new(password, iterations: 10000);
  }

  public T? GetValue(string password) => _pbkdf2.IsMatch(password) ? _value : default;
}
