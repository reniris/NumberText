using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NumberText
{
    /// <summary>
    /// 数値桁変換基底クラス
    /// </summary>
    public abstract class AbstractNumber
    {
        /// <summary>
        /// 数値を桁数ごとに分解
        /// </summary>
        /// <param name="n">数値</param>
        /// <param name="place">桁</param>
        /// <returns></returns>
        protected List<int> SplitNumber(ulong n, int place)
        {
            var list = new List<int>();

            //累乗
            var sunit = (ulong)Pow(10, place);

            for (; n > 0; n /= sunit)
            {
                int x = (int)(n % sunit);
                list.Add(x);
            }
            return list;
        }
        
        /// <summary>
        /// 累乗
        /// </summary>
        /// <param name="base_num">基数</param>
        /// <param name="exponent">指数</param>
        /// <returns></returns>
        private int Pow(int base_num, int exponent)
        {
            int result = base_num;
            for (int i = 1; i < exponent; i++)
            {
                result *= base_num;
            }

            return result;
        }

        /// <summary>
        /// 区切られた数値に単位をつける（万とか兆とか）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">区切られた数値のリスト</param>
        /// <returns></returns>
        protected virtual List<string> FormatStringList<T>(IList<T> list) where T : IEquatable<T>
        {
            var str = new List<string>();
            str.Add(list[0].ToString());

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].Equals(default(T)) == true) //0の場合は空文字列をセット
                {
                    str.Insert(0, "");
                }
                else
                {
                    str.Insert(0, list[i].ToString() + unit[i - 1]);
                }
            }

            return str;
        }

        /// <summary>
        /// 桁ごとに区切って数字単位つき文字列のリストに変換
        /// </summary>
        /// <param name="n">変換する数値</param>
        /// <returns></returns>
        public List<string> FomatNumberList(ulong n)
        {
            var list = SplitNumber(n, this.place);   //桁ごとに区切る
            List<string> str = FormatStringList(list);
            return str;
        }

        /// <summary>
        /// 桁ごとに区切って数字単位つき文字列に変換
        /// </summary>
        /// <param name="n">変換する数値</param>
        /// <param name="take">先頭から何区切り取るか（０以下の場合は全部取る）</param>
        /// <returns></returns>
        public string TakeFormatNumber(ulong n, int take)
        {
            var list = FomatNumberList(n);

            //区切りが０以下の場合は全部取る
            if (take <= 0) { take = list.Count(); }

            return string.Join("", list.Take(take));
        }
        
        /// <summary>
        /// 先頭２ブロックを小数点つきで表示（例：1.2万）
        /// </summary>
        /// <param name="n">変換する数値</param>
        /// <param name="len">小数点何桁表示するか</param>
        /// <returns></returns>
        public string DecimalFormatNumber(ulong n, int len)
        {
            //表示桁数が０未満または区切り桁数を超えたら例外
            if (len > this.place || len < 0) { throw new ArgumentOutOfRangeException(); }

            var list = SplitNumber(n, this.place);   //桁ごとに区切る
            
            return InnerFormatNumber(len, list);
        }

        private string InnerFormatNumber(int len, List<int> list)
        {
            var u = unit[list.Count - 2]; //単位

            list.Reverse();
            var tlist = list.Take(2);
            string str;

            if (tlist.Count() == 2) //小数点つき
            {
                //書式文字列
                var format = string.Join("", Enumerable.Repeat("0", place));
                str = tlist.First().ToString() + "." + tlist.Last().ToString(format).Substring(0, len);
            }
            else　   //小数点なし
            {
                str = tlist.First().ToString();
            }

            return str + u;
        }

        /// <summary>
        /// aaから始まる単位文字列の作成（zzまで）
        /// </summary>
        /// <param name="count">一桁目を何週させるか（１～２６まで）</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        protected string[] CreateUnit(int count)
        {
            if (1 > count || count > 26) { throw new ArgumentOutOfRangeException(); }

            var ret = new string[count * 26];
            char end_c = 'a';
            end_c += Convert.ToChar(count - 1);
            int i = 0;
            for (char c1 = 'a'; c1 <= end_c; c1++)
            {
                for (char c2 = 'a'; c2 <= 'z'; c2++)
                {
                    var buf = new char[] { c1, c2 };
                    ret[i++] = new string(buf);
                }
            }

            return ret;
        }

        #region BigInteger

        /// <summary>
        /// 数値を桁数ごとに分解
        /// </summary>
        /// <param name="n">数値</param>
        /// <param name="place">桁</param>
        /// <returns></returns>
        protected List<int> SplitNumber(BigInteger n, int place)
        {
            var list = new List<int>();

            //累乗
            var sunit = BigInteger.Pow(10, place);

            for (; n > 0; n /= sunit)
            {
                int x = (int)(n % sunit);
                list.Add(x);
            }
            return list;
        }

        /// <summary>
        /// 桁ごとに区切って数字単位つき文字列のリストに変換
        /// </summary>
        /// <param name="n">変換する数値</param>
        /// <returns></returns>
        public List<string> FomatNumberList(BigInteger n)
        {
            var list = SplitNumber(n, this.place);   //桁ごとに区切る
            List<string> str = FormatStringList(list);
            return str;
        }

        /// <summary>
        /// 桁ごとに区切って数字単位つき文字列に変換
        /// </summary>
        /// <param name="n">変換する数値</param>
        /// <param name="take">先頭から何区切り取るか（０以下の場合は全部取る）</param>
        /// <returns></returns>
        public string TakeFormatNumber(BigInteger n, int take)
        {
            var list = FomatNumberList(n);

            //区切りが０以下の場合は全部取る
            if (take <= 0) { take = list.Count(); }

            return string.Join("", list.Take(take));
        }

        /// <summary>
        /// 先頭２ブロックを小数点つきで表示（例：1.2万）
        /// </summary>
        /// <param name="n">変換する数値</param>
        /// <param name="len">小数点何桁表示するか</param>
        /// <returns></returns>
        public string DecimalFormatNumber(BigInteger n, int len)
        {
            //表示桁数が０未満または区切り桁数を超えたら例外
            if (len > this.place || len < 0) { throw new ArgumentOutOfRangeException(); }

            var list = SplitNumber(n, this.place);   //桁ごとに区切る

            return InnerFormatNumber(len, list);
        }
        #endregion

        /// <summary>
        /// 数の単位一覧
        /// </summary>
        abstract protected string[] unit { get; set; }

        /// <summary>
        /// 区切る桁数
        /// </summary>
        abstract protected int place { get; }

    }
}
