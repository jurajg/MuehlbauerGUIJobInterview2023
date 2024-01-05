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
using System.Globalization;

namespace TaskEditor
{
    public enum TaskStatusEnum { todo, ongoing, done };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DependencyProperty IsHiddenRowAddTaskProperty = DependencyProperty.Register("IsHiddenRowAddTaskProperty", typeof(bool),typeof(MainWindow));

        public bool IsHiddenRowAddTask
        {
            get { return (bool)GetValue(IsHiddenRowAddTaskProperty); }
            set { SetValue(IsHiddenRowAddTaskProperty, value); }
        }
        
        DependencyProperty IsHiddenRowTasksTopPanelProperty = DependencyProperty.Register("IsHiddenRowTasksTopPanelProperty", typeof(bool), typeof(MainWindow));

        public bool IsHiddenRowTasksTopPanel
        {
            get { return (bool)GetValue(IsHiddenRowTasksTopPanelProperty); }
            set { SetValue(IsHiddenRowTasksTopPanelProperty, value); }
        }

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
            taskGrid.DataContext = this;
            IsHiddenRowTasksTopPanel = false;
            IsHiddenRowAddTask = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            taskModel.Save();
        }

        private void buttonAddTask_Click(object sender, RoutedEventArgs e)
        {
            taskModel.taskTable.CreateTask();
            dataGridTasks.Items.Refresh();

            IsHiddenRowTasksTopPanel = true;
            IsHiddenRowAddTask = false;
        }

        private void buttonAddTaskApply_Click(object sender, RoutedEventArgs e)
        {
            IsHiddenRowTasksTopPanel = false;
            IsHiddenRowAddTask = true;
        }

        private void buttonCancelAddTask_Click(object sender, RoutedEventArgs e)
        {
            IsHiddenRowTasksTopPanel = false;
            IsHiddenRowAddTask = true;
        }
    }

    // source: https://stackoverflow.com/questions/2502178/hide-grid-row-in-wpf
    [ValueConversion(typeof(bool), typeof(GridLength))]
    public class BoolToGridRowHeightAutoSizeConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("ASDASDSADS");
            //return new GridLength(1, GridUnitType.Star); // new GridLength(0);
            return ((bool)value == true) ? new GridLength(0) : new GridLength(1, GridUnitType.Auto);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
