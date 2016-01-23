using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.Ast
{
    public class ClosePosition : PortfolioOperation
    {
        public override void Init(Parsing.ParsingContext context, Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Variable = (ExpressionAstNode) AddChild("variable",treeNode.ChildNodes[0]);
        }
        public ExpressionAstNode Variable { get; set; }
    }
}
