using System;
using System.Collections.Generic;
using System.Text;

namespace FastColoredTextBoxNS.SyntaxHighlighters {

    public class CSharpSyntaxHighlighter : SyntaxHighlighter {

        public CSharpSyntaxHighlighter() : base(Language.CSharp) {
        }

    }

    public class VBSyntaxHighlighter : SyntaxHighlighter {

        public VBSyntaxHighlighter() : base(Language.VB) {
        }

    }

    public class XMLSyntaxHighlighter : SyntaxHighlighter {

        public XMLSyntaxHighlighter() : base(Language.XML) {
        }

    }

    public class HTMLSyntaxHighlighter : SyntaxHighlighter {

        public HTMLSyntaxHighlighter() : base(Language.HTML) {
        }

    }

    public class SQLSyntaxHighlighter : SyntaxHighlighter {

        public SQLSyntaxHighlighter() : base(Language.SQL) {
        }

    }

    public class PHPSyntaxHighlighter : SyntaxHighlighter {

        public PHPSyntaxHighlighter() : base(Language.PHP) {
        }

    }

    public class JSSyntaxHighlighter : SyntaxHighlighter {

        public JSSyntaxHighlighter() : base(Language.JS) {
        }

    }

    public class JSONSyntaxHighlighter : SyntaxHighlighter {

        public JSONSyntaxHighlighter() : base(Language.JSON) {
        }

    }

    public class LuaSyntaxHighlighter : SyntaxHighlighter {

        public LuaSyntaxHighlighter() : base(Language.Lua) {
        }

    }

    public class CustomSyntaxHighlighter : SyntaxHighlighter {

        public CustomSyntaxHighlighter() : base(Language.Custom) {
        }

    }

}
