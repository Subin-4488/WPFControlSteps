using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WPFControlSteps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Student>  students = new ObservableCollection<Student>();
        public ObservableCollection<Student> Students { 
            get => students; 
            set 
            { 
                students = value;
                OnPropertyChanged(nameof(Students)); 
            } 
        }

        public Branch branch {  get; set; }

        private string studentData = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string StudentData
        {
            get
            {
                return studentData;
            }
            set
            {
                studentData = value;
                Console.WriteLine(studentData);
                OnPropertyChanged(nameof(StudentData));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            //InitBinding();
            DataContext = this;
        }

        protected void OnPropertyChanged([CallerMemberName]string? name = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //private void InitBinding()
        //{
        //    Binding binding = new Binding();
        //    binding.Source = this;
        //    binding.Mode = BindingMode.OneWay;
        //    binding.Path = new PropertyPath("StudentData");
        //    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        //    tbInputData.SetBinding(TextBlock.TextProperty, binding);  //Binding UI to variable
        //}

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            var name = tbName.Text;
            var branch = cbBranch.Text;
            var gender = (bool)rbMale.IsChecked!?"Male" : "Female";

            //tbInputData.Text = name +" "+branch +" "+gender;  //instead do binding
            StudentData = branch;

            object? br;
            object? gen;

            branch = branch.Replace(" ", "");

            Enum.TryParse(typeof(Branch), branch, out br);
            Enum.TryParse(typeof(Gender), gender, out gen);

            Students.Add(new Student() { 
                Name = name,
                StudentBranch = (Branch)br,
                StudentGender = (Gender)gen });
            
            StudentListBox.Items.Add(Students.Last().ToString());
            //StudentListView.Items.Add(Students.Last().ToString());  //instead do binding
        }

        private void StudentListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                ListBox studentListBox = sender as ListBox;
                tbInputData.Text = studentListBox!.SelectedItem.ToString();
            }
        }
    }
}
