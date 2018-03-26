using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUp2
{

    class Task
    {
        public string DropBoxFolderName { get; set; }
        public int DropboxClear { get; set; }
        public string Name { get; set; }
        public bool TaskActive { get; set; }
        public string TaskStart { get; set; }
        public string TaskEnd { get; set; }
        public int TaskRepeat { get; set; }
        public string TaskLast { get; set; }

        public Task(string dropBoxFolderName, int dropboxClear, string name, bool taskActive, string taskStart, string taskEnd, int taskRepeat, string taskLast)
        {
            DropBoxFolderName = dropBoxFolderName;
            DropboxClear = dropboxClear;
            Name = name;
            TaskActive = taskActive;
            TaskStart = taskStart;
            TaskEnd = taskEnd;
            TaskRepeat = taskRepeat;
            TaskLast = taskLast;
        }

    }

    class UploadFolderTask : Task
    {
        public string FolderPath { get; private set; }


        public UploadFolderTask(string dropBoxFolderName, int dropboxClear, string folderPath, string name, bool taskActive, string taskStart, string taskEnd, int taskRepeat, string taskLast) : base(dropBoxFolderName, dropboxClear, name, taskActive, taskStart, taskEnd, taskRepeat, taskLast)
        {
            this.FolderPath = folderPath;
        }

        public void PrintTask()
        {
            Console.WriteLine("Task name: " + Name);
            if (TaskActive == true) Console.WriteLine("Task is active");
            Console.WriteLine("Task start: " + TaskStart);
            Console.WriteLine("Task end: " + TaskEnd + "\n");
        }
    }

    class BackupAndUploadTask : Task
    {
        public string DatabaseType { get; set; }
        public string SqlConnectionString { get; set; }
        public string SqlDatabaseName { get; set; }
        public string BackupsFolder { get; set; }
        public string RemoteFolder { get; set; }
        public string LocalFolder { get; set; }
        public string BackupFolder1 { get; set; }
        public string BackupFolder2 { get; set; }
        public int ClearBackupsFolder { get; set; }
        public int ClearRemoteFolder { get; set; }
        public int ClearLocalFolder { get; set; }
        public int ClearBackupFolder1 { get; set; }
        public int ClearBackupFolder2 { get; set; }

        public BackupAndUploadTask(string dropBoxFolderName, int dropboxClear, string databaseType, string sqlConnectionString, string sqlDatabaseName, string backupsFolder, string remoteFolder,
            string localFolder, string backupFolder1, string backupFolder2, int clearBackupsFolder, int clearRemoteFolder, int clearLocalFolder,
            int clearBackupFolder1, int clearBackupFolder2, string name, bool taskActive, string taskStart, string taskEnd, int taskRepeat, string taskLast)
            : base(dropBoxFolderName, dropboxClear, name, taskActive, taskStart, taskEnd, taskRepeat, taskLast)
        {
            DatabaseType = databaseType;
            SqlConnectionString = sqlConnectionString;
            SqlDatabaseName = sqlDatabaseName;
            BackupsFolder = backupsFolder;
            RemoteFolder = remoteFolder;
            LocalFolder = localFolder;
            BackupFolder1 = backupFolder1;
            BackupFolder2 = backupFolder2;
            ClearBackupsFolder = clearBackupsFolder;
            ClearRemoteFolder = clearRemoteFolder;
            ClearLocalFolder = clearLocalFolder;
            ClearBackupFolder1 = clearBackupFolder1;
            ClearBackupFolder2 = clearBackupFolder2;
        }
        public void PrintTask()
        {
            Console.WriteLine("Task name: " + Name);
            if (TaskActive == true) Console.WriteLine("Task is active");
            Console.WriteLine("Task start: " + TaskStart);
            Console.WriteLine("Task end: " + TaskEnd + "\n");
        }
    }
}
