using System;
using OpenQA.Selenium;
using RPA_Onliner_Bot.DataFile;

namespace RPA_Onliner_Bot.Helpers
{
    public static class SeleniumServiceHelper
    {
        public static MicrowaveData ToMicrowaveData(this IWebElement webElement)
        {
            if (webElement == null)
            {
                throw new ArgumentNullException(nameof(webElement));
            }

            string nameFull = webElement.FindElement(By.ClassName("schema-product__title")).Text;
            string name = nameFull.Remove(0, 19);
            string price = webElement.FindElement(By.ClassName("schema-product__price")).Text;
            string link = webElement.FindElement(By.TagName("a")).GetAttribute("href");
            return new MicrowaveData
            {
                Name = name,
                Price = price,
                Link = link,
            };
        }
    }
}
