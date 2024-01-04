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

        private void dataGridTasks_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Cancel)
            {
                //e.Cancel = false;
                return;
            }

            if (e.EditAction == DataGridEditAction.Commit)
            {
                DataGridRow dgr = e.Row;
                Debug.WriteLine($"{e.Row.Item}");
                if( ((TaskModelLib.Task)e.Row.Item).Id==0) {
                    ((TaskModelLib.Task)e.Row.Item).Id = taskModel.taskTable.CreateUniqueId();
                }
            }
        }

        private void dataGridTasks_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            /*Debug.WriteLine($"{e.NewItem}");
            e.NewItem = taskModel.taskTable.CreateTask(); // TODO creates duplicate row
            dataGridTasks.Items.Refresh();*/
        }

        private void dataGridTasks_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            Debug.WriteLine($"PreparingCellForEdit {e.Row.Item}");
            if (((TaskModelLib.Task)e.Row.Item).Id == 0)
            {
                ((TaskModelLib.Task)e.Row.Item).Id = taskModel.taskTable.CreateUniqueId();
                Debug.WriteLine($"PreparingCellForEdit {((TaskModelLib.Task)e.Row.Item).Id }");
            }
        }
    }
}
