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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Windows.Markup;
using System.Xml;


namespace AddressBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filename = "store.json";

        string[] Passwords = new string[] { "asdasd", "f44Eas" };

        Store store;

        Grid tmp;

        Random random = new Random();


        public MainWindow()
        {
            InitializeComponent();

            tmp = TmpBookElement;

            Store_Grid.Visibility = Visibility.Hidden;
            Admin_Signin_Grid.Visibility = Visibility.Hidden;
            Admin_Grid.Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            load_store();

            ShowCategories();

            ShowAllBooks();

            Store_Grid.Visibility = Visibility.Visible;
            Main_Grid.Visibility = Visibility.Hidden;
        }

        void ShowCategories()
        {
            BookCategories.Items.Clear();

            ListBoxItem i = new ListBoxItem();
            i.Content = "All Categories";
            BookCategories.Items.Add(i);

            foreach (var c in store.categories)
            {
                i = new ListBoxItem();
                i.Content = c;
                BookCategories.Items.Add(i);
            }

            BookCategories.SelectedIndex = 0;
        }

        Grid NewBookItem()
        {
            string gridXaml = XamlWriter.Save(tmp);

            StringReader stringReader = new StringReader(gridXaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Grid newGrid = (Grid)XamlReader.Load(xmlReader);

            return newGrid;
        }

        void ShowOneBook(Book b)
        {
            var it = NewBookItem();

            foreach (var t in it.Children)
                if (t.GetType() == typeof(TextBlock))
                {
                    var q = (TextBlock)t;
                    q.Text = b.id+". "+b.title;
                } else
                    if (t.GetType() == typeof(Label)) 
                    {
                        var q = (Label)t;
                        q.Content = b.price + " $";
                    }

            BookList.Items.Add(it);
        }

        void ShowAllBooks()
        {
            BookList.Items.Clear();

            foreach (var b in store.books)
                ShowOneBook(b);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Incorrect_UsernameOrPassword.Visibility = Visibility.Hidden;
            TextBox_Username.Clear();
            TextBox_Password.Clear();
            Admin_Signin_Grid.Visibility = Visibility.Visible;
            Main_Grid.Visibility = Visibility.Hidden;
        }

        private void Admin_SI_Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Grid.Visibility = Visibility.Visible;
            Admin_Signin_Grid.Visibility = Visibility.Hidden;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main_Grid.Visibility = Visibility.Visible;
            Admin_Grid.Visibility = Visibility.Hidden;
        }

        private void Admin_Signin_Button_Click(object sender, RoutedEventArgs e)
        {
            string username = TextBox_Username.Text;
            string password = TextBox_Password.Password;

            if (username != "admin" || !Passwords.Contains(password))
            {
                Incorrect_UsernameOrPassword.Visibility = Visibility.Visible;
                return;
            }
            
            clear_new_book_values();

            load_store();

            Admin_Grid.Visibility = Visibility.Visible;
            Admin_Signin_Grid.Visibility = Visibility.Hidden;
        }

        void load_store()
        {
            bool ok = false;
            try
            {
                if (File.Exists(filename))
                {
                    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Store));
                    StreamReader sr = new StreamReader(filename);
                    store = (Store)js.ReadObject(sr.BaseStream);
                    sr.Close();

                    ok = true;
                }
            }
            catch(Exception ex)
            {
                // MessageBox.Show("" + ex);
            }

            if (!ok)
            {
                store = new Store();
            }

            if (store.books == null)
                store.books = new List<Book>();
        }

        void save_store()
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Store));
            StreamWriter sw = new StreamWriter(filename);
            js.WriteObject(sw.BaseStream, store);
            sw.Close();
        }

        void clear_new_book_values()
        {
            Book_Author.Text = "";
            Book_Category.Text = "";
            Book_Pages.Text = "";
            Book_Tags.Text = "";
            Book_Title.Text = "";
        }

        void add_new_book()
        {
            Book b = new Book();
            b.id = random.Next(100, 1000);
            b.author = Book_Author.Text;
            b.category = Book_Category.Text;
            b.price = int.Parse(Book_Pages.Text);
            b.tags = Book_Tags.Text.Split(new char[] { ',', ' ', ';' });
            b.title = Book_Title.Text;

            store.books.Add(b);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            add_new_book();
            clear_new_book_values();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            clear_new_book_values();
        }

        private void Button_Save_DB_Click(object sender, RoutedEventArgs e)
        {
            save_store();
        }
    }
}
