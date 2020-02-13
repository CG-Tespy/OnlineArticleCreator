using System.ComponentModel.DataAnnotations;

namespace CGT.AspNetMvc
{
    public static class EmailUtils
    {

        public static bool ValidEmailAddress(string email)
        {
            // Credit to Habib from StackOverflow
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
