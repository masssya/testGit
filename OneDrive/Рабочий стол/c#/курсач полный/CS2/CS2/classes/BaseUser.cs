using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2.classes
{
    abstract class BaseUser
    {
        private string login;
        private string password;
        private int is_admin;
        public string Login
        {
            get { return login; }
            set
            {
                if (value != null) { login = value; }
                else { Console.WriteLine("Введите логин, это не может быть пустая строка"); }
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (value != null) { password = value; }
                else { Console.WriteLine("Введите пароль без пробелов"); }
            }
        }
        public int IsAdmin
        {
            get { return is_admin; }
            set { is_admin = value; }
        }

    }
}
