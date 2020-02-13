using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGT.File
{
    public class FileWriteArgs
    {
        string fileName;
        public string FileName 
        { 
            get { return fileName; }
            set
            {
                fileName = value;
                UpdateFullFilePath();
            }
        }

        string folderPath;
        public string FolderPath
        {
            get { return folderPath; }
            set
            {
                folderPath = value;
                UpdateFullFilePath();
            }
        }

        string fullFilePath;
        /// <summary>
        /// The folder path with the file name (and extension) at the end.
        /// </summary>
        public string FullFilePath 
        {
            get { return fullFilePath; }
        }

        string fileExtension;
        
        public string FileExtension 
        { 
            get { return fileExtension; }
            set
            {
                fileExtension = value;
                UpdateFullFilePath();
            }
        }

        /// <summary>
        /// May throw ArgumentException upon being provided an invalid file name or path.
        /// </summary>
        public FileWriteArgs(string fileName = "", string folderPath = "", string fileExtension = "txt")
        {
            this.fileName = fileName;
            this.folderPath = folderPath;
            this.fileExtension = fileExtension;
            UpdateFullFilePath();
        }

        void UpdateFullFilePath()
        {
            fullFilePath = string.Concat(FolderPath, FileName, dot, FileExtension);
        }

        const string dot = ".";

 

    }
}
