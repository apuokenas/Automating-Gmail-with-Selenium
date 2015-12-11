using NUnit.Framework;
using PST_LD1;

namespace Tests
{
    [TestFixture]
    public class GmailTest
    {
        [SetUp]
        public void Setup()
        {
            Pages.SignUpPage.GoTo();
        }

        [Test]
        public void CanComposeAndSendNewMessage()
        {
            Pages.SignUpPage
                          .EnterValidEmailAddress()
                          .PressOnNextButton()
                          .EnterValidPassword()
                          .DisablePersistentCookies()
                          .PressOnSignInButton();

            Pages.HomePage.PressOnComposeButton()
                          .EnterRecipientEmailAddress()
                          .PressOnCcButtonElement()
                          .EnterCcRecipientEmailAddress()
                          .EnterSubjectOfTheLetter()
                          .EnterTextForTheLetter()
                          .PressOnSendLetterButton()
                          .AssertUserGotMail();

            Pages.HomePage.GoToSentMessages()
                          .GoToMessage()
                          .DeleteMessage();

            Pages.HomePage.AssertMessageWasDeleted();
        }

        [TearDown]
        public void CleanUp()
        {
            Pages.HomePage.LogOut();
            Browser.DeleteCookies();
            Browser.Close();
        }
    }
}
