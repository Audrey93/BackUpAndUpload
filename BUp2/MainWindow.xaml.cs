using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;



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
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using System.Diagnostics;

namespace BUp2
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //shut down app
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //nav menu animation
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        //read task from xml file
        internal void ReadXml(ref Dictionary<string, UploadFolderTask> d1, ref Dictionary<string, BackupAndUploadTask> d2)
        {
            XmlReader reader = XmlReader.Create(@"C:\Users\Fujitsu E751\source\repos\BUp2\TasksConfig.xml");

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "UploadFolderTask"))
                {
                    List<string> ListAtribut = new List<string> { };

                    while (reader.Read() && (reader.Name != "UploadFolderTask"))
                    {
                        if (reader.GetAttribute("Value") != null) ListAtribut.Add(reader.GetAttribute("Value"));
                    }

                    string dropBoxFolderName = ListAtribut.ElementAt(0);
                    int dropboxClear = int.Parse(ListAtribut.ElementAt(1));
                    string folderPath = ListAtribut.ElementAt(2);
                    string name = ListAtribut.ElementAt(3);
                    bool taskActive = bool.Parse(ListAtribut.ElementAt(4));
                    string taskStart = ListAtribut.ElementAt(5);
                    string taskEnd = ListAtribut.ElementAt(6);
                    int taskRepeat = int.Parse(ListAtribut.ElementAt(7));
                    string taskLast = ListAtribut.ElementAt(8);
                    d1.Add(name, new UploadFolderTask(dropBoxFolderName, dropboxClear, folderPath, name, taskActive, taskStart, taskEnd, taskRepeat, taskLast));

                }


                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "BackupAndUploadTask"))
                {
                    List<string> ListAtribut = new List<string> { };

                    while (reader.Read() && (reader.Name != "BackupAndUploadTask"))
                    {
                        if (reader.GetAttribute("Value") != null) ListAtribut.Add(reader.GetAttribute("Value"));
                    }

                    string dropBoxFolderName = ListAtribut.ElementAt(0);
                    int dropboxClear = int.Parse(ListAtribut.ElementAt(1));
                    string databaseType = ListAtribut.ElementAt(2);
                    string sqlConnectionString = ListAtribut.ElementAt(3);
                    string sqlDatabaseName = ListAtribut.ElementAt(4);
                    string backupsFolder = ListAtribut.ElementAt(5);
                    string remoteFolder = ListAtribut.ElementAt(6);
                    string localFolder = ListAtribut.ElementAt(7);
                    string backupFolder1 = ListAtribut.ElementAt(8);
                    string backupFolder2 = ListAtribut.ElementAt(9);
                    int clearBackupsFolder = int.Parse(ListAtribut.ElementAt(10));
                    int clearRemoteFolder = int.Parse(ListAtribut.ElementAt(11));
                    int clearLocalFolder = int.Parse(ListAtribut.ElementAt(12));
                    int clearBackupFolder1 = int.Parse(ListAtribut.ElementAt(13));
                    int clearBackupFolder2 = int.Parse(ListAtribut.ElementAt(14));
                    string name = ListAtribut.ElementAt(15);
                    bool taskActive = bool.Parse(ListAtribut.ElementAt(16));
                    string taskStart = ListAtribut.ElementAt(17);
                    string taskEnd = ListAtribut.ElementAt(18);
                    int taskRepeat = int.Parse(ListAtribut.ElementAt(19));
                    string taskLast = ListAtribut.ElementAt(20);
                    d2.Add(name, new BackupAndUploadTask(dropBoxFolderName, dropboxClear, databaseType, sqlConnectionString, sqlDatabaseName, backupsFolder, remoteFolder,
                    localFolder, backupFolder1, backupFolder2, clearBackupsFolder, clearRemoteFolder, clearLocalFolder,
                    clearBackupFolder1, clearBackupFolder2, name, taskActive, taskStart, taskEnd, taskRepeat, taskLast));

                }
            }
        }

        // read task from Dictionary
        internal void ReadTask(ref Dictionary<string, UploadFolderTask> d1, ref Dictionary<string, BackupAndUploadTask> d2)
        {
            foreach (UploadFolderTask task in d1.Values)
            {
                Color color = (Color)ColorConverter.ConvertFromString("#fc00ff");
                TextBlock nameTask = new TextBlock();
                PackIcon packIcon = new PackIcon();
                StackPanel stackPanel = new StackPanel();
                nameTask.Text = task.Name;
                nameTask.Width = 200;
                nameTask.FontWeight = FontWeights.Bold;
                nameTask.VerticalAlignment = VerticalAlignment.Center;
                packIcon.Kind = PackIconKind.FolderUpload;
                packIcon.Width = 40;
                packIcon.Height = 40;
                packIcon.Foreground = new SolidColorBrush(color);
                packIcon.VerticalAlignment = VerticalAlignment.Center;
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Children.Add(packIcon);
                stackPanel.Children.Add(nameTask);

                // start and end task 
                StackPanel date = new StackPanel();
                date.VerticalAlignment = VerticalAlignment.Center;
                date.Margin = new Thickness(100, 0, 10, 0);
                TextBlock startTask = new TextBlock();
                TextBlock endTask = new TextBlock();
                startTask.FontWeight = FontWeights.ExtraLight;
                endTask.FontWeight = FontWeights.ExtraLight;
                startTask.Width = 200;
                endTask.Width = 200;
                startTask.Text = "Task start: " + task.TaskStart;
                endTask.Text = "Task end:  " + task.TaskEnd;
                date.Orientation = Orientation.Vertical;
                date.Children.Add(startTask);
                date.Children.Add(endTask);
                stackPanel.Children.Add(date);



                // delete and update option
                PackIcon delete = new PackIcon();
                PackIcon update = new PackIcon();
                delete.Kind = PackIconKind.Delete;
                delete.HorizontalAlignment = HorizontalAlignment.Right;
                delete.VerticalAlignment = VerticalAlignment.Center;
                delete.Foreground = new SolidColorBrush(color);
                update.Kind = PackIconKind.TableEdit;
                update.HorizontalAlignment = HorizontalAlignment.Right;
                update.VerticalAlignment = VerticalAlignment.Center;
                update.Foreground = new SolidColorBrush(color);
                update.Width = 30;
                delete.Width = 30;
                update.Height = 30;
                delete.Height = 30;
                StackPanel stackPnl = new StackPanel();
                stackPnl.Children.Add(delete);
                System.Windows.Controls.Button bt = new System.Windows.Controls.Button();
                bt.Content = stackPnl;
                StackPanel stackPn2 = new StackPanel();
                stackPn2.Children.Add(update);
                System.Windows.Controls.Button bt2 = new System.Windows.Controls.Button();
                bt2.Content = stackPn2;
                bt.Background = bt2.Background = null;
                bt.BorderBrush = bt2.BorderBrush = null;
                bt.Click += (s, f) => { Delete(task.Name, "UploadFolderTask"); };

                //buttons.Children.Add(delete);
                //buttons.Children.Add(update);
                stackPanel.Children.Add(bt);
                stackPanel.Children.Add(bt2);

                // stackPanel add in list item
                ListTask.Items.Add(stackPanel);

            }

            
            //d2.Remove("BackupAndUpload-ViseBaza-NIBIS");
            foreach (BackupAndUploadTask task in d2.Values)
            {

                Color color = (Color)ColorConverter.ConvertFromString("#fc00ff");
                TextBlock nameTask = new TextBlock();
                PackIcon packIcon = new PackIcon();
                StackPanel stackPanel = new StackPanel();
                nameTask.Text = task.Name;
                nameTask.Width = 200;
                nameTask.FontWeight = FontWeights.Bold;
                nameTask.VerticalAlignment = VerticalAlignment.Center;
                packIcon.Kind = PackIconKind.Database;
                packIcon.Width = 40;
                packIcon.Height = 40;
                packIcon.Foreground = new SolidColorBrush(color);
                packIcon.VerticalAlignment = VerticalAlignment.Center;
                stackPanel.Orientation = System.Windows.Controls.Orientation.Horizontal;
                stackPanel.Children.Add(packIcon);
                stackPanel.Children.Add(nameTask);

                // start and end task 
                StackPanel date = new StackPanel();
                date.VerticalAlignment = VerticalAlignment.Center;
                date.Margin = new Thickness(100, 0, 10, 0);
                TextBlock startTask = new TextBlock();
                TextBlock endTask = new TextBlock();
                startTask.FontWeight = FontWeights.ExtraLight;
                endTask.FontWeight = FontWeights.ExtraLight;
                startTask.Width = 200;
                endTask.Width = 200;
                startTask.Text = "Task start: " + task.TaskStart;
                endTask.Text = "Task end:  " + task.TaskEnd;
                date.Orientation = System.Windows.Controls.Orientation.Vertical;
                date.Children.Add(startTask);
                date.Children.Add(endTask);
                stackPanel.Children.Add(date);
                
               

                // delete and update option
                PackIcon delete = new PackIcon();
                PackIcon update = new PackIcon();
                delete.Kind = PackIconKind.Delete;
                delete.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                delete.VerticalAlignment = VerticalAlignment.Center;
                delete.Foreground = new System.Windows.Media.SolidColorBrush(color);
                update.Kind = PackIconKind.TableEdit;
                update.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                update.VerticalAlignment = VerticalAlignment.Center;
                update.Foreground = new System.Windows.Media.SolidColorBrush(color);  
                update.Width = 30;
                delete.Width = 30;
                update.Height = 30;
                delete.Height = 30;
                StackPanel stackPnl = new StackPanel();
                stackPnl.Children.Add(delete);
                Button bt = new Button();
                bt.Content = stackPnl;
                StackPanel stackPn2 = new StackPanel();
                stackPn2.Children.Add(update);
                System.Windows.Controls.Button bt2 = new System.Windows.Controls.Button();
                bt2.Content = stackPn2;
                bt.Background = bt2.Background = null;
                bt.BorderBrush = bt2.BorderBrush = null;
                string novi = task.Name;
                bt.Click += (s, f) => { Delete(task.Name, "BackupAndUploadTask"); };
    


                //buttons.Children.Add(delete);
                //buttons.Children.Add(update);
                stackPanel.Children.Add(bt);
                stackPanel.Children.Add(bt2);
                
                // stackPanel add in list item
                ListTask.Items.Add(stackPanel);
            }

            Console.ReadLine();  
        }

        // delete task
        private void Delete(string name, string taskType)
        {
            XDocument doc = XDocument.Load(@"C:\Users\Fujitsu E751\source\repos\BUp2\TasksConfig.xml");
            XElement root = doc.Root;
            foreach (XElement BackupAndUploadTask1 in root.Elements(taskType))
            {
                if (BackupAndUploadTask1.Element("Name").Attribute("Value").Value == name)
                    BackupAndUploadTask1.Remove();
            }
            doc.Save(@"C:\Users\Fujitsu E751\source\repos\BUp2\TasksConfig.xml");
            MessageBox.Show(name + " uspješno izbrisan!");
            ListTask.Items.Clear();
            Dictionary<string, UploadFolderTask> d1 = new Dictionary<string, UploadFolderTask>();
            Dictionary<string, BackupAndUploadTask> d2 = new Dictionary<string, BackupAndUploadTask>();
            ReadXml(ref d1, ref d2);
            ReadTask(ref d1, ref d2);

        }

        //read and view task
        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            logView.Visibility = Visibility.Collapsed;
            ListTask1.Visibility = Visibility.Collapsed;
            ListTask.Items.Clear();
            ListTask.Visibility = Visibility.Visible;
            logReader.Visibility = Visibility.Collapsed;


            Dictionary<string, UploadFolderTask> d1 = new Dictionary<string, UploadFolderTask>();
            Dictionary<string, BackupAndUploadTask> d2 = new Dictionary<string, BackupAndUploadTask>();
            ReadXml(ref d1,ref d2);
            ReadTask(ref d1, ref d2);
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            ListTask1.Visibility = Visibility.Visible;
            ListTask.Visibility = Visibility.Collapsed;
            logView.Visibility = Visibility.Collapsed;
            logReader.Visibility = Visibility.Collapsed;
           
        }

        //directory info about .log files
        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            ListTask1.Visibility = Visibility.Collapsed;
            ListTask.Visibility = Visibility.Collapsed;
            logView.Visibility = Visibility.Visible;
            logReader.Visibility = Visibility.Collapsed;
            logView.Items.Clear();
            Color color = (Color)ColorConverter.ConvertFromString("#fc00ff");
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C: \Users\Fujitsu E751\source\repos\BUp2\Logs");
            if (directoryInfo != null)
            {
                foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = fileInfo.Name;
                    textBlock.FontWeight = FontWeights.Bold;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;

                    PackIcon folder = new PackIcon();
                    folder.Kind = PackIconKind.Folder;
                    folder.Width = 30;
                    folder.Height = 30;
                    folder.Foreground = new System.Windows.Media.SolidColorBrush(color);
                    folder.Margin = new Thickness(0, 0, 10, 10);
                    folder.VerticalAlignment = VerticalAlignment.Center;

                    PackIcon open = new PackIcon();
                    open.Kind = PackIconKind.FolderOpen;
                    open.Width = 30;
                    open.Height = 30;
                    open.Foreground = new System.Windows.Media.SolidColorBrush(color);

                    StackPanel stackPnl = new StackPanel();
                    stackPnl.Children.Add(open);

                    Button button = new Button();
                    button.Margin = new Thickness(350, 0, 10, 0);
                    button.Content = stackPnl;
                    button.Background = null;
                    button.BorderBrush = null;
                    button.Click += (s, f) => { Funct( fileInfo.FullName); };

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    stackPanel.Children.Add(folder);
                    stackPanel.Children.Add(textBlock);
                    stackPanel.Children.Add(button);

                    logView.Items.Add(stackPanel);
                }
                    

            }
        }

        //read .log files
        private void Funct(object text2)
        {
            logReader.Visibility = Visibility.Visible;
            logReader.Text = null;
            try
            {  

                using (StreamReader sr = new StreamReader(text2.ToString()))
                {
 
                    String line = sr.ReadToEnd();
                    logReader.Text = line;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            XDocument xDocument = XDocument.Load(@"C:\Users\Fujitsu E751\source\repos\BUp2\TasksConfig.xml");
            XElement root = new XElement("UploadFolderTask");

            root.Add(new XElement("DropBoxFolderName", new XAttribute("Value", DropBoxFolderName.Text )));
            root.Add(new XElement("DropboxClear", new XAttribute("Value", DropboxClear.Text)));
            root.Add(new XElement("FolderPath", new XAttribute("Value", FolderPath.Text)));
            root.Add(new XElement("Name", new XAttribute("Value", Name.Text)));
            root.Add(new XElement("TaskActive", new XAttribute("Value", Active.IsChecked)));
            root.Add(new XElement("TaskStart", new XAttribute("Value", TaskStartD.Text +" " + TaskStartT.Text)));
            root.Add(new XElement("TaskEnd", new XAttribute("Value", TaskEndD.Text + " " + TaskEndT.Text)));
            root.Add(new XElement("TaskRepeat", new XAttribute("Value", TaskRepeat.Text)));
            root.Add(new XElement("TaskLast", new XAttribute("Value", TaskLastD.Text +" " +TaskLastT.Text)));

            xDocument.Element("TasksConfig").Add(root);
            xDocument.Save(@"C:\Users\Fujitsu E751\source\repos\BUp2\TasksConfig.xml");
            MessageBox.Show("Task uspješno dodan!");
            ListTask.Items.Clear();
            ListTask.Visibility = Visibility.Visible;
            ListTask1.Visibility = Visibility.Collapsed;


            Dictionary<string, UploadFolderTask> d1 = new Dictionary<string, UploadFolderTask>();
            Dictionary<string, BackupAndUploadTask> d2 = new Dictionary<string, BackupAndUploadTask>();
            ReadXml(ref d1, ref d2);
            ReadTask(ref d1, ref d2);

        }
    }
}
