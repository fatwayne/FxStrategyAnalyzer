using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class PositionSetAstNode : FXStrategy.LanguageParsing.AstNodes.CollectionAstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _positionSetName = treeNode.ChildNodes[0].FindTokenAndGetText();
           // _positionSetName = (Irony.Ast.IdentifierNode)treeNode.ChildNodes[0].AstNode;
        }

        private string _positionSetName;

        public string PositionSetName
        {
            get { return _positionSetName; }
        }
    }
}
