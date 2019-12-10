using System;
using System.Collections.Generic;
using System.Linq;

namespace finalWork.Models
{
    public static class Vigenere
    {
        static readonly List<char> alphabet = new List<char>() {'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й',
            'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

        public static string Encode(string text, string key)
        {

            string encodedText = "";
            key = KeyCheck(key);
            if (text == null)
            {
                throw new Exception("Текст не может быть пустым!");
            }
            int i1 = 0; // text
            int i2 = 0; // key
            while(i1 < text.Length)
            {
                char letter = text[i1];
                char letterK = key[i2];
                if (alphabet.IndexOf(letter) != -1)
                {
                    int newLetter = (alphabet.IndexOf(letter) + alphabet.IndexOf(letterK)) % 33;
                    encodedText += alphabet[newLetter];
                    i1++;
                    i2 = (i2 + 1) % key.Length;
                }
                else if(alphabet.IndexOf(letter.ToString().ToLower()[0]) != -1)
                {
                    int newLetter = (alphabet.IndexOf(letter.ToString().ToLower()[0]) + alphabet.IndexOf(letterK)) % 33;
                    encodedText += alphabet[newLetter].ToString().ToUpper();
                    i1++;
                    i2 = (i2 + 1) % key.Length;
                }
                else
                {
                    encodedText += letter;
                    i1++;
                }
            }
            return encodedText;
        }

        public static string Decode(string encodedText, string key)
        {
            string text = "";
            if (encodedText == null)
            {
                throw new Exception("Текст не может быть пустым!");
            }
            key = KeyCheck(key);
            int i1 = 0; // text
            int i2 = 0; // key
            while (i1 < encodedText.Length)
            {
                char letter = encodedText[i1];
                char letterK = key[i2];
                if (alphabet.IndexOf(letter) != -1)
                {
                    int newLetter = (alphabet.IndexOf(letter) - alphabet.IndexOf(letterK)) % 33;
                    if (newLetter < 0)
                    {
                        newLetter += 33;
                    }
                    text += alphabet[newLetter];
                    i1++;
                    i2 = (i2+1) % key.Length;
                }
                else if (alphabet.Contains(letter.ToString().ToLower()[0]))
                {
                    int newLetter = (alphabet.IndexOf(letter.ToString().ToLower()[0]) - alphabet.IndexOf(letterK)) % 33;
                    if (newLetter < 0)
                    {
                        newLetter += 33;
                    }
                    text += alphabet[newLetter].ToString().ToUpper();
                    i1++;
                    i2 = (i2 + 1) % key.Length;
                }
                else
                {
                    text += letter;
                    i1++;
                }
            }
            return text;
        }

        private static string KeyCheck(string key)
        {

            key = key?.ToLower() ?? "";
            if (key.Length == 0)
            {
                throw new Exception("Отсутствует ключ!");
            }
            foreach (char c in key)
            {
                if (alphabet.IndexOf(c) == -1)
                {
                    throw new Exception("Ключ введен в неверном формате!");
                }
            }
            return key;
        }
    }
}
