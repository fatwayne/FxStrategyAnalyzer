using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;

namespace Irony.Ast
{
    public class StrategyParameterAstNode : AstNode
    {

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Name = treeNode.ChildNodes[0].FindTokenAndGetText();
            Value = treeNode.ChildNodes[1].FindTokenAndGetText();
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
