using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using WAT.Helpers;
using WAT.Models;
using WAT.Shared;

namespace WAT.Extensions
{
    public static class DriverExtensions
    {
        public static ResponseInfo Click(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }

                element.Click();
                response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public static ResponseInfo TypeText(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }

                var trueValue = ValueHelper.TrueValue(stepParams);
                element.SendKeys(trueValue);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public static ResponseInfo Clear(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }

                element.Clear();
                response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public static ResponseInfo Select(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }

                SelectElement selectElement = new SelectElement(element);
                switch (stepParams.Step.Stepdetail.GetOptionalByType.ToLower())
                {
                    case "text":
                        selectElement.SelectByText(ValueHelper.TrueValue(stepParams));
                        response.IsSuccess = true;
                        break;
                    case "index":
                        var index = Int32.Parse(stepParams.Step.Stepdetail.OptionalValue);
                        selectElement.SelectByIndex(index);
                        response.IsSuccess = true;
                        break;
                    case "value":
                        selectElement.SelectByValue(stepParams.Step.Stepdetail.OptionalValue);
                        response.IsSuccess = true;
                        break;
                    default:
                        response.IsSuccess = false;
                        break;
                }

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public static ResponseInfo Submit(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }

                element.Submit();
                response.IsSuccess = true;

            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
            }

