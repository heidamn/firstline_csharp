using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using finalWork.Models;

namespace finalWork.Controllers
{
    public class HomeController : Controller
    {
        public static string lastTextValue;
        private static string lastKeyText;
        private static string lastKeyFile;
        private static string lastTextName;
        private static string lastFileName;

        public ActionResult Index()
        {
            ViewBag.TextValue = lastTextValue ?? "";
            ViewBag.KeyText = lastKeyText ?? "";
            ViewBag.KeyFile = lastKeyFile ?? "";
            ViewBag.TextName = lastTextName ?? "";
            ViewBag.FileName = lastFileName ?? "";
            lastTextValue = null;
            lastKeyText = null;
            lastKeyFile = null;
            lastTextName = null;
            lastFileName = null;
            return View("Index");
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TextEncode(Text text)
        {
            try
            {
                lastTextValue = Vigenere.Encode(text.TextValue, text.KeyText);
                lastKeyText = text.KeyText;
            }
            catch (Exception e)
            {
                lastKeyText = e.Message;
                lastTextValue = text.TextValue;
            }
            lastTextName = text.TextName;
            return Redirect("/Home");


        }

        [HttpPost]
        public ActionResult TextDecode(Text text)
        {
            try
            {
                lastTextValue = Vigenere.Decode(text.TextValue, text.KeyText);
                lastKeyText = text.KeyText;
            }
            catch (Exception e)
            {
                lastKeyText = e.Message;
                lastTextValue = text.TextValue;
            }
            lastTextName = text.TextName;
            return Redirect("/Home");
        }

        [HttpPost]
        public ActionResult TextSave(Text text)
        {
            lastTextValue = text.TextValue;
            lastKeyText = text.KeyText;
            lastTextName = text.TextName;
            try
            {
                byte[] arr = Parser.SaveText(text.TextValue);

                string fName = "Text.docx";
                if (text.TextName != null)
                {
                    fName = text.TextName + ".docx";
                }
                return File(arr, "application/docx", fName);
            }
            catch (Exception e)
            {
                lastKeyText = e.Message;
                return Redirect("/Home");
            }
        }

        [HttpPost]
        public ActionResult DocxEncode(UploadedFile file)
        {
            lastKeyFile = file.KeyFile;
            lastFileName = file.FileName;
            try
            {
                //"application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] arr = new byte[file.UploadedFileValue.ContentLength];
                    file.UploadedFileValue.InputStream.Read(arr, 0, file.UploadedFileValue.ContentLength);
                    ms.Write(arr, 0, arr.Length);
                    string body = Parser.ParseDocx(ms);
                    body = Vigenere.Encode(body, file.KeyFile);
                    byte[] result = Parser.SaveDocx(body);
                    string fName = "Encoded.docx";
                    if (file.FileName != null)
                    {
                        fName = file.FileName + ".docx";
                    }
                    return File(result, "application/docx", fName);
                }
            }
            catch (Exception e)
            {
                lastKeyFile = e.Message;
                return Redirect("/Home");
            }
        }
        [HttpPost]
        public ActionResult DocxDecode(UploadedFile file)
        {
            lastKeyFile = file.KeyFile;
            lastFileName = file.FileName;
            try
            {
                //"application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] arr = new byte[file.UploadedFileValue.ContentLength];
                    file.UploadedFileValue.InputStream.Read(arr, 0, file.UploadedFileValue.ContentLength);
                    ms.Write(arr, 0, arr.Length);
                    string body = Parser.ParseDocx(ms);
                    body = Vigenere.Decode(body, file.KeyFile);
                    byte[] result = Parser.SaveDocx(body);
                    string fName = "Decoded.docx";
                    if (file.FileName != null)
                    {
                        fName = file.FileName + ".docx";
                    }
                    return File(result, "application/docx", fName);
                }
            }
            catch (Exception e)
            {
                lastKeyFile = e.Message;
                return Redirect("/Home");
            }
        }


    }
}