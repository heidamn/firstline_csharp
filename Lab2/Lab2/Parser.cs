using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;

namespace Lab2
{

    class Parser
    {
        private static string path = @"thrlist.txt"; //путь к сохраненному файлу
        private static string splitter = "qwertyqwerty"; // Разделитель для txt
        private static string DownloadTable()  //грузит таблицу
        {
            var client = new WebClient();
            try
            {
                client.DownloadFile("https://bdu.fstec.ru/documents/files/thrlist.xlsx", "thrlist.xlsx");
            }
            catch
            {
                return "Скачивавние таблицы невозможно.";
            }
            return "";
        }
        private static object ParseTable() //парсит xlsx в array
        {
            try
            {
                List<Threat> tList = new List<Threat>();
                FileInfo fileInfo = new FileInfo("thrlist.xlsx");
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet sheet = excelPackage.Workbook.Worksheets["Sheet"];
                    var id = sheet.Cells[3, 1].Value;
                    int i = 3;
                    while (id != null)
                    {
                        Threat t = new Threat(id.ToString(), sheet.Cells[i, 2].Value.ToString(), sheet.Cells[i, 3].Value.ToString(),
                            sheet.Cells[i, 4].Value.ToString(), sheet.Cells[i, 5].Value.ToString(), sheet.Cells[i, 6].Value.ToString(),
                            sheet.Cells[i, 7].Value.ToString(), sheet.Cells[i, 8].Value.ToString());
                        tList.Add(t);
                        id = sheet.Cells[i + 1, 1].Value;
                        i++;
                    }
                }
                return tList;
            }
            catch
            {
                return "Парсинг таблицы невозможен.";
            }
        }
        private static string CreateTxt(List<Threat> tList)
        {
            try
            {
                string text = "";
                foreach (Threat t in tList)
                {
                    text += t.TxtString(splitter) + "\n\n\n";
                }
                return text.Trim();
            }
            catch
            {
                return "Error";
            }
        }
        private static string SaveTxt(string text) // сохраняет то, что запарсили в txt на главный стол(или в свою папку)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(text);
                }
                return "";
            }
            catch
            {
                return "Невозможно сохранить txt.";
            }
        }
        private static string DeleteTable() //удаляет скачанную таблицу, чтоб не мешалась
        {
            try
            {
                File.Delete("thrlist.xlsx");
                return "";
            }
            catch
            {
                return "Невозможно удалить таблицу";
            }
        }
        public static object ParseTxt() // парсит txt в array
        {
            try
            {

                List<Threat> tList = new List<Threat>();
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string text = streamReader.ReadToEnd();
                    foreach (string tLine in text.Split(new string[] { "\n\n\n" }, System.StringSplitOptions.None))
                    {
                        string[] tParts = tLine.Split(new string[] { splitter }, System.StringSplitOptions.None);
                        Threat t = new Threat(tParts[0], tParts[1], tParts[2], tParts[3], tParts[4], tParts[5], tParts[6], tParts[7]);
                        tList.Add(t);
                    }
                }
                return tList;
            }
            catch
            {
                return "Файл txt отсутствует или поврежден, необходимо скачать таблицу.";
            }
        }
        public static List<object> Update() // Обновляет данные
        {
            string s = DownloadTable();
            if (s != "")
            {
                return new List<object>() { s, new List<object>() { "", "" } };
            }
            object o = ParseTable();
            if (o as List<Threat> == null)
            {
                return new List<object>() { o, new List<object>() { "", "" } };
            }
            List<Threat> tList = o as List<Threat>;
            object old = ParseTxt();
            List<object> changes = new List<object>() {"", ""};
            if (old as List<Threat> != null)
            {
                changes = CheckChanges(old as List<Threat>, tList);
            }
                string text = CreateTxt(tList);
            if (text == "Error")
            {
                return new List<object>() { "Создание txt невозможно", new List<object>() { "", ""} };
            }
            s = SaveTxt(text);
            if (s != "")
            {
                return new List<object>() { s, new List<object>() { "", "" } };
            }
            s = DeleteTable();
            if (s != "")
            {
                return new List<object>() { s, new List<object>() { "", "" } };
            }
            return new List<object>() { tList, changes };
        }
        private static List<object> CheckChanges(List<Threat> oldtList, List<Threat> newtList)
        {
            string changes = "";
            int counter = 0;
            foreach(Threat newt in newtList)
            {
                foreach(Threat oldt in oldtList)
                {
                    if (!oldt.Equals(newt) && oldt.id == newt.id)
                    {
                        counter++;
                        changes += $"{oldt.id}:\n";
                        string was = "";
                        string become = "";
                        if(newt.name != oldt.name)
                        {
                            was += $"Наименование: {oldt.name}, ";
                            become += $"Наименование: {newt.name}, ";
                        }
                        if (newt.description != oldt.description)
                        {
                            was += $"Описание: {oldt.description}, ";
                            become += $"Описание: {newt.description}, ";
                        }
                        if (newt.sourse != oldt.sourse)
                        {
                            was += $"Источник: {oldt.sourse}, ";
                            become += $"Источник: {newt.sourse}, ";
                        }
                        if (newt.influenceObject != oldt.influenceObject)
                        {
                            was += $"Объект воздействия: {oldt.influenceObject}, ";
                            become += $"Объект воздействия: {newt.influenceObject}, ";
                        }
                        if (newt.confidentialityBreach != oldt.confidentialityBreach)
                        {
                            was += $"Нарушение конфиденциальности: {oldt.confidentialityBreach}, ";
                            become += $"Нарушение конфиденциальности: {newt.confidentialityBreach}, ";
                        }
                        if (newt.continuityBreach != oldt.continuityBreach)
                        {
                            was += $"Нарушение целостности: {oldt.continuityBreach}, ";
                            become += $"Нарушение целостности: {newt.continuityBreach}, ";
                        }
                        if (newt.avaliabilityBreach != oldt.avaliabilityBreach)
                        {
                            was += $"Нарушение доступности: {oldt.avaliabilityBreach}, ";
                            become += $"Нарушение доступности: {newt.avaliabilityBreach}, ";
                        }

                        changes += $"Было: {was}\nСтало: {become}";
                        changes += '\n';
                    }
                }
            }
            return new List<object>() { changes, counter.ToString()};
        }
    }
        }