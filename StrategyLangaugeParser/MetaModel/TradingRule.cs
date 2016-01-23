using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;

namespace StrategyLanguageParser.MetaModel
{
    public class TradingRule : AstNode
    {
        public IEnumerable<Statement> Statements { get; set; }
        public string Name { get; set; }
        public TimeIntervalDefinition ExecuteFrequency { get; set; }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Name = treeNode.ChildNodes[0].FindTokenAndGetText();
            Statements = treeNode.ChildNodes[2].ChildNodes.Select(c => c.AstNode as Statement);
            ExecuteFrequency = treeNode.ChildNodes[1].AstNode as TimeIntervalDefinition;
        }

        
    }
}
