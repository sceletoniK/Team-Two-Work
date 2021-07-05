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
    public partial class Form3 : Form
    {
        public Form3(Form1 l)
        {
            InitializeComponent();
            this.l = l;
        }

        Form1 l;

        private void button1_Click(object sender, EventArgs e)
        {
            var fr = l.api.Friends.Get(new FriendsGetParams
            {
                UserId = l.id,
                Fields = ProfileFields.BirthDate,
            });

            foreach (User item in fr)
            {
                //if (item.BirthDate == null || item.BirthDate )
                //{

                //}
            }
        }
    }
}
