using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class DecimalAstNode : ExpressionAstNode
    {
        private decimal _value;

        public decimal Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _value = Convert.ToDecimal(treeNode.Token.Value);
        }


        public override Type GetType()
        {
            return typeof(decimal);
        }
    }
}
