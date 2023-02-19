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
    public class CSharp2SyntaxHighlighter : SyntaxHighlighter {

        public CSharp2SyntaxHighlighter(params object[] args) : base("CSharp3") {
        }

    }

}
