using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Linq;
using System.Threading;
using System.IO;
using System;


namespace HomeworkAdvancedSelenium1
{
    public class Homework
    {

        IWebDriver driver;
        
       

        [Test]
        [Order(1)]

        public void HowerScreen()
        {
            TestContext.WriteLine("Initialization Chrome driver with headless option");
            var broOptions = new ChromeOptions();
            broOptions.AddArguments("--headless");
            var driver = new ChromeDriver(broOptions);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://www.leafground.com/home.html");

            TestContext.WriteLine("Go to new tab");
            By newTab = By.LinkText("HyperLink");
            new Actions(driver).KeyDown(Keys.Control).Click(driver.FindElement(newTab)).Perform();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(1000);
            Assert.AreEqual("TestLeaf - Interact with HyperLinks", driver.Title);

            TestContext.WriteLine("Hover effect");
            new Actions(driver).MoveToElement(driver.FindElement(By.XPath("//div[1]/div/div/a"))).Perform();

            TestContext.WriteLine("Taking screenshot");
            var screenshot = driver.GetScreenshot();
            var destinationPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var screenshotPath = Path.Combine(destinationPath, "screenshot.png");
            screenshot.SaveAsFile(screenshotPath);

            TestContext.AddTestAttachment(screenshotPath);
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles[0]);

        }
        [Test]
        [Order(2)]

        public void DragDrop()
        {
            TestContext.WriteLine("Initialization Chrome driver with headless option");
            var browserOptions = new ChromeOptions();
            browserOptions.AddArguments("--headless");
            var driver = new ChromeDriver(browserOptions);

            TestContext.WriteLine("Go to test site");
            driver.Navigate().GoToUrl("https://jqueryui.com/demos");
            driver.FindElement(By.LinkText("Droppable")).Click();

            TestContext.WriteLine("Go to frame");
            By dragDropFrame = By.CssSelector("iframe[class='demo-frame']");
            driver.SwitchTo().Frame(driver.FindElement(dragDropFrame));

            TestContext.WriteLine("Drag & drop");
            By draggable = By.Id("draggable");
            By droppable = By.Id("droppable");
            var dropElement = driver.FindElement(droppable);
            var draggableElement = driver.FindElement(draggable);
            var actions = new Actions(driver);
            actions.DragAndDrop(draggableElement, dropElement).Perform();
            Assert.AreEqual("Dropped!", dropElement.Text);
            TestContext.WriteLine("Dropped!");
            driver.Quit();

        }


    }
}
