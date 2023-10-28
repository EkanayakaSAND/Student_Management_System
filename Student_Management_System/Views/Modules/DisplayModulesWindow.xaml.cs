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

namespace Student_Management_System.Views.Modules
{
    /// <summary>
    /// Interaction logic for DisplayModulesWindow.xaml
    /// </summary>
    public partial class DisplayModulesWindow : Window
    {
        public DisplayModulesWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
