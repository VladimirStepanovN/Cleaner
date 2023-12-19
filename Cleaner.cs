namespace Cleaner;

internal class Cleaner
{
    internal (double sizeFolderBeforeClean, double sizeFreeSpaceAfterDelete, int countDeletedFiles) result;
    internal (double sizeFolderBeforeClean, double sizeFreeSpaceAfterDelete, int countDeletedFilesint) Clean(string Path, bool isSubfolder = false)
    {
        try
        {
            var directory = new DirectoryInfo(Path);
            if (directory.Exists)
            {
                foreach (DirectoryInfo dir in directory.GetDirectories())
                {
                    try
                    {
                        Clean(dir.FullName, true);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Недопустимое действие или недостаточно прав на выполнение для {dir.FullName}.");
                        continue;
                    }
                }
                FileInfo[] files = directory.GetFiles();
                if (files.Length > 0)
                {
                    foreach (FileInfo file in files)
                    {
                        try
                        {
                            result.sizeFolderBeforeClean += file.Length;
                            if (file.LastAccessTime.AddMinutes(30.0) < DateTime.Now)
                            {
                                result.sizeFreeSpaceAfterDelete += file.Length;
                                result.countDeletedFiles++;
                                file.Delete();
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            Console.WriteLine($"Недопустимое действие или недостаточно прав на выполнение для {file.FullName}.");
                            continue;
                        }
                    }
                }
                // если папка пустая и вложенная (признак isSubfolder), то удаляем
                if (directory.GetFiles().Length == 0 && directory.GetDirectories().Length == 0 && isSubfolder) directory.Delete();
            }
            else
            {
                Console.WriteLine($"Указанный каталог {Path} не существует");
            }
        }
        catch (Exception)
        {
            throw;
        }
        return result;
    }
}