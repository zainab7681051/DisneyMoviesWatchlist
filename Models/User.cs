using Microsoft.AspNetCore.Identity;

namespace DisneyMoviesWatchlist.Models;

public class AppUser : IdentityUser
{
    public byte[]? ProfilePicture { get; set; }
    public int[]? MovieIdWatchist { get; set; }
}