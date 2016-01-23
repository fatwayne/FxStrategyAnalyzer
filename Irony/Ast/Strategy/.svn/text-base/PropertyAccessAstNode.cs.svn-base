using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.Ast
{
    public class PropertyAccessAstNode : Irony.Ast.ExpressionAstNode
    {
        public override void Init(Parsing.ParsingContext context, Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _property = treeNode.ChildNodes[0].AstNode as IdentifierNode;
            _variable = treeNode.ChildNodes[1].AstNode as VariableAstNode;
        }

        private Ast.IdentifierNode _property;
        public Ast.IdentifierNode Property
        {
            get
            {
                return _property;
            }
        }

        private VariableAstNode _variable;
        public VariableAstNode Variable
        {
            get
            {
                return _variable;
            }
        }
    }
}
