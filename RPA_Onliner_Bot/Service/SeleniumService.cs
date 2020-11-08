using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RPA_Onliner_Bot.DataFile;
using RPA_Onliner_Bot.Service.Abstract;

namespace RPA_Onliner_Bot.Service
{
    public class SeleniumService : ISeleniumService
    {
        private const string CatalogElementPath = "Каталог";
        private const string AppliancesElementPath = "//*[@id='container']/div/div/div/div/div[1]/ul/li[4]";

        private const string CookingFoodElementPath = "//*[@id='container']/div/div/div/div/div[1]/div[3]/div/div[3]/div[1]/div/div[6]/div[1]";

        private const string MicrowavesElementPath = "//*[@id='container']/div/div/div/div/div[1]/div[3]/div/div[3]/div[1]/div/div[6]/div[2]/div/a[1]/span/span[2]";
        private const string SortingElementPath = "schema-order__link";

        private const string WithReviewsElementPath = "/html/body/div[1]/div/div/div/div/div/div[2]/div[2]/div[4]/div[3]/div[1]/div[2]/div/div[5]/span";
        private const string ProductListElementsPath = "//*[@class='schema-product__group']";

        private readonly IWebDriver driver = new ChromeDriver();

        private int userDelay;

        public SeleniumService(int useDelay)
        {
            this.userDelay = useDelay;
        }

        public List<MicrowaveData> GetData()
        {
            if (this.driver == null)
            {
                throw new ArgumentException();
            }

            List<MicrowaveData> resultList = new List<MicrowaveData>();

            this.driver.Manage().Window.Maximize();
            this.driver.Navigate().GoToUrl("http://onliner.by");

            Thread.Sleep(this.userDelay);

            this.driver.FindElement(By.LinkText(CatalogElementPath)).Click();
            Thread.Sleep(this.userDelay);

            this.driver.FindElement(By.XPath(AppliancesElementPath)).Click();
            Thread.Sleep(this.userDelay);

            this.driver.FindElement(By.XPath(CookingFoodElementPath)).Click();
            Thread.Sleep(this.userDelay);

            this.driver.FindElement(By.XPath(MicrowavesElementPath)).Click();
            Thread.Sleep(this.userDelay);

            this.driver.FindElement(By.ClassName(SortingElementPath)).Click();
            Thread.Sleep(this.userDelay);

            this.driver.FindElement(By.XPath(WithReviewsElementPath)).Click();
            Thread.Sleep(this.userDelay);

            var listData = this.driver.FindElements(By.XPath(ProductListElementsPath)).ToList();

            foreach (IWebElement element in listData)
            {
                string name = element.FindElement(By.ClassName("schema-product__title")).Text;
                string price = element.FindElement(By.ClassName("schema-product__price")).Text;
                string link = element.FindElement(By.TagName("a")).GetAttribute("href");

                resultList.Add(new MicrowaveData()
                {
                    Name = name,
                    Price = price,
                    Link = link,
                });
            }

            return resultList;
        }
    }
}
