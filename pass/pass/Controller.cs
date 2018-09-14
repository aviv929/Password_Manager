using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pass
{
    class Controller
    {
        List<Password> data;
        public Controller()
        {
            data = new List<Password>();
            string[] tmp;         
            try
            {
                tmp = System.IO.File.ReadAllLines("data.txt");
            }
            catch
            {
                tmp = new string[0];
                System.IO.File.WriteAllLines(@"data.txt", new string[0]);
            }
            for (int i = 0; i < tmp.Length; i+=7)
                data.Add(new Password(tmp[i], tmp[i+1], tmp[i+2], tmp[i+3], tmp[i+4], tmp[i+5], tmp[i+6]));
            
        }
        public bool valid()//check wether the file has corupted
        {
            if (System.IO.File.ReadAllLines("data.txt").Length % 7 != 0)
                return false;
            return true;             
        }

        public string add(string name,string username,string password,string email,string link,string answer,string details)//add new record
        {
            for (int i = 0; i < data.Count; i++)
                if (name==data[i].name && username==data[i].username)                
                    return "account already exist";
            data.Add(new Password(name, username, password, email, link, answer,details));
            return "account was added successfully";
        }

        public Password[] filter(string name, string username)
        {
            List<Password> ans = new List<Password>();
            for (int i = 0; i < data.Count; i++)           
                if (data[i].name.Contains(name) && data[i].username.Contains(username))                
                    ans.Add(data[i]);
            return ans.ToArray();
        }
        public void update(string name, string username, string password, string email, string link, string answer, string details)
        {
            int index = -1;
            for (int i = 0; i < data.Count; i++)
                if (data[i].name == name && data[i].username == username)
                    index = i;
            if (index != -1)
            {
                data[index].password = password;
                data[index].email = email;
                data[index].link = link;
                data[index].answer = answer;
                data[index].details = details;
            }
        }
        public void delete(string name, string username)
        {
            int index = -1;
            for (int i = 0; i < data.Count; i++)
                if (data[i].name == name && data[i].username == username)
                    index = i;
            data.RemoveAt(index);
        }

        public void save()//save the data in text file
        {
            string[] tmp = new string[data.Count * 7];
            for (int i = 0; i < data.Count; i++)
            {
                tmp[i * 7] = data[i].name;
                tmp[i * 7 + 1] = data[i].username;
                tmp[i * 7 + 2] = data[i].password;
                tmp[i * 7 + 3] = data[i].email;
                tmp[i * 7 + 4] = data[i].link;
                tmp[i * 7 + 5] = data[i].answer;
                tmp[i * 7 + 6] = data[i].details;
            }
            System.IO.File.WriteAllLines("data.txt", tmp);
        }
    }
}
