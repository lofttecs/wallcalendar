namespace wallcalendar
{
    public class Settings
    {
        private string _fontname;
        private bool _bold_today;
        private bool _italic_today;
        private bool _underline_today;
        private int _fontsize_day;
        private int _fontsize_month;
        private int _position_top;
        private int _position_left;
        private bool _topmost;
        private string _color_weekday;
        private string _color_holiday;
        private string _color_saturday;
        private string _color_month;
        private string _color_background;
        private bool _transparent_background;
        private string _color_today_back;
        private bool _transparent_today_back;
        private int _opacity;
        private string _language;
        private bool _monday_start;

        public Settings()
        {

        }
        public string fontname
        {
            get { return _fontname; }
            set { _fontname = value; }
        }
        public bool bold_today
        {
            get { return _bold_today; }
            set { _bold_today = value; }
        }
        public bool italic_today
        {
            get { return _italic_today; }
            set { _italic_today = value; }
        }
        public bool underline_today
        {
            get { return _underline_today; }
            set { _underline_today = value; }
        }
        public int fontsize_day
        {
            get { return _fontsize_day; }
            set { _fontsize_day = value; }
        }
        public int fontsize_month
        {
            get { return _fontsize_month; }
            set { _fontsize_month = value; }
        }
        public int position_top
        {
            get { return _position_top; }
            set { _position_top = value; }
        }
        public int position_left
        {
            get { return _position_left; }
            set { _position_left = value; }
        }
        public bool topmost
        {
            get { return _topmost; }
            set { _topmost = value; }
        }
        public string color_weekday
        {
            get { return _color_weekday; }
            set { _color_weekday = value; }
        }
        public string color_holiday
        {
            get { return _color_holiday; }
            set { _color_holiday = value; }
        }
        public string color_saturday
        {
            get { return _color_saturday; }
            set { _color_saturday = value; }
        }
        public string color_month
        {
            get { return _color_month; }
            set { _color_month = value; }
        }
        public string color_background
        {
            get { return _color_background; }
            set { _color_background = value; }
        }
        public bool transparent_background
        {
            get { return _transparent_background; }
            set { _transparent_background = value; }
        }
        public string color_today_back
        {
            get { return _color_today_back; }
            set { _color_today_back = value; }
        }
        public bool transparent_today_back
        {
            get { return _transparent_today_back; }
            set { _transparent_today_back = value; }
        }
        public int opacity
        {
            get { return _opacity; }
            set { _opacity = value; }
        }
        public string language
        {
            get { return _language; }
            set { _language = value; }
        }
        public bool monday_start
        {
            get { return _monday_start; }
            set { _monday_start = value; }
        }
    }
}
