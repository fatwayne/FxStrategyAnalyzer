using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class AtTimeAstNode : ExpressionAstNode
    {
        public override Type GetType()
        {
            return typeof(decimal);
        }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _exchangeRateAccessor = AddChild("exchangeRateAccessor", treeNode.ChildNodes[0]) as ExchangeRateAccessorAstNode;
            _dateExpr = AddChild("dateExpr", treeNode.ChildNodes[1]) as ExpressionAstNode;

        }

        private ExchangeRateAccessorAstNode _exchangeRateAccessor;

        public ExchangeRateAccessorAstNode ExchangeRateAccessor
        {
            get { return _exchangeRateAccessor; }
            set { _exchangeRateAccessor = value; }
        }
        private ExpressionAstNode _dateExpr;

        public ExpressionAstNode DateExpr
        {
            get { return _dateExpr; }
            set { _dateExpr = value; }
        }
    }
}
