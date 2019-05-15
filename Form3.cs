using System;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace wallcalendar
{
    public partial class Form3 : Form
    {
        Settings settings = new Settings();

        String[] string_fontfamily;
        List<ComboBox2ItemSet> comboBox2Items = new List<ComboBox2ItemSet>();

        public String ComboBox1
        {
            get
            {
                return (comboBox1.SelectedItem ?? "").ToString();
            }
            set
            {
                comboBox1.SelectedIndex = Array.IndexOf(string_fontfamily, value);
            }
        }
        public int NumericUpDown1
        {
            get
            {
                return (int)numericUpDown1.Value;
            }
            set
            {
                numericUpDown1.Value = value;
            }
        }
        public int NumericUpDown2
        {
            get
            {
                return (int)numericUpDown2.Value;
            }
            set
            {
                numericUpDown2.Value = value;
            }
        }
        public int NumericUpDown3
        {
            get
            {
                return (int)numericUpDown3.Value;
            }
            set
            {
                numericUpDown3.Value = (int)value;
            }
        }
        public int NumericUpDown4
        {
            get
            {
                return (int)numericUpDown4.Value;
            }
            set
            {
                numericUpDown4.Value = value;
            }
        }
        public int NumericUpDown5
        {
            get
            {
                return (int)numericUpDown5.Value;
            }
            set
            {
                numericUpDown5.Value = value;
            }
        }
        public string Button3
        {
            get
            {
                return button3.BackColor.A.ToString("X2") + button3.BackColor.R.ToString("X2") + button3.BackColor.G.ToString("X2") + button3.BackColor.B.ToString("X2");
            }
            set
            {
                button3.BackColor = Color.FromArgb(Convert.ToInt32(value, 16));
            }
        }
        public string Button4
        {
            get
            {
                return button4.BackColor.A.ToString("X2") + button4.BackColor.R.ToString("X2") + button4.BackColor.G.ToString("X2") + button4.BackColor.B.ToString("X2");
            }
            set
            {
                button4.BackColor = Color.FromArgb(Convert.ToInt32(value, 16));
            }
        }
        public string Button5
        {
            get
            {
                return button5.BackColor.A.ToString("X2") + button5.BackColor.R.ToString("X2") + button5.BackColor.G.ToString("X2") + button5.BackColor.B.ToString("X2");
            }
            set
            {
                button5.BackColor = Color.FromArgb(Convert.ToInt32(value, 16));
            }
        }
        public string Button6
        {
            get
            {
                return button6.BackColor.A.ToString("X2") + button6.BackColor.R.ToString("X2") + button6.BackColor.G.ToString("X2") + button6.BackColor.B.ToString("X2");
            }
            set
            {
                button6.BackColor = Color.FromArgb(Convert.ToInt32(value, 16));
            }
        }
        public string Button7
        {
            get
            {
                return button7.BackColor.A.ToString("X2") + button7.BackColor.R.ToString("X2") + button7.BackColor.G.ToString("X2") + button7.BackColor.B.ToString("X2");
            }
            set
            {
                button7.BackColor = Color.FromArgb(Convert.ToInt32(value, 16));
            }
        }
        public string Button8
        {
            get
            {
                return button8.BackColor.A.ToString("X2") + button8.BackColor.R.ToString("X2") + button8.BackColor.G.ToString("X2") + button8.BackColor.B.ToString("X2");
            }
            set
            {
                button8.BackColor = Color.FromArgb(Convert.ToInt32(value, 16));
            }
        }
        public Boolean CheckBox1
        {
            get
            {
                return checkBox1.Checked;
            }
            set
            {
                checkBox1.Checked = value;
            }
        }
        public Boolean CheckBox2
        {
            get
            {
                return checkBox2.Checked;
            }
            set
            {
                checkBox2.Checked = value;
            }
        }
        public Boolean CheckBox3
        {
            get
            {
                return checkBox3.Checked;
            }
            set
            {
                checkBox3.Checked = value;
            }
        }
        public Boolean CheckBox4
        {
            get
            {
                return checkBox4.Checked;
            }
            set
            {
                checkBox4.Checked = value;
            }
        }
        public Boolean CheckBox5
        {
            get
            {
                return checkBox5.Checked;
            }
            set
            {
                checkBox5.Checked = value;
            }
        }
        public Boolean CheckBox6
        {
            get
            {
                return checkBox6.Checked;
            }
            set
            {
                checkBox6.Checked = value;
            }
        }

        public String ComboBox2
        {
            get
            {
                return comboBox2.SelectedValue.ToString();
            }
            set
            {
                comboBox2.SelectedValue = (ComboBox2ItemSetItemValueContains(comboBox2Items, value)) ? value : "ja-JP";
            }
        }
        public class ComboBox2ItemSet
        {
            public String ItemKey { get; set; }
            public String ItemValue { get; set; }
            public ComboBox2ItemSet(String key, String val)
            {
                ItemKey = key;
                ItemValue = val;
            }
        }
        public bool ComboBox2ItemSetItemValueContains(List<ComboBox2ItemSet> items, string itemvalue)
        {
            foreach (ComboBox2ItemSet item in items)
            {
                if (item.ItemValue == itemvalue) { return true; }
            }
            return false;
        }
        public Boolean radioButton_1_2
        {
            get
            {
                return radioButton2.Checked;
            }
            set
            {
                radioButton1.Checked = !value;
                radioButton2.Checked = value;
            }
        }

        public Form3()
        {
            InitializeComponent();

            string_fontfamily = new String[0];

            foreach (FontFamily item in FontFamily.Families)
            {
                if (item.IsStyleAvailable(FontStyle.Regular))
                {
                    comboBox1.Items.Add(item.Name);

                    Array.Resize(ref string_fontfamily, string_fontfamily.Length + 1);
                    string_fontfamily[string_fontfamily.Length - 1] = item.Name;
                    Application.DoEvents();
                }
                item.Dispose();
            }
            comboBox1.MaxDropDownItems = 15;
            //comboBox1.DropDownHeight = comboBox1.MaxDropDownItems * (int)comboBox1.Font.Size;
            //comboBox1.DropDownHeight = 180;

            comboBox2Items.Add(new ComboBox2ItemSet("日本語", "ja-JP"));
            comboBox2Items.Add(new ComboBox2ItemSet("English", "en-GB"));
            comboBox2Items.Add(new ComboBox2ItemSet("French", "fr-FR"));
            comboBox2Items.Add(new ComboBox2ItemSet("German", "de-DE"));
            comboBox2Items.Add(new ComboBox2ItemSet("Italian", "it-IT"));

            foreach (ComboBox2ItemSet item in comboBox2Items)
            {
                comboBox2.Items.Add(item.ItemKey);
            }
            comboBox2.DataSource = comboBox2Items;
            comboBox2.DisplayMember = "ItemKey";
            comboBox2.ValueMember = "ItemValue";
        }

        private void ComboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1) return;
            //背景を描画する
            //項目が選択されている時は強調表示される
            e.DrawBackground();

            ComboBox cmb = (ComboBox)sender;
            //項目に表示する文字列
            string txt = e.Index > -1 ? cmb.Items[e.Index].ToString() : cmb.Text;

            try
            {
                FontFamily fontFamily = new FontFamily(txt);
                //Console.WriteLine(fontFamily.IsStyleAvailable(FontStyle.Italic).ToString());
                //使用するフォント
                //Font f = new Font(txt, cmb.Font.Size);
                Font f = new Font(txt, cmb.Font.Size, GraphicsUnit.Pixel);
                if (fontFamily.IsStyleAvailable(FontStyle.Regular))
                    f = new Font(txt, cmb.Font.Size, FontStyle.Regular, GraphicsUnit.Pixel);
                fontFamily.Dispose();
                //使用するブラシ
                Brush b = new SolidBrush(e.ForeColor);
                //文字列を描画する
                Font ff = new Font(cmb.Font.FontFamily, 9);
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                float ym = (e.Bounds.Height - e.Graphics.MeasureString(txt, ff).Height) / 2;
                e.Graphics.DrawString(txt, ff, b, e.Bounds.X, e.Bounds.Y + ym);

                System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(ComboBox2);

                e.Graphics.DrawString(DateTime.Today.ToString("MMMM yyyy", cultureInfo), f, b, e.Graphics.MeasureString(txt, ff).Width + 10, e.Bounds.Y + ym);
                //Console.WriteLine(e.Bounds.ToString());

                f.Dispose();
                ff.Dispose();
                b.Dispose();

                //フォーカスを示す四角形を描画
                e.DrawFocusRectangle();
                e.Graphics.Dispose();

            }
            catch (Exception ex)
            {
                cmb.DrawMode = DrawMode.Normal;
                //MessageBox.Show("フォントの描画の際にエラーが発生しました。\nテキスト表示に切り替えます。");
                //MessageBox.Show(ex.ToString());
            }
            //e.Graphics.DrawString(comboBox1.Items[e.Index].ToString(),
            //                      new Font(comboBox1.Items[e.Index].ToString(), 10),
            //                      new SolidBrush(Color.Black),
            //                      e.Bounds.X,
            //                      e.Bounds.Y);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Color_Button_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = ((Button)sender).BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            button3.Enabled = !((CheckBox)sender).Checked;
        }

        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            button4.Enabled = !((CheckBox)sender).Checked;
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            FontFamily fontFamily = new FontFamily(((ComboBox)sender).Text);
            checkBox1.Enabled = fontFamily.IsStyleAvailable(FontStyle.Bold);
            checkBox2.Enabled = fontFamily.IsStyleAvailable(FontStyle.Italic);
            checkBox3.Enabled = fontFamily.IsStyleAvailable(FontStyle.Underline);
            fontFamily.Dispose();
        }
    }
}
