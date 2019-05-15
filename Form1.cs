using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace wallcalendar
{
    public partial class Form1 : Form
    {
        Point point_mouse;
        DateTime datetime_today;
        DateTime datetime_display;
        Label[] label_days;
        Label label_next;
        Label label_back;
        Label label_month;
        Label label_week1, label_week2, label_week3, label_week4, label_week5, label_week6, label_week7;
        String string_config_path;
        Settings settings = new Settings();
        System.Xml.Serialization.XmlSerializer xmlserializer1;
        //System.Timers.Timer timer;
        Timer timer;

        public Form1()
        {
            InitializeComponent();

            //string_config_path = Directory.GetCurrentDirectory() + "\\config.xml";
            string_config_path = AppDomain.CurrentDomain.BaseDirectory + "\\config.xml";

            xmlserializer1 = new System.Xml.Serialization.XmlSerializer(typeof(Settings));

            if (!File.Exists(string_config_path))
            {
                init_settings();
                write_settings();
            }

            try
            {
                using (StreamReader streamreader1 = new StreamReader(string_config_path, new UTF8Encoding(false)))
                    settings = (Settings)xmlserializer1.Deserialize(streamreader1);

            }
            catch (Exception ex)
            {
                MessageBox.Show("設定値が正しくありません。\r\n" + ex.Message);
                init_settings();
            }

            if (settings.fontsize_month > 500
                || settings.fontsize_month <= 0
                || settings.fontsize_day > 500
                || settings.fontsize_day <= 0
                || (settings.position_left < -32768 && settings.position_left > 32767)
                || (settings.position_top < -32768 && settings.position_top > 32767)
                || settings.opacity <= 0
                || settings.opacity > 100
                || !System.Text.RegularExpressions.Regex.IsMatch(settings.color_weekday ?? "", "^[0-9a-f]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                || !System.Text.RegularExpressions.Regex.IsMatch(settings.color_holiday ?? "", "^[0-9a-f]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                || !System.Text.RegularExpressions.Regex.IsMatch(settings.color_saturday ?? "", "^[0-9a-f]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                || !System.Text.RegularExpressions.Regex.IsMatch(settings.color_month ?? "", "^[0-9a-f]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                || !System.Text.RegularExpressions.Regex.IsMatch(settings.color_background ?? "", "^[0-9a-f]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                || !System.Text.RegularExpressions.Regex.IsMatch(settings.color_today_back ?? "", "^[0-9a-f]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                )
            {
                MessageBox.Show("設定値が正しくありません。");
                init_settings();
            }

            label_days = new Label[31];
            for (int i = 0; i < 31; i++)
            {
                label_days[i] = (Label)Controls.Find("label" + (i + 1).ToString(), true)[0];
                label_days[i].MouseDown += new MouseEventHandler(Form1_MouseDown);
            }
            label_next = (Label)Controls.Find("label32", true)[0];
            label_back = (Label)Controls.Find("label33", true)[0];
            label_month = (Label)Controls.Find("label34", true)[0];

            datetime_today = DateTime.Today;
            datetime_display = datetime_today;

            label_week1 = (Label)Controls.Find("label35", true)[0];
            label_week2 = (Label)Controls.Find("label36", true)[0];
            label_week3 = (Label)Controls.Find("label37", true)[0];
            label_week4 = (Label)Controls.Find("label38", true)[0];
            label_week5 = (Label)Controls.Find("label39", true)[0];
            label_week6 = (Label)Controls.Find("label40", true)[0];
            label_week7 = (Label)Controls.Find("label41", true)[0];

            label_month.MouseDown += new MouseEventHandler(Form1_MouseDown);

            label_week1.MouseDown += new MouseEventHandler(Form1_MouseDown);
            label_week2.MouseDown += new MouseEventHandler(Form1_MouseDown);
            label_week3.MouseDown += new MouseEventHandler(Form1_MouseDown);
            label_week4.MouseDown += new MouseEventHandler(Form1_MouseDown);
            label_week5.MouseDown += new MouseEventHandler(Form1_MouseDown);
            label_week6.MouseDown += new MouseEventHandler(Form1_MouseDown);
            label_week7.MouseDown += new MouseEventHandler(Form1_MouseDown);
            flowLayoutPanel1.MouseDown += new MouseEventHandler(Form1_MouseDown);

            show_callendar();
        }
        public void init_settings()
        {
            settings.fontsize_day = 16;
            settings.fontsize_month = 24;
            settings.opacity = 100;
            settings.color_background = "FFFFFFFF";
            settings.color_today_back = "00FFFFFF";
            settings.color_weekday = "FF000000";
            settings.color_holiday = "FFFF0000";
            settings.color_saturday = "FF0000FF";
            settings.color_month = "FF000000";
            settings.position_left = 0;
            settings.position_top = 0;
            settings.fontname = SystemFonts.MessageBoxFont.FontFamily.Name;
        }

        public void write_settings()
        {
            try
            {
                using (StreamWriter streamwriter1 = new StreamWriter(string_config_path, false, new UTF8Encoding(false)))
                    xmlserializer1.Serialize(streamwriter1, settings);
            }
            catch (Exception ex)
            {
                MessageBox.Show("設定ファイルへの書き込みができません。\r\n" + ex.Message);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Capture = true;
                point_mouse = new Point(e.X, e.Y);

                if (sender.Equals(label_month))
                {
                    point_mouse.X += label_month.Left;
                    point_mouse.Y += label_month.Top;
                }
                else if (sender.GetType().Equals(typeof(Label)))
                {
                    point_mouse.X += flowLayoutPanel1.Left;
                    point_mouse.Y += flowLayoutPanel1.Top;
                    point_mouse.X += ((Label)sender).Left;
                    point_mouse.Y += ((Label)sender).Top;
                }
                else if (sender.Equals(flowLayoutPanel1))
                {
                    point_mouse.X += flowLayoutPanel1.Left;
                    point_mouse.Y += flowLayoutPanel1.Top;
                }
                //Console.WriteLine("start" + point_mouse.ToString());
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && Capture)
            {
                Left += e.X - point_mouse.X;
                Top += e.Y - point_mouse.Y;
                //Console.WriteLine(Location.ToString());
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            settings.position_left = Left;
            settings.position_top = Top;
            write_settings();
            Capture = false;
            //Console.WriteLine("end" + Location.ToString());
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem2.Enabled = false;
            Cursor = Cursors.WaitCursor;
            Form3 form3 = new Form3();
            form3.ComboBox1 = settings.fontname;
            form3.NumericUpDown1 = settings.fontsize_month;
            form3.NumericUpDown2 = settings.fontsize_day;
            form3.NumericUpDown3 = settings.position_left;
            form3.NumericUpDown4 = settings.position_top;
            form3.NumericUpDown5 = settings.opacity;
            form3.Button3 = settings.color_background;
            form3.Button4 = settings.color_today_back;
            form3.Button5 = settings.color_weekday;
            form3.Button6 = settings.color_holiday;
            form3.Button7 = settings.color_saturday;
            form3.Button8 = settings.color_month;
            form3.CheckBox4 = settings.transparent_background;
            form3.CheckBox5 = settings.transparent_today_back;
            form3.CheckBox6 = settings.topmost;
            form3.CheckBox1 = settings.bold_today;
            form3.CheckBox2 = settings.italic_today;
            form3.CheckBox3 = settings.underline_today;
            form3.ComboBox2 = settings.language;
            form3.radioButton_1_2 = settings.monday_start;
            form3.ShowDialog(this);
            Cursor = Cursors.Default;

            if (form3.DialogResult == DialogResult.OK)
            {
                settings.fontname = form3.ComboBox1;
                settings.fontsize_month = form3.NumericUpDown1;
                settings.fontsize_day = form3.NumericUpDown2;
                settings.position_left = form3.NumericUpDown3;
                settings.position_top = form3.NumericUpDown4;
                settings.opacity = form3.NumericUpDown5;
                settings.color_background = form3.Button3;
                settings.color_today_back = form3.Button4;
                settings.color_weekday = form3.Button5;
                settings.color_holiday = form3.Button6;
                settings.color_saturday = form3.Button7;
                settings.color_month = form3.Button8;
                settings.transparent_background = form3.CheckBox4;
                settings.transparent_today_back = form3.CheckBox5;
                settings.topmost = form3.CheckBox6;
                settings.bold_today = form3.CheckBox1;
                settings.italic_today = form3.CheckBox2;
                settings.underline_today = form3.CheckBox3;
                settings.language = form3.ComboBox2;
                settings.monday_start = form3.radioButton_1_2;
                write_settings();
                show_callendar();
            }
            form3.Dispose();
            ToolStripMenuItem2.Enabled = true;
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            ToolStripMenuItem3.Checked = TopMost;
            settings.topmost = TopMost;
            write_settings();
        }

        private void Label32_Click(object sender, EventArgs e)
        {
            NextMonth();
        }

        private void Label33_Click(object sender, EventArgs e)
        {
            PreviousMonth();
        }

        private void NotifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Activate();
            }
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem4.Enabled = false;
            Form2 form2 = new Form2();
            form2.ShowDialog(this);
            form2.Dispose();
            ToolStripMenuItem4.Enabled = true;
        }

        public void show_callendar()
        {
            DateTime datetime_current;

            //FontStyle fontStyle_today = FontStyle.Bold;
            FontStyle fontStyle_today = FontStyle.Regular;
            if (settings.bold_today) fontStyle_today |= FontStyle.Bold;
            if (settings.italic_today) fontStyle_today |= FontStyle.Italic;
            if (settings.underline_today) fontStyle_today |= FontStyle.Underline;
            //fontStyle |= FontStyle.Italic;
            //fontStyle_today |= FontStyle.Underline;
            //fontStyle = FontStyle.Regular;

            Font font_day = new Font(settings.fontname, settings.fontsize_day, FontStyle.Regular, GraphicsUnit.Pixel);
            Font font_today = new Font(settings.fontname, settings.fontsize_day, fontStyle_today, GraphicsUnit.Pixel);
            Font font_month = new Font(settings.fontname, settings.fontsize_month, FontStyle.Regular, GraphicsUnit.Pixel);
            Color color_holiday = Color.FromArgb(Convert.ToInt32(settings.color_holiday, 16));
            Color color_weekday = Color.FromArgb(Convert.ToInt32(settings.color_weekday, 16));
            Color color_saturday = Color.FromArgb(Convert.ToInt32(settings.color_saturday, 16));
            Color color_background = Color.FromArgb(Convert.ToInt32(settings.color_background, 16));
            Color color_month = Color.FromArgb(Convert.ToInt32(settings.color_month, 16));
            Color color_today_back = Color.FromArgb(Convert.ToInt32(settings.color_today_back, 16));

            datetime_today = DateTime.Today;

            TransparencyKey = Color.FromArgb(Convert.ToInt32("FFFFFFFF", 16));
            AllowTransparency = settings.transparent_background;
            if (settings.transparent_background)
            {
                BackColor = TransparencyKey;
                while (TransparencyKey == color_holiday
                    || TransparencyKey == color_saturday
                    || TransparencyKey == color_weekday
                    || TransparencyKey == color_today_back
                    || TransparencyKey == color_month)
                {
                    TransparencyKey = Color.FromArgb(Convert.ToInt32("FF" + (BackColor.R - 1).ToString("X2") + BackColor.G.ToString("X2") + BackColor.B.ToString("X2"), 16));
                    BackColor = TransparencyKey;
                }
            }

            Left = settings.position_left;
            Top = settings.position_top;
            TopMost = settings.topmost;
            Opacity = (float)settings.opacity / 100;

            int int_day = 1;
            int int_maxwidth = 0;
            int int_maxheight = 0;
            int int_1dayofweek = 0;

            //flowLayoutPanel1.AutoSize = true;
            panel1.BackColor = color_background;
            if (settings.transparent_background) panel1.BackColor = Color.Transparent;


            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("ja-JP");

            //cultureInfo = System.Globalization.CultureInfo.InvariantCulture;

            label_month.Font = font_month;

            String string_dateformat;
            if (settings.language == "en-GB" || settings.language == "fr-FR" || settings.language == "de-DE" || settings.language == "it-IT")
            {
                cultureInfo = new System.Globalization.CultureInfo(settings.language);
                string_dateformat = "MMMM yyyy";
            }
            else
            {
                string_dateformat = "yyyy'年'M'月'";
            }
            label_month.Text = datetime_display.ToString(string_dateformat, cultureInfo);
            label_month.Size = new Size(0, label_month.PreferredHeight);
            label_month.Width = label_month.PreferredWidth;
            label_month.Height = label_month.PreferredHeight;
            label_month.ForeColor = color_month;

            //label_month.AutoSize = true;

            if (settings.transparent_background)
            {
                DrowLabelImage(label_month, font_month);
            }
            else
            {
                label_month.Image = null;
            }

            DateTime datetime_week = new DateTime(2015, 2, 1);
            label_week1.Text = datetime_week.ToString("ddd", cultureInfo);
            datetime_week = datetime_week.AddDays(1);
            label_week2.Text = datetime_week.ToString("ddd", cultureInfo);
            datetime_week = datetime_week.AddDays(1);
            label_week3.Text = datetime_week.ToString("ddd", cultureInfo);
            datetime_week = datetime_week.AddDays(1);
            label_week4.Text = datetime_week.ToString("ddd", cultureInfo);
            datetime_week = datetime_week.AddDays(1);
            label_week5.Text = datetime_week.ToString("ddd", cultureInfo);
            datetime_week = datetime_week.AddDays(1);
            label_week6.Text = datetime_week.ToString("ddd", cultureInfo);
            datetime_week = datetime_week.AddDays(1);
            label_week7.Text = datetime_week.ToString("ddd", cultureInfo);

            //Console.WriteLine(System.Globalization.CultureInfo.InvariantCulture);
            //label_week1.AutoSize = true;
            //label_week2.AutoSize = true;
            //label_week3.AutoSize = true;
            //label_week4.AutoSize = true;
            //label_week5.AutoSize = true;
            //label_week6.AutoSize = true;
            //label_week7.AutoSize = true;

            label_week1.Font = font_day;
            label_week2.Font = font_day;
            label_week3.Font = font_day;
            label_week4.Font = font_day;
            label_week5.Font = font_day;
            label_week6.Font = font_day;
            label_week7.Font = font_day;

            label_week1.ForeColor = color_holiday;
            label_week2.ForeColor = color_weekday;
            label_week3.ForeColor = color_weekday;
            label_week4.ForeColor = color_weekday;
            label_week5.ForeColor = color_weekday;
            label_week6.ForeColor = color_weekday;
            label_week7.ForeColor = color_saturday;

            label_week1.Height = label_week1.PreferredHeight;
            label_week2.Height = label_week2.PreferredHeight;
            label_week3.Height = label_week3.PreferredHeight;
            label_week4.Height = label_week4.PreferredHeight;
            label_week5.Height = label_week5.PreferredHeight;
            label_week6.Height = label_week6.PreferredHeight;
            label_week7.Height = label_week7.PreferredHeight;

            if (int_maxwidth < label_week1.PreferredWidth) int_maxwidth = label_week1.PreferredWidth;
            if (int_maxwidth < label_week2.PreferredWidth) int_maxwidth = label_week2.PreferredWidth;
            if (int_maxwidth < label_week3.PreferredWidth) int_maxwidth = label_week3.PreferredWidth;
            if (int_maxwidth < label_week4.PreferredWidth) int_maxwidth = label_week4.PreferredWidth;
            if (int_maxwidth < label_week5.PreferredWidth) int_maxwidth = label_week5.PreferredWidth;
            if (int_maxwidth < label_week6.PreferredWidth) int_maxwidth = label_week6.PreferredWidth;
            if (int_maxwidth < label_week7.PreferredWidth) int_maxwidth = label_week7.PreferredWidth;

            if (settings.transparent_background)
            {
                DrowLabelImage(label_week1, font_day);
                DrowLabelImage(label_week2, font_day);
                DrowLabelImage(label_week3, font_day);
                DrowLabelImage(label_week4, font_day);
                DrowLabelImage(label_week5, font_day);
                DrowLabelImage(label_week6, font_day);
                DrowLabelImage(label_week7, font_day);
            }
            else
            {
                label_week1.Image = null;
                label_week2.Image = null;
                label_week3.Image = null;
                label_week4.Image = null;
                label_week5.Image = null;
                label_week6.Image = null;
                label_week7.Image = null;
            }
            if (settings.monday_start)
            {
                flowLayoutPanel1.SetFlowBreak(label_week7, false);
                flowLayoutPanel1.Controls.SetChildIndex(label_week1, 6);
                flowLayoutPanel1.SetFlowBreak(label_week1, true);
            }
            else
            {
                flowLayoutPanel1.SetFlowBreak(label_week1, false);
                flowLayoutPanel1.Controls.SetChildIndex(label_week1, 0);
                flowLayoutPanel1.SetFlowBreak(label_week7, true);
            }

            foreach (Label label_day in label_days)
            {
                flowLayoutPanel1.SetFlowBreak(label_day, false);
                int int_dayinmonth = DateTime.DaysInMonth(datetime_display.Year, datetime_display.Month);
                label_day.BackColor = panel1.BackColor;

                if (int_dayinmonth >= int_day)
                {
                    datetime_current = new DateTime(datetime_display.Year, datetime_display.Month, int_day);
                    label_day.Font = font_day;
                    label_day.Text = int_day.ToString();
                    //label_day.AutoSize = false;
                    label_day.ForeColor = color_weekday;

                    if (datetime_current == datetime_today)
                    {
                        label_day.Font = font_today;
                        if (!settings.transparent_today_back) label_day.BackColor = color_today_back;
                    }
                    if ((int)datetime_current.DayOfWeek == 0)
                    {
                        label_day.ForeColor = color_holiday;

                        if (settings.monday_start && int_dayinmonth != int_day)
                        {
                            flowLayoutPanel1.SetFlowBreak(label_day, true);
                        }
                    }
                    if ((int)datetime_current.DayOfWeek == 6)
                    {
                        label_day.ForeColor = color_saturday;
                        if (!settings.monday_start && int_dayinmonth != int_day)
                        {
                            flowLayoutPanel1.SetFlowBreak(label_day, true);
                        }
                    }
                    if (GenCalendar.HolidayChecker.Holiday(datetime_current).holiday != 0)
                    {
                        label_day.ForeColor = color_holiday;
                    }

                    if (int_day == 1)
                    {
                        int_1dayofweek = (int)datetime_current.DayOfWeek;
                    }

                    if (int_maxwidth < label_day.PreferredWidth)
                    {
                        int_maxwidth = label_day.PreferredWidth;
                    }
                    if (int_maxheight < label_day.PreferredHeight)
                    {
                        int_maxheight = label_day.PreferredHeight;
                    }

                    if (settings.transparent_background && datetime_current == datetime_today)
                    {
                        DrowLabelImage(label_day, font_today);
                    }
                    else if (settings.transparent_background)
                    {
                        DrowLabelImage(label_day, font_day);
                    }
                    else
                    {
                        label_day.Image = null;
                    }

                }
                else
                {
                    label_day.Text = "";
                    label_day.Image = null;
                }
                //label_day.AutoSize = false;

                int_day++;
            }


            foreach (Label label_day in label_days)
            {
                label_day.Width = int_maxwidth;
                label_day.Height = int_maxheight;
            }

            if (settings.monday_start)
            {
                switch (int_1dayofweek)
                {
                    case 0:
                        label_days[0].Margin = new Padding(int_maxwidth * 6, 0, 0, 0);
                        break;
                    default:
                        label_days[0].Margin = new Padding(int_maxwidth * (int_1dayofweek - 1), 0, 0, 0);
                        break;
                }
            }
            else
            {
                label_days[0].Margin = new Padding(int_maxwidth * int_1dayofweek, 0, 0, 0);
            }


            //label_week1.AutoSize = false;
            //label_week2.AutoSize = false;
            //label_week3.AutoSize = false;
            //label_week4.AutoSize = false;
            //label_week5.AutoSize = false;
            //label_week6.AutoSize = false;
            //label_week7.AutoSize = false;

            label_week1.Width = int_maxwidth;
            label_week2.Width = int_maxwidth;
            label_week3.Width = int_maxwidth;
            label_week4.Width = int_maxwidth;
            label_week5.Width = int_maxwidth;
            label_week6.Width = int_maxwidth;
            label_week7.Width = int_maxwidth;

            //flowLayoutPanel1.AutoSize = false;
            flowLayoutPanel1.Width = int_maxwidth * 7;
            flowLayoutPanel1.Height = flowLayoutPanel1.PreferredSize.Height;
            int int_header_width = label_month.Width + label_back.Width + label_next.Width;
            int int_panel1_width = int_maxwidth * 7 + panel1.Padding.Left + panel1.Padding.Right;
            if (int_maxwidth * 7 > int_header_width)
            {
                flowLayoutPanel1.Left = panel1.Padding.Left;
                label_month.Width = flowLayoutPanel1.Width;
                label_next.Left = flowLayoutPanel1.Width + panel1.Padding.Left - panel1.Padding.Left - label_next.Width;
                label_month.Left = (int_panel1_width - label_month.Width) / 2;
            }
            else
            {
                label_month.Width = label_month.PreferredWidth;
                int_panel1_width = int_header_width + panel1.Padding.Left + panel1.Padding.Right;
                flowLayoutPanel1.Left = (int_panel1_width - flowLayoutPanel1.Width) / 2;
                label_next.Left = label_month.Width + label_back.Width;
                label_month.Left = panel1.Padding.Top + label_back.Width;
            }
            label_back.Left = panel1.Padding.Left;
            label_back.Top = panel1.Padding.Top;
            label_next.Top = panel1.Padding.Top;
            label_month.Top = panel1.Padding.Top;

            flowLayoutPanel1.Top = label_month.Height + panel1.Padding.Top + 3;

            //label_month.AutoSize = false;
            //label_month.Width = label_month.PreferredWidth;

            DrowNotifyIcon();

            font_day.Dispose();
            font_today.Dispose();
            font_month.Dispose();

            TimeSpan timeSpan = (DateTime.Today.AddDays(1)) - DateTime.Now;
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
            if (timeSpan.TotalMilliseconds > 0)
            {
                timer = new Timer();
                timer.Tick += new EventHandler(TimerShowCalendar);
                timer.Interval = (int)timeSpan.TotalMilliseconds;
                timer.Start();
            }
        }
        private void TimerShowCalendar(object sender, EventArgs e)
        {
            if (datetime_display.Month == DateTime.Today.AddMonths(-1).Month) datetime_display = DateTime.Today;

            show_callendar();
        }
        private void DrowLabelImage(Label label, Font font)
        {
            //使用するブラシ
            Brush brush = new SolidBrush(label.ForeColor);
            //文字列を描画する
            Bitmap bitmap = new Bitmap(label.PreferredWidth, label.PreferredHeight);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            graphics.DrawString(label.Text, font, brush, 0, 0);
            label.Image = bitmap;
            label.ImageAlign = ContentAlignment.MiddleCenter;
            label.Text = "";
            brush.Dispose();
            graphics.Dispose();
            //bitmap.Dispose();
        }
        private void DrowNotifyIcon()
        {
            Brush brush = new SolidBrush(Color.Black);
            Bitmap bitmap = new Bitmap(32, 32);
            Image image = new Bitmap(Properties.Resources.Calender);
            Graphics graphics = Graphics.FromImage(bitmap);
            String txt = datetime_today.Day.ToString();
            graphics.DrawImage(image, 0, 0, 32, 32);
            //Font font = new Font(font_day.FontFamily, 30, FontStyle.Regular, GraphicsUnit.Pixel);
            Font font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Regular, GraphicsUnit.Pixel);
            //graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            graphics.DrawString(txt, font, brush
                , (bitmap.Width - graphics.MeasureString(txt, font).Width) / 2
                , (bitmap.Height - graphics.MeasureString(txt, font).Height) / 2 + 3);
            notifyIcon1.Icon = System.Drawing.Icon.FromHandle(bitmap.GetHicon());
            font.Dispose();
            brush.Dispose();
            graphics.Dispose();
            image.Dispose();
            bitmap.Dispose();
        }
        private void NextMonth()
        {
            datetime_display = datetime_display.AddMonths(1);
            show_callendar();
        }
        private void PreviousMonth()
        {
            datetime_display = datetime_display.AddMonths(-1);
            show_callendar();
        }
    }
}
