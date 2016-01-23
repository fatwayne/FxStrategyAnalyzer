using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class ClosePositionAstNode : PortfolioOperationAstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Variable = (ExpressionAstNode) AddChild("variable",treeNode.ChildNodes[0]);
        }
        public ExpressionAstNode Variable { get; set; }
    }
}
