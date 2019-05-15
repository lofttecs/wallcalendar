/*
_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
_/
_/  --- C# 移植版 ( Update: 2018/12/8 ) ---
_/
_/  CopyRight(C) K.Tsunoda(AddinBox) 2001 All Rights Reserved.
_/  ( AddinBox  http://addinbox.sakura.ne.jp/index.htm )
_/  (    旧サイト  http://www.h3.dion.ne.jp/~sakatsu/index.htm )
_/
_/  この祝日マクロは『kt関数アドイン』で使用しているものです。
_/  このロジックは、レスポンスを第一義として、可能な限り少ない
_/  【条件判定の実行】で結果を出せるように設計してあります。
_/
_/  この関数では以下の祝日変更までサポートしています。
_/    (a) 2019年施行の「天皇誕生日の変更」 12/23⇒2/23 (補：2019年には[天皇誕生日]はありません)
_/    (b) 2019年の徳仁親王の即位日(5/1) および
_/       祝日に挟まれて「国民の休日」となる 4/30(平成天皇の退位日) ＆ 5/2 の２休日
_/    (c) 2019年の「即位の礼 正殿の儀 (10/22) 」
_/    (d) 2020年施行の「体育の日の改名」⇒スポーツの日
_/    (e) 五輪特措法による2020年の「祝日移動」
_/       海の日：7/20(3rd Mon)⇒7/23, スポーツの日:10/12(2nd Mon)⇒7/24, 山の日：8/11⇒8/10
_/
_/  (*1)このマクロを引用するに当たっては、必ずこのコメントも
_/      一緒に引用する事とします。
_/  (*2)他サイト上で本マクロを直接引用する事は、ご遠慮願います。
_/      【 http://addinbox.sakura.ne.jp/holiday_logic.htm 】
_/      へのリンクによる紹介で対応して下さい。
_/  (*3)[ktHolidayName]という関数名そのものは、各自の環境に
_/      おける命名規則に沿って変更しても構いません。
_/
_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
*/

/*  ＶＢＡ用コードを書き直した「こほろぎ AsaPi! さん編集 [C ]用コード」を
    更に[ C# ]用に書き直したものです。「小山 隆史さん」さんによる編集です。 */

/*  2005/12/03 C# 2.0 Version by Takashi Oyama (lepton@hmsoft.co.jp)
    １．判定メソッドを HolidayChecker のメンバーメソッドとした。
        メソッド名はそのままとした。
    ２．祝日判定メソッドの戻り値を、曜日と祝日と名称を含むクラスとした。
    注；名前空間は適当に変更して使ってください。  */

using System;
using System.Collections.Generic;
using System.Text;

namespace GenCalendar
{
    static class HolidayChecker
    {
        // 春分の日を返すメソッド
        public static int Syunbun(int yy)
        {
            int dd;
            if (yy <= 1947)
            {
                dd = 99;  // 祝日法施行前
            }
            else if (yy <= 1979)
            {
                dd = (int)(20.8357 + (0.242194 * (yy - 1980)) - (int)((yy - 1983) / 4));
            }
            else if (yy <= 2099)
            {
                dd = (int)(20.8431 + (0.242194 * (yy - 1980)) - (int)((yy - 1980) / 4));
            }
            else if (yy <= 2150)
            {
                dd = (int)(21.851 + (0.242194 * (yy - 1980)) - (int)((yy - 1980) / 4));
            }
            else
            {
                dd = 99;  // 2151年以降は略算式が無いので不明
            }
            return dd;
        }

        // 秋分の日を返すメソッド
        public static int Syubun(int yy)
        {
            int dd;
            if (yy <= 1947)
            {
                dd = 99;  // 祝日法施行前
            }
            else if (yy <= 1979)
            {
                dd = (int)(23.2588 + (0.242194 * (yy - 1980)) - (int)((yy - 1983) / 4));
            }
            else if (yy <= 2099)
            {
                dd = (int)(23.2488 + (0.242194 * (yy - 1980)) - (int)((yy - 1980) / 4));
            }
            else if (yy <= 2150)
            {
                dd = (int)(24.2488 + (0.242194 * (yy - 1980)) - (int)((yy - 1980) / 4));
            }
            else
            {
                dd = 99;  // 2151年以降は略算式が無いので不明
            }
            return dd;
        }

