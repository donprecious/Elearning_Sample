using Microsoft.AspNetCore.Identity;

namespace ELearningTest.Data
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
