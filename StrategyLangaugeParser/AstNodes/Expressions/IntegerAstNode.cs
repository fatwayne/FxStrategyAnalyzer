using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class IntegerAstNode : ExpressionAstNode
    {
        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _value = (int) treeNode.Token.Value ;
        }

        public override Type GetType()
        {
            return typeof(int);
        }
    }
}
