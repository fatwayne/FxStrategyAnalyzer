using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class VariableAstNode : FXStrategy.LanguageParsing.AstNodes.ExpressionAstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _name = treeNode.ChildNodes[0].FindTokenAndGetText();
            //_name = AddChild("name", treeNode.ChildNodes[0]);
        }


        private string _name;

        public string Name
        {
            get { return _name; }
        }

        public override Type GetType()
        {
            throw new NotImplementedException();
        }
    }
}
