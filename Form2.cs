using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using VkNet;
using VkNet.Model;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace Team_Two_Work
{
    public partial class Form2 : Form
    {
        public Form2(Form1 l)
        {
            InitializeComponent();
            this.l = l;
        }
        
        public Form1 l;

        private string CheckData(object l)
        {
            if (l == null) return "Не определено";
            else return Encoding.UTF8.GetString(Encoding.Default.GetBytes(l.ToString()));
        }

        private string CheckArrayData(System.Collections.ObjectModel.ReadOnlyCollection<User> l)
        {
            string result = "\r\n";
            foreach (User item in l)
            {
                result += "    " + Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.FirstName + " " + item.LastName)) + "\r\n";
            }
            if (result == "\r\n") return "Не определено";
            else return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var p = l.api.Users.Get(new long[] { l.id });
                var fr = l.api.Friends.Get(new FriendsGetParams
                {
                    UserId = l.id,
                    Fields = ProfileFields.FirstName,
                });

                Dictionary<string, string> lol = new Dictionary<string, string>
                {
                    {"Имя", CheckData(p[0].FirstName)},
                    {"Фамилия", CheckData(p[0].LastName)},
                    {"Пол", CheckData(p[0].Sex)},
                    {"ДР", CheckData(p[0].BirthDate)},
                    {"Страна", CheckData(p[0].Country)},
                    {"Статус", CheckData(p[0].Status)},
                    {"Друзья", CheckArrayData(fr)},
                };

                textBox1.Text = "";

                foreach (string item in checkedListBox1.CheckedItems)
                {
                    textBox1.Text += item + ": " + lol[item] + "\r\n";
                };
            }
            catch (VkNet.Exception.UserDeletedOrBannedException)
            {
                textBox1.Text = "Пользователь удален или получил банхаммером.";
            }
            catch (VkNet.Exception.VkApiMethodInvokeException)
            {
                textBox1.Text = "Пользователь стесняется и скрыл профиль.";
            }

        }
    }
}
