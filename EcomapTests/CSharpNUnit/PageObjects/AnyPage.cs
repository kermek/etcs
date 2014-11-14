using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNUnit.PageObjects
{
    class AnyPage : MapPage
    {
        private static readonly By LOGIN_LINK                    = By.LinkText("\u0412\u0425\u0406\u0414");
        private static readonly By EMAIL_FIELD                   = By.Name("email");
        private static readonly By PASSWORD_FIELD                = By.Name("password");
        private static readonly By LOGIN_BUTTON                  = By.Id("login-button");
        private static readonly By USER_PICTOGRAM                = By.ClassName("fa-user");
        private static readonly By LOGOUT_LINK                   = By.LinkText("\u0412\u0418\u0419\u0422\u0418");
        private static readonly By ADD_PROBLEM_BUTTON            = By.XPath("//*[@class='navbar-brand b-menu__button']");
        private static readonly By ADD_PROBLEM_NEXT_TAB2_BUTTON  = By.XPath("//button[@class='btn btn-default btn-sm ng-scope']");
        private static readonly By PROBLEM_NAME_TEXT_BOX         = By.Id("problemName");
        private static readonly By PROBLEM_TYPE_DROP_DOWN_LIST   = By.CssSelector("#select-field option");
        private static readonly By PROBLEM_DESCRIPTION_FIELD     = By.Id("description-field");
        private static readonly By PROBLEM_PROPOSE_FIELD         = By.Id("proposal-field");
        private static readonly By DROP_ZONE                     = By.XPath("//div[contains(@class,'dz-clickable')]/span");
        private static readonly By IMAGE_COMMENT_TEXT_BOX        = By.CssSelector("textarea.comment_field");
        private static readonly By ADD_PROBLEM_SUBMIT_BUTTON     = By.Id("btn-submit");
        private static readonly By ALERT                         = By.ClassName("alert");
        private static readonly By CLOSE_CROSS                   = By.ClassName("close");
        private static readonly By BODY                          = By.XPath("//body");
        private static readonly By ADD_PROBLEM_TAB3_IMAGE        = By.ClassName("fa-file-photo-o");
        private static readonly By DELETE_PROBLEM_BUTTON         = By.XPath("//button[contains(@ng-click,'deleteProblemFromDb()')]");
        private static readonly By APPROVE_DELETE_PROBLEM_BUTTON = By.ClassName("btn-warning");
        private static readonly By PAGE_TITLE                    = By.XPath("//h1");

        private IWebDriver driver;

        public AnyPage(IWebDriver driver) : base(driver) {
            this.driver = driver;
        }

        public void logIn(String email, String password)
        {
            driver.FindElement(LOGIN_LINK).Click();
            driver.FindElement(EMAIL_FIELD).Clear();
            driver.FindElement(EMAIL_FIELD).SendKeys(email);
            driver.FindElement(PASSWORD_FIELD).Clear();
            driver.FindElement(PASSWORD_FIELD).SendKeys(password);
            driver.FindElement(LOGIN_BUTTON).Click();
        }

        public void logOut()
        {
            driver.FindElement(USER_PICTOGRAM).Click();
            driver.FindElement(LOGOUT_LINK).Click();
        }

        public void AddProblem(double       latitude,            double       longitude,
                               String       problemName,         String       problemType,
                               String       problemDescription,  String       problemSolution,
                               IList<String> imageUrls,          IList<String> imageComments) {

            driver.Manage().Window.Maximize();
            driver.FindElement(ADD_PROBLEM_BUTTON).Click();
            SetView(latitude, longitude, 18);
            ClickAtMapCenter(0);
            driver.FindElement(ADD_PROBLEM_NEXT_TAB2_BUTTON).Click();
            driver.FindElement(PROBLEM_NAME_TEXT_BOX).SendKeys(problemName);

            IList<IWebElement> elements = driver.FindElements(PROBLEM_TYPE_DROP_DOWN_LIST);
            foreach (IWebElement element in elements) {
                if (problemType.Equals(element.Text))
                    element.Click();
            }

            driver.FindElement(PROBLEM_DESCRIPTION_FIELD).SendKeys(problemDescription);
            driver.FindElement(PROBLEM_PROPOSE_FIELD).SendKeys(problemSolution);
            driver.FindElement(BODY).SendKeys(Keys.Control + Keys.Home);
            driver.FindElement(ADD_PROBLEM_TAB3_IMAGE).Click();

            foreach (String url in imageUrls) {
                if (url.Length == 0) {
                    continue;
                }
                // Sikuli
                driver.FindElement(DROP_ZONE).Click();
                /*
                try {
                    Thread.sleep(3000);
                } catch (Exception e) {
                }
                thread.interrupt();
                */
            }

            if (imageUrls.Count > 0) {
                IList<IWebElement> commentElements = driver.FindElements(IMAGE_COMMENT_TEXT_BOX);
                int i = 0;
                foreach (IWebElement element in commentElements) {
                    element.SendKeys(imageComments[i]);
                    i++;
                }
            }

            driver.FindElement(ADD_PROBLEM_SUBMIT_BUTTON).Click();
            /*
            if (driver.FindElements(LOGIN_LINK).Count > 0) {
                IWebElement alert = (new WebDriverWait(driver, TimeSpan.FromSeconds(10)))
                        .Until(ExpectedConditions.ElementIsVisible(ALERT));
                alert.FindElement(CLOSE_CROSS).Click();
            }
             */
        }

        public void DeleteCurrentProblem() {
            driver.FindElement(DELETE_PROBLEM_BUTTON).Click();
            driver.FindElement(APPROVE_DELETE_PROBLEM_BUTTON).Click();
        }

        public String GetPageTitle() {
            return driver.FindElement(PAGE_TITLE).Text;
        }
    }
}
