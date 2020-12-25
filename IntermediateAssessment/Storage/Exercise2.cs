using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace IntermediateAssessment.Storage
{
    /// <summary>
    /// РК 1 задание № 2
    /// </summary>
    public class Exercise2 : Exercise0
    {
        /// <summary>
        /// Строка кода
        /// </summary>
        public virtual CodeRow CodeRow { get; set; }

        /// <summary>
        /// Исправленная строка кода
        /// </summary>
        [MaxLength(255)]
        public string AnswerString { get; set; }

        /// <summary>
        /// Представление строки кода для HTML
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(CodeRow.Code))
            {
                return "&nbsp;";
            }

            var match = Regex.Match(CodeRow.Code,@"^(?<space>\s*)(?<text>\S.*)$");
            if (match.Success)
            {
                string s = "";
                for (int i = 0; i < match.Groups["space"].Value.Length; i++)
                {
                    s += "&nbsp;";
                }
                // Экранирование специальных для HTML символов
                string h = System.Net.WebUtility.HtmlEncode(match.Groups["text"].Value);
                return s + h;
            }
            else
            {
                return CodeRow.Code;
            }

            return string.IsNullOrEmpty(CodeRow.Code) ? "&nbsp;" : CodeRow.Code.Replace(" ", "&nbsp;");
        }
    }
}