using Microsoft.AspNetCore.Authorization;

namespace Logitar.Wishes.Web.Authorization;

internal class RoleAuthorizationRequirement : IAuthorizationRequirement
{
  private readonly string _uniqueNameNormalized;

  public string UniqueName { get; }

  public RoleAuthorizationRequirement(string uniqueName)
  {
    UniqueName = uniqueName;
    _uniqueNameNormalized = Normalize(uniqueName);
  }

  public bool IsEqual(string uniqueName) => _uniqueNameNormalized == Normalize(uniqueName);

  private static string Normalize(string s) => s.Trim().ToUpper();
}
