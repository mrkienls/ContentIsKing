using AutoItX3Lib;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentIsKing
{
    public static class Minds
    {
        public static void http_post(string content, string pathImage, string user, string pass)
        {
            
    

            var client = new RestClient("https://www.minds.com/login");
            var request = new RestRequest(Method.GET);
            CookieContainer _cookieJar = new CookieContainer();
            client.CookieContainer = _cookieJar;

         //   ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
       //     ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            //request.AddHeader("Host", "www.minds.com");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.2; rv:63.0) Gecko/20100101 Firefox/63.0");
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            request.AddHeader("Accept-Language", "en-US,en;q=0.5");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("DNT", "1");
          //  request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Upgrade-Insecure-Requests", "1");
            //request.AddHeader("TE", "Trailers");
        


            IRestResponse response = client.Execute(request);
            string XSRF_TOKEN = response.Cookies[0].Value;

            // post
            client.BaseUrl="https://www.minds.com/api/v1/authenticate";
            var request1 = new RestRequest(Method.POST);
            request1.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.2; rv:63.0) Gecko/20100101 Firefox/63.0");
            request1.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            request1.AddHeader("Accept-Language", "en-US,en;q=0.5");
            request1.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request1.AddHeader("X-XSRF-TOKEN", XSRF_TOKEN);

            request1.AddParameter("username", "kienmnm");
            request1.AddParameter("password", "Mnm@1234");
            IRestResponse response1 = client.Execute(request1);

            string html = response1.Content;
        }



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
