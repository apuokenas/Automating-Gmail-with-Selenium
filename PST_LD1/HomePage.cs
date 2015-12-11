using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace PST_LD1
{
    public class HomePage
    {
        public static string TitleBeforeSendingEmail;
        private readonly string _subjectName = Constants.SubjectName;

        // https://mail.google.com/mail/u/0/?shva=1#inbox?compose=new
        // https://mail.google.com/mail/u/0/?view=cm&fs=1&tf=1
        //[FindsBy(How = How.CssSelector, Using = (".v*"))] // Could not find element by: By.CssSelector:.v*
        [FindsBy(How = How.XPath, Using = "//div[text()='COMPOSE']")]
        private IWebElement ComposeButton;

        [FindsBy(How = How.CssSelector, Using = ".vO[name='to']")]
        private IWebElement RecipientEmailField;

        //[FindsBy(How = How.CssSelector, Using = "[data-tooltip='Pridėti „Cc“ (kopijos) gavėjus ?(Ctrl –Shift –C)?']")]
        //[FindsBy(How = How.CssSelector, Using = "[data-tooltip='Add Cc Recipients (Ctrl-Shift-C)']")]
        [FindsBy(How = How.XPath, Using = "//span[text()='Cc']")]
        private IWebElement CcButtonElement;

        [FindsBy(How = How.CssSelector, Using = "[name='cc'")]
        private IWebElement CcRecipientEmailField;

        [FindsBy(How = How.CssSelector, Using = "[name='subjectbox']")]
        private IWebElement SubjectOfLetter;

        // class="Am Al editable LW-avf" aria-label="Message Body"
        [FindsBy(How = How.CssSelector, Using = ".editable")]
        private IWebElement MessageBodyField;

        //[FindsBy(How = How.XPath, Using = "//div[text()='Siųsti']")]
        // data-tooltip="Send ‪(Ctrl-Enter)‬"
        [FindsBy(How = How.XPath, Using = "//div[text()='Send']")]
        private IWebElement SendButton;

        //[FindsBy(How = How.CssSelector, Using = "[href*='https://accounts.google.com/SignOutOptions?hl=lt&continue=https://mail.google.com/mail&service=mail']")]
        [FindsBy(How = How.CssSelector, Using = "[href*='https://accounts.google.com/SignOutOptions?hl=en-GB&continue=https://mail.google.com/mail&service=mail']")]
        private IWebElement ProfilePictureElement;

        [FindsBy(How = How.CssSelector, Using = "#gb_71")]
        private IWebElement LogOutElement;

        //[FindsBy(How = How.XPath, Using = "//*[contains(@class , 'J-Ke')and(text()='Išsiųsti laiškai')]")]
        //[FindsBy(How = How.XPath, Using = "//*[contains(@class , 'J-Ke')and(text()='Sent Mail')]")]

        [FindsBy(How = How.CssSelector, Using = "[href*='https://mail.google.com/mail/u/0/#sent']")]
        private IWebElement SentMailsElement;

        /*
            // Somehow this works only in Firefox 
            //[FindsBy(How = How.XPath, Using = "//*[@aria-label='Ištrinti'][not(ancestor::*[@style='display: none;'])]")]
            [FindsBy(How = How.XPath, Using = "//*[@aria-label='Delete'][not(ancestor::*[@style='display: none;'])]")]
            private IWebElement DeleteMailButton;
        */

        public static IList<IWebElement> FindIfEmailWasSent(string name)
        {
            //return Browser.driver.FindElements(By.CssSelector(".UI[name='aš']"));
            return Browser.Driver.FindElements(By.CssSelector(".UI[name='me']"));
        }

        public HomePage EnterTextForTheLetter()
        {
            MessageBodyField.SendKeys(Constants.TextMessage);
            return this;
        }

        public HomePage AssertUserLoggedIn()
        {
            Browser.WaitForComposeButton();
            Assert.IsNotNull(ComposeButton);
            return this;
        }

        public HomePage PressOnComposeButton()
        {
            Browser.WaitForComposeButton();
            ComposeButton.Click();
            return this;
        }

        public HomePage EnterRecipientEmailAddress()
        {
            Browser.WaitForMessageComposer();
            RecipientEmailField.SendKeys(Constants.RecipientEmailAddress);
            return this;
        }

        public HomePage PressOnCcButtonElement()
        {
            CcButtonElement.Click();
            return this;
        }

        public HomePage EnterCcRecipientEmailAddress()
        {
            CcRecipientEmailField.SendKeys(Constants.CcEmailAddress);
            return this;
        }

        public HomePage EnterSubjectOfTheLetter()
        {
            SubjectOfLetter.Clear();
            SubjectOfLetter.Click();
            SubjectOfLetter.SendKeys(_subjectName);
            return this;
        }

        public HomePage PressOnSendLetterButton()
        {
            TitleBeforeSendingEmail = Browser.Title;
            /*
                // Works on Firefox only 
                MessageBodyField.SendKeys(Keys.Control + Keys.Enter);
            */
            SendButton.Click();
            return this;
        }

        public HomePage AssertUserGotMail()
        {
            Browser.WaitUntilUserGetsMessage();
            string titleAfterUserGotEmail = Browser.Title;
            Assert.AreNotEqual(TitleBeforeSendingEmail, titleAfterUserGotEmail);
            return this;
        }

        public HomePage GoToSentMessages()
        {
            Browser.WaitForSentMessagesElement();
            Thread.Sleep(1000);
            SentMailsElement.Click();
            return this;
        }

        public HomePage FindTheMessageAndClick()
        {
            IWebElement sentMailListElement = Browser.Driver.FindElement(By.XPath("//div[@class='y6']//*[text()='" + Constants.SubjectName + "']"));
            sentMailListElement.Click();
            return this;
        }

        public HomePage GoToMessage()
        {
            Browser.WaitForMessage();
            FindTheMessageAndClick();
            return this;
        }

        public HomePage DeleteMessage()
        {
            /*
                Goes through every fake element created by Google and stored in the list and clicks it
                as elements are fake, exeption must be cought in order to perform this task
            */
            IList<IWebElement> list = Browser.Driver.FindElements(By.CssSelector(".ar9"));
            int listLength = Browser.Driver.FindElements(By.CssSelector(".ar9")).Count;
            for (int i = 0; i < listLength; i++)
            {
                try { list[i].Click(); }
                catch (ElementNotVisibleException) { }
            }
            return this;
        }

        public HomePage AssertMessageWasDeleted()
        {
            FindIfLetterExistAfterDeleting();
            return this;
        }

        public HomePage FindIfLetterExistAfterDeleting()
        {
            try
            {
                FindTheMessageAndClick();
                Assert.Fail();
            }

            catch (NoSuchElementException)
            {
                Console.WriteLine("[GREAT SUCCESS] Tests passed!");
            }

            return this;
        }

        public HomePage LogOut()
        {
            ProfilePictureElement.Click();
            LogOutElement.Click();
            return this;
        }
    }
}
