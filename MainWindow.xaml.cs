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

        public TextBox ttbName;
        public ComboBox ccbBranch;
        public RadioButton rrbMale;

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

        public event PropertyChangedEventHandler? PropertyChanged;  //from INotifyPropertyChanged
        public string StudentData
        {
            get
            {
                return studentData;
            }
            set
            {
                studentData = value;
                OnPropertyChanged(nameof(StudentData));
            }
        }

        Student newStudent;
        public Student NewStudent
        {
            get => newStudent;
            set
            {
                newStudent = value;
                OnPropertyChanged(nameof(NewStudent));
            }
        }

        Student editStudent;
        public Student EditStudent
        {
            get => editStudent;
            set
            {
                editStudent = value;
                OnPropertyChanged(nameof(EditStudent));
            }
        }

        public MainWindow()
        {
            EditStudent = new Student("Subin David", Branch.ComputerScience, Gender.Male);
            InitializeComponent();
  
            //InitBinding();
            DataContext = this;



            //StudentView studentView = new StudentView();
            //StudentGridMain.Children.Add(studentView);

            ttbName = StudentViewUserControl.TBName;
            ccbBranch = StudentViewUserControl.CBBranch;
            rrbMale = StudentViewUserControl.RBMale;
            
        }

        protected void OnPropertyChanged(string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
            var name = ttbName.Text;
            var branch = ccbBranch.Text;
            var gender = (bool)rrbMale.IsChecked!?"Male" : "Female";

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


            //EditStudent = new Student("Dumm", Branch.None, Gender.Female);  //2 way checking

            StudentListBox.Items.Add(EditStudent);
            //StudentListBox.Items.Add(Students.Last().ToString());  //instead add using EditStudent
            //StudentListView.Items.Add(Students.Last().ToString());  //instead do binding
        }

        Command _addNewStudent;
        public ICommand AddStudentCommand
        {
            get
            {
                _addNewStudent ??= new Command(this.AddNewStudent, this.CanAddNewStudent);
                return _addNewStudent;
            }
        }

        public void AddNewStudent(object parameter)
        {
            NewStudent = new Student() { Name=EditStudent.Name, StudentBranch = EditStudent.StudentBranch, StudentGender=EditStudent.StudentGender };
            
            StudentListBox.Items.Add(EditStudent);
            var name = ttbName.Text;
            var branch = ccbBranch.Text;
            var gender = (bool)rrbMale.IsChecked! ? "Male" : "Female";

            //tbInputData.Text = name +" "+branch +" "+gender;  //instead do binding
            StudentData = branch;

            object? br;
            object? gen;

            branch = branch.Replace(" ", "");

            Enum.TryParse(typeof(Branch), branch, out br);
            Enum.TryParse(typeof(Gender), gender, out gen);

            Students.Add(new Student()
            {
                Name = name,
                StudentBranch = (Branch)br,
                StudentGender = (Gender)gen
            });

        }

        public bool CanAddNewStudent(object parameter)
        {
            return EditStudent.StudentBranch != Branch.None;
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