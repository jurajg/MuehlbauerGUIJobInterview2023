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

        DependencyProperty RowHeightProperty = DependencyProperty.Register("RowHeightProperty", typeof(GridLength), typeof(MainWindow),
            new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });
        public GridLength RowHeight
        {
            get { return new GridLength(10); Debug.WriteLine("ASDDDASDAD"); }
            set { SetValue(RowHeightProperty, value); }
        }


        TaskModelLib.TaskModel taskModel;
        /*
        bool _IsHiddenRowAddTask = false;
        public bool IsHiddenRowAddTask {
            get
            {
                return _IsHiddenRowAddTask;
            }
            set
            {
                _IsHiddenRowAddTask = value;
            }
        }
        */
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
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            taskModel.Save();
        }

        private void buttonAddTask_Click(object sender, RoutedEventArgs e)
        {
            taskModel.taskTable.CreateTask();
            dataGridTasks.Items.Refresh();

            IsHiddenRowAddTask = !IsHiddenRowAddTask;
            RowHeight = new GridLength(20);
        }

        

    }

    // source: https://stackoverflow.com/questions/2502178/hide-grid-row-in-wpf
    [ValueConversion(typeof(bool), typeof(GridLength))]
    public class BoolToGridRowHeightConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("ASDASDSADS");
            //return new GridLength(1, GridUnitType.Star); // new GridLength(0);
            return ((bool)value == true) ? new GridLength(1, GridUnitType.Auto) : new GridLength(0);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
