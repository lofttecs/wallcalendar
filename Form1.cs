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
        DateTime dtm_current;
        Label[] lbl_arr;
        user_config user_config;
        Label label_r;
        Label label_l;
        FlowLayoutPanel flowLayoutPanel1;

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
            Hide();

            dtm_today = DateTime.Today;

            this.BackColor = Color.FromArgb(255, user_config.colorint_background[1], user_config.colorint_background[2], user_config.colorint_background[3]);
            this.Opacity = user_config.opacityint_form1;
            this.Padding = new Padding(0, 0, 0, 0);
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = true;
            dtm_tgt = new DateTime(dtm_tgt.Year, dtm_tgt.Month, 1);
            dtm_current = dtm_tgt;

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
            lbl_arr[0].Left = 0;
            lbl_arr[0].Top = 0;
            lbl_arr[0].TextAlign = ContentAlignment.MiddleCenter;
            lbl_arr[0].Font = new Font(user_config.fontname_month, user_config.fontsize_month, user_config.fontstyle_month_enum());
            lbl_arr[0].Padding = new Padding(0, 0, 0, 5);
            lbl_arr[0].Margin = new Padding(0);
            lbl_arr[0].ForeColor = Color.FromArgb(user_config.colorint_month[0], user_config.colorint_month[1], user_config.colorint_month[2], user_config.colorint_month[3]);
            lbl_arr[0].AutoSize = true;

            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Padding = new Padding(0);
            Font font_day = new Font(user_config.fontname_day, user_config.fontsize_day, user_config.fontstyle_day_enum()); 
            for (int i = 1; i <= int_dim; i++)
            {
                int_day = i;
                dtm_tgt = new DateTime(dtm_tgt.Year, dtm_tgt.Month, int_day);
                int_col = (int)dtm_tgt.DayOfWeek;

                lbl_arr[i] = new Label();
                lbl_arr[i].Text = int_day.ToString();
                lbl_arr[i].BackColor = Color.FromArgb(0, 255, 255, 255);
                lbl_arr[i].AutoSize = false;
                lbl_arr[i].Width = lbl_arr[i].PreferredWidth;
                lbl_arr[i].Height = lbl_arr[i].PreferredHeight;
                lbl_arr[i].Padding = new Padding(0);
                lbl_arr[i].Margin = new Padding(0);
                lbl_arr[i].Font = font_day;
                lbl_arr[i].TextAlign = ContentAlignment.MiddleCenter;
                lbl_arr[i].ForeColor = Color.FromArgb(user_config.colorint_weekday[0], user_config.colorint_weekday[1], user_config.colorint_weekday[2], user_config.colorint_weekday[3]);

                flowLayoutPanel1.Controls.Add(lbl_arr[i]);

                if (int_col == 6)
                {
                    lbl_arr[i].ForeColor = Color.FromArgb(user_config.colorint_saturday[0], user_config.colorint_saturday[1], user_config.colorint_saturday[2], user_config.colorint_saturday[3]);
                    int_row++;
                    flowLayoutPanel1.SetFlowBreak(lbl_arr[i], true);
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

                if (lbl_arr[i].PreferredWidth > int_w || lbl_arr[i].PreferredHeight > int_h)
                {
                    int_w = lbl_arr[i].PreferredWidth;
                    int_h = lbl_arr[i].PreferredHeight;
                }
            }
            this.Controls.Add(lbl_arr[0]);
            this.Controls.Add(flowLayoutPanel1);
            flowLayoutPanel1.Location = new Point(0, lbl_arr[0].Height);

            lbl_arr[0].MouseDown += new MouseEventHandler(this.Form1_MouseDown);
            lbl_arr[0].MouseMove += new MouseEventHandler(this.Form1_MouseMove);
            lbl_arr[0].MouseUp += new MouseEventHandler(this.Form1_MouseUp);

            flowLayoutPanel1.MouseDown += new MouseEventHandler(this.Form1_MouseDown);
            flowLayoutPanel1.MouseMove += new MouseEventHandler(this.Form1_MouseMove);
            flowLayoutPanel1.MouseUp += new MouseEventHandler(this.Form1_MouseUp);

            int_row = 0;
            for (int i = 1; i <= int_dim; i++)
            {
                int_day = i;
                dtm_tgt = new DateTime(dtm_tgt.Year, dtm_tgt.Month, int_day);
                int_col = (int)dtm_tgt.DayOfWeek;
                lbl_arr[i].AutoSize = false;
                lbl_arr[i].Width = int_w;
                lbl_arr[i].Height = int_h;
                if (int_col == 6)
                {
                    int_row++;
                }
            }
            lbl_arr[1].Margin = new Padding(int_w * (int)(new DateTime(dtm_tgt.Year, dtm_tgt.Month, 1)).DayOfWeek, 0, 0, 0);
            flowLayoutPanel1.AutoSize = false;
            flowLayoutPanel1.Width = int_w * 7;
            flowLayoutPanel1.Height = flowLayoutPanel1.PreferredSize.Height;

            label_r = new Label();
            label_r.AutoSize = false;
            label_r.Text = ">";
            label_r.Left = this.Width - label_r.PreferredWidth;
            label_r.Top = 0;
            label_r.Width = label_r.PreferredWidth;
            label_r.Height = lbl_arr[0].Height;

            label_l = new Label();
            label_l.AutoSize = false;
            label_l.Text = "<";
            label_l.Left = 0;
            label_l.Top = 0;
            label_l.Width = label_r.PreferredWidth;
            label_l.Height = lbl_arr[0].Height;

            this.Controls.Add(label_r);
            this.Controls.Add(label_l);

            label_r.ForeColor = Color.FromArgb(user_config.colorint_month[0], user_config.colorint_month[1], user_config.colorint_month[2], user_config.colorint_month[3]);
            label_l.ForeColor = Color.FromArgb(user_config.colorint_month[0], user_config.colorint_month[1], user_config.colorint_month[2], user_config.colorint_month[3]);
            label_r.BackColor = Color.FromArgb(0, 0, 0, 0);
            label_l.BackColor = Color.FromArgb(0, 0, 0, 0);
            label_r.Visible = true;
            label_l.Visible = true;
            label_r.Click += new EventHandler(this.show_next_month);
            label_l.Click += new EventHandler(this.show_previous_month);

            if (label_l.Width + label_r.Width + lbl_arr[0].Width > this.Width)
            {
                lbl_arr[0].Left = label_l.Width;
                label_r.Left = lbl_arr[0].Left + lbl_arr[0].Width;
            }

            flowLayoutPanel1.Left = this.Width / 2 - flowLayoutPanel1.Width / 2;

            if (lbl_arr[0].Width < flowLayoutPanel1.Width)
            {
                lbl_arr[0].Left = flowLayoutPanel1.Width / 2 - lbl_arr[0].Width / 2;
            }
            this.Show();
        }

        private void show_next_month(object sender, EventArgs e)
        {
            this.Controls.Clear();
            show_calendar(dtm_current.AddMonths(1));
        }
        private void show_previous_month(object sender, EventArgs e)
        {
            this.Controls.Clear();
            show_calendar(dtm_current.AddMonths(-1));
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
