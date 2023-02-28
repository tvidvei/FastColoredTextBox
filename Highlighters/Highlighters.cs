using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Highlighters
{
    [SyntaxHighlighter(Name = "CSharp3")]
    public class CSharp2SyntaxHighlighter : CSharpSyntaxHighlighter {

        public CSharp2SyntaxHighlighter() : base() {
        }

        public override void InitStyleSchema() {
            StringStyle = BrownItalicStyle;
            CommentStyle = GreenItalicStyle;
            NumberStyle = GrayStyle;
            AttributeStyle = GreenItalicStyle;
            ClassNameStyle = BlackStyle;
            KeywordStyle = BlueStyle;
            CommentTagStyle = GrayStyle;
        }

    }

}
