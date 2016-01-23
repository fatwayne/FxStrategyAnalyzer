using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class DateAstNode : ExpressionAstNode
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _value = (DateTime) treeNode.ChildNodes[0].Token.Value;
        }

        private DateTime _value;

        public DateTime Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public override Type GetType()
        {
            return typeof(DateTime);
        }
    }
}
