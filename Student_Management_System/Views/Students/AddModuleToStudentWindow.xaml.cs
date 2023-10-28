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
using System.Windows.Shapes;

namespace Student_Management_System.Views.Students
{
    /// <summary>
    /// Interaction logic for AddModuleToStudentWindow.xaml
    /// </summary>
    public partial class AddModuleToStudentWindow : Window
    {
        public AddModuleToStudentWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
