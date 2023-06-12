using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;

namespace ZIGapp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Book> Books { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Books = new ObservableCollection<Book>
            {
                new Book { Title = "Przykładowa książka 1", Author = "Jan Kowalski", Year = 2001 },
                new Book { Title = "Przykładowa książka 2", Author = "Anna Nowak", Year = 2010 },
                new Book { Title = "Przykładowa książka 3", Author = "Piotr Wiśniewski", Year = 2009 },
                new Book { Title = "Przykładowa książka 4", Author = "Adam Koc", Year = 2003 },
                new Book { Title = "Przykładowa książka 5", Author = "Zygmunt Dzwon", Year = 2023 }
            };
            BooksDataGrid.ItemsSource = Books;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newBookWindow = new NewBookWindow();
            if (newBookWindow.ShowDialog() == true)
            {
                Books.Add(newBookWindow.Book);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book book)
            {
                Books.Remove(book);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book book)
            {
                var newBookWindow = new NewBookWindow(book);
                if (newBookWindow.ShowDialog() == true)
                {
                    Books.Remove(book);
                    Books.Add(newBookWindow.Book);
                }
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(
                    File.ReadAllText(openFileDialog.FileName)
                );

                foreach (var book in books)
                {
                    this.Books.Add(book);
                }
            }
        }
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
    }
}
