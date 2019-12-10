using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using finalWork.Models;
using System.IO;

namespace finalWork.Tests.Models
{
    [TestClass]
    public class ParserTest
    {
        static public string createdTextXml = "<w:body xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"><w:p><w:r><w:t>Созданный текст.</w:t></w:r></w:p></w:body>";
        static public string simpleTextXml = "<w:body xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"><w:p w:rsidRPr=\"00233E30\" w:rsidR=\"00083FF8\" w:rsidRDefault=\"00677512\" " +
            "w14:paraId=\"4A1BEDA2\" w14:textId=\"518FBEF1\" xmlns:w14=\"http://schemas.microsoft.com/office/word/2010/wordml\"><w:r><w:t>Простой текст.</w:t></w:r><w:bookmarkStart w:name=\"_GoBack\" w:id=\"0\"" +
            " /><w:bookmarkEnd w:id=\"0\" /></w:p><w:sectPr w:rsidRPr=\"00233E30\" w:rsidR=\"00083FF8\"><w:pgSz w:w=\"11906\" w:h=\"16838\" /><w:pgMar w:top=\"1134\" w:right=\"850\" w:bottom=\"1134\" w:left=\"1701\" " +
            "w:header=\"708\" w:footer=\"708\" w:gutter=\"0\" /><w:cols w:space=\"708\" /><w:docGrid w:linePitch=\"360\" /></w:sectPr></w:body>";


        [TestMethod]
        public void ParseDocxSimpleTextTest()
        {

            using (FileStream fs = File.OpenRead("../../Files/SimpleText.Docx"))
            {
                byte[] arr = new byte[(int)fs.Length];
                fs.Read(arr, 0, (int)fs.Length);
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    string result = Parser.ParseDocx(ms);
                    Assert.AreEqual(simpleTextXml, result);
                }
            }
        }
        [TestMethod]
        public void ParseDocxWrongFileTypeTest()
        {
            using (FileStream fs = File.OpenRead("../../Files/WrongType.txt"))
            {
                byte[] arr = new byte[(int)fs.Length];
                fs.Read(arr, 0, (int)fs.Length);
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    try
                    {
                        string result = Parser.ParseDocx(ms);
                    }
                    catch (Exception e)
                    {
                        Assert.AreEqual("Файл содержит поврежденные данные.", e.Message);
                    }

                }
            }
        }
        [TestMethod]
        public void ParseDocxNullTest()
        {
            byte[] arr = new byte[0];
            using (MemoryStream ms = new MemoryStream(arr))
            {
                try
                {
                    string result = Parser.ParseDocx(ms);
                }
                catch (Exception e)
                {
                    Assert.AreEqual("Необходимо передать файл!", e.Message);
                }
            }
        }

        [TestMethod]
        public void SaveTextNullTest()
        {
            string text = null;
            try
            {
                Parser.SaveText(text);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Текст не может быть пустым!", e.Message);
            }

        }
        [TestMethod]
        public void SaveTextCreatedTextTest()
        {
            string text = "Созданный текст.";
            byte[] arr = Parser.SaveText(text);
            string xml;
            using (MemoryStream ms = new MemoryStream(arr))
            {
                xml = Parser.ParseDocx(ms);
            }
            Assert.AreEqual(createdTextXml, xml);

        }
        [TestMethod]

        public void SaveDocxNullTest()
        {
            string body = null;
            try
            {
                Parser.SaveDocx(body);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Документ не может быть пустым!", e.Message);
            }
        }
        [TestMethod]
        public void SaveDocxSimpleTextTest()
        {
           
            byte[] result = Parser.SaveDocx(simpleTextXml);
            Assert.IsNotNull(result);
        }

    }
}
