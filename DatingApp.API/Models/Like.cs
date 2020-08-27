namespace DatingApp.API.Models
{
    public class Like
    {
        public int LikerId { get; set; }
        public int LikeeId { get; set; }
        // User tablosuna referans verdigimiz icin User modeline ICollection eklememiz gerekiyor
        public User Liker { get; set; }
        public User Likee { get; set; }
    }
}