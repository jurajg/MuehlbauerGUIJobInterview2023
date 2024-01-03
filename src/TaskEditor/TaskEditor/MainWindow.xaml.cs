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
using TaskModelLib;
using CSVStorageLib;

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



            Debug.WriteLine(CSVStorage.GetStorageFolder());

            TaskModelLib.Task taskType = new();
            List<ICSVRow> result = CSVStorage.ReadCSV("D:\\test.csv", taskType);
            List<TaskModelLib.Task> tasks = new( result.Cast<TaskModelLib.Task>() );
            foreach (TaskModelLib.Task t in tasks)
            {
                Debug.WriteLine(t.StartDate.ToString());
            }

            //CSVStorage.WriteCSV("D:\\test.csv", result, taskType);

            TaskModelLib.TaskModel model = new();
            model.Load();


        }
    }
}
