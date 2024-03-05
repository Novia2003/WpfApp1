using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryClasses;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private LibraryBook? _book;
        public MainWindow()
        {
            InitializeComponent();
            foreach (BookState state in Enum.GetValues(typeof(BookState)))
            {
                CurrentState.Items.Add(state);
                ChangedState.Items.Add(state);
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            BookState _selectedState;

            if (CurrentState.SelectedItem == null)
            {
                MessageBox.Show("Specify the current state");
                return;
            }
            else 
            {
                string selectedStateText = CurrentState.SelectedItem.ToString();

                if (!Enum.TryParse(selectedStateText, out _selectedState))
                {
                    throw new InvalidOperationException("The selected value could not be converted to BookState.");
                }
            }


            try 
            {
                _book = new LibraryBook(
                    LibraryCode.Text,
                    Title.Text,
                    Authors.Text,
                    Convert.ToInt32(PageCount.Text),
                    ThematicSection.Text,
                    _selectedState
                    );
                MessageBox.Show(_book.ToString());

                LibraryCode.Text = "";
                Title.Text = "";
                Authors.Text = "";
                PageCount.Text = "";
                ThematicSection.Text = "";
                CurrentState.Text = "";
            }
            catch (ArgumentException exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect values have been entered for numeric fields.");
            }


            RefreshBookLabel();
        }

        private void ChangeCodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_book == null)
            {
                MessageBox.Show("You cannot perform actions with a book until you create it");
                return;
            }

            _book.ChangeLibraryCode(ChangedLibraryCode.Text);
            ChangedLibraryCode.Text = "";
            RefreshBookLabel();
        }

        private void ChangeStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_book == null)
            {
                MessageBox.Show("You cannot perform actions with a book until you create it");
                return;
            }

            BookState _selectedState;

            if (ChangedState.SelectedItem == null)
            {
                MessageBox.Show("Specify the current state");
                return;
            }
            else
            {
                string selectedStateText = ChangedState.SelectedItem.ToString();

                if (!Enum.TryParse(selectedStateText, out _selectedState))
                {
                    throw new InvalidOperationException("The selected value could not be converted to BookState.");
                }
            }

            _book.ChangeState(_selectedState);
            ChangedState.Text = "";
            RefreshBookLabel();
        }

        private void RefreshBookLabel()
        {
            if (_book != null)
                BookLabel.Text = _book.ToString();
        }
    }
}