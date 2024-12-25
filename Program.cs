using Library.Data;
using Library.Services;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataStore = new DataStore();
            var libraryService = new LibraryService(dataStore);
            var userService = new UserService(dataStore);

            System.Console.WriteLine("Kutubxona dasturiga xush kelibsiz!");
            int userId = userService.Authenticate();
            if(userId == -1) return;

            while(true)
            {
                System.Console.WriteLine("\nMenu");
                System.Console.WriteLine("1. Kitoblar ro\'yxatini ko\'rish");
                System.Console.WriteLine("2. Kitob haqida ma\'lumot oqish");
                System.Console.WriteLine("3. Kitob olish");
                System.Console.WriteLine("4. Kitob qaytarish");
                System.Console.WriteLine("0. Chiqish");

                System.Console.Write("Menudan bitta raqamni tanlang:");
                var choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                    libraryService.GetAllBooks();
                    break;
                    case "2":
                    System.Console.Write("Kitob Id ni kiriting:");
                    if(int.TryParse(Console.ReadLine(), out int bookId)); 
                    libraryService.BookDetails(bookId);
                    break;
                    case "3":
                    System.Console.Write("Kitob Id ni kiriting:");
                    if(int.TryParse(Console.ReadLine(), out bookId));
                    libraryService.BorrowBook(userId,bookId);
                    break;
                    case "4":
                    System.Console.WriteLine("Kitob nomini kiriting:");
                    var bookName = Console.ReadLine();
                    libraryService.ReturnBook(userId, bookName);
                    break;
                    case "0":
                    System.Console.WriteLine("Xayr salomat bo\'ling!");
                    return;
                    default:
                    System.Console.WriteLine("Noto\'g\'ri raqam kiritdingiz!!!");
                    break;
                }
            }
        }
    }
}