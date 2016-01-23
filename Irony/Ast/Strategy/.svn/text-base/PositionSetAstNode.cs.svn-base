using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.Ast
{
    public class PositionSetAstNode : Irony.Ast.CollectionAstNode
    {
        public override void Init(Parsing.ParsingContext context, Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _positionSetName = (IdentifierNode)treeNode.ChildNodes[0].AstNode;
        }

        private Ast.IdentifierNode _positionSetName;

        public Ast.IdentifierNode PositionSetName
        {
            get { return _positionSetName; }
        }
    }
}
