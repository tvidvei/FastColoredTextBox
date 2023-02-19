using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace FastColoredTextBoxNS {

    [SyntaxHighlighter(Name = "None")]
    public class NoneSyntaxHighlighter : SyntaxHighlighter {

        public NoneSyntaxHighlighter() : base() {
        }

    }

    [SyntaxHighlighter(Name = "CSharp")]
    public class CSharpSyntaxHighlighter : SyntaxHighlighter {

        public CSharpSyntaxHighlighter() : base() {
        }

        public override void InitStyleSchema() {
            StringStyle = BrownStyle;
            CommentStyle = GreenStyle;
            NumberStyle = MagentaStyle;
            AttributeStyle = GreenStyle;
            ClassNameStyle = BoldStyle;
            KeywordStyle = BlueStyle;
            CommentTagStyle = GrayStyle;
        }

    }


    [SyntaxHighlighter(Name = "VB")]
    public class VBSyntaxHighlighter : SyntaxHighlighter {

        public VBSyntaxHighlighter() : base() {
        }

        public override void InitStyleSchema() {
            StringStyle = BrownStyle;
            CommentStyle = GreenStyle;
            NumberStyle = MagentaStyle;
            ClassNameStyle = BoldStyle;
            KeywordStyle = BlueStyle;
        }

    }


    [SyntaxHighlighter(Name = "XML")]
    public class XMLSyntaxHighlighter : SyntaxHighlighter {

        public XMLSyntaxHighlighter() : base() {
        }

        public override void InitStyleSchema() {
            CommentStyle = GreenStyle;
            XmlTagBracketStyle = BlueStyle;
            XmlTagNameStyle = MaroonStyle;
            XmlAttributeStyle = RedStyle;
            XmlAttributeValueStyle = BlueStyle;
            XmlEntityStyle = RedStyle;
            XmlCDataStyle = BlackStyle;
        }
    }


    [SyntaxHighlighter(Name = "HTML")]
    public class HTMLSyntaxHighlighter : SyntaxHighlighter {

        public HTMLSyntaxHighlighter() : base() {
        }

        public override void InitStyleSchema() {
            CommentStyle = GreenStyle;
            TagBracketStyle = BlueStyle;
            TagNameStyle = MaroonStyle;
            AttributeStyle = RedStyle;
            AttributeValueStyle = BlueStyle;
            HtmlEntityStyle = RedStyle;
        }
    }


    [SyntaxHighlighter(Name = "SQL")]
    public class SQLSyntaxHighlighter : SyntaxHighlighter {

        public SQLSyntaxHighlighter() : base() {
        }

        public override void InitStyleSchema() {
            StringStyle = RedStyle;
            CommentStyle = GreenStyle;
            NumberStyle = MagentaStyle;
            KeywordStyle = BlueBoldStyle;
            StatementsStyle = BlueBoldStyle;
            FunctionsStyle = MaroonStyle;
            VariableStyle = MaroonStyle;
            TypesStyle = BrownStyle;
        }

    }


    [SyntaxHighlighter(Name = "PHP")]
    public class PHPSyntaxHighlighter : SyntaxHighlighter {

        public PHPSyntaxHighlighter() : base() {
        }

        public override void InitStyleSchema() {
            StringStyle = RedStyle;
            CommentStyle = GreenStyle;
            NumberStyle = RedStyle;
            VariableStyle = MaroonStyle;
            KeywordStyle = MagentaStyle;
            KeywordStyle2 = BlueStyle;
            KeywordStyle3 = GrayStyle;
        }
    }


    [SyntaxHighlighter(Name = "JS")]
    public class JSSyntaxHighlighter : SyntaxHighlighter {

        public JSSyntaxHighlighter() : base() {
        }

        public override void InitStyleSchema() {
            StringStyle = BrownStyle;
            CommentStyle = GreenStyle;
            NumberStyle = MagentaStyle;
            KeywordStyle = BlueStyle;
        }

    }


    [SyntaxHighlighter(Name = "JSON")]
    public class JSONSyntaxHighlighter : SyntaxHighlighter {

        public JSONSyntaxHighlighter() : base() {
        }

        public override void InitStyleSchema() {
            StringStyle = BrownStyle;
            NumberStyle = MagentaStyle;
            KeywordStyle = BlueStyle;
        }
    }


    [SyntaxHighlighter(Name = "Lua")]
    public class LuaSyntaxHighlighter : SyntaxHighlighter {

        public LuaSyntaxHighlighter() : base() {
        }

        public override void InitStyleSchema() {
            StringStyle = BrownStyle;
            CommentStyle = GreenStyle;
            NumberStyle = MagentaStyle;
            KeywordStyle = BlueBoldStyle;
            FunctionsStyle = MaroonStyle;
        }

    }


    [SyntaxHighlighter(Name = "Custom", IsConfigurable = true)]
    public class CustomSyntaxHighlighter : SyntaxHighlighter {

        public CustomSyntaxHighlighter(string descriptionFile = null) : base($"Custom:{descriptionFile}") {
            
            //Create the SyntaxDescriptor from descriptionFile
            DescriptionFile = descriptionFile;
            if (!string.IsNullOrWhiteSpace(DescriptionFile)) {
                var doc = new XmlDocument();
                string filepath = DescriptionFile;
                if (!File.Exists(filepath)) {
                    filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(filepath));
                }
                if (File.Exists(filepath)) {
                    doc.LoadXml(File.ReadAllText(filepath));
                    SyntaxDescriptor = ParseXmlDescription(doc);
                }
            }

        }

    }

}
