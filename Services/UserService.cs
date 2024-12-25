using Library.Data;
namespace Library.Services
{
    public class UserService
    {
        private readonly DataStore _dataStore;
        public UserService(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public int Authenticate()
        {
            System.Console.WriteLine("Login orqali kiring yoki yangi ro\'yxatdan o\'ting");
            System.Console.Write("Login:");
            var login = Console.ReadLine();
            System.Console.Write("Parol:");
            var password = Console.ReadLine();

            var user = _dataStore.GetUsers().FirstOrDefault(x => x.Login == login && x.Password == password);
            if(user != null)
            {
                System.Console.WriteLine($"Kutubxona dasturiga xush kelibsiz, {user.FirstName}");
                return user.Id;
            }
            return RegisterNewUser(login, password);
        }

        public int RegisterNewUser(string? login, string? password)
        {
            System.Console.WriteLine("Ro'yxatdan o'tishingiz kerak.");
            System.Console.Write("Ism: ");
            var firsName = Console.ReadLine();
            System.Console.Write("Familya: ");
            var lastName = Console.ReadLine();
            System.Console.Write("Yosh: ");
            if(!int.TryParse(Console.ReadLine(), out int age))  age = 18;

            var newUser = new User
            {
                Id = _dataStore.GetUsers().Count + 1,
                FirstName = firsName,
                LastName = lastName,
                Age = age,
                Login = login,
                Password = password  
            };

            var users = _dataStore.GetUsers();
            users.Add(newUser);
            _dataStore.SaveUsers(users);
            //_dataStore.Save();

            System.Console.WriteLine("Ro\'yxatdan muvaffaqiyatli o'ttingiz.");
            return newUser.Id;
        }
    }
}
