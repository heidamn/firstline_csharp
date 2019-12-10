using System;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace finalWork.Models
{
    public class Parser
    {

        public static string ParseDocx(MemoryStream ms)
        {
            string s = "";
            if (ms.Length == 0)
            {
                throw new Exception("Необходимо передать файл!");
            }
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(ms, false))
            {
                s = wordDoc.MainDocumentPart.Document.Body.OuterXml;
                wordDoc.Close();
            }
            return s;
        }

        public static byte[] SaveText(string text)
        {
            MemoryStream ms = new MemoryStream();
            using (WordprocessingDocument wordDocument =
                    WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                Paragraph para = body.AppendChild(new Paragraph());
                Run run = para.AppendChild(new Run());
                if (text == null)
                {
                    throw new Exception("Текст не может быть пустым!");
                }
                run.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Text(text));
                wordDocument.Close();
                byte[] arr = ms.ToArray();
                return arr;
            }

        }
        public static byte[] SaveDocx(string docxBody)
        {   
            if (docxBody == null)
            {
                throw new Exception("Документ не может быть пустым!");
            }
            MemoryStream ms = new MemoryStream();
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body(docxBody));
                wordDocument.Close();
                return ms.ToArray();
            }
        }

    }
}
