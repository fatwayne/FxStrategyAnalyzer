using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.LanguageParsing.AstNodes;
using Irony.Parsing;

namespace FXStrategy.LanguageParsing
{
    /// <summary>
    /// Responsible for parsing the FXSL source code
    /// into abstract syntax tree using Irony Parser
    /// </summary>
    public class StrategyLanguageParser
    {
        Irony.Parsing.Parser _parser;
        public StrategyLanguageParser()
        {
            _parser = new Irony.Parsing.Parser(new StrategyLanguageGrammar());
        }

        /// <summary>
        /// Parse the source code of FXSL
        /// </summary>
        /// <param name="fxCode">source code of FXSL that defines a trading strategy</param>
        /// <returns>The root node of the abstract syntax tree</returns>
        public TradingStrategyAstNode Parse(string fxCode)
        {
            ParseTree parseTree = _parser.Parse(fxCode);
            if (parseTree.HasErrors())
                throw new Exception("Parsing error: " +
                    parseTree.ParserMessages.Select(m => m.Message.ToString()).ToString());

            return parseTree.Root.AstNode as TradingStrategyAstNode;
        }
    }
}
