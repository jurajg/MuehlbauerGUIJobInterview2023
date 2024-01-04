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
    public enum TaskStatusEnum { todo, ongoing, done };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaskModelLib.TaskModel taskModel;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            taskModel = new();
            taskModel.Load();
            dataGridPersons.ItemsSource = taskModel.personTable.data;
            dataGridTasks.ItemsSource = taskModel.taskTable.data;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            taskModel.Save();
        }
    }
}
