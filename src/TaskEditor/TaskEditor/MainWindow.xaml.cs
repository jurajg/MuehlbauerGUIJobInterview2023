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
        public TaskModelLib.TaskModel taskModel;
        private TaskModelLib.Task addedTask;
        private TaskModelLib.Person selectedPerson;


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
            personGrid.DataContext = this;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            taskModel.Save();
        }

        private void buttonAddTask_Click(object sender, RoutedEventArgs e)
        {
            addedTask = taskModel.taskTable.CreateTask();
            dataGridTasks.Items.Refresh();

            textTaskName.Text = addedTask.Name;
            textTaskDescription.Text = addedTask.Description;
            datePickerStartDate.DisplayDate = addedTask.StartDate;
            datePickerDueDate.DisplayDate = addedTask.DueDate;
            // TODO: combobox responsible person
        }

        private void buttonApplyTask_Click(object sender, RoutedEventArgs e)
        {
            TaskModelLib.Task t = (TaskModelLib.Task)dataGridTasks.SelectedValue;
            if (t == null) return;

            Debug.WriteLine(dataGridTasks.SelectedValue);

            t.Name = textTaskName.Text;
            t.Description = textTaskDescription.Text;
            t.StartDate = datePickerStartDate.DisplayDate;
            t.DueDate = datePickerDueDate.DisplayDate;
            // TODO: combobox responsible person

            dataGridTasks.Items.Refresh();
        }

        private void buttonDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            TaskModelLib.Task task = null;
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    task = (TaskModelLib.Task)row.Item;
                    break;
                }
            }
            if (task == null) return;
            
            MessageBoxResult mbResult = MessageBox.Show($"Are you sure to delete task named '{task.Name}'?", "Deleting Task", MessageBoxButton.YesNo);
            if (mbResult == MessageBoxResult.Yes)
            {
                taskModel.taskTable.DeleteTask(task);
                dataGridTasks.ItemsSource = null;
                dataGridTasks.ItemsSource = taskModel.taskTable.data;
            }
        }


        private void buttonCancelAddTask_Click(object sender, RoutedEventArgs e)
        {
            taskModel.taskTable.DeleteTaskById(addedTask.Id);
            dataGridTasks.Items.Refresh();
        }

        private void SelectPerson(Person person)
        {
            selectedPerson = person;
            if (person == null)
            {
                textPersonName.Text = "";
                datePickerPersonBirthDay.DisplayDate = DateTime.Now;
                textPersonEmail.Text = "";

                textPersonName.IsEnabled = false;
                datePickerPersonBirthDay.IsEnabled = false;
                textPersonEmail.IsEnabled = false;
                return;
                
            }
            textPersonName.Text = person.Name;
            datePickerPersonBirthDay.SelectedDate = person.BirthDay;
            textPersonEmail.Text = person.Email;

            textPersonName.IsEnabled = true;
            datePickerPersonBirthDay.IsEnabled = true;
            textPersonEmail.IsEnabled = true;
        }

        private void buttonPersonAdd_Click(object sender, RoutedEventArgs e)
        {
            Person person = taskModel.personTable.CreatePerson();
            dataGridPersons.Items.Refresh();
            SelectPerson(person);
            dataGridPersons.SelectedItem = person;
        }

        private void buttonPersonApply_Click(object sender, RoutedEventArgs e)
        {
            //Person p = (TaskModelLib.Person)dataGridPersons.SelectedValue;
            Person p = selectedPerson;
            if (p == null) return;

            p.Name = textPersonName.Text;
            p.BirthDay = datePickerPersonBirthDay.SelectedDate ?? DateTime.Now;
            p.Email = textPersonEmail.Text;

            dataGridPersons.Items.Refresh();
        }

        private void buttonPersonDelete_Click(object sender, RoutedEventArgs e)
        {
            DeletePerson(selectedPerson);
            SelectPerson(null);
        }

            private void buttonPersonGridDelete_Click(object sender, RoutedEventArgs e)
        {
            Person person = null;
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    person = (Person)row.Item;
                    break;
                }
            }

            DeletePerson(person);
            SelectPerson(null);
        }

        private void DeletePerson(Person person)
        {
            if (person == null) return;
            MessageBoxResult mbResult = MessageBox.Show($"Are you sure to delete person named '{person.Name}'?", "Deleting Person", MessageBoxButton.YesNo);
            if (mbResult == MessageBoxResult.Yes)
            {
                taskModel.personTable.DeletePerson(person);
                dataGridPersons.ItemsSource = null;
                dataGridPersons.ItemsSource = taskModel.personTable.data;
            }
        }

        private void buttonDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            DeletePerson(selectedPerson);
        }

        private void dataGridTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            Debug.WriteLine($"tasks selection changed {dg.SelectedValue}");
            //
        }

        private void dataGridPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            selectedPerson = (Person)dg.SelectedItem;
            SelectPerson(selectedPerson);
        }
    }
}
