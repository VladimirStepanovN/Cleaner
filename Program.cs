namespace Cleaner;

internal class Program
{
    internal static void Main(string[] args)
    {
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            try
            {
                Cleaner cleaner = new();
                var (sizeFolderBeforeClean, sizeFreeSpaceAfterDelete, countDeletedFilesint) = cleaner.Clean(args[0]);
                Console.WriteLine($"Исходный размер папки: {sizeFolderBeforeClean} байт");
                Console.WriteLine($"Удалено: {countDeletedFilesint} файлов");
                Console.WriteLine($"Освобождено: {sizeFreeSpaceAfterDelete} байт");
                Console.WriteLine($"Текущий размер папки: {sizeFolderBeforeClean - sizeFreeSpaceAfterDelete}");
                Console.ReadKey();
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Недопустимая часть пути к каталогу.");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Путь или имя файла превышает максимальную длину, определенную системой.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        else
        {
            Console.WriteLine("Путь к каталогу не указан.");
        }
    }
}