using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using finalWork.Models;

namespace finalWork.Tests.Models
{
    [TestClass]
    public class VigenereTest
    {
        [TestMethod]
        public void EncodeLowerLetters()
        {
            string text = "привет";
            string key = "б";
            string result = Vigenere.Encode(text, key);
            Assert.AreEqual("рсйгёу", result);
        }
        [TestMethod]
        public void DecodeLowerLetters()
        {
            string text = "привет";
            string key = "б";
            string result = Vigenere.Decode(text, key);
            Assert.AreEqual("опзбдс", result);
        }
        [TestMethod]
        public void EncodeUpperLetters()
        {
            string text = "ПривеТ";
            string key = "б";
            string result = Vigenere.Encode(text, key);
            Assert.AreEqual("РсйгёУ", result);
        }
        [TestMethod]
        public void DecodeUpperLetters()
        {
            string text = "ПривеТ";
            string key = "б";
            string result = Vigenere.Decode(text, key);
            Assert.AreEqual("ОпзбдС", result);
        }
        [TestMethod]
        public void EncodeWrongLetters()
        {
            string text = "Пrи8еТ";
            string key = "б";
            string result = Vigenere.Encode(text, key);
            Assert.AreEqual("Рrй8ёУ", result);
        }
        [TestMethod]
        public void DecodeWrongLetters()
        {
            string text = "Пrи8еТ";
            string key = "б";
            string result = Vigenere.Decode(text, key);
            Assert.AreEqual("Оrз8дС", result);
        }
        [TestMethod]
        public void EncodeEmpty()
        {
            string text = "";
            string key = "б";
            string result = Vigenere.Encode(text, key);
            Assert.AreEqual("", result);
        }
        [TestMethod]
        public void DecodeEmpty()
        {
            string text = "";
            string key = "б";
            string result = Vigenere.Decode(text, key);
            Assert.AreEqual("", result);
        }
        [TestMethod]
        public void EncodeNull()
        {
            string text = null;
            string key = "б";
            try
            {
                string result = Vigenere.Encode(text, key);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Текст не может быть пустым!", e.Message);
            }
        }
        [TestMethod]
        public void DecodeNull()
        {
            string text = null;
            string key = "б";
            try
            {
                string result = Vigenere.Decode(text, key);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Текст не может быть пустым!", e.Message);
            }
        }
        [TestMethod]
        public void KeyCheckLower()
        {
            string text = "аааааа";
            string key = "абв";
            string result = Vigenere.Encode(text,key);
            Assert.AreEqual("абвабв", result);
        }
        [TestMethod]
        public void KeyCheckUpper()
        {
            string text = "аааааа";
            string key = "Абв";
            string result = Vigenere.Encode(text, key);
            Assert.AreEqual("абвабв", result);
        }
        [TestMethod]
        public void KeyCheckEmpty()
        {
            string text = "ааааааа";
            string key = "";
            try
            {
                Vigenere.Encode(text, key);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Отсутствует ключ!", e.Message);
            }
        }
        [TestMethod]
        public void KeyCheckWrongFormat()
        {
            string text = "ааааааа";
            string key = "Abc";
            try
            {
                Vigenere.Encode(text, key);
            }
            catch(Exception e) 
            {
                Assert.AreEqual("Ключ введен в неверном формате!", e.Message);
            }
        }
        [TestMethod]
        public void KeyCheckNull()
        {
            string text = "ааааааа";
            string key = null;
            try
            {
                Vigenere.Encode(text, key);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Отсутствует ключ!", e.Message);
            }
        }
    }
}
