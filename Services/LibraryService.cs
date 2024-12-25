using Library.Data;

namespace Library.Services
{
    public class LibraryService
    {
        private readonly DataStore _dataStore;
        public LibraryService(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public void GetAllBooks()
        {

            System.Console.WriteLine("Kitoblar royxati:");
            var books = _dataStore.GetBooks();
            foreach(var book in books)
            {
                System.Console.WriteLine($"Id:{book.Id}, Nomi:{book.Name}, Available Copies:{book.AvailableCopies}");
            }
        }
        public void GetAllUsers()
        {
            System.Console.WriteLine("Users:");
            var users = _dataStore.GetUsers();
            foreach(var user in users)
            {
                System.Console.WriteLine($"Id:{user.Id}, Name:{user.FirstName}, age:{user.Age}");
            }
        }

        public void BookDetails(int id)
        {
            System.Console.WriteLine("Tanlangan kitob haqida ma\'lumot:");
            var book = _dataStore.GetBooks().FirstOrDefault(c => c.Id == id);
            if(book == null)
            {
                System.Console.WriteLine("Kitob topilmadi!");
                return;
            }
            System.Console.WriteLine($"Nomi: {book.Name}\nJanri: {book.Genre}\nMuallifi: {book.Author}\nMavjud nusxalar: {book.AvailableCopies}");
        }

        public void BorrowBook(int userId, int bookId)
        {
            var book = _dataStore.GetBooks().FirstOrDefault(x => x.Id == bookId);
            var user = _dataStore.GetUsers().FirstOrDefault(y => y.Id == userId);

            if(book == null || user == null || book.AvailableCopies <= 0)
            {
                System.Console.WriteLine("Kitobni olish imkoni yoq!");
                return;
            }

            book.AvailableCopies--;
            user.BorrowedBookCount++;
            user.BorrowedBooks.Add(book.Name);
            book.Readers.Add(user.FirstName);
            
            _dataStore.Save();
            System.Console.WriteLine($"{book.Name} kitobi muvaffiqiyatli qo'shildi.");
        }

        public void ReturnBook(int userId, string? bookName)
        {
            var user = _dataStore.GetUsers().FirstOrDefault(u => u.Id == userId);
            if(user == null)
            {
                System.Console.WriteLine($"Foydalanuvchi topilmadi!");
                return;
            }

            var bookInUserList = user.BorrowedBooks.FirstOrDefault(b => b.Equals(bookName, StringComparison.OrdinalIgnoreCase));
            if(bookInUserList == null)
            {
                System.Console.WriteLine($"Siz {bookName} nomli kitob olmagansiz.");
                return;
            }
            
            var book = _dataStore.GetBooks().FirstOrDefault(b => b.Name.Equals(bookName, StringComparison.OrdinalIgnoreCase));
            if(book == null)
            {
                System.Console.WriteLine($"Kutubxonada {bookName} nomli kitob mavjud emas.");
                return;
            }
            
            book.AvailableCopies++;
            user.BorrowedBooks.Remove(bookName);
            user.BorrowedBookCount--;
            _dataStore.Save();
            System.Console.WriteLine($"{bookName} nomli kitobni qaytarib berdingiz.");
        }
    }
}