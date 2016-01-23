using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyLanguageParser.MetaModel
{
    public class Constant : Expression
    {
        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            Value = treeNode.Token.Value;
        }

        public object Value { get; set; }

        private Type _type;
        public override Type Type
        {
            get { return _type; }
        }

        public override object Eval()
        {
            return Value;
        }
    }
}
