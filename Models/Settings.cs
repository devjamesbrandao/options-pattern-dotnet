using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class Settings
    {
        [Required]
        public string JWTKey { get; set; }
    }
}