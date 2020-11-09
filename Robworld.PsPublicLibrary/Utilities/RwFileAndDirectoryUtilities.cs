using System.Collections.Generic;
using System.IO;
using Tecnomatix.Engineering;

namespace Robworld.PsPublicLibrary.Utilities
{
    /// <summary>
    /// Static methods for working with files and directories
    /// </summary>
    public static class RwFileAndDirectoryUtilities
    {
        #region Methods
       /// <summary>
        /// Get the extension of the file
        /// </summary>
        /// <param name="file">The file from which to get the extension</param>
        /// <returns>The extension of the file</returns>
        public static string GetFileExtension(string file)
        {
            return Path.GetExtension(file);
        }

        /// <summary>
        /// Get the filename without its extension
        /// </summary>
        /// <param name="file">The file from which to get the name without its extension</param>
        /// <returns>The filename without extension</returns>
        public static string GetFilenameWithoutExtension(string file)
        {
            return Path.GetFileNameWithoutExtension(file);
        }

        /// <summary>
        /// Get the filename with its extension
        /// </summary>
        /// <param name="file">The file from which to get the name with its extension</param>
        /// <returns>The filename with extension</returns>
        public static string GetFilenameWithExtension(string file)
        {
            return Path.GetFileName(file);
        }

        /// <summary>
        /// Changes the file extension
        /// </summary>
        /// <param name="file">The file with the current file extension</param>
        /// <param name="newExtension">The new file extension with or without the leadin dot</param>
        /// <returns>The file with the new extension </returns>
        public static string ChangeFileExtension(string file, string newExtension)
        {
            return Path.ChangeExtension(file, newExtension);
        }

        /// <summary>
        /// Check if the given file exists
        /// </summary>
        /// <param name="file">The file that should be tested</param>
        /// <returns>The boolean value of the testing result</returns>
        public static bool DoesFileExist(string file)
        {
            return File.Exists(file);
        }

        /// <summary>
        /// Combine path segments to one path
        /// </summary>
        /// <param name="path">The first part of the path</param>
        /// <param name="filename">The second part of the path or the filename</param>
        /// <returns>The combined path</returns>
        public static string CombinePathSegments(string path, string filename)
        {
            return Path.Combine(path, filename);
        }

        /// <summary>
        /// Combine path segments to one path
        /// </summary>
        /// <param name="basePath">The first part of the path</param>
        /// <param name="subPath">The second part of the path</param>
        /// <param name="filename">The third part of the path or the filename</param>
        /// <returns>The combined path</returns>
        public static string CombinePathSegments(string basePath, string subPath, string filename)
        {
            return Path.Combine(basePath, subPath, filename);
        }

        /// <summary>
        /// Check if the given directory exists
        /// </summary>
        /// <param name="path">The path to the directory that should be tested</param>
        /// <returns>The boolean value of the testing result</returns>
        public static bool DoesDirectoryExist(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// Get the directory name
        /// </summary>
        /// <param name="path">The path including the directory</param>
        /// <returns>The name of the directory</returns>
        public static string GetDirectory(string path)
        {
            return Path.GetDirectoryName(path);
        }

        /// <summary>
        /// Create a directory
        /// </summary>
        /// <param name="path">The path where the directory should be created</param>
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Use of the OpenFileDialog to select a single file
        /// </summary>
        /// <param name="dialogData">The parameters used in the OpenFileDialog</param>
        /// <returns>The selected single file</returns>
        public static string OpenFileDialog(RwOpenFileDialogCreationData dialogData)
        {
            string file = string.Empty;

            Microsoft.Win32.OpenFileDialog openFileDialog = OpenDialog(dialogData);
            if (openFileDialog.ShowDialog() == true)
            {
                file = openFileDialog.FileName;
            }

            return file;
        }

        /// <summary>
        /// Use of the OpenFileDialog to select multiple files
        /// </summary>
        /// <param name="dialogData">The parameters used in the OpenFileDialog</param>
        /// <returns>The selected files</returns>
        public static List<string> OpenFilesDialog(RwOpenFileDialogCreationData dialogData)
        {
            List<string> files = new List<string>();

            Microsoft.Win32.OpenFileDialog openFileDialog = OpenDialog(dialogData);
            if (openFileDialog.ShowDialog() == true)
            {
                files.AddRange(openFileDialog.FileNames);
            }

            return files;
        }

        /// <summary>
        /// Use of the OpenFileDialog
        /// </summary>
        /// <param name="dialogData">The parameters used in the OpenFileDialog</param>
        /// <returns>Reference to the OpenFileDialog</returns>
        private static Microsoft.Win32.OpenFileDialog OpenDialog(RwOpenFileDialogCreationData dialogData)
        {
            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = dialogData.Title,
                Filter = dialogData.Filter,
                Multiselect = dialogData.Multiselect
            };

            return openDialog;
        }

        /// <summary>
        /// Use of the SaveFileDialog to select a single file
        /// </summary>
        /// <param name="dialogData">The parameters used in the SaveFileDialog</param>
        /// <returns>The selected single file</returns>
        public static string SaveFileDialog(RwSaveFileDialogCreationData dialogData)
        {
            string file = string.Empty;

            Microsoft.Win32.SaveFileDialog saveFileDialog = SaveDialog(dialogData);
            if (saveFileDialog.ShowDialog() == true)
            {
                file = saveFileDialog.FileName;
            }

            return file;
        }

        /// <summary>
        /// Use of the SaveFileDialog
        /// </summary>
        /// <param name="dialogData">The parameters used in the SaveFileDialog</param>
        /// <returns>Reference to the SaveFileDialog</returns>
        private static Microsoft.Win32.SaveFileDialog SaveDialog(RwSaveFileDialogCreationData dialogData)
        {
            Microsoft.Win32.SaveFileDialog saveDialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = dialogData.Title,
                Filter = dialogData.Filter
            };
            return saveDialog;
        }


        /// <summary>
        /// Use of the FolderBrowserDialog to select a directory
        /// </summary>
        /// <param name="dialogData">The parameters used in the FolderBrowserDialog</param>
        /// <returns>The selected directory</returns>
        public static string FolderBrowsingDialog(RwFolderBrowserDialogCreationData dialogData)
        {
            string selectedDirectory = dialogData.SelectedPath;

            System.Windows.Forms.FolderBrowserDialog folderBrowsingDialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = dialogData.Description,
                SelectedPath = dialogData.SelectedPath,
                ShowNewFolderButton = dialogData.ShowNewFolderButton,
                RootFolder = dialogData.RootFolder,
                Site = dialogData.Site,
                Tag = dialogData.Tag
            };

            System.Windows.Forms.DialogResult result = folderBrowsingDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                selectedDirectory = folderBrowsingDialog.SelectedPath;
            }

            return selectedDirectory;
        }

        /// <summary>
        /// Converts an absolute path to a relative path of the SystemRoot
        /// </summary>
        /// <param name="fullAbsolutePath">The absolute path</param>
        /// <returns>The relative path to the SystemRoot</returns>
        public static string AbsoluteToRelativeLibraryPath(string fullAbsolutePath)
        {
            string absolutePath = Path.GetDirectoryName(fullAbsolutePath);
            string sysRoot = TxApplication.SystemRootDirectoryNormalized;
            string relativePath = absolutePath.Replace(sysRoot, "");
            return relativePath;
        }
        #endregion
    }
}
