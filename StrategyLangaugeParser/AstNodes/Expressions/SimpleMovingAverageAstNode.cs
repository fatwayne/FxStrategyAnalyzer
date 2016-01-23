using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXStrategy.LanguageParsing.AstNodes
{
    /// <summary>
    /// Represents a node for getting simple moving average
    /// of exchange rate on an abstract syntax tree
    /// </summary>
    public class SimpleMovingAverageAstNode : ExpressionAstNode
    {
        public override Type GetType()
        {
            return typeof(decimal);
        }

        public override void Init(Irony.Parsing.ParsingContext context, Irony.Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            _lengthOfPeriod = AddChild("numberOfDays", treeNode.ChildNodes[0]) as PeriodicTimeDefinitionAstNode;
            _exchangeRateAccessor = AddChild("exchangeRateAccessor", treeNode.ChildNodes[1]) as ExchangeRateAccessorAstNode;
            _dateExpr = AddChild("dateExpr", treeNode.ChildNodes[2]) as ExpressionAstNode;

        }

        private PeriodicTimeDefinitionAstNode _lengthOfPeriod;

        public PeriodicTimeDefinitionAstNode LengthOfPeriod
        {
            get { return _lengthOfPeriod; }
            set { _lengthOfPeriod = value; }
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
