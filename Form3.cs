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
            button5.BackColor = Color.FromArgb(255, user_config.colorint_background[1], user_config.colorint_background[2], user_config.colorint_background[3]);
            button6.BackColor = Color.FromArgb(255, user_config.colorint_today[1], user_config.colorint_today[2], user_config.colorint_today[3]);
            button7.BackColor = Color.FromArgb(255, user_config.colorint_weekday[1], user_config.colorint_weekday[2], user_config.colorint_weekday[3]);
            button8.BackColor = Color.FromArgb(255, user_config.colorint_holiday[1], user_config.colorint_holiday[2], user_config.colorint_holiday[3]);
            button9.BackColor = Color.FromArgb(255, user_config.colorint_saturday[1], user_config.colorint_saturday[2], user_config.colorint_saturday[3]);
            button10.BackColor = Color.FromArgb(255, user_config.colorint_month[1], user_config.colorint_month[2], user_config.colorint_month[3]);
            numericUpDown3.Value = (int)(user_config.opacityint_form1 * 100);
            colorDialog1.FullOpen = true;
            colorDialog1.AnyColor = true;
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
            user_config.colorint_background = new int[] { button5.BackColor.A, button5.BackColor.R, button5.BackColor.G, button5.BackColor.B };
            user_config.colorint_today = new int[] { button6.BackColor.A, button6.BackColor.R, button6.BackColor.G, button6.BackColor.B };
            user_config.colorint_weekday = new int[] { button7.BackColor.A, button7.BackColor.R, button7.BackColor.G, button7.BackColor.B };
            user_config.colorint_holiday = new int[] { button8.BackColor.A, button8.BackColor.R, button8.BackColor.G, button8.BackColor.B };
            user_config.colorint_saturday = new int[] { button9.BackColor.A, button9.BackColor.R, button9.BackColor.G, button9.BackColor.B };
            user_config.colorint_month = new int[] { button10.BackColor.A, button10.BackColor.R, button10.BackColor.G, button10.BackColor.B };
            user_config.opacityint_form1 = (double)(numericUpDown3.Value / 100);
            user_config.reg_all();
            this.Close();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button5.BackColor;
            colorDialog1.CustomColors = new int[] { 255, button5.BackColor.R, button5.BackColor.G, button5.BackColor.B };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button5.BackColor = colorDialog1.Color;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button6.BackColor;
            colorDialog1.CustomColors = new int[] { 255, button6.BackColor.R, button6.BackColor.G, button6.BackColor.B };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button6.BackColor = colorDialog1.Color;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button7.BackColor;
            colorDialog1.CustomColors = new int[] { 255, button7.BackColor.R, button7.BackColor.G, button7.BackColor.B };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button7.BackColor = colorDialog1.Color;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button8.BackColor;
            colorDialog1.CustomColors = new int[] { 255, button8.BackColor.R, button8.BackColor.G, button8.BackColor.B };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button8.BackColor = colorDialog1.Color;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button9.BackColor;
            colorDialog1.CustomColors = new int[] { 255, button9.BackColor.R, button9.BackColor.G, button9.BackColor.B };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button9.BackColor = colorDialog1.Color;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button10.BackColor;
            colorDialog1.CustomColors = new int[] { 255, button10.BackColor.R, button10.BackColor.G, button10.BackColor.B };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button10.BackColor = colorDialog1.Color;
            }
        }

    }
}