        // 祝日情報（戻り値）
        public struct HolidayInfo
        {
            public enum HOLIDAY
            {
                WEEKDAY = 0,  // 平日
                HOLIDAY = 1,  // 休日
                C_HOLIDAY = 2,  // 振休
                SYUKUJITSU = 3,  // 祝日
            };
            public HOLIDAY holiday;  // その日の種類
            public DayOfWeek week;  // その日の曜日
            public String name;  // その日に名前が付いている場合はその名前。
        };

        private static readonly DateTime SYUKUJITSU = new DateTime(1948, 7, 20);  // 祝日法施行日
        private static readonly DateTime FURIKAE = new DateTime(1973, 4, 12);  // 振替休日制度の開始日

        // その日が何かを調べるメソッド
        public static HolidayInfo Holiday(DateTime t)
        {
            int yy = t.Year;
            int mm = t.Month;   // 月(1～12)
            int dd = t.Day;
            DayOfWeek ww = t.DayOfWeek;   // 日(0), 月(1) ... 土(6)

            HolidayInfo result = new HolidayInfo();
            result.week = ww;
            result.holiday = HolidayInfo.HOLIDAY.WEEKDAY;

            // 祝日法施行以前
            if (t < SYUKUJITSU) return result;

            switch (mm)
            {
                // １月 //
                case 1:
                    if (dd == 1)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "元日";
                    }
                    else
                    {
                        if (yy >= 2000)
                        {
                            if (((int)((dd - 1) / 7) == 1) && (ww == DayOfWeek.Monday))
                            {   // 2nd Monday
                                result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                                result.name = "成人の日";
                            }
                        }
                        else
                        {
                            if (dd == 15)
                            {
                                result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                                result.name = "成人の日";
                            }
                        }
                    }
                    break;

                // ２月 //
                case 2:
                    if (dd == 11)
                    {
                        if (yy >= 1967)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "建国記念の日";
                        }
                    }
                    else if (dd == 23)
                    {
                        if (yy >= 2020)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "天皇誕生日";
                        }
                    }
                    else if ((yy == 1989) && (dd == 24))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "昭和天皇の大喪の礼";
                    }
                    break;

                // ３月 //
                case 3:
                    if (dd == Syunbun(yy))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "春分の日";
                    }
                    break;

                // ４月 //
                case 4:
                    if (dd == 29)
                    {
                        if (yy >= 2007)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "昭和の日";
                        }
                        else if (yy >= 1989)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "みどりの日";
                        }
                        else
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "天皇誕生日";  // 昭和天皇
                        }
                    }
                    else if ((yy == 2019) && (dd == 30))
                    {    // 平成天皇の退位日
                        result.holiday = HolidayInfo.HOLIDAY.HOLIDAY;
                        result.name = "国民の休日";
                    }
                    else if ((yy == 1959) && (dd == 10))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "皇太子明仁親王の結婚の儀";
                    }
                    break;

                // ５月 //
                case 5:
                    if (dd == 3)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "憲法記念日";
                    }
                    else if (dd == 4)
                    {
                        if (yy >= 2007)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "みどりの日";
                        }
                        else if (yy >= 1986)
                        {
                            // 5/4が日曜日は『只の日曜』､月曜日は『憲法記念日の振替休日』(～2006年)
                            if (ww > DayOfWeek.Monday)
                            {  // 火曜 以降(火～土)
                                result.holiday = HolidayInfo.HOLIDAY.HOLIDAY;
                                result.name = "国民の休日";
                            }
                        }
                    }
                    else if (dd == 5)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "こどもの日";
                    }
                    else if (dd == 6)
                    {
                        // [5/3,5/4が日曜]ケースのみ、ここで判定
                        if ((yy >= 2007) && ((ww == DayOfWeek.Tuesday) || (ww == DayOfWeek.Wednesday)))
                        {
                            result.holiday = HolidayInfo.HOLIDAY.C_HOLIDAY;
                            result.name = "振替休日";
                        }
                    }
                    else
                    {
                        if ((yy == 2019) && (dd == 1))
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "即位の日";    // 徳仁親王
                        }
                        else if ((yy == 2019) && (dd == 2))
                        {
                            result.holiday = HolidayInfo.HOLIDAY.HOLIDAY;
                            result.name = "国民の休日";
                        }
                    }
                    break;

                // ６月 //
                case 6:
                    if ((yy == 1993) && (dd == 9))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "皇太子徳仁親王の結婚の儀";
                    }
                    break;

                // ７月 //
                case 7:
                    if (yy >= 2021)
                    {
                        if (((int)((dd - 1) / 7) == 2) && (ww == DayOfWeek.Monday))
                        {  // 3rd Monday
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "海の日";
                        }
                    }
                    else if (yy == 2020)
                    {
                        // 2020年はオリンピック特措法により
                        //「海の日」が 7/23 , 「スポーツの日」が 7/24 に移動
                        if (dd == 23)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "海の日";
                        }
                        else if (dd == 24)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "スポーツの日";
                        }
                    }
                    else if (yy >= 2003)
                    {
                        if (((int)((dd - 1) / 7) == 2) && (ww == DayOfWeek.Monday))
                        {  // 3rd Monday
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "海の日";
                        }
                    }
                    else if (yy >= 1996)
                    {
                        if (dd == 20)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "海の日";
                        }
                    }
                    break;

                // ８月 //
                case 8:
                    if (yy >= 2021)
                    {
                        if (dd == 11)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "山の日";
                        }
                    }
                    else if (yy == 2020)
                    {
                        // 2020年はオリンピック特措法により「山の日」が 8/10 に移動
                        if (dd == 10)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "山の日";
                        }
                    }
                    else if (yy >= 2016)
                    {
                        if (dd == 11)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "山の日";
                        }
                    }
                    break;

                // ９月 //
                case 9:
                    if (dd == Syubun(yy))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "秋分の日";
                    }
                    else
                    {
                        if (yy >= 2003)
                        {
                            if (((int)((dd - 1) / 7) == 2) && (ww == DayOfWeek.Monday))
                            {   // 3rd Monday
                                result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                                result.name = "敬老の日";
                            }
                            else if (ww == DayOfWeek.Tuesday)
                            {
                                if (dd == (Syubun(yy) - 1))
                                {
                                    result.holiday = HolidayInfo.HOLIDAY.HOLIDAY;
                                    result.name = "国民の休日";  // 火曜日＆[秋分日の前日]
                                }
                            }
                        }
                        else if (yy >= 1966)
                        {
                            if (dd == 15)
                            {
                                result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                                result.name = "敬老の日";
                            }
                        }
                    }
                    break;

                // １０月 //
                case 10:
                    if (yy >= 2021)
                    {
                        if (((int)((dd - 1) / 7) == 1) && (ww == DayOfWeek.Monday))
                        {  // 2nd Monday
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "スポーツの日";    // 2020年より改名
                        }
                    }
                    else if (yy == 2020)
                    {
                        // 2020年はオリンピック特措法により「スポーツの日」が 7/24 に移動
                    }
                    else if (yy >= 2000)
                    {
                        if (((int)((dd - 1) / 7) == 1) && (ww == DayOfWeek.Monday))
                        {  // 2nd Monday
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "体育の日";
                        }
                        else if ((yy == 2019) && (dd == 22))
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "即位礼正殿の儀";  // 徳仁親王
                        }
                    }
                    else if (yy >= 1966)
                    {
                        if (dd == 10)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "体育の日";
                        }
                    }
                    break;

                // １１月 //
                case 11:
                    if (dd == 3)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "文化の日";
                    }
                    else if (dd == 23)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "勤労感謝の日";
                    }
                    else if ((yy == 1990) && (dd == 12))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "即位礼正殿の儀";  // 平成天皇
                    }
                    break;

                // １２月 //
                case 12:
                    if (dd == 23)
                    {
                        if ((yy >= 1989) && (yy <= 2018))
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "天皇誕生日";  // 平成天皇
                        }
                    }
                    break;

                default:
                    break;
            }

            // ----- 振替休日の判定 (振替休日施行日:1973/4/12) -----
            // [ 対象日≠祝日/休日 ＆ 対象日＝月曜日 ]のみ、前日(＝日曜日)を祝日判定する。
            // 前日(＝日曜日)が祝日の場合は”振替休日”となる。
            // 尚、５月６日の扱いを
            //     「火曜 or 水曜(みどりの日(5/4) or 憲法記念日(5/3)の振替休日)」⇒５月ブロック内で判定済
            //     「月曜(こどもの日(5/5)の振替休日」⇒ここの判定処理で判定
            // とする事により、ここでの判定対象は『対象日が月曜日』のみ となります。
            if (string.IsNullOrEmpty(result.name) && (ww == DayOfWeek.Monday))
            {
                if (t >= FURIKAE)
                {    // 振替休日施行以降
                    if (Holiday(t.AddDays(-1)).holiday == HolidayInfo.HOLIDAY.SYUKUJITSU)
                    {    // 再帰呼出
                        result.holiday = HolidayInfo.HOLIDAY.C_HOLIDAY;
                        result.name = "振替休日";
                    }
                }
            }

            return result;
        }
    }
}

