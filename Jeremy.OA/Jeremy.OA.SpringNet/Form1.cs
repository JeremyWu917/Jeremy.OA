using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jeremy.OA.SpringNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 返回的就是一个已经根据<objects/>节点的内容配置好的容器对象
            IApplicationContext ctx = ContextRegistry.GetContext();

            IUserInfoService lister = (IUserInfoService)ctx.GetObject("UserInfoService");
            MessageBox.Show(lister.ShowMsg(), "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
