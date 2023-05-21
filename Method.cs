namespace Voting_App;
class Method
{
    public static List<User> UserList = new List<User>{ new User{userName="Ahmet", userPassword="3434"},new User{userName ="Tolunay", userPassword ="1234"}, new User{userName="Ahmet1", userPassword="3434"}};
    public static List<Category> CategoryList = new List<Category>{ new Category{CategoryName = "Watch Category"},new Category{CategoryName = "Tech Stack Category"},new Category{CategoryName = "Sport Category"}};
    public static User nowUser;
    private static bool exitControl = false;

    public static void MainMethod(){
        while(true){
            // Console.Clear();
            if(nowUser ==null)
            {
                Back:
                System.Console.WriteLine(" -- Welcome to the category voting app. -- ");
                System.Console.WriteLine(" - Menu -\n1 - Login\n2 - Register\n3 - Vote\n0 - Exit");
                System.Console.Write("Which one : ");
                if(Int32.TryParse(Console.ReadLine(), out int chose))
                {
                    switch (chose)
                    {
                        case 1:
                            LogIn();
                            break;
                        case 2:
                            register();
                            break;
                        case 3:
                            System.Console.WriteLine("You must be logged in to vote categories...");
                            LogIn();
                            break;
                        case 0:
                            System.Console.WriteLine("You are exiting.");
                            exitControl = true;
                            break;
                        default:
                            System.Console.WriteLine("You entered wrong. Please, choose again...");
                            goto Back;
                    }
                }else{
                    System.Console.WriteLine("You entered wrong. Please, choose again...");
                    goto Back;
                }
            }else{
                Back:
                System.Console.WriteLine($"Welcome {nowUser.userName},");
                System.Console.WriteLine(" - Menu -\n1 - Vote\n2 - Voting Result\n3 - Logout\n0 - Exit");
                System.Console.Write("Which one : ");
                if(Int32.TryParse(Console.ReadLine(), out int chose))
                {
                    switch (chose)
                    {
                        case 1:
                            vote();
                            if (nowUser.voteTrue)
                            {
                                resultMethod();
                            }
                            break;
                        case 2:
                            resultMethod();
                            break;
                        case 3:
                            Logout();
                            break;
                        case 0:
                            System.Console.WriteLine("You are exiting.");
                            exitControl = true;
                            break;
                        default:
                            System.Console.WriteLine("You entered wrong. Please, choose again...");
                            goto Back;
                    }
                }else{
                    System.Console.WriteLine("You entered wrong. Please, choose again...");
                    goto Back;
                }
            }
            
            if (exitControl)
            {
                break;
            }
            
        }
       
    }

    public static void LogIn()
    {
    Login:
        User user = null;
        System.Console.WriteLine("Please login to vote.");
        System.Console.Write("Username : ");
        string uName = Console.ReadLine();
        user = Search(uName);
        if (user == null)
        {
            System.Console.WriteLine("User not found.You must be registered to vote.");
        Back:
            System.Console.WriteLine(" - Menu -\n1 - Login\n2 - Register\n0 - Exit");
            System.Console.Write("Which one : ");
            if(Int32.TryParse(Console.ReadLine(), out int chose))
            {
                switch (chose)
                {
                    case 1:
                        goto Login;
                    case 2:
                        register();
                        break;
                    case 0:
                        exitControl = true;
                        break;
                    default:
                        System.Console.WriteLine("You entered wrong. Please, choose again...");
                        goto Back;
                }
            }
            else
            {
                System.Console.WriteLine("You entered wrong. Please, choose again...");
                goto Back;
            }
        }
        else
        {
        Password:
            System.Console.Write("Password : ");
            string password = Console.ReadLine();
            if (password != user.userPassword)
            {
                System.Console.WriteLine("Password is not correct.");
            Back:
                System.Console.WriteLine(" - Menu -\n1 - Enter Password Again\n0 - Exit");
                if(Int32.TryParse(Console.ReadLine(), out int chose))
                {
                    switch (chose)
                    {
                        case 1:
                            goto Password;
                        case 0:
                            exitControl = true;
                            break;
                        default:
                            System.Console.WriteLine("You entered wrong. Please, choose again...");
                            goto Back;
                    }
                }
            }
            else
            {
                System.Console.WriteLine("Password is correct. You logged...");
                nowUser = user;
            }
        }
    }

    private static User Search(string uName)
    {
        foreach (var item in UserList)
        {
            if (item.userName == uName)
            {
                return item;
            }
        }

        return null;
    }

    public static void vote(){
        if(nowUser != null){
            System.Console.WriteLine($"Welcome {nowUser.userName}, \nPlease, choose which category you want to vote for.");
            Back:
            System.Console.WriteLine(" - Vote Menu -");
            int count = 0;
            foreach (var item in CategoryList)
            {
                count++;
                System.Console.WriteLine($"{count} - {item.CategoryName}");
            }
            System.Console.WriteLine("9 - Logout");
            System.Console.WriteLine("0 - Exit");
            System.Console.Write("Please, choose one for voting : ");
            if(Int32.TryParse(Console.ReadLine(), out int chose) && chose >= 1 && chose<=  CategoryList.Count){
                if(nowUser.voteTrue == false){
                    CategoryList[chose-1].increaseMethod();
                    nowUser.voteTrue=true;
                }         
                else{
                    CategoryList[nowUser.vote].decreaseMethod();
                    CategoryList[chose-1].increaseMethod();
                }
                
                nowUser.vote = chose-1;
                System.Console.WriteLine("You voted.");
            }else if(chose==0)
            {
                exitControl = true;
                System.Console.WriteLine("Çalıştı ");
            }else if(chose == 9)
            {
                Logout();      
            }
            else
            {
                System.Console.WriteLine("You entered wrong. Please, choose again.");
                goto Back;
            }
        }
    }

    private static void Logout()
    {
        nowUser = null;
    }

    public static void resultMethod(){
        double totalVote = CategoryList.Sum(p => p.voitCount);
        System.Console.WriteLine(" - Category Vote Rate - ");
        System.Console.WriteLine($"Total Number of votes : {totalVote} ");

        foreach (var item in CategoryList)
        {
            System.Console.WriteLine($"{item.CategoryName} Number of votes : {item.voitCount} (% {((double)item.voitCount / (double)totalVote)*100})");
        }
    }

    public static void register(){
        Back:
        User user = null;
        System.Console.WriteLine(" - Register - ");
        System.Console.Write("Please, Enter Username : ");
        string username = Console.ReadLine().Trim();
        user = Search(username);
        if (user == null)
        {
            Password:
            System.Console.Write("Please, Enter Password : ");
            string password = Console.ReadLine().Trim();
            if (password != "" && password.Length>=5)
            {
                User userNew = new User{userName = username, userPassword=password};
                UserList.Add(userNew);
                System.Console.WriteLine("User registered.");
                nowUser = userNew;
            }else{
                System.Console.WriteLine("Password cannot be empty or less than five. Please, Enter password again.");
                goto Password;
            }
        }else{
            System.Console.WriteLine("There is a user with this username.");
            Again:
            System.Console.WriteLine(" - Menu -\n1 - Go back to register\n0 - Exit");
            if(Int32.TryParse(Console.ReadLine(), out int chose))
            {
                switch (chose)
                {
                case 1:
                    goto Back;
                case 0:
                    exitControl = true;
                    break;
                default:
                    System.Console.WriteLine("You entered wrong. Please, choose again...");
                    goto Again;
                }
            }
            else
            {
                System.Console.WriteLine("You entered wrong. Please, choose again...");
                goto Back;
            }

        }


    }
}
