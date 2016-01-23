using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FXStrategy.LanguageParsing.AstNodes;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class StrategyParameterAstNode : Irony.Ast.AstNode
    {

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Name = treeNode.ChildNodes[0].FindTokenAndGetText();
            Value = AddChild("value",treeNode.ChildNodes[1]) as ExpressionAstNode;
        }

        public string Name { get; set; }

        public ExpressionAstNode Value { get; set; }
    }
}
