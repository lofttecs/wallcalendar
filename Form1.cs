using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.CodeDom.Compiler;

namespace wallpaper_calendar
{
    public partial class Form1 : Form
    {
        Point mousePoint;
        DateTime dtm_today;
        Label[] lbl_arr;
        user_config user_config;

        public Form1()
        {
            InitializeComponent();

            user_config = new user_config();
            dtm_today = DateTime.Today;
            this.Left = user_config.position_left;
            this.Top = user_config.position_top;
            this.TopMost = user_config.topmost > 0;
            ToolStripMenuItem2.Checked = this.TopMost;

            show_calendar(dtm_today);
        }

        private void Form1_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                mousePoint = new Point(e.X, e.Y);
            }
        }
        private void Form1_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        private void show_calendar(DateTime dtm_tgt)
        {
            this.BackColor = Color.FromArgb(255, user_config.colorint_background[1], user_config.colorint_background[2], user_config.colorint_background[3]);
            this.Opacity = user_config.opacityint_form1;
            dtm_tgt = new DateTime(dtm_tgt.Year, dtm_tgt.Month, 1);

            int int_dim = DateTime.DaysInMonth(dtm_tgt.Year, dtm_tgt.Month);
            int int_row = 0;
            int int_col = 0;
            int int_w = 17;
            int int_h = 17;
            int int_day;

            lbl_arr = new Label[int_dim + 1];
            lbl_arr[0] = new Label();
            lbl_arr[0].Text = dtm_tgt.Year.ToString() + "年" + dtm_tgt.Month.ToString() + "月";
            lbl_arr[0].BackColor = Color.FromArgb(0, 255, 255, 255);
            lbl_arr[0].Width = this.Width;
            lbl_arr[0].Left = 0;
            lbl_arr[0].Top = 0;
            lbl_arr[0].TextAlign = ContentAlignment.MiddleCenter;
            lbl_arr[0].Font = new Font(user_config.fontname_month, user_config.fontsize_month, user_config.fontstyle_month_enum());
            lbl_arr[0].Padding = new Padding(0, 0, 0, 5);
            lbl_arr[0].Margin = new Padding(0);
            lbl_arr[0].ForeColor = Color.FromArgb(user_config.colorint_month[0], user_config.colorint_month[1], user_config.colorint_month[2], user_config.colorint_month[3]);
            lbl_arr[0].AutoSize = true;
            lbl_arr[0].Anchor = (AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right);

            Font font_day = new Font(user_config.fontname_day, user_config.fontsize_day, user_config.fontstyle_day_enum()); 
            for (int i = 1; i <= int_dim; i++)
            {
                int_day = i;
                dtm_tgt = new DateTime(dtm_tgt.Year, dtm_tgt.Month, int_day);
                int_col = (int)dtm_tgt.DayOfWeek;

                lbl_arr[i] = new Label();
                lbl_arr[i].Text = int_day.ToString();
                lbl_arr[i].BackColor = Color.FromArgb(0, 255, 255, 255);
                lbl_arr[i].AutoSize = true;
                lbl_arr[i].Padding = new Padding(0);
                lbl_arr[i].Margin = new Padding(0);
                lbl_arr[i].Font = font_day;
                lbl_arr[i].TextAlign = ContentAlignment.MiddleCenter;
                lbl_arr[i].ForeColor = Color.FromArgb(user_config.colorint_weekday[0], user_config.colorint_weekday[1], user_config.colorint_weekday[2], user_config.colorint_weekday[3]);

                if (int_col == 6)
                {
                    lbl_arr[i].ForeColor = Color.FromArgb(user_config.colorint_saturday[0], user_config.colorint_saturday[1], user_config.colorint_saturday[2], user_config.colorint_saturday[3]);
                    int_row++;
                }
                if (int_col == 0 || GenCalendar.HolidayChecker.Holiday(dtm_tgt).holiday != 0)
                {
                    lbl_arr[i].ForeColor = Color.FromArgb(user_config.colorint_holiday[0], user_config.colorint_holiday[1], user_config.colorint_holiday[2], user_config.colorint_holiday[3]);
                }
                if (GenCalendar.HolidayChecker.Holiday(dtm_tgt).holiday != 0)
                {
                    toolTip1.SetToolTip(lbl_arr[i], GenCalendar.HolidayChecker.Holiday(dtm_tgt).name);
                }
                if (dtm_today == dtm_tgt)
                {
                    lbl_arr[i].BackColor = Color.FromArgb(user_config.colorint_today[0], user_config.colorint_today[1], user_config.colorint_today[2], user_config.colorint_today[3]);
                }
                
            }
            this.Controls.AddRange(lbl_arr);

            lbl_arr[0].AutoSize = false;
            lbl_arr[0].Width = this.Width;
            lbl_arr[0].MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            lbl_arr[0].MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            lbl_arr[0].MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);

            if (lbl_arr[int_dim].Width > int_w || lbl_arr[int_dim].Height > int_h)
            {
                int_w = lbl_arr[int_dim].Width;
                int_h = lbl_arr[int_dim].Height;
            }
            int_row = 0;
            for (int i = 1; i <= int_dim; i++)
            {
                int_day = i;
                dtm_tgt = new DateTime(dtm_tgt.Year, dtm_tgt.Month, int_day);
                int_col = (int)dtm_tgt.DayOfWeek;
                lbl_arr[i].Left = int_col * int_w;
                lbl_arr[i].Top = lbl_arr[0].Height + int_row * int_h;
                lbl_arr[i].AutoSize = false;
                lbl_arr[i].Width = int_w;
                lbl_arr[i].Height = int_h;
                if (int_col == 6)
                {
                    int_row++;
                }
            }
            this.Width = int_w * 7;
            this.Height = lbl_arr[0].Height + lbl_arr[int_dim].Top + int_h / 2;
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            ToolStripMenuItem2.Checked = this.TopMost;
            user_config.reg_topmost(this.TopMost ? 1 : 0);
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form2 frm_2 = new Form2();
            frm_2.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 frm_3 = new Form3();
            frm_3.FormClosed += new FormClosedEventHandler(Form3_FormClosed);
            frm_3.Show(this);
        }
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            user_config = new user_config();
            this.Controls.Clear();
            this.Left = user_config.position_left;
            this.Top = user_config.position_top;
            this.TopMost = user_config.topmost > 0;
            ToolStripMenuItem2.Checked = this.TopMost;

            show_calendar(dtm_today);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            user_config.reg_position_top(this.Top);
            user_config.reg_position_left(this.Left);
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Activate();
        }
    }
}
