using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irony.Ast
{
    public class PredefinedSetAstNode : Irony.Ast.CollectionAstNode
    {
        public override void Init(Parsing.ParsingContext context, Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _setName = treeNode.ChildNodes[0].FindTokenAndGetText();

        }

        private string _setName;
        public string SetName
        {
            get { return _setName; }
        }
    }
}
