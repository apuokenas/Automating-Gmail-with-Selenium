// Edit this class appropriately and rename this file to Constants.cs afterwards
using System;

namespace PST_LD1
{
    public static class ConstantsSample // Class to be renamed to Constants
    {
        public static string Url = "https://accounts.google.com/ServiceLogin?service=mail&continue=https://mail.google.com/mail/&hl=en-GB#identifier"; // UI language specified
        public static string UserEmailAddress = "who-sends-the-test-letter@gmail.com"; // Google Account username
        public static string RecipientEmailAddress = "who-gets-the-test-letter@gmail.com"; // Main recipient
        public static string UserPassword = "iAm$tr0ng"; // Google Account password which will be bruteforced 
                                                         // with an average home computer in approximately 29 secs
        public static string LogoutUrl = "https://accounts.google.com/SignOutOptions?hl=en-GB&continue=https://mail.google.com/mail&service=mail"; // English (United Kingdom) chosen
        public static string SubjectName = DateTime.Now.ToString(); // Uses current culture by default. Specify string culture explicitly.
        //public static string SubjectName = "Test"; // Alternative letter subject
        public static string TextMessage = "Test"; // Body message
        public static string CcEmailAddress = "who-else-gets-the-test-letter@gmail.com"; // Carbon copy addressee
    }
}
