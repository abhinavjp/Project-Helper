using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using HelperProject.Filters;

namespace HelperProject.Helpers
{
    public class DirectoryHelper
    {
        /// <summary>
        /// Helps in creating a directory if it does not exist or else will not do anything
        /// </summary>
        /// <param name="path">The full path of the directory</param>
        /// <returns>Error Handler object which will tell you if the operation succeeded or not. If not it gives the reason as a message</returns>        
        public static ErrorHandler CreatDirectoryIfNotExists(string path)
        {
            try
            {
                if (Validator.CheckParametersAreNull(path))
                    return new ErrorHandler("Arguments cannot be null");
                string message;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    message = "Directory created successfully";
                }
                else
                {
                    message = "Directory already exists";
                }
                return new ErrorHandler(true, message);
            }
            catch (Exception ex)
            {
                return new ErrorHandler(ex);
            }
        }

        /// <summary>
        /// Helps in moving a directory if it exists or else will not do anything
        /// </summary>
        /// <param name="sourcePath">The full path of the directory where it currently exists</param>
        /// <param name="destinationPath">The full path of the directory where it has to be moved</param>
        /// <returns>Error Handler object which will tell you if the operation succeeded or not. If not it gives the reason as a message</returns>        
        public static ErrorHandler MoveDirectoryIfExists(string sourcePath, string destinationPath)
        {
            try
            {
                if (Validator.CheckParametersAreNull(sourcePath, destinationPath))
                    return new ErrorHandler("Arguments cannot be null");
                if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, destinationPath);
                    return new ErrorHandler(true, "Directory successfully moved");
                }
                return new ErrorHandler(false, "Specified source path does not exist");
            }
            catch (Exception ex)
            {
                return new ErrorHandler(ex);
            }
        }

        /// <summary>
        /// Helps in moving a file if it exists or else will not do anything
        /// </summary>
        /// <param name="fileName">The name of the file with extension to be moved</param>
        /// <param name="sourcePath">The full path of the directory where the file exists</param>
        /// <param name="destinationPath">The full path of the directory where the file has to be moved</param>
        /// <param name="isForcedMove">Used to know if the file has to be moved even if the destination path does not exist.
        /// If destination path does not exist it is created.</param>
        /// <returns>Error Handler object which will tell you if the operation succeeded or not. If not it gives the reason as a message</returns>        
        public static ErrorHandler MoveFileIfExists(string fileName, string sourcePath, string destinationPath, bool isForcedMove)
        {
            try
            {
                if (Validator.CheckParametersAreNull(fileName, sourcePath, destinationPath))
                    return new ErrorHandler("Arguments cannot be null");
                if (Directory.Exists(sourcePath))
                {
                    var oldCombinedPath = Path.Combine(sourcePath, fileName);
                    var newCombinedPath = Path.Combine(destinationPath, fileName);
                    if (File.Exists(oldCombinedPath))
                    {
                        if (Directory.Exists(destinationPath))
                        {
                            if (!File.Exists(newCombinedPath))
                            {
                                File.Move(sourcePath, destinationPath);
                                return new ErrorHandler(true, "File Moved Successfully");
                            }
                            else
                            {
                                return new ErrorHandler(false, "File with same name already exists at the destination path");
                            }
                        }
                        else
                        {
                            if (isForcedMove)
                            {
                                File.Move(sourcePath, destinationPath);
                                return new ErrorHandler(true, "File Moved Successfully");
                            }
                            else
                            {
                                return new ErrorHandler(false, "The destination path does not exist. Please use 'forced move' to move irreelvant to the existence of destination path");
                            }
                        }
                    }
                    else
                    {
                        return new ErrorHandler(false, "File does not exist at the source path");
                    }
                }
                return new ErrorHandler(false, "Specified source path does not exist");
            }
            catch (Exception ex)
            {
                return new ErrorHandler(ex);
            }

        }

        /// <summary>
        /// Helps in deleting a directory if it exists or else will not do anything.
        /// All the subdirectories and files in the directory will also be deleted.
        /// </summary>
        /// <param name="path">The full path of the directory</param>
        /// <returns>Error Handler object which will tell you if the operation succeeded or not. If not it gives the reason as a message</returns>        
        public static ErrorHandler DeleteDirectoryIfExists(string path)
        {
            try
            {
                if (Validator.CheckParametersAreNull(path))
                    return new ErrorHandler("Arguments cannot be null");
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    return new ErrorHandler(true, "Deleted directory successfully");
                }
                return new ErrorHandler("Directory path does not exist");
            }
            catch (Exception ex)
            {
                return new ErrorHandler(ex);
            }
        }

        /// <summary>
        /// Helps in deleting a directory if it exists or else will not do anything.
        /// If there are any subdirectory or file in the directory, the directory will not be deleted.
        /// </summary>
        /// <param name="path">The full path of the directory</param>
        /// <returns>Error Handler object which will tell you if the operation succeeded or not. If not it gives the reason as a message</returns>        
        public static ErrorHandler DeleteDirectoryIfEmpty(string path)
        {
            try
            {
                if (Validator.CheckParametersAreNull(path))
                    return new ErrorHandler("Arguments cannot be null");
                if (Directory.Exists(path))
                {
                    if (Directory.EnumerateDirectories(path).Count() <= 0 || Directory.EnumerateFiles(path).Count() <= 0)
                    {
                        Directory.Delete(path, false);
                        return new ErrorHandler(true, "Directory deleted");
                    }
                    else
                        return new ErrorHandler("The directory is not empty");
                }
                return new ErrorHandler("The path does of directory does not exist");
            }
            catch (Exception ex)
            {
                return new ErrorHandler(ex);
            }
        }

        /// <summary>
        /// Helps in deleting a file if it exists or else will not do anything.
        /// </summary>
        /// <param name="path">The full path of the directory where the file exists without the file name</param>
        /// <param name="fileName">The file name with the extension</param>
        /// <returns>Error Handler object which will tell you if the operation succeeded or not. If not it gives the reason as a message</returns>        
        public static ErrorHandler DeleteFileIfExists(string path, string fileName)
        {
            try
            {
                if (Validator.CheckParametersAreNull(path, fileName))
                    return new ErrorHandler("Arguments cannot be null");
                if (Directory.Exists(path))
                {
                    var combinedPath = Path.Combine(path, fileName);
                    if (File.Exists(combinedPath))
                    {
                        File.Delete(combinedPath);
                        return new ErrorHandler(true, "File deletd successfully");
                    }
                    return new ErrorHandler("File to delete does not exist at the specified path.");
                }
                return new ErrorHandler("The path specified does not exist");
            }
            catch (Exception ex)
            {
                return new ErrorHandler(ex);
            }
        }

        /// <summary>
        /// Gets the list of all directories at a specified path
        /// </summary>
        /// <param name="path">The full path of the directory</param>
        /// <returns>Returns the list of all directory names at the specified path if any within a wrapper object.
        /// Access it using Result property of the object returned</returns>        
        public static ErrorHandler<List<string>> GetAllDirectoriesFromPath(string path)
        {
            try
            {
                if (Validator.CheckParametersAreNull(path))
                    return new ErrorHandler<List<string>>("Arguments cannot be null");
                if (Directory.Exists(path))
                {
                    return new ErrorHandler<List<string>>(Directory.EnumerateDirectories(path).ToList(), "Successfully returned sub directory names in the directory");
                }
                else
                {
                    return new ErrorHandler<List<string>>("The specified path does not exist");
                }
            }
            catch (Exception ex)
            {
                return new ErrorHandler<List<string>>(ex);
            }
        }

        /// <summary>
        /// Gets the list of all files at a specified path
        /// </summary>
        /// <param name="path">The full path of the directory</param>
        /// <returns>Returns the list of all file names at the specified path if any within a wrapper object.
        /// Access it using Result property of the object returned</returns>        
        public static ErrorHandler<List<string>> GetAllFilesFromPath(string path)
        {
            try
            {
                if (Validator.CheckParametersAreNull(path))
                    return new ErrorHandler<List<string>>("Arguments cannot be null");
                if (Directory.Exists(path))
                {
                    return new ErrorHandler<List<string>>(Directory.EnumerateFiles(path).ToList(), "Successfully returned file names in the directory");
                }
                else
                {
                    return new ErrorHandler<List<string>>("The specified path does not exist");
                }
            }
            catch (Exception ex)
            {
                return new ErrorHandler<List<string>>(ex);
            }
        }

        /// <summary>
        /// Helps in searching directories with a particular searchtext at a specified path
        /// </summary>
        /// <param name="path">The full path of the directory to search sub directories</param>
        /// <param name="searchText">The keyword used to search files which has filename including the keyword</param>
        /// <param name="searchAsExact">To check if you want to search files with filenames which contains the search keyword or matches the exact keyword.
        /// If true, directory names that exactly matches the seaech keyword are returned or else the file names that contain the search keyword</param>
        /// <returns>Returns the list of matching directory names at the specified path if any within a wrapper object.
        /// Access it using Result property of the object returned</returns>        
        public static ErrorHandler<List<string>> SearchDirectoriesFromPath(string path, string searchText = "", bool searchAsExact = false)
        {
            try
            {
                if (Validator.CheckParametersAreNull(path))
                    return new ErrorHandler<List<string>>("Arguments cannot be null");
                if (searchText == "")
                    return GetAllDirectoriesFromPath(path);
                if (Directory.Exists(path))
                {
                    if (!searchAsExact)
                        return new ErrorHandler<List<string>>(Directory.EnumerateDirectories(path, "*" + searchText + "*").ToList(), "Successfully returned matching sub directory names in the directory");
                    else
                        return new ErrorHandler<List<string>>(Directory.EnumerateDirectories(path, "*" + searchText + "*").ToList(), "Successfully returned matching sub directory names in the directory");
                }
                else
                {
                    return new ErrorHandler<List<string>>("The specified path does not exist");
                }
            }
            catch (Exception ex)
            {
                return new ErrorHandler<List<string>>(ex);
            }
        }

        /// <summary>
        /// Helps in searching files with a particular searchtext at a specified path
        /// </summary>
        /// <param name="path">The full path of the directory to search the files</param>
        /// <param name="searchText">The keyword used to search files which has filename including the keyword</param>
        /// <param name="searchAsExact">To check if you want to search files with filenames which contains the search keyword or matches the exact keyword
        /// If true, file names that exactly matches the seaech keyword are returned or else the file names that contain the search keyword</param>
        /// <returns>Returns the list of matching file names at the specified path if any within a wrapper object.
        /// Access it using Result property of the object returned.
        /// Error Handler object which will also tell you if the operation succeeded or not. If not it gives the reason as a message</returns>        
        public static ErrorHandler<List<string>> SearchFilesFromPath(string path, string searchText = "", bool searchAsExact = false)
        {
            try
            {
                if (Validator.CheckParametersAreNull(path))
                    return new ErrorHandler<List<string>>("Arguments cannot be null");
                if (searchText == "")
                    return GetAllFilesFromPath(path);
                if (Directory.Exists(path))
                {
                    if (!searchAsExact)
                        return new ErrorHandler<List<string>>(Directory.EnumerateFiles(path, "*" + searchText + "*").ToList(), "Successfully returned matching file names in the directory");
                    else
                        return new ErrorHandler<List<string>>(Directory.EnumerateFiles(path, "*" + searchText + "*").ToList(), "Successfully returned matching file names in the directory");
                }
                else
                {
                    return new ErrorHandler<List<string>>("The specified path does not exist");
                }
            }
            catch (Exception ex)
            {
                return new ErrorHandler<List<string>>(ex);
            }
        }

        /// <summary>
        /// Helps to rename a directory is it exists
        /// </summary>
        /// <param name="path">The full path to the directory(excluding directory name)</param>
        /// <param name="oldName">Old name of the directory</param>
        /// <param name="newName">New name of the directory</param>
        /// <returns>Error Handler object which will tell you if the operation succeeded or not. If not it gives the reason as a message</returns>        
        public static ErrorHandler RenameDirectoryIfExists(string path, string oldName, string newName)
        {
            try
            {
                if (Validator.CheckParametersAreNull(path, oldName, newName))
                    return new ErrorHandler("Arguments cannot be null");
                if (Directory.Exists(path))
                {
                    var oldCombinedPath = Path.Combine(path, oldName);
                    var newCombinedPath = Path.Combine(path, newName);
                    if (Directory.Exists(oldCombinedPath))
                    {
                        if (!Directory.Exists(newCombinedPath))
                        {
                            Directory.Move(oldCombinedPath, newCombinedPath);
                            return new ErrorHandler(true, "Renamed directory Successfully");
                        }
                        return new ErrorHandler("The new name already exists at the specified path");
                    }
                    return new ErrorHandler("The directory to rename does not exist at the specified path");
                }
                return new ErrorHandler("The path specified does not exist");
            }
            catch (Exception ex)
            {
                return new ErrorHandler(ex);
            }
        }

        /// <summary>
        /// Helps to rename a file is it exists
        /// </summary>
        /// <param name="path">The full path to the file(excluding file name)</param>
        /// <param name="oldFileName">Old name of the file</param>
        /// <param name="newFileName">New name of the file</param>
        /// <returns>Error Handler object which will tell you if the operation succeeded or not. If not it gives the reason as a message</returns>        
        public static ErrorHandler RenameFileIfExists(string path, string oldFileName, string newFileName)
        {
            try
            {
                if (Validator.CheckParametersAreNull(path, oldFileName, newFileName))
                    return new ErrorHandler("Arguments cannot be null");
                if (Directory.Exists(path))
                {
                    var oldCombinedPath = Path.Combine(path, oldFileName);
                    var newCombinedPath = Path.Combine(path, newFileName);
                    if (File.Exists(oldCombinedPath))
                    {
                        if (!File.Exists(newCombinedPath))
                        {
                            File.Move(oldCombinedPath, newCombinedPath);
                            return new ErrorHandler(true, "Renamed file Successfully");
                        }
                        return new ErrorHandler("A file with new name specified already exists at the specified path");
                    }
                    return new ErrorHandler("The file to rename does not exist at the specified path");
                }
                return new ErrorHandler("The path specified does not exist.");
            }
            catch (Exception ex)
            {
                return new ErrorHandler(ex);
            }
        }
    }


}