using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pass
{
    class Password
    {
        public string name;
        public string username;
        public string password;
        public string email;
        public string link;
        public string answer;
        public string details;
        public Password(string name, string username, string password, string email, string link, string answer,string details)
        {
            this.name = name;
            this.username = username;
            this.password = password;
            this.email = email;
            this.link = link;
            this.answer = answer;
            this.details = details;
        }

        public string standartShow()
        {
            return name + ":\n\n" + username + "\n\n" + password;
        }

    }
}
