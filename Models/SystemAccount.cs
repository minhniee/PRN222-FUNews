using System.ComponentModel.DataAnnotations;

namespace FUnews_PRN_ver2.Models;

public partial class SystemAccount
{
    public short AccountId { get; set; }

    public string? AccountName { get; set; }
    [Required(ErrorMessage = "Please enter your email")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string? AccountEmail { get; set; }

    public int? AccountRole { get; set; }

    [Required(ErrorMessage = "Please enter your password")]
    [DataType(DataType.Password)]
    public string? AccountPassword { get; set; }

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
