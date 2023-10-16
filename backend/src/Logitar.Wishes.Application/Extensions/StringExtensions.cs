namespace Logitar.Wishes.Application.Extensions;

internal static class StringExtensions
{
  public static string Slugify(this string s)
  {
    List<string> words = new(capacity: s.Length);
    StringBuilder word = new();

    foreach (char c in s)
    {
      if (char.IsLetterOrDigit(c))
      {
        word.Append(c);
      }
      else if (word.Length > 0)
      {
        words.Add(word.ToString());
        word.Clear();
      }
    }

    if (word.Length > 0)
    {
      words.Add(word.ToString());
    }

    return string.Join('-', words);
  }
}
