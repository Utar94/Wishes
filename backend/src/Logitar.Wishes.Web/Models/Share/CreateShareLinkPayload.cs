namespace Logitar.Wishes.Web.Models.Share;

public record CreateShareLinkPayload
{
  public string DisplayName { get; set; } = string.Empty;
  public string? Description { get; set; }
  public DateTime? ExpiresOn { get; set; }
}
