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
        DateTime dtm_today = DateTime.Today;
        Label[] lbl_arr;
        

        public Form1()
        {
            InitializeComponent();

            this.Left = Properties.Settings.Default.form_x;
            this.Top = Properties.Settings.Default.form_y;
            this.TopMost = Properties.Settings.Default.form_topmost;
            最前面に表示ToolStripMenuItem.Checked = this.TopMost;

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
                Properties.Settings.Default.form_x = this.Left;
                Properties.Settings.Default.form_y = this.Top;
            }
        }

        private void Form1_Closing(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void show_calendar(DateTime dtm_tgt)
        {
            dtm_tgt = new DateTime(dtm_tgt.Year, dtm_tgt.Month, 1);

            int int_dim = DateTime.DaysInMonth(dtm_tgt.Year, dtm_tgt.Month);
            int int_row = 0;
            int int_col = 0;
            int int_w = 17;
            int int_h = 17;
            int int_day;

            lbl_arr = new Label[int_dim+1];
            lbl_arr[0] = new Label();
            lbl_arr[0].Text = dtm_tgt.Year.ToString() + "年" + dtm_tgt.Month.ToString() + "月";
            lbl_arr[0].BackColor = Color.FromArgb(0, 0, 0, 0);
            lbl_arr[0].Width = 110;
            lbl_arr[0].Height = 20;
            lbl_arr[0].Left = 5;
            lbl_arr[0].Top = 0;
            lbl_arr[0].TextAlign = ContentAlignment.MiddleCenter;
            lbl_arr[0].Font = new Font("Meirio", 12, GraphicsUnit.Pixel);
            lbl_arr[0].Padding = new Padding(0);
            lbl_arr[0].Margin = new Padding(0);
            lbl_arr[0].ForeColor = Color.FromArgb(0, 51, 51, 51);


            for (int i = 1; i <= int_dim; i++)
            {
                int_day = i;
                dtm_tgt = new DateTime(dtm_tgt.Year, dtm_tgt.Month, int_day);
                int_col = (int)dtm_tgt.DayOfWeek;

                lbl_arr[i] = new Label();
                lbl_arr[i].Text = int_day.ToString();
                lbl_arr[i].BackColor = Color.FromArgb(0, 0, 0, 0);
                lbl_arr[i].Width = int_w +3;
                lbl_arr[i].Height = int_h;
                lbl_arr[i].Left = int_col * int_w;
                lbl_arr[i].Top = lbl_arr[0].Height + int_row * int_h;
                lbl_arr[i].TextAlign = ContentAlignment.MiddleCenter;
                lbl_arr[i].Font = new Font("Meirio", 10, GraphicsUnit.Pixel);
                lbl_arr[i].Padding = new Padding(0);
                lbl_arr[i].Margin = new Padding(0);
                if (int_col == 6)
                {
                    lbl_arr[i].ForeColor = Color.FromArgb(0, 51, 51, 204);
                    int_row++;
                }
                if (int_col == 0 || GenCalendar.HolidayChecker.Holiday(dtm_tgt).holiday != 0)
                {
                    lbl_arr[i].ForeColor = Color.FromArgb(0, 204, 51, 51);
                }
                if (GenCalendar.HolidayChecker.Holiday(dtm_tgt).holiday != 0)
                {
                    toolTip1.SetToolTip(lbl_arr[i], GenCalendar.HolidayChecker.Holiday(dtm_tgt).name);
                }
                
            }
            this.Controls.AddRange(lbl_arr);
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void 最前面に表示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            最前面に表示ToolStripMenuItem.Checked = this.TopMost;
            Properties.Settings.Default.form_topmost = this.TopMost;
        }

        private void wallpaperCalendarについてToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.AddOwnedForm(Form2);
            Form2 frm_2 = new Form2();
            frm_2.Show();
        }

    }
}
