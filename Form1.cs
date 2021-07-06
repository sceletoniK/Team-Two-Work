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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string token = "";
        public long id;
        public VkApi api;

        public string getAuthForUser()
        {
            try
            {
                using (StreamReader sr = new StreamReader("Data.txt"))
                {
                    token = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return token;
        }
        private void Login_Click(object sender, EventArgs e)
        {
            try
            {
                api = new VkApi();
                id = long.Parse(IdBox.Text);

                api.Authorize(new ApiAuthParams
                {
                    AccessToken = getAuthForUser(),
                    Settings = Settings.All
                });

                var check = api.Users.Get(new long[] { id });
                check[0].FirstName += "";

                label2.Text = "Успешно авторизовано.";

                Login.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            catch
            {
                label2.Text = "Неправильный ввод id";
            }

        }

        private void IdBox_TextChanged(object sender, EventArgs e)
        {
            Login.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

            label2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 aboba = new Form2(this);
            aboba.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 aboba = new Form3(this);
            aboba.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 aboba = new Form4(this);
            aboba.ShowDialog();
        }
    }
}
