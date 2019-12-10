using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using finalWork;
using finalWork.Controllers;
using finalWork.Models;
using System.Web;
using System.IO;
using Moq;

namespace finalWork.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController controller;
        private ViewResult result;

        [TestInitialize]
        public void SetupContext()
        {
            controller = new HomeController();
            result = controller.Index() as ViewResult;
        }
        [TestMethod]
        public void IndexIsNotNullTest()
        {
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ContactIsNotNullTest()
        {
            ViewResult result = controller.Contact() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void IndexViewEqualIndexCshtmlTest()
        {
            Assert.AreEqual("Index", result.ViewName);
        }
        [TestMethod]
        public void IndexEncodeTextTest()
        {
            RedirectResult redir = controller.TextEncode(new Text("Привет мир!", "б", "test")) as RedirectResult;
            result = controller.Index() as ViewResult;
            Assert.AreEqual(Vigenere.Encode("Привет мир!", "б"), result.ViewBag.TextValue);

        }
        [TestMethod]
        public void IndexDecodeTextTest()
        {
            RedirectResult redir = controller.TextDecode(new Text("Привет мир!", "б", "test")) as RedirectResult;
            result = controller.Index() as ViewResult;
            Assert.AreEqual(Vigenere.Decode("Привет мир!", "б"), result.ViewBag.TextValue);
        }
        [TestMethod]
        public void IndexSaveTextTest()
        {
            FileContentResult file = controller.TextSave(new Text("Привет мир!", "б", "test")) as FileContentResult;
            Assert.AreEqual("application/docx", file.ContentType);
        }
        [TestMethod]
        public void IndexDecodeDocxTest()
        {
            FileStream fs = new FileStream("../../Files/SimpleText.Docx", FileMode.Open);
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(x => x.ContentType).Returns("application/docx");
            uploadedFile.Setup(x => x.InputStream).Returns(fs);
            uploadedFile.Setup(x => x.ContentLength).Returns((int)fs.Length);
            FileContentResult result = controller.DocxDecode(new UploadedFile(uploadedFile.Object, "б", "test")) as FileContentResult;
            Assert.AreEqual("application/docx", result?.ContentType);
            Assert.AreEqual("test.docx", result.FileDownloadName);
            fs.Close();

        }
        [TestMethod]
        public void IndexEncodeDocxTest()
        {
            FileStream fs = new FileStream("../../Files/SimpleText.Docx", FileMode.Open);
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(x => x.ContentType).Returns("application/docx");
            uploadedFile.Setup(x => x.InputStream).Returns(fs);
            uploadedFile.Setup(x => x.ContentLength).Returns((int)fs.Length);
            FileContentResult result = controller.DocxEncode(new UploadedFile(uploadedFile.Object, "б", "test")) as FileContentResult;
            Assert.AreEqual("application/docx", result?.ContentType);
            Assert.AreEqual("test.docx", result.FileDownloadName);
            fs.Close();
        }


    }
}
