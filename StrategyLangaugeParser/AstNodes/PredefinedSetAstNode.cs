using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class PredefinedSetAstNode : FXStrategy.LanguageParsing.AstNodes.CollectionAstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
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
