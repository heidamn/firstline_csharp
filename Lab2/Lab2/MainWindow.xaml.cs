using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Threat> tList = new List<Threat>();
        int tOnPage = 15; // угроз на странице
        public string changes= ""; // изменения в файле
        int numberOfChanges = 0; // количество измененных записей
        int pageNumber = 0; // номер страницы
        public MainWindow()
        {
            InitializeComponent();
            bPreviousPage.IsEnabled = false;
            bFirstPage.IsEnabled = false;
            bUpdateInfo.IsEnabled = false;
            object o = Parser.ParseTxt();
            if (o as List<Threat> == null)
            {
                MessageBox.Show(o as string);
                List<object> answer = Parser.Update();
                o = answer[0];
                if (o as List<Threat> == null)
                {
                    MessageBox.Show(o as string);
                }
                else
                {
                    tList = o as List<Threat>;
                }
            }
            else
            {
                tList = o as List<Threat>;
            }
            UpdateButtons();  
            tView.ItemsSource = UpdateListPart();
        }

        private void Info(object sender, RoutedEventArgs e) // вывести справку
        {
            MessageBox.Show("Автор: Шоломов Даннил\n Скачанная таблица находится в папке программы");
        }

        private void Update(object sender, RoutedEventArgs e) // обновить данные
        {
            List<object> answer = Parser.Update();
            object o = answer[0];
            List<object> cList = answer[1] as List<object>;
            MessageBox.Show($"a{cList[0] as string}a{cList[1] as string}aa");
            changes = cList[0] as string;
            int.TryParse(cList[1] as string, out numberOfChanges);
            if (o as List<Threat> == null)
            {
                MessageBox.Show(o as string);
            }
            else
            {
                tList = o as List<Threat>;
                MessageBox.Show($"Обновлено {numberOfChanges} записей.");
                bUpdateInfo.IsEnabled = true;
            }
            tView.ItemsSource = UpdateListPart();
        }

        private void MoveToFirst(object sender, RoutedEventArgs e) // на первую страницу
        {
            pageNumber = 0;
            UpdateButtons();
            tView.ItemsSource = UpdateListPart();
        }

        private void MoveToNext(object sender, RoutedEventArgs e) // на следующую страницу
        {
            
            if (pageNumber != tList.Count / tOnPage) { pageNumber++; }
            UpdateButtons(); 
            tView.ItemsSource = UpdateListPart();
        }

        private void MoveToPrevious(object sender, RoutedEventArgs e) // на предыдущую страницу
        {
            
            if (pageNumber != 0) { pageNumber--; }
            UpdateButtons();
            
            tView.ItemsSource = UpdateListPart();
        }

        private void MoveToLast(object sender, RoutedEventArgs e) // на последнюю страницу
        {
            pageNumber = tList.Count/ tOnPage;
            UpdateButtons();
            tView.ItemsSource= UpdateListPart();
        }

        

        private void UpdateInfo(object sender, RoutedEventArgs e)// информация об обновлении
        {
            MessageBox.Show(changes);
        } 
        
        private List<Threat> UpdateListPart()// обновление показываемой части
        {
            List<Threat> tListPart = new List<Threat>();
            for (int i = 0; i < tOnPage; i++)
            {
                if ((i + tOnPage * pageNumber) <tList.Count)
                {
                    tListPart.Add(tList[i + tOnPage * pageNumber]);
                }
                else
                {
                    break;
                }
            }
            return tListPart;
        }

        private void UpdateButtons() // обновление кнопок
        {
            if(pageNumber == 0)
            {
                bPreviousPage.IsEnabled = false;
                bFirstPage.IsEnabled = false;
                bNextPage.IsEnabled = true;
                bLastPage.IsEnabled = true;
            }
            else if(pageNumber == tList.Count / tOnPage)
            {
                bPreviousPage.IsEnabled = true;
                bFirstPage.IsEnabled = true;
                bNextPage.IsEnabled = false;
                bLastPage.IsEnabled = false;
            }
            else
            {
                bPreviousPage.IsEnabled = true;
                bFirstPage.IsEnabled = true;
                bNextPage.IsEnabled = true;
                bLastPage.IsEnabled = true;
            }
        }

        

        private void ShowFullInfo(object sender, RoutedEventArgs e)
        {
            string id = tbID.Text;
            if (int.TryParse(tbID.Text, out _))
            {
                id = "УБИ." + tbID.Text;
            }
            foreach(Threat t in tList)
            {
                if (t.id == id)
                {
                    MessageBox.Show(t.ToString());
                    return;
                }
            }
            MessageBox.Show("Идентификатор не существует!");
        }
    }
}
