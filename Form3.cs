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
    public partial class Form3 : Form
    {
        user_config user_config = new user_config();

        public Form3()
        {
            InitializeComponent();

            textBox1_text();
            textBox2_text();
            numericUpDown1.Value = user_config.position_left;
            numericUpDown2.Value = user_config.position_top;
            checkBox1.Checked = user_config.topmost > 0;
        }

        private void textBox1_text()
        {
            textBox1.Text = user_config.fontname_month + ", " + user_config.fontsize_month;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
        }
        private void textBox2_text()
        {
            textBox2.Text = user_config.fontname_day + ", " + user_config.fontsize_day;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = new Font(user_config.fontname_month, user_config.fontsize_month, user_config.fontstyle_month_enum());
            try
            {
                if (fontDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    user_config.fontname_month = fontDialog1.Font.FontFamily.Name;
                    user_config.fontstyle_month = fontDialog1.Font.Style.ToString();
                    user_config.fontsize_month = (int)Math.Round(fontDialog1.Font.Size);
                }
                textBox1_text();
            }
            catch
            {
                MessageBox.Show("True Typeフォントのみ使用できます。");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = new Font(user_config.fontname_day, user_config.fontsize_day, user_config.fontstyle_day_enum());
            try
            {
                if (fontDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    user_config.fontname_day = fontDialog1.Font.FontFamily.Name;
                    user_config.fontstyle_day = fontDialog1.Font.Style.ToString();
                    user_config.fontsize_day = (int)Math.Round(fontDialog1.Font.Size);
                }
                textBox2_text();
            }
            catch
            {
                MessageBox.Show("True Typeフォントのみ使用できます。");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            user_config.position_top = (int)numericUpDown2.Value;
            user_config.position_left = (int)numericUpDown1.Value;
            user_config.topmost = checkBox1.Checked ? 1 : 0;
            user_config.reg_all();
            this.Close();
        }
    }
}
