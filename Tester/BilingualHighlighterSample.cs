using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace Tester
{
    public partial class BilingualHighlighterSample : Form
    {
        private ISyntaxHighlighter HTMLHighlighter;

        private ISyntaxHighlighter PHPHighlighter;

        public BilingualHighlighterSample()
        {
            InitializeComponent();

            HTMLHighlighter = SyntaxHighlighter.GetHighlighter("HTML");
            PHPHighlighter = SyntaxHighlighter.GetHighlighter("PHP");
        }


        private void tb_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            var tb = (FastColoredTextBox) sender;

            //highlight html
            HTMLHighlighter.HighlightSyntax(tb.Range);
            tb.Range.ClearFoldingMarkers();
            //find PHP fragments
            foreach(var r in tb.GetRanges(@"<\?php.*?\?>", RegexOptions.Singleline))
            {
                //remove HTML highlighting from this fragment
                r.ClearStyle(StyleIndex.All);
                //do PHP highlighting
                PHPHighlighter.HighlightSyntax(r);
            }
        }
    }
}
