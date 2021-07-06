using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Model;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace Team_Two_Work
{
    public partial class Form4 : Form
    {
        public Form4(Form1 l)
        {
            InitializeComponent();
            this.l = l;
        }

        Form1 l;
        Dictionary<string, Group> aboba = new Dictionary<string, Group>();

        private void Form4_Load(object sender, EventArgs e)
        {
            var Groups = l.api.Groups.Get(new GroupsGetParams
            {
                UserId = l.id,
                Extended = true
            });

            foreach (Group item in Groups)
            {
                aboba.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.Name)), item);
                comboBox1.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.Name)));
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";

            WallGetObject Wall = l.api.Wall.Get(new WallGetParams
            {
                OwnerId = aboba[comboBox1.SelectedItem.ToString()].Id * -1,
                Extended = true,
                Count = 40
            });

            foreach (VkNet.Model.Attachments.Post Post in Wall.WallPosts)
            {
                string PV, PL;
                if (Post.Likes == null) PL = "Скрыто";
                else PL = Post.Likes.Count.ToString();

                if (Post.Views == null) PV = "Скрыто";
                else PV = Post.Views.Count.ToString();


                textBox1.Text += $"Пост от {Post.Date}, Просмотров: {PV}, Лукасы: {PL} \r\n";
                if (Post.Text == "") textBox1.Text += "<Текста неть> \r\n \r\n";
                else textBox1.Text += Encoding.UTF8.GetString(Encoding.Default.GetBytes(Post.Text)) + "\r\n" + "\r\n";
            }
        }
    }
}
