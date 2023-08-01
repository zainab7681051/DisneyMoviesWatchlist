using Microsoft.AspNetCore.Identity;

namespace DisneyMoviesWatchlist.Models;

public class AppUser : IdentityUser
{
    public virtual ICollection<Movie>? Movies { get; set; }
    public byte[]? ProfilePicture { get; set; }
}