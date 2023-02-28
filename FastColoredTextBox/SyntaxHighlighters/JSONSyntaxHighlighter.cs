using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FastColoredTextBoxNS {

    [SyntaxHighlighter(Name = "JSON")]
    public class JSONSyntaxHighlighter : SyntaxHighlighterBase {

        public static readonly Regex JSONKeywordRegex;
        public static readonly Regex JSONNumberRegex;
        public static readonly Regex JSONStringRegex;

        static JSONSyntaxHighlighter() {
            JSONStringRegex = new Regex(@"""([^\\""]|\\"")*""", RegexCompiledOption);
            JSONNumberRegex = new Regex(@"\b(\d+[\.]?\d*|true|false|null)\b", RegexCompiledOption);
            JSONKeywordRegex = new Regex(@"(?<range>""([^\\""]|\\"")*"")\s*:", RegexCompiledOption);
        }

        /// <summary>
        /// Highlights JSON code
        /// </summary>
        /// <param name="range"></param>
        public override void HighlightSyntax(Range range) {
            range.tb.LeftBracket = '[';
            range.tb.RightBracket = ']';
            range.tb.LeftBracket2 = '{';
            range.tb.RightBracket2 = '}';
            range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;

            range.tb.AutoIndentCharsPatterns
                = @"
^\s*[\w\.]+(\s\w+)?\s*(?<range>=)\s*(?<range>[^;]+);
";

            //clear style of changed range
            range.ClearStyle(StringStyle, NumberStyle, KeywordStyle);
            //keyword highlighting
            range.SetStyle(KeywordStyle, JSONKeywordRegex);
            //string highlighting
            range.SetStyle(StringStyle, JSONStringRegex);
            //number highlighting
            range.SetStyle(NumberStyle, JSONNumberRegex);
            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            range.SetFoldingMarkers(@"\[", @"\]"); //allow to collapse comment block
        }

        public JSONSyntaxHighlighter() : base() {
        }

        public override void InitStyleSchema() {
            StringStyle = BrownItalicStyle;
            NumberStyle = MagentaStyle;
            KeywordStyle = BlueStyle;
        }
    }



}
