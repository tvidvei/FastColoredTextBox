using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastColoredTextBoxNS {

    [AttributeUsage(System.AttributeTargets.Class)]
    public class SyntaxHighlighterAttribute : Attribute {
        
        public string Name { get; set; }

        public SyntaxHighlighterAttribute() { }

    }

}
