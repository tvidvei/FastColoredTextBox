using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Schema;

namespace FastColoredTextBoxNS
{

    /// <summary>
    /// Languages.  (Used as short names for the predefined SyntaxHighlighter classes)
    /// </summary>
    public static class Language {
        public const string None = "None";
        public const string CSharp = "CSharp";
        public const string VB = "VB";
        public const string HTML = "HTML";
        public const string XML = "XML";
        public const string SQL = "SQL";
        public const string PHP = "PHP";
        public const string JS = "JS";
        public const string Lua = "Lua";
        public const string JSON = "JSON";
        public const string Custom = "Custom";
    }

    public interface ISyntaxHighlighter : IDisposable 
    {

        string Name { get; }

        string DescriptionFile { get; }

        void HighlightSyntax(Range range);

        void AutoIndentNeeded(object sender, AutoIndentEventArgs args);

        void InitStyleSchema();

    }

    public abstract class SyntaxHighlighterBase : ISyntaxHighlighter {

        public static RegexOptions RegexCompiledOption {
            get {
                if (platformType == Platform.X86)
                    return RegexOptions.Compiled;
                else
                    return RegexOptions.None;
            }
        }

        /// <summary>
        /// Cache for SyntaxHighlighters
        /// </summary>
        private static Dictionary<(string, string, string), ISyntaxHighlighter> Highlighters = new Dictionary<(string, string, string), ISyntaxHighlighter>();


        /// <summary>
        /// Find a SyntaxHighlighter class with the given name in an assembly
        /// </summary>
        private static Type FindHighlighterType(Assembly asm, string name) {
            return asm?.GetExportedTypes().FirstOrDefault(t => t.GetCustomAttribute<SyntaxHighlighterAttribute>()?.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) ?? false && t.IsAssignableFrom(typeof(ISyntaxHighlighter))) ?? null;
        }

        /// <summary>
        /// Factory method: Get or create a highlighter for a given language
        /// </summary>
        /// <param name="langue">Language to implement highlighter for</param>
        /// <returns></returns>
        public static ISyntaxHighlighter GetHighlighter(string name = Language.None, string descriptionFile = null, string libraries = null) {
            ISyntaxHighlighter result;

            if (!Highlighters.TryGetValue((name, "*", libraries), out result)) {
                if (!Highlighters.TryGetValue((name, descriptionFile, libraries), out result)) {
                    // Find SyntaxHighlighter type
                    Type hltype = null;
                    if (!String.IsNullOrWhiteSpace(libraries)) {
                        // First, search for hltype in libraries
                        var libA = libraries.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
                        foreach (var library in libA) {
                            Assembly asm = null;
                            try {
                                if (!String.IsNullOrWhiteSpace(library)) asm = Assembly.Load(library);
                            } catch (Exception ex) {
                                asm = null;
                            }
                            hltype = FindHighlighterType(asm, name);
                            if (hltype != null) break;
                        }
                    }

                    if (hltype == null) {
                        hltype = FindHighlighterType(Assembly.GetEntryAssembly(), name) ??
                                 FindHighlighterType(Assembly.GetCallingAssembly(), name) ??
                                 FindHighlighterType(Assembly.GetExecutingAssembly(), name) ??
                                 typeof(NoneSyntaxHighlighter);
                    }

                    if (hltype.GetCustomAttribute<SyntaxHighlighterAttribute>().IsConfigurable) {
                        result = Activator.CreateInstance(hltype, descriptionFile) as SyntaxHighlighterBase;
                        Highlighters[(name, descriptionFile, libraries)] = result;
                    } else {
                        result = Activator.CreateInstance(hltype) as SyntaxHighlighterBase;
                        Highlighters[(name, "*", libraries)] = result;
                    }

                }
            }

            return result;
        }


        public string Name { get; }

        /// <summary>
        /// Xml-file with syntax description for Custom Highlighters
        /// </summary>
        public string DescriptionFile { get; protected set; }


        public SyntaxHighlighterBase(string name = null) {
            Name = !String.IsNullOrWhiteSpace(name) ? name : this.GetType().GetCustomAttribute<SyntaxHighlighterAttribute>()?.Name ?? this.GetType().FullName;

            InitStyleSchema();
        }


        public virtual void Dispose() { }


        public virtual void InitStyleSchema() { }

        /// <summary>
        /// Highlights syntax
        /// </summary>
        public virtual void HighlightSyntax(Range range) { }


