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
        public BilingualHighlighterSample()
        {
            InitializeComponent();
        }

        private void tb_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            var tb = (FastColoredTextBox) sender;
            
            //highlight html
            tb.HighlighterBase.InitStyleSchema(Language.HTML);
            tb.HighlighterBase.HTMLSyntaxHighlight(tb.Range);
            tb.Range.ClearFoldingMarkers();
            //find PHP fragments
            foreach(var r in tb.GetRanges(@"<\?php.*?\?>", RegexOptions.Singleline))
            {
                //remove HTML highlighting from this fragment
                r.ClearStyle(StyleIndex.All);
                //do PHP highlighting
                tb.HighlighterBase.InitStyleSchema(Language.PHP);
                tb.HighlighterBase.PHPSyntaxHighlight(r);
            }
        }
    }
}
