using System.Security.Principal;

namespace MyToDo.shared.Dtos
{
    public class UserDto : BaseDto
    {
        private string account;
        private string name;
        private string passwd;

        public string Account 
        {
            get { return account; } 
            set { account = value; OnPropertyChanged(); } 
        }
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }
        public string Passwd
        {
            get { return passwd; }
            set { passwd = value; OnPropertyChanged(); }
        }
    }
}