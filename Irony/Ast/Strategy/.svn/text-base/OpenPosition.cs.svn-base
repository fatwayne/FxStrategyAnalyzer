using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyLanguageParser.AstNodes
{
    public class OpenPosition : PortfolioOperation
    {
        public override void Init(Parsing.ParsingContext context, Parsing.ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            _positionSetAstNode = (PositionSetAstNode)AddChild("PositionSetAstNode", treeNode.ChildNodes[0]);
            _expressionAstNode = (ExpressionAstNode)AddChild("ExpressionAstNode", treeNode.ChildNodes[1]);
        }

        private PositionSetAstNode _positionSetAstNode;

        public PositionSetAstNode PositionSetAstNode
        {
            get { return _positionSetAstNode; }
        }

        private ExpressionAstNode _expressionAstNode;

        public ExpressionAstNode ExpressionAstNode
        {
            get { return _expressionAstNode; }
        }
       
    }
}
