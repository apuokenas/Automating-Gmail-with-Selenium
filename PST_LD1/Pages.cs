using OpenQA.Selenium.Support.PageObjects;

namespace PST_LD1
{
    public static class Pages
    {
        public static SignUpPage SignUpPage
        {
            get
            {
                var signUpPage = new SignUpPage();
                PageFactory.InitElements(Browser.Driver, signUpPage);
                return signUpPage;
            }
        }

        public static HomePage HomePage
        {
            get
            {
                var homePage = new HomePage();
                PageFactory.InitElements(Browser.Driver, homePage);
                return homePage;
            }
        }
    }
}
