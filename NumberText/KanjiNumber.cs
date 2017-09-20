using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberText
{
    /// <summary>
    /// 数値を漢数字に変換
    /// </summary>
    /// <seealso cref="NumberText.AbstractNumber" />
    public class KanjiNumber : AbstractNumber
    {
        /// <summary>
        /// 数の単位一覧
        /// </summary>
        protected override string[] unit { get; set; }
            = new string[] { "万", "億", "兆", "京", "垓", "秭", "穣", "溝", "澗", "正",
            "載", "極 ", "恒河沙", "阿僧祇", "那由他", "不可思議", "無量大数" };

        /// <summary>
        /// 区切る桁数
        /// </summary>
        protected override int place { get => 4; }
    }
}