        public virtual void AutoIndentNeeded(object sender, AutoIndentEventArgs args) {
            var tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);
        }


        #region Styles

        //styles
        protected static readonly Platform platformType = PlatformType.GetOperationSystemPlatform();
        public readonly Style BlueBoldStyle = new TextStyle(Brushes.Blue, null, FontStyle.Bold);
        public readonly Style BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        public readonly Style BoldUnderlineStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        public readonly Style BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular);
        public readonly Style BrownItalicStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        public readonly Style GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        public readonly Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        public readonly Style GreenItalicStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        public readonly Style MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        public readonly Style MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        public readonly Style DarkRedStyle = new TextStyle(Brushes.DarkRed, null, FontStyle.Regular);
        public readonly Style DarkCyanStyle = new TextStyle(Brushes.DarkCyan, null, FontStyle.Regular);
        public readonly Style BlackStyle = new TextStyle(Brushes.Black, null, FontStyle.Regular);

        protected readonly List<Style> resilientStyles = new List<Style>(5);



        /// <summary>
        /// String style
        /// </summary>
        public Style StringStyle { get; set; }

        /// <summary>
        /// Comment style
        /// </summary>
        public Style CommentStyle { get; set; }

        /// <summary>
        /// Number style
        /// </summary>
        public Style NumberStyle { get; set; }

        /// <summary>
        /// C# attribute style
        /// </summary>
        public Style AttributeStyle { get; set; }

        /// <summary>
        /// Class name style
        /// </summary>
        public Style ClassNameStyle { get; set; }

        /// <summary>
        /// Keyword style
        /// </summary>
        public Style KeywordStyle { get; set; }

        /// <summary>
        /// Compiler directive style
        /// </summary>
        public Style DirectiveStyle { get; set; }

        /// <summary>
        /// Style of tags in comments of C#
        /// </summary>
        public Style CommentTagStyle { get; set; }

        /// <summary>
        /// HTML attribute value style
        /// </summary>
        public Style AttributeValueStyle { get; set; }

        /// <summary>
        /// HTML tag brackets style
        /// </summary>
        public Style TagBracketStyle { get; set; }

        /// <summary>
        /// HTML tag name style
        /// </summary>
        public Style TagNameStyle { get; set; }

        /// <summary>
        /// HTML Entity style
        /// </summary>
        public Style HtmlEntityStyle { get; set; }

        /// <summary>
        /// XML attribute style
        /// </summary>
        public Style XmlAttributeStyle { get; set; }

        /// <summary>
        /// XML attribute value style
        /// </summary>
        public Style XmlAttributeValueStyle { get; set; }

        /// <summary>
        /// XML tag brackets style
        /// </summary>
        public Style XmlTagBracketStyle { get; set; }

        /// <summary>
        /// XML tag name style
        /// </summary>
        public Style XmlTagNameStyle { get; set; }

        /// <summary>
        /// XML Entity style
        /// </summary>
        public Style XmlEntityStyle { get; set; }

        /// <summary>
        /// XML CData style
        /// </summary>
        public Style XmlCDataStyle { get; set; }

        /// <summary>
        /// Variable style
        /// </summary>
        public Style VariableStyle { get; set; }

        /// <summary>
        /// Specific PHP keyword style
        /// </summary>
        public Style KeywordStyle2 { get; set; }

        /// <summary>
        /// Specific PHP keyword style
        /// </summary>
        public Style KeywordStyle3 { get; set; }

        /// <summary>
        /// SQL Statements style
        /// </summary>
        public Style StatementsStyle { get; set; }

        /// <summary>
        /// SQL Functions style
        /// </summary>
        public Style FunctionsStyle { get; set; }

        /// <summary>
        /// SQL Types style
        /// </summary>
        public Style TypesStyle { get; set; }


        /// <summary>
        /// Adds the given <paramref name="style"/> as resilient style. A resilient style is additionally available when highlighting is 
        /// based on a syntax descriptor that has been derived from an XML description file. In the run of the highlighting routine 
        /// the styles used by the FCTB are always dropped and replaced with the (initial) ones from the syntax descriptor. Resilient styles are 
        /// added afterwards and can be used anyway. 
        /// </summary>
        /// <param name="style">Style to add</param>
        public virtual void AddResilientStyle(FastColoredTextBox currentTb, Style style) {
            if (resilientStyles.Contains(style)) return;
            currentTb.CheckStylesBufferSize(); // Prevent buffer overflow
            resilientStyles.Add(style);
        }


        #endregion
    }


}
