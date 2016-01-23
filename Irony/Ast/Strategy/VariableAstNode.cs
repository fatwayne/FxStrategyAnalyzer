using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.Ast
{
    public class VariableAstNode : Irony.Ast.ExpressionAstNode
    {
        public override void Init(Parsing.ParsingContext context, Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _name = (IdentifierNode) AddChild("name", treeNode.ChildNodes[0]);
        }


        private Ast.IdentifierNode _name;

        public Ast.IdentifierNode Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
