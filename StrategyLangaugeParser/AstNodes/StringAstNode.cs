using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class StringAstNode : ExpressionAstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _value = treeNode.Token.Value.ToString();
        }


        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }


        public override Type GetType()
        {
            return typeof(string);
        }
    }
}
