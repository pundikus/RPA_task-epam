using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RPA_Onliner_Bot.DataFile;
using RPA_Onliner_Bot.Helpers;
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

        private const string SourceUrl = "http://onliner.by";

        private readonly int userDelay;

        public SeleniumService(int useDelay)
        {
            this.userDelay = useDelay;
        }

        public List<MicrowaveData> GetData()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(SourceUrl);

                Thread.Sleep(this.userDelay);

                driver.FindElement(By.LinkText(CatalogElementPath)).Click();
                Thread.Sleep(this.userDelay);

                driver.FindElement(By.XPath(AppliancesElementPath)).Click();
                Thread.Sleep(this.userDelay);

                driver.FindElement(By.XPath(CookingFoodElementPath)).Click();
                Thread.Sleep(this.userDelay);

                driver.FindElement(By.XPath(MicrowavesElementPath)).Click();
                Thread.Sleep(this.userDelay);

                driver.FindElement(By.ClassName(SortingElementPath)).Click();
                Thread.Sleep(this.userDelay);

                driver.FindElement(By.XPath(WithReviewsElementPath)).Click();
                Thread.Sleep(this.userDelay);

                var listData = driver.FindElements(By.XPath(ProductListElementsPath)).ToList();
                return listData.Select(data => data.ToMicrowaveData()).ToList();
            }
        }
    }
}
