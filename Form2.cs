using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace wallpaper_calendar
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            string ver = Application.ProductVersion;
            label3.Text = "version " + ver;
            //AssemblyCopyrightの取得
            System.Reflection.AssemblyCopyrightAttribute asmcpy =
                (System.Reflection.AssemblyCopyrightAttribute)
                Attribute.GetCustomAttribute(
                System.Reflection.Assembly.GetExecutingAssembly(),
                typeof(System.Reflection.AssemblyCopyrightAttribute));
            label2.Text = asmcpy.Copyright + " All Rights Reserved.";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://web.loft-net.co.jp/lofttecs/");
        }
    }
}
