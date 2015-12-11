using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace PST_LD1
{
    public class SignUpPage
    {
        [FindsBy(How = How.Id, Using = "Email")]
        private IWebElement LoginFieldElement;

        [FindsBy(How = How.Id, Using = "next")]
        private IWebElement NextButton;

        [FindsBy(How = How.Id, Using = "Passwd")]
        private IWebElement PasswordFieldElement;

        [FindsBy(How = How.Id, Using = "signIn")]
        private IWebElement SignInButton;

        // Checkbox "Stay signed in"
        [FindsBy(How = How.CssSelector, Using = "#PersistentCookie")]
        private IWebElement DisablePersistentCookie;

        public SignUpPage GoTo()
        {
            Browser.GoTo(Constants.Url);
            return this;
        }

        public SignUpPage EnterValidEmailAddress()
        {
            Browser.WaitForLoginField();
            LoginFieldElement.SendKeys(Constants.UserEmailAddress);
            return this;
        }

        public void AssertLoginFieldIsPresent() // Unused?
        {
            Assert.NotNull(LoginFieldElement);
        }

        public SignUpPage PressOnNextButton()
        {
            NextButton.Click();
            return this;
        }

        public SignUpPage EnterValidPassword()
        {
            Browser.WaitForPasswordField();
            PasswordFieldElement.SendKeys(Constants.UserPassword);
            return this;
        }

        public SignUpPage DisablePersistentCookies()
        {
            DisablePersistentCookie.Click();
            return this;
        }

        public SignUpPage PressOnSignInButton()
        {
            SignInButton.Click();
            return this;
        }
    }
}