            return response;
        }
        public static ResponseInfo DragAndDrop(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;

                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }

                var x = Int32.Parse(ValueHelper.TrueValue(stepParams.Step.Stepdetail.OptionalValue, stepParams.GlobalVariablesCache, stepParams.VariablesCache));
                var y = Int32.Parse(ValueHelper.TrueValue(stepParams.Step.Stepdetail.Optional2Value, stepParams.GlobalVariablesCache, stepParams.VariablesCache));

                var dragAndDropAction = new Actions(_driver);
                dragAndDropAction.DragAndDropToOffset(element, x, y);
                response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public static ResponseInfo IsVisible(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }

                // Invert Selection
                var shouldInvert = stepParams.Step.Stepdetail.InvertStep;
                if (shouldInvert)
                {
                    if (element.Displayed == false)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
                else
                {
                    if (element.Displayed == true)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public static ResponseInfo IsSelected(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }

                // Invert Selection
                var shouldInvert = stepParams.Step.Stepdetail.InvertStep;
                if (shouldInvert)
                {
                    if (element.Selected == false)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
                else
                {
                    if (element.Selected == true)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
            }

            return response;
        }
        public static ResponseInfo IsEnabled(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }

                // Invert Selection
                var shouldInvert = stepParams.Step.Stepdetail.InvertStep;
                if (shouldInvert)
                {
                    if (element.Enabled == false)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
                else
                {
                    if (element.Enabled == true)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public static ResponseInfo NavigateTo(this IWebDriver _driver, WATHelper helper, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                var trueValue = ValueHelper.TrueValue(stepParams);
                var url = helper.FormatUrl(stepParams.DefaultUrl, trueValue);
                _driver.Navigate().GoToUrl(url);
                var wait = helper.WaitUntilPageLoad(_driver, TimeSpan.FromMilliseconds(1000));
                if (wait.IsSuccess) { response.IsSuccess = true; }
                else { return wait; }
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
            }

            return response;
        }
        public static ResponseInfo NavigateForward(this IWebDriver _driver, WATHelper helper)
        {
            var response = new ResponseInfo();
            try
            {
                _driver.Navigate().Forward();
                var wait = helper.WaitUntilPageLoad(_driver, TimeSpan.FromMilliseconds(1000));
                if (wait.IsSuccess) { response.IsSuccess = true; }
                else { return wait; }
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
            }

            return response;
        }
        public static ResponseInfo NavigateBack(this IWebDriver _driver, WATHelper helper)
        {
            var response = new ResponseInfo();
            try
            {
                _driver.Navigate().Back();
                var wait = helper.WaitUntilPageLoad(_driver, TimeSpan.FromMilliseconds(1000));
                if (wait.IsSuccess) { response.IsSuccess = true; }
                else { return wait; }
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
            }

            return response;
        }
        public static ResponseInfo RefreshPage(this IWebDriver _driver, WATHelper helper)
        {
            var response = new ResponseInfo();
            try
            {
                _driver.Navigate().Refresh();
                var wait = helper.WaitUntilPageLoad(_driver, TimeSpan.FromMilliseconds(1000));
                if (wait.IsSuccess) { response.IsSuccess = true; }
                else { return wait; }
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
            }

            return response;
        }
        public static ResponseInfo Wait(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                var time = Int32.Parse(ValueHelper.TrueValue(stepParams));
                Thread.Sleep(time);
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
            }

            return response;
        }
        public static ResponseInfo GetText(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }

                var key = stepParams.Step.Stepdetail.OptionalValue.Replace("@", "");
                if (stepParams.GlobalVariablesCache.ContainsKey(key))
                {
                    stepParams.GlobalVariablesCache[key] = element.Text;
                    response.IsSuccess = true;
                }
                else
                {
                    stepParams.VariablesCache[key] = element.Text;
                    response.IsSuccess = true;
                }




            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public static ResponseInfo GetAttribute(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }
                
                var key = stepParams.Step.Stepdetail.Optional2Value.Replace("@", "");
                var attribute = ValueHelper.TrueValue(stepParams.Step.Stepdetail.OptionalValue, stepParams.GlobalVariablesCache, stepParams.VariablesCache).ToLower();
                if (stepParams.GlobalVariablesCache.ContainsKey(key))
                {
                    stepParams.GlobalVariablesCache[key] = element.GetAttribute(attribute);
                    response.IsSuccess = true;
                }
                else
                {
                    stepParams.VariablesCache[key] = element.GetAttribute(attribute);
                    response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public static ResponseInfo GetUrl(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                var key = stepParams.Step.Stepdetail.StepValue.Replace("@", "");
                if (stepParams.GlobalVariablesCache.ContainsKey(key))
                {
                    stepParams.GlobalVariablesCache[key] = _driver.Url;
                    response.IsSuccess = true;
                }
                else
                {
                    stepParams.VariablesCache[key] = _driver.Url;
                    response.IsSuccess = true;
                }
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
            }

            return response;
        }
        public static ResponseInfo IsTextEqualTo(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }

                var trueValue = ValueHelper.TrueValue(stepParams.Step.Stepdetail.OptionalValue, stepParams.GlobalVariablesCache, stepParams.VariablesCache);

                // Invert Selection
                var shouldInvert = stepParams.Step.Stepdetail.InvertStep;
                if (shouldInvert)
                {
                    if (trueValue != element.Text)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
                else
                {
                    if (trueValue == element.Text)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public static ResponseInfo IsAttributeEqualTo(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                IWebElement element = null;
                try
                {
                    By by = ByHelper.GetBy(stepParams);
                    element = _driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    if (stepParams.Step.Stepdetail.InvertStep)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Element did not exist on the page: " + ex.Message;
                        return response;
                    }
                }
                var trueValue = ValueHelper.TrueValue(stepParams.Step.Stepdetail.Optional2Value, stepParams.GlobalVariablesCache, stepParams.VariablesCache);
                var trueValueAttr = ValueHelper.TrueValue(stepParams.Step.Stepdetail.OptionalValue, stepParams.GlobalVariablesCache, stepParams.VariablesCache);

                // Invert Selection
                var shouldInvert = stepParams.Step.Stepdetail.InvertStep;
                if (shouldInvert)
                {
                    if (trueValue != element.GetAttribute(trueValueAttr).ToLower())
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
                else
                {
                    if (trueValue == element.GetAttribute(trueValueAttr).ToLower())
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        public static ResponseInfo IsUrlEqualTo(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                var trueValue = ValueHelper.TrueValue(stepParams);

                // Invert Selection
                var shouldInvert = stepParams.Step.Stepdetail.InvertStep;
                if (shouldInvert)
                {
                    if (trueValue != _driver.Url)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
                else
                {
                    if (trueValue == _driver.Url)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
            }

            return response;
        }
        public static ResponseInfo IsEqualTo(this IWebDriver _driver, StepParams stepParams)
        {
            var response = new ResponseInfo();
            try
            {
                var trueValueA = ValueHelper.TrueValue(stepParams.Step.Stepdetail.OptionalValue, stepParams.GlobalVariablesCache, stepParams.VariablesCache);
                var trueValueB = ValueHelper.TrueValue(stepParams.Step.Stepdetail.Optional2Value, stepParams.GlobalVariablesCache, stepParams.VariablesCache);

                // Invert Selection
                var shouldInvert = stepParams.Step.Stepdetail.InvertStep;
                if (shouldInvert)
                {
                    if (trueValueA != trueValueB)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
                else
                {
                    if (trueValueA == trueValueB)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public static Dictionary<string, string> GetGlobalVariables(this WATConfig config)
        {
            var vars = config.GlobalVariables;
            if (vars != null)
            {
                var dict = new Dictionary<string, string>();
                foreach (var variable in vars)
                {
                    dict.Add(variable, "");
                }
                return dict;
            }
            else
            {
                return new Dictionary<string, string>();
            }
        }
    }
}