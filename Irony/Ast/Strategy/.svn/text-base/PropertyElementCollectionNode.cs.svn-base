using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.Ast
{
    public class PropertyElementCollectionNode : Irony.Ast.CollectionAstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _property = treeNode.ChildNodes[0].FindTokenAndGetText();
            _variable = treeNode.ChildNodes[1].AstNode as VariableAstNode;
        }

        private VariableAstNode _variable;

        public VariableAstNode Variable
        {
            get { return _variable; }
        }


        private string _property;
        public string Property
        {
            get { return _property; }
        }

    }
}
