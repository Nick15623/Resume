using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WAT.Models;

namespace WAT.Helpers
{
    public static class ValueHelper
    {
        public static string TrueValue(StepParams stepParams)
        {
            var value = stepParams?.Step?.Stepdetail?.StepValue ?? "";
            if (value.StartsWith("@"))
            {
                return ParseValue(value, stepParams.GlobalVariablesCache, stepParams.VariablesCache);
            }
            else
            {
                return value;
            }
        }
        public static string TrueValue(string value, Dictionary<string, string> globalVariablesCache, Dictionary<string, string> variablesCache)
        {
            if (value?.StartsWith("@") ?? false)
            {
                return ParseValue(value, globalVariablesCache, variablesCache);
            }
            else
            {
                return value;
            }
        }
        private static string ParseValue(string value, Dictionary<string, string> globalVariablesCache, Dictionary<string, string> variablesCache)
        {
            if (value.StartsWith("@RandomNumber"))
            {
                return GenerateRandomNumber(value);
            }
            else if (value.StartsWith("@RandomPhoneNumber"))
            {
                return GenerateRandomPhoneNumber();
            }
            else if (value.StartsWith("@RandomString"))
            {
                return GenerateRandomString(value);
            }
            else if (globalVariablesCache?.ContainsKey(value.Replace("@", "")) ?? false)
            {
                return GetFromGlobalVariables(value, globalVariablesCache);
            }
            else
            {
                return GetFromVariables(value, variablesCache);
            }
        }
        private static string GenerateRandomNumber(string value)
        {
            var range = value.Replace("@RandomNumber", "").Split('-');
            int min = int.Parse(range[0]);
            int max = int.Parse(range[1]);
            var rand = new Random();
            return rand.Next(min, max).ToString();
        }
        private static string GenerateRandomPhoneNumber()
        {
            var rand = new Random();
            return rand.Next(100, 999).ToString() + rand.Next(100, 999).ToString() + rand.Next(1000, 9999).ToString();
        }
        private static string GenerateRandomString(string value)
        {
            var randStr = "";
            var length = int.Parse(value.Replace("@RandomString", ""));
            while (randStr.Length < length)
            {
                var randomString = Path.GetRandomFileName().Replace(".", "");
                foreach (var character in randomString)
                {
                    randStr += character;
                }
            }
            return randStr;
        }
        private static string GetFromGlobalVariables(string value, Dictionary<string, string> globalVariablesCache)
        {
            try
            {
                return globalVariablesCache[value.Replace("@", "")];
            }
            catch 
            {
                return value;
            }
        }
        private static string GetFromVariables(string value, Dictionary<string, string> variablesCache)
        {
            try
            {
                return variablesCache[value.Replace("@", "")];
            }
            catch
            {
                return value;
            }
        }

    }
}