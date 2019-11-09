using System;
using System.Collections.Generic;

namespace Lab1
{
    class PhoneBook
    {   
        public static List<PhoneBookPage> phoneBook = new List<PhoneBookPage>();
        public static string helpCommand = "команды \n create - создание записи \n edit id - редактирование пользователя с id \n delete id - удаление пользователя с id \n info id - просмотр полной и информации о пользователе \n allInfo - информация о всех пользователях \n clear - очистка консоли \n help - список команд \n exit - выход";
        static void Main(string[] args)
        {
            
            Console.WriteLine("Добро пожаловать, введите команду, чтобы начать! (help для получения списка команд)");
            while (true)
            {
                Console.Write(">");
                string command = Console.ReadLine();
                if (command == "exit")
                {
                    Console.WriteLine("Удачи! Все файлы были удалены, хе.");
                    break;
                }
                else if (command == "help")
                {
                    Console.WriteLine(helpCommand);
                }
                else if (command == "allInfo")
                {
                    PhoneBook.ViewAll();
                }
                else if (command == "create")
                {
                    PhoneBookPage page = new PhoneBookPage();
                    phoneBook.Add(page);
                }
                else
                {
                    string[] commands = command.Split(" ");
                    if (commands[0] == "edit")
                    {
                        if (HaveId(commands))
                        {
                            for (int i = 0; i <= phoneBook.Count; i++)
                            {
                                if (!int.TryParse(commands[1], out _))
                                {
                                    Console.WriteLine("id должен быть числом!");
                                    break;
                                }
                                else if (i == phoneBook.Count)
                                {
                                    Console.WriteLine("id не существует!");
                                }
                                else if (phoneBook[i].Id == int.Parse(commands[1]))
                                {
                                    phoneBook[i].EditPage();
                                    break;
                                }
                            }
                        }
                    }
                    else if (commands[0] == "delete")
                    {
                        if (HaveId(commands))
                        {
                            for (int i = 0; i <= phoneBook.Count; i++)
                            {
                                if (!int.TryParse(commands[1], out _))
                                {
                                    Console.WriteLine("id должен быть числом!");
                                    break;
                                }
                                else if (i == phoneBook.Count)
                                {
                                    Console.WriteLine("id не существует!");
                                }
                                else if (phoneBook[i].Id == int.Parse(commands[1]))
                                {
                                    Console.Write($"Чтобы удалить запись с id {commands[1]}, введите Y, чтобы отменить, нажмите Enter: ");
                                    if ("Y" == Console.ReadLine())
                                    {
                                        phoneBook.Remove(phoneBook[i]);
                                        Console.WriteLine("Запись удалена");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Удаление отменено");
                                    }
                                    break;

                                }
                            }
                        }
                    }
                    else if (commands[0] == "info")
                    {
                        if (HaveId(commands))
                        {
                            for (int i = 0; i <= phoneBook.Count; i++)
                            {
                                if (!int.TryParse(commands[1], out _))
                                {
                                    Console.WriteLine("id должен быть числом!");
                                    break;
                                }
                                else if (i == phoneBook.Count)
                                {
                                    Console.WriteLine("id не существует!");
                                }
                                else if (phoneBook[i].Id == int.Parse(commands[1]))
                                {
                                    phoneBook[i].FullInfo();
                                    break;
                                }
                            }

                        }
                    }
                    else if (commands[0] == "clear") 
                    {
                        Console.Clear();
                    }

                    else
                    {
                        Console.WriteLine("Команды не существует, попробуйте еще раз");
                    }
                }
            }
        }

        private static bool HaveId(string[] commands)
        {
            if (commands.Length == 1)
            {
                Console.WriteLine("Введите id");
                return false;
            }
            else
            {
                return true;
            }
        }

        static void ViewAll()
        {
            if (phoneBook.Count == 0)
            {
                Console.WriteLine("Записей нет!");
            }
            else
            {
                foreach (PhoneBookPage page in phoneBook)
                {
                    Console.WriteLine(page);
                }
            }
        }
    }
}