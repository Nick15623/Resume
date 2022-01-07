using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WAT.Models;

namespace WAT.Helpers
{
    public static class ByHelper
    {
        public static By GetBy(StepParams stepParams)
        {
            var trueValue = ValueHelper.TrueValue(stepParams?.Step?.Stepdetail?.StepValue, stepParams?.GlobalVariablesCache, stepParams?.VariablesCache);
            switch (stepParams?.Step?.Stepdetail?.GetElementByType?.ToLower() ?? "")
            {
                case "name":
                    return By.Name(trueValue);
                case "id":
                    return By.Id(trueValue);
                case "class name":
                    return By.ClassName(trueValue);
                case "link text":
                    return By.PartialLinkText(trueValue);
                case "cssselector":
                    return By.CssSelector(trueValue);
                case "xpath":
                    return By.XPath(trueValue);
                default:
                    return null;
            }

        }

    }
}