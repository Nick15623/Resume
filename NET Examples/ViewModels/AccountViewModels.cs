using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schooldesk.Models
{
    public class TeacherLoginViewModel
    {
        [Required]
        public string School { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginPageViewModel
    {
        public string ReturnUrl { get; set; }
    }
}
