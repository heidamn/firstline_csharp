using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    class PhoneBookPage
    {
        private static int lastId = 0;
        public int Id { get; private set; }
        private string firstName;
        private string secondName; // необязательно
        private string lastName;
        private long phoneNumber;
        private string country;
        private DateTime bDay; // необязательно
        private string organisation; //необязательно
        private string position; //необязательно
        private string notes; //необязательно

        public string FirstName
        {
            get { return firstName; }
            set
            {
                while (value == "")
                {
                    Console.WriteLine("Поле не может быть пустым");
                    value = Console.ReadLine();
                }
                firstName = value;
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                while (value == "")
                {
                    Console.WriteLine("Поле не может быть пустым");
                    value = Console.ReadLine();
                }
                lastName = value;
            }
        }

        public string PhoneNumber
        {
            get { return phoneNumber.ToString(); }
            set
            {

                while (value == "" || !value.All(char.IsDigit))
                {
                    Console.WriteLine("Поле не может быть пустым и должно содержать только числа");
                    value = Console.ReadLine();
                }
                phoneNumber = long.Parse(value);
            }
        }

        public string Country
        {
            get { return Country; }
            set
            {
                while (value == "")
                {
                    Console.WriteLine("Поле не может быть пустым");
                    value = Console.ReadLine();
                }
                country = value;
            }
        }

        public string BDay
        {
            get 
            {
                if (bDay == DateTime.Parse("01.01.0001"))
                {
                    return "Не задана";
                }
                else
                {
                    return bDay.ToShortDateString();
                }
            }
            set
            {
                if (value == "")
                {
                    bDay = DateTime.Parse("01.01.0001");
                }
                else
                {
                    while (!DateTime.TryParse(value, out bDay))
                    {
                        Console.WriteLine("Неверный формат, попробуйте снова.");
                        value = Console.ReadLine();
                    }
                }
            }
        }


        public PhoneBookPage()
        {
            this.Id = lastId;
            EditPage();
            lastId++;
            Console.WriteLine("Страница создана!"); 
        }
        
        //редактирование
        public void EditPage() 
        {
            Console.WriteLine("Введите имя:");
            FirstName = Console.ReadLine();
            Console.WriteLine("Введите отчество: \n или нажмите Enter, чтобы продолжить");
            secondName = Console.ReadLine();
            Console.WriteLine("Введите фамилию:");
            LastName = Console.ReadLine();
            Console.WriteLine("Введите номер телефона (число):");
            PhoneNumber = Console.ReadLine();
            Console.WriteLine("Введите страну:");
            Country = Console.ReadLine();
            Console.WriteLine("Введите дату рождения: \n  или нажмите Enter, чтобы продолжить");
            BDay = Console.ReadLine();
           
            Console.WriteLine("Введите компанию: \n  или нажмите Enter, чтобы продолжить");
            organisation = Console.ReadLine();
            Console.WriteLine("Введите должность: \n  или нажмите Enter, чтобы продолжить");
            position = Console.ReadLine();
            Console.WriteLine("Введите заметки: \n  или нажмите Enter, чтобы продолжить");
            notes = Console.ReadLine();
            
        }
        
       
        public override string ToString()
        {
            return $"id: {this.Id}\n{this.lastName} {this.firstName}\n{this.phoneNumber}";
        }
        public void FullInfo()
        {
            Console.WriteLine($"id: {Id}");
            if (secondName == "")
            {
                Console.WriteLine($"{firstName} {lastName}");
            }
            else
            {
                Console.WriteLine($"{FirstName} {secondName} {LastName}");
            }
                Console.WriteLine($"{phoneNumber}");
                Console.WriteLine($"{country}");
            if (bDay != DateTime.Parse("01.01.0001"))
            {
                Console.WriteLine($"Дата рождения: {BDay}");
            }
            if (organisation != "")
            {
                Console.WriteLine($"Организация: {organisation}");
            }
            if (position != "")
            {
                Console.WriteLine($"Должность: {position}");
            }
            if (notes != "")
            {
                Console.WriteLine($"Заметки: {notes}");
            }
        }


            

    }
}