namespace Library.Data
{
    using System.Text.Json;

    public class DataStore
    {
        private const string BooksFilePath = "books.json";
        private const string UsersFilePath = "users.json";
        private List<Book> _books;
        private List<User> _users;
        public DataStore()
        {
            _books = LoadBooks();
            _users = LoadUsers();
        }

        public List<Book> GetBooks() => _books;
        public List<User> GetUsers() => _users;
        public List<Book> LoadBooks()
        {
            if(!File.Exists(BooksFilePath)) return new List<Book>();
            var jsonBooks = File.ReadAllText(BooksFilePath);
            return JsonSerializer.Deserialize<List<Book>>(jsonBooks) ?? new List<Book>();
        }
        public List<User> LoadUsers()
        {
            if(!File.Exists(UsersFilePath)) return new List<User>();
            var jsonUsers = File.ReadAllText(UsersFilePath);
            return JsonSerializer.Deserialize<List<User>>(jsonUsers) ?? new List<User>();
        }

        public void SaveUsers(List<User> users)
        {
            File.WriteAllText(UsersFilePath, JsonSerializer.Serialize(users));
        }

        public void Save()
        {
            try
            {
                File.WriteAllText(BooksFilePath, JsonSerializer.Serialize(_books));
                File.WriteAllText(UsersFilePath, JsonSerializer.Serialize(_users));
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}