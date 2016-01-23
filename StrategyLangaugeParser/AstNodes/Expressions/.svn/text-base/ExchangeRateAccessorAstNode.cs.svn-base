using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    public class ExchangeRateAccessorAstNode : TimeDataSetAccessorAstNode
    {
        private ExpressionAstNode _baseCurrencyExpr;

        public ExpressionAstNode BaseCurrencyExpr
        {
            get { return _baseCurrencyExpr; }
        }
        private ExpressionAstNode _variableCurrencyExpr;

        public ExpressionAstNode VariableCurrencyExpr
        {
            get { return _variableCurrencyExpr; }
        }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            _baseCurrencyExpr = AddChild("baseCurrencyExpr", treeNode.ChildNodes[0]) as ExpressionAstNode;
            _variableCurrencyExpr = AddChild("variableCurrencyExpr", treeNode.ChildNodes[2]) as ExpressionAstNode;
        }

        public override Type GetType()
        {
            throw new NotImplementedException();
        }
    }
}
