// Edit this class appropriately and rename this to Browser.cs afterwards
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.IE;

namespace PST_LD1
{
    public static class BrowserSample // Class to be renamed to Browser
    {
        public static IWebDriver WebDriver;

        public static string Title => WebDriver.Title;

        public static ISearchContext Driver => WebDriver ??
                                               (WebDriver =
                                                   new InternetExplorerDriver(
                                                       // Specify where IE WebDriver is located at:
                                                       @"C:\Users\...\packages\Selenium.WebDriver.IEDriver.2.48.0.0\driver\"));

        public static void DeleteCookies()
        {
            WebDriver.Manage().Cookies.DeleteAllCookies();
        }

        public static void GoTo(string url)
        {
            WebDriver.Url = url;
        }

        public static string Url => WebDriver.Url;

        public static void Logout(string logoutUrl)
        {
            WebDriver.Url = logoutUrl;
        }

        public static void Close()
        {
            WebDriver.Close();
            WebDriver = null;
        }

        public static void WaitForLoginField()
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(15));
            wait.Until((d) => d.FindElement(By.Id("Email"))); // lambda expression
        }

        public static void WaitForPasswordField()
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(15));
            wait.Until((d) => d.FindElement(By.Id("Passwd")));
        }

        public static void WaitForComposeButton()
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(15));
            wait.Until((d) => d.FindElement(By.CssSelector(".z0")));
        }

        public static void WaitForMessageComposer()
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20));
            wait.Until((d) => d.FindElement(By.CssSelector(".vO[name='to']")));
        }

        public static void WaitUntilUserGetsMessage()
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20));
            wait.Until((d) => d.Title != HomePage.TitleBeforeSendingEmail);
        }

        public static void WaitForSentMessagesElement()
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20));
            //wait.Until((d) => { return d.FindElement(By.CssSelector("[title*='Išsiųsti laiškai']")); });
            wait.Until((d) => d.FindElement(By.CssSelector("[title*='Sent Mail']")));
        }

        public static void WaitForMessage()
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(15));
            wait.Until((d) => d.FindElement(By.XPath("//div[@class='y6']//*[text()='" + Constants.SubjectName + "']")));
        }
    }
}
