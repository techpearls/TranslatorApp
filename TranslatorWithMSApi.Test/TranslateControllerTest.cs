using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TranslatorWithMSApi.Interfaces;
using TranslatorWithMSApi.Models;
using TranslatorWithMSApi.Controllers;
using System.Web.Mvc;

namespace TranslatorWithMSApi.Test
{
    [TestClass]
    public class TranslateControllerTest
    {
        Authentication authentication;

        [TestInitialize]
        public void Init()
        {
            authentication = new Authentication();
        }

        [TestMethod]
        public void DoTranslate_Convert_English_German_Success()
        {
            //3A's
            //arrange/act
            TranslateController svcController = new TranslateController(authentication);
            JsonResult result = svcController.DoTranslate("Hello", "en", "de") as JsonResult;

            //assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Data, "\"Hallo\"");
        }

        [TestMethod]
        public void DoTranslate_Convert_German_English_Success()
        {
            //3A's
            //arrange/act
            TranslateController svcController = new TranslateController(authentication);
            JsonResult result = svcController.DoTranslate("Hallo", "de", "en") as JsonResult;

            //assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Data, "\"Hello\"");
        }

        [TestMethod]
        public void DoTranslate_Convert_German_UnknownLanguage_Failed()
        {
            //3A's
            //arrange/act
            TranslateController svcController = new TranslateController(authentication);
            JsonResult result = svcController.DoTranslate("Hallo", "de", "xyz") as JsonResult;

            //assert            
            StringAssert.Contains(result.Data.ToString(), "'to' must be a valid language");
        }
    }
}
