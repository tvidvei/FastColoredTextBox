using System;
using System.Collections.Generic;
using System.Text;

namespace FastColoredTextBoxNS {

    [SyntaxHighlighter(Name = "None")]
    public class NoneSyntaxHighlighter : SyntaxHighlighter {

        public NoneSyntaxHighlighter(params object[] args) : base(Language.None) {
        }

    }

    [SyntaxHighlighter(Name = "CSharp")]
    public class CSharpSyntaxHighlighter : SyntaxHighlighter {

        public CSharpSyntaxHighlighter(params object[] args) : base(Language.CSharp) {
        }

    }

    [SyntaxHighlighter(Name = "VB")]
    public class VBSyntaxHighlighter : SyntaxHighlighter {

        public VBSyntaxHighlighter(params object[] args) : base(Language.VB) {
        }

    }

    [SyntaxHighlighter(Name = "XML")]
    public class XMLSyntaxHighlighter : SyntaxHighlighter {

        public XMLSyntaxHighlighter(params object[] args) : base(Language.XML) {
        }

    }

    [SyntaxHighlighter(Name = "HTML")]
    public class HTMLSyntaxHighlighter : SyntaxHighlighter {

        public HTMLSyntaxHighlighter(params object[] args) : base(Language.HTML) {
        }

    }

    [SyntaxHighlighter(Name = "SQL")]
    public class SQLSyntaxHighlighter : SyntaxHighlighter {

        public SQLSyntaxHighlighter(params object[] args) : base(Language.SQL) {
        }

    }

    [SyntaxHighlighter(Name = "PHP")]
    public class PHPSyntaxHighlighter : SyntaxHighlighter {

        public PHPSyntaxHighlighter(params object[] args) : base(Language.PHP) {
        }

    }

    [SyntaxHighlighter(Name = "JS")]
    public class JSSyntaxHighlighter : SyntaxHighlighter {

        public JSSyntaxHighlighter(params object[] args) : base(Language.JS) {
        }

    }

    [SyntaxHighlighter(Name = "JSON")]
    public class JSONSyntaxHighlighter : SyntaxHighlighter {

        public JSONSyntaxHighlighter(params object[] args) : base(Language.JSON) {
        }

    }

    [SyntaxHighlighter(Name = "Lua")]
    public class LuaSyntaxHighlighter : SyntaxHighlighter {

        public LuaSyntaxHighlighter(params object[] args) : base(Language.Lua) {
        }

    }

    [SyntaxHighlighter(Name = "Custom", IsConfigurable = true)]
    public class CustomSyntaxHighlighter : SyntaxHighlighter {

        public CustomSyntaxHighlighter(params object[] args) : base(Language.Custom, args[0] as string) {
        }

    }

}
