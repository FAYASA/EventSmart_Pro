namespace EventService.Models
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateProfileDto
    {
        public string FullName { get; set; }
    }

    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; }
        public int VenueId { get; set; }
        public string OrganizerId { get; set; } // User ID of the organizer
    }

    public class CreateEventDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }
        public int VenueId { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateEventDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }
        public int VenueId { get; set; }
        public int CategoryId { get; set; }
    }

    public class RegistrationDto
    {
        public int EventId { get; set; }
        public string UserId { get; set; } // User ID of the registrant
    }

    public class CreateVenueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class CreateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class UserProfileDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; } // Password reset token
    }


    public class ForgotPasswordDto
    {
        public string Email { get; set; } // Email to send the reset link to
    }
}