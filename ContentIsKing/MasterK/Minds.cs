using AutoItX3Lib;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentIsKing
{
    public static class Minds
    {

        public static void webtalk(string user, string pass, string content, string pathMedia)
        {

            var driverService = FirefoxDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            var firefoxDriver = new FirefoxDriver(driverService, new FirefoxOptions());
            firefoxDriver.Url = "https://www.webtalk.co/o/signin";
            firefoxDriver.Navigate();
            var oName = firefoxDriver.FindElementByName("userName");
            oName.SendKeys(user);

            var oPass = firefoxDriver.FindElementByName("password");
            oPass.SendKeys(pass);

            var oSubmit = firefoxDriver.FindElementById("signInSubmit");
            oSubmit.Click();

            var message = firefoxDriver.FindElement(By.XPath("/html/body/section/wt-default-page/div[3]/div[2]/div/div/div[1]/span/main-content/wt-home-page-view/div[1]/form/wt-talk-box/div/div[1]/div[3]/wt-mentions-panel/div"), 5, displayed: true);
            message.SendKeys(content);

           
            var oFile = firefoxDriver.FindElementsByClassName("open_file_upload_popup")[1];
                oFile.Click();

            firefoxDriver.ExecuteScript("document.getElementsByClassName('fileUploadFileField')[0].click();");

            Thread.Sleep(TimeSpan.FromSeconds(3));
            
            AutoItX3 autoIT = new AutoItX3();


            autoIT.WinActivate("File Upload");
            autoIT.Send(pathMedia);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            // gửi phím enter sau khi truyền link vào
            autoIT.Send("{ENTER}");

            Thread.Sleep(TimeSpan.FromSeconds(10));
            var buttonPost = firefoxDriver.FindElementByClassName("talkboxbutton");
            buttonPost.Click();
            Thread.Sleep(TimeSpan.FromSeconds(5));
            firefoxDriver.Quit();


        }
        public static void Post(string user, string pass, string content, string path)
        {
            

            var driverService = FirefoxDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            var firefoxDriver = new FirefoxDriver(driverService, new FirefoxOptions());
            firefoxDriver.Url = "https://minds.com/login";
            firefoxDriver.Navigate();
            var oUser = firefoxDriver.FindElementById("username");
            oUser.SendKeys(user);
            var oPassword = firefoxDriver.FindElementById("password");
            oPassword.SendKeys(pass);
            var oSubmit = firefoxDriver.FindElementsByClassName("m-btn--login")[1];
            oSubmit.Click();

            // post


            var message = firefoxDriver.FindElement(By.Id("message"), 5, displayed: true);
            message.SendKeys(content);


            var oFile = firefoxDriver.FindElementByClassName("attachment-button");
            oFile.Click();

            Thread.Sleep(TimeSpan.FromSeconds(3));


            AutoItX3 autoIT = new AutoItX3();


            autoIT.WinActivate("File Upload");
            autoIT.Send(path);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            // gửi phím enter sau khi truyền link vào
            autoIT.Send("{ENTER}");
            if (path.Contains("mp4"))
            {
                Thread.Sleep(TimeSpan.FromSeconds(180));
            } else { Thread.Sleep(TimeSpan.FromSeconds(5)); }
            
            var buttonPost = firefoxDriver.FindElementByXPath("/html/body/m-app/m-body/m-newsfeed/div[2]/div[2]/m-newsfeed--subscribed/minds-newsfeed-poster/div/div/form/div/button");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            buttonPost.Click();

            firefoxDriver.Quit();

        
    }

        public static IWebElement FindElement(this ISearchContext context, By by, uint timeout, bool displayed = false)
        {
            var wait = new DefaultWait<ISearchContext>(context);
            wait.Timeout = TimeSpan.FromSeconds(timeout);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait.Until(ctx => {
                var elem = ctx.FindElement(by);
                if (displayed && !elem.Displayed)
                    return null;

                return elem;
            });
        }
    }

    //https://stackoverflow.com/questions/6992993/selenium-c-sharp-webdriver-wait-until-element-is-present/15142611#15142611
   

}
