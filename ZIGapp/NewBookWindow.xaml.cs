using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using static System.Reflection.Metadata.BlobBuilder;


namespace ZIGapp
{
    public partial class NewBookWindow : Window
    {
        public Book Book { get; private set; }

        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();


        public NewBookWindow(Book book = null)
        {
            InitializeComponent();

            if (book != null)
            {
                TitleTextBox.Text = book.Title;
                AuthorTextBox.Text = book.Author;
                YearTextBox.Text = book.Year.ToString();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int year;
            if (string.IsNullOrEmpty(TitleTextBox.Text) || string.IsNullOrEmpty(AuthorTextBox.Text) ||
                string.IsNullOrEmpty(YearTextBox.Text) || !int.TryParse(YearTextBox.Text, out year))
            {
                MessageBox.Show("Proszę poprawnie wypełnić wszystkie pola!");
                return;
            }

            Book = new Book { Title = TitleTextBox.Text, Author = AuthorTextBox.Text, Year = year };
            DialogResult = true;
        }
        
    }
}
