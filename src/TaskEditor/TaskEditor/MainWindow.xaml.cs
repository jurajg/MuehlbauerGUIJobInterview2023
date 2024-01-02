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
using System.Diagnostics;
using TaskModel;
using CSVStorage;

namespace TaskEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            

            TaskModel.Task taskType = new();
            List<ICSVRow> result = CSVStorage.CSVStorage.ReadCSV("D:\\test.csv", taskType);
            List<TaskModel.Task> tasks = new List<TaskModel.Task>( result.Cast<TaskModel.Task>() );
            foreach (TaskModel.Task t in tasks)
            {
                Debug.WriteLine(t.StartDate.ToString());
            }

            //CSVStorage.CSVStorage.WriteCSV("D:\\test.csv", result, taskType);

        }
    }
}
